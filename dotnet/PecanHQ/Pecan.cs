// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using PecanHQ.Util;
using PecanHQ.Grant;
using PecanHQ.Grant.Resources;
using PecanHQ.Grant.Types;

namespace PecanHQ
{


    /// <summary>
    /// The main service object, containing all cached state relating to the authorization schema.
    /// </summary>
    public sealed class Pecan
    {

        private static readonly Uri API = new Uri("https://www.pecanhq.com/grant/");

        private static readonly HttpClient CLIENT = new();

        private readonly IHttpHandler handler;

        private volatile AppManifest manifest;

        private volatile Dictionary<string, int> masks;

        private volatile Dictionary<string, ServiceRegistration> services;

        private volatile Dictionary<string, PermissionClaim> claims;

        private volatile Dictionary<string, Permissions> restricted;

        private volatile ConcurrentDictionary<(Guid, int), Registration> registrations;

        internal volatile Dictionary<string, Permissions> permissions;

        private Pecan(
            IHttpHandler handler,
            AppManifest manifest,
            ClaimsPrincipal user,
            Guid accountId,
            string artifact,
            decimal schema,
            GrantService service,
            IGrantResource resource)
        {
            this.handler = handler;
            this.manifest = manifest;
            this.masks = new();
            this.services = new();
            this.claims = new();
            this.restricted = new();
            this.registrations = new();
            this.permissions = new();

            this.User = user;
            this.AccountId = accountId;
            this.Artifact = artifact;
            this.Schema = schema;
            this.Service = service;
            this.Resource = resource;
        }

        /// <summary>
        /// The system user.
        /// </summary>
        public ClaimsPrincipal User { get; }

        /// <summary>
        /// The system user account identifier.
        /// </summary>
        public Guid AccountId { get; }

        /// <summary>
        /// The issuing authority.
        /// </summary>
        public string Issuer { get => manifest.Authority; }

        /// <summary>
        /// The artifact name.
        /// </summary>
        public string Artifact { get; }

        /// <summary>
        /// The schema version.
        /// </summary>
        public decimal Schema { get; }

        /// <summary>
        /// The main entrypoint service for direct access to the API.
        /// </summary>
        public GrantService Service { get; }

        /// <summary>
        /// The root resource for HATEOS access to the API.
        /// </summary>
        public IGrantResource Resource { get; }

        /// <summary>
        /// All registered service claims.
        /// </summary>
        public IReadOnlyDictionary<string, ServiceRegistration> Registrations { get => services; }

        /// <summary>
        /// All system permissions.
        /// </summary>
        public IEnumerable<KeyValuePair<string, Permissions>> Permissions { get => permissions; }

        /// <summary>
        /// All restricted system permissions.
        /// </summary>
        public IEnumerable<KeyValuePair<string, Permissions>> Restricted { get => restricted; }

        /// <summary>
        /// Evaluate access to a resource for a permissions object
        /// </summary>
        public bool CheckAccess(Permissions permissions, string access, Guid resourceId)
        {
            if (!this.masks.TryGetValue(access, out var mask)
                || !this.registrations.TryGetValue((resourceId, permissions.Version), out var registration)
                || (registration.Mask&mask) != mask)
            {
                return false;
            }

            return mask == 0 || permissions.HasPermissions(registration.Version, registration.Part, mask);
        }

        /// <summary>
        /// Look up an account using a key-valued claim.
        /// </summary>
        public async Task<AccountAssertion?> FindAsync(string key, string value, string? tenant = null, CancellationToken token = default)
        {
            if (!this.Resource.HasLookupAccountUri)
            {
                return default;
            }

            return await this.Resource.AsLookupAccountUri(token: token)
                .AsQuery(key, value, tenant)
                .GetAsync();
        }

        /// <summary>
        /// Load all claims for an account.
        /// </summary>
        public async Task<ClaimResponse> LoadAsync(Guid accountId, CancellationToken token = default)
        {
            if (!this.Resource.HasRefreshProfileUri)
            {
                return default;
            }

            var profile = await this.Resource.AsRefreshProfileUri(token: token).PostAsync(accountId);
            if (profile == null)
            {
                return default;
            }

            var claims = new Dictionary<string, string>(profile.Assertions.Count);
            foreach (var claim in profile.Assertions)
            {
                var key = $"{claim.Issuer}{claim.Key}";
                claims[key] = claim.Value;

                if (this.claims.TryGetValue(key, out var permission)
                    && !claim.Value.StartsWith(permission.Prefix))
                {
                    var prefix = claim.Value.Substring(0, 6);
                    if (!permission.Versions.ContainsKey(prefix)
                        && this.Resource.HasPermissionsUri)
                    {
                        permission.Versions[prefix] = await LazyLoad(prefix, permission.Key, token);
                    }
                }
            }

            return new ClaimResponse(true, profile.Authority, accountId, profile.Display, claims);
        }

        /// <summary>
        /// Create a new authorization session for a claims principal.
        /// </summary>
        public Session Session(ClaimsPrincipal? principal)
        {
            return new Session(this, principal);
        }

        /// <summary>
        /// Persist the current service state to UTF8 JSON bytes.
        /// </summary>
        public byte[] AsJson()
        {
            var state = new ServiceState(
                this.Service.Uri,
                this.Artifact,
                this.Schema,
                this.manifest,
                this.User.Claims.ToDictionary(x => x.Type, x => x.Value),
                this.AccountId);
            if (this.Resource.TrySave(out var links))
            {
                state.links = links;
            }

            return JsonSerializer.SerializeToUtf8Bytes(state);
        }

        /// <summary>
        /// Persist a claim response to UTF8 JSON bytes.
        /// </summary>
        public byte[] AsJson(ClaimResponse response)
        {
            return JsonSerializer.SerializeToUtf8Bytes(response);
        }

        /// <summary>
        /// Load authorization claims from a cached response.
        /// </summary>
        /// <remarks>
        /// An API call will only occur if there are permission claims without a matching version registered.
        /// </remarks>
        public async Task<ClaimResponse> FromJsonAsync(byte[] utf8Json, CancellationToken token = default)
        {
            try
            {
                var response = JsonSerializer.Deserialize<ClaimResponse>(utf8Json);
                foreach (var claim in response.Claims)
                {
                    if (!this.claims.TryGetValue(claim.Key, out var permission)
                        || claim.Value.StartsWith(permission.Prefix))
                    {
                        continue;
                    }

                    var prefix = claim.Value.Substring(0, 6);
                    if (!permission.Versions.ContainsKey(prefix)
                        && this.Resource.HasPermissionsUri)
                    {
                        permission.Versions[prefix] = await LazyLoad(prefix, permission.Key, token);
                    }
                }
                return response;
            }
            catch (JsonException)
            {
                return default(ClaimResponse);
            }
        }
        /// <summary>
        /// Reload all authorization schema information.
        /// </summary>
        public async ValueTask<bool> ReloadAsync(CancellationToken token = default)
        {
            if (!this.Resource.HasManifestUri)
            {
                return false;
            }

            var manifest = await this.Resource.AsManifestUri(token)
                .AsQuery(this.Artifact, this.Schema)
                .GetAsync();

            return Reload(this, manifest);
        }

        private async Task<int> LazyLoad(string prefix, string key, CancellationToken token)
        {
            var bytes = Convert.FromBase64String($"{prefix}==");
            var version = BitConverter.ToInt32(bytes);
            var permissions = await this.Resource.AsPermissionsUri(token)
                .AsQuery(key, version)
                .GetAsync();

            await foreach (var entry in permissions)
            {
                this.registrations[(entry.ResourceId, version)] = new Registration(
                    entry.ResourceId,
                    version,
                    entry.Position,
                    entry.Mask);
            }
            return version;
        }

        private static bool Reload(Pecan pecan, AppManifest? manifest)
        {
            if (manifest == null)
            {
                return false;
            }

            var masks = pecan.masks.Count == 0 ? pecan.masks : new();
            var services = pecan.services.Count == 0 ? pecan.services : new();
            var claims = pecan.claims.Count == 0 ? pecan.claims : new();
            var permissions = pecan.permissions.Count == 0 ? pecan.permissions : new();
            var restricted = pecan.restricted.Count == 0 ? pecan.restricted : new();
            var registrations = pecan.registrations.Count == 0 ? pecan.registrations : new();
            foreach (var permission in manifest.Permissions)
            {
                masks[permission.Key] = permission.Mask;
            }

            foreach (var entry in manifest.Services)
            {
                var resources = new Dictionary<string, Guid>(entry.Permissions.Count);

                var system = new BitArray(Math.Max(32, entry.Hwm));
                system.SetVersion(entry.Version);
                var worker = new BitArray(Math.Max(32, entry.Hwm));
                worker.SetVersion(entry.Version);
                var include = false;
                foreach (var child in entry.Permissions)
                {
                    resources[child.Name] = child.ResourceId;
                    system.SetPermissions(child.Position, child.Mask);
                    var mask = child.Mask&(child.Mask^child.Restricted);
                    if (mask > 0)
                    {
                        include = true;
                        worker.SetPermissions(child.Position, mask);
                    }
                    registrations[(child.ResourceId, entry.Version)] = new Registration(
                        child.ResourceId,
                        entry.Version,
                        child.Position,
                        child.Mask);
                }

                var key = $"{entry.Authority}{entry.Claim}";
                services[entry.Name] = new ServiceRegistration(entry.Name, key, resources)
                {
                    Subject = entry.Subject != null ? $"{entry.Authority}{entry.Subject}" : null,
                    Tenant = entry.Tenant != null ? $"{entry.Authority}{entry.Tenant}" : null,
                };

                var prefix = Convert.ToBase64String(BitConverter.GetBytes(entry.Version)).Substring(0, 6);
                claims[key] = new PermissionClaim(entry.Claim, prefix, new());
                permissions[key] = new Permissions(entry.Version, system);
                if (include)
                {
                    restricted[key] = new Permissions(entry.Version, worker);
                }
            }

            lock (pecan)
            {
                pecan.masks = masks;
                pecan.services = services;
                pecan.claims = claims;
                pecan.permissions = permissions;
                pecan.restricted = restricted;
                pecan.registrations = registrations;
                pecan.manifest = manifest;
            }
            return true;
        }

        /// <summary>
        /// Try create a new service using cached state.
        /// </summary>
        public static bool TryCreate(
            byte[] utf8Json,
            string keyId,
            string secret,
            string artifact,
            decimal schema,
            [MaybeNullWhen(false)] out Pecan? pecan)
        {
            ServiceState? state;
            try
            {
                state = JsonSerializer.Deserialize<ServiceState>(utf8Json) ?? throw new ArgumentException("No service state found");
            }
            catch (JsonException)
            {
                pecan = null;
                return false;
            }

            byte[] key;
            try
            {
                key = Convert.FromBase64String(secret);
            }
            catch (FormatException)
            {
                pecan = null;
                return false;
            }

            var handler = new SigningHttpHandler(CLIENT, keyId, key);
            var service = new GrantService(handler, state.Uri);
            if (state.links == null
                || state.Artifact != artifact
                || state.Schema != schema
                || !GrantResource.TryLoad(handler, state.links, out var entrypoint))
            {
                pecan = null;
                return false;
            }
            var identity = new ClaimsIdentity(
                state.User.Select(x => new System.Security.Claims.Claim(x.Key, x.Value, null, state.Manifest.Authority))
            );
            var principal = new ClaimsPrincipal(identity);
            pecan = new Pecan(
                handler,
                state.Manifest,
                principal,
                state.AccountId,
                artifact,
                schema,
                service,
                entrypoint);
            return Reload(pecan, state.Manifest);
        }

        /// <summary>
        /// Try create a new service using cached state.
        /// </summary>
        /// <remarks>
        /// A utility method for a custom HTTP handler.
        /// </remarks>
        public static bool TryCreate(
            byte[] utf8Json,
            IHttpHandler handler,
            string artifact,
            decimal schema,
            [MaybeNullWhen(false)] out Pecan? pecan)
        {
            ServiceState? state;
            try
            {
                state = JsonSerializer.Deserialize<ServiceState>(utf8Json) ?? throw new ArgumentException("No service state found");
            }
            catch (JsonException)
            {
                pecan = null;
                return false;
            }

            var service = new GrantService(handler, state.Uri);
            if (state.links == null
                || state.Artifact != artifact
                || state.Schema != schema
                || !GrantResource.TryLoad(handler, state.links, out var entrypoint))
            {
                pecan = null;
                return false;
            }
            var identity = new ClaimsIdentity(
                state.User.Select(x => new System.Security.Claims.Claim(x.Key, x.Value, null, state.Manifest.Authority))
            );
            var principal = new ClaimsPrincipal(identity);
            pecan = new Pecan(
                handler,
                state.Manifest,
                principal,
                state.AccountId,
                artifact,
                schema,
                service,
                entrypoint);
            return Reload(pecan, state.Manifest);
        }

        /// <summary>
        /// Create a new service using the provided application credentials.
        /// </summary>
        public static Task<Pecan> CreateAsync(
            string keyId,
            string secret,
            string artifact,
            decimal schema,
            Uri? uri = null,
            CancellationToken token = default)
        {
            byte[] key;
            try
            {
                key = Convert.FromBase64String(secret);
            }
            catch (FormatException e)
            {
                throw new ArgumentException("Expecting a base-64 encoded secret", nameof(secret), e);
            }

            var handler = new SigningHttpHandler(CLIENT, keyId, key);
            return CreateAsync(handler, artifact, schema, uri, token);
        }

        /// <summary>
        /// Create a new service using the provided application credentials.
        /// </summary>
        /// <remarks>
        /// A utility method for a custom HTTP handler.
        /// </remarks>
        public static async Task<Pecan> CreateAsync(
            IHttpHandler handler,
            string artifact,
            decimal schema,
            Uri? uri = null,
            CancellationToken token = default)
        {
            var service = new Grant.GrantService(handler, uri ?? API);
            var entrypoint = await service.GetAsync(token);
            var manifest = await entrypoint.AsManifestUri(token)
                .AsQuery(artifact, schema)
                .GetAsync();
            if (manifest == null) throw new ArgumentException(
                "No matching artifact version found",
                nameof(artifact)
            );
            // TODO: include session in manifest
            var session = await entrypoint.AsRefreshProfileUri(token)
                .PostAsync(manifest.AccountId);
            if (session == null) throw new ArgumentException(
                "Unable to refresh the account for this artifact",
                nameof(artifact)
            );
            var identity = new ClaimsIdentity(
                session.Assertions.Select(x => new System.Security.Claims.Claim(x.Key, x.Value, null, manifest.Authority))
            );
            var principal = new ClaimsPrincipal(identity);
            var pecan = new Pecan(handler, manifest, principal, session.AccountId, artifact, schema, service, entrypoint);
            Reload(pecan, manifest);
            return pecan;
        }

    }

}