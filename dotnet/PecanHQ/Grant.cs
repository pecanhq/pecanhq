// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable

using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Security;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System;
using meta = PecanHQ;

namespace PecanHQ.Grant
{

    /// <summary>
    /// All service interfaces for the Grant portal.
    /// </summary>
    /// <remarks>
    /// This service is configured with the remote API endpoint and
    /// provides the main entrypoint for HATEOS navigation.
    /// </remarks>
    public class GrantService
    {

        private readonly IHttpHandler handler;

        /// <summary>
        /// The public constructor for the remote API.
        /// </summary>
        /// <remarks>
        /// The service can be cast to one of the relevant service interfaces, or
        /// used directly to access all local resources.
        /// </remarks>
        public GrantService(IHttpHandler handler, Uri uri)
        {
            this.handler = handler;

            var builder = new UriBuilder(uri);
            builder.Query = null;
            builder.Path = builder.Path.TrimEnd('/');
            this.Uri = builder.Uri;
        }

        /// <summary>
        /// The main entrypoint Uri.
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// The main entrypoint for HATEOS navigation for the Grant portal.
        /// </summary>
        public async Task<Resources.IGrantResource> GetAsync(CancellationToken token = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, this.Uri);
            request.Headers.Add("accept", "application/json");
            var response = await handler.SendAsync(request, token);
            var origin = response?.RequestMessage?.RequestUri ?? throw new SecurityException(
                "Attempted to access an unknown resource"
            );
            if (!origin.OriginalString.EndsWith("/"))
            {
                origin = new Uri(origin.GetLeftPart(UriPartial.Path) + "/");
            }
            else if (!string.IsNullOrWhiteSpace(origin.Query))
            {
                origin = new Uri(origin.GetLeftPart(UriPartial.Path));
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new GrantResource(handler, origin, null);
            }
            else if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var src = await JsonSerializer.DeserializeAsync<Resources.GrantLinks>(stream, handler.Json);
                    return new GrantResource(handler, origin, src?.links);
                }
            }
            else throw await ServiceException.CreateAsync(response, options: handler.Json);
        }

        /// <summary>
        /// A deep link into the artifact resource.
        /// </summary>
        /// <param name="claimGroup">The name of the claim group for this artifact. </param>
        public async Task<Link<Types.ArtifactDetails, Resources.IArtifactResource>> ToArtifactAsync(
            string claimGroup)
        {
            var r = await handler.GetFromJsonAsync<Types.ArtifactDetails>(
                $"{this.Uri}/artifact/{ Uri.EscapeDataString(claimGroup.ToString()) }"
            ) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The no label available. service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
            return new Link<Types.ArtifactDetails, Resources.IArtifactResource>(
                r,
                new GrantResource(this.handler, this.Uri, r.links));
        }

        /// <summary>
        /// A deep link into the service resource.
        /// </summary>
        /// <param name="claimGroup">The name of the claim group for this artifact. </param>
        /// <param name="resourceGroup">The name of the resource group for this service. </param>
        public async Task<Link<Types.ServiceDetails, Resources.IServiceResource>> ToServiceAsync(
            string claimGroup,
            string resourceGroup)
        {
            var r = await handler.GetFromJsonAsync<Types.ServiceDetails>(
                $"{this.Uri}/service/{ Uri.EscapeDataString(claimGroup.ToString()) }/{ Uri.EscapeDataString(resourceGroup.ToString()) }"
            ) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The no label available. service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
            return new Link<Types.ServiceDetails, Resources.IServiceResource>(
                r,
                new GrantResource(this.handler, this.Uri, r.links));
        }

        /// <summary>
        /// A deep link into the role resource.
        /// </summary>
        /// <param name="key">The provider key. </param>
        /// <param name="policyGroup">The policy group name for this role. </param>
        public async Task<Link<Types.RoleDetails, Resources.IRoleResource>> ToRoleAsync(
            string key,
            string policyGroup)
        {
            var r = await handler.GetFromJsonAsync<Types.RoleDetails>(
                $"{this.Uri}/role/{ Uri.EscapeDataString(key.ToString()) }/{ Uri.EscapeDataString(policyGroup.ToString()) }"
            ) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The no label available. service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
            return new Link<Types.RoleDetails, Resources.IRoleResource>(
                r,
                new GrantResource(this.handler, this.Uri, r.links));
        }

        /// <summary>
        /// A deep link into the claim resource.
        /// </summary>
        /// <param name="key">The claim key. </param>
        public async Task<Link<Types.ClaimDetails, Resources.IClaimResource>> ToClaimAsync(
            string key)
        {
            var r = await handler.GetFromJsonAsync<Types.ClaimDetails>(
                $"{this.Uri}/claim/{ Uri.EscapeDataString(key.ToString()) }"
            ) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The no label available. service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
            return new Link<Types.ClaimDetails, Resources.IClaimResource>(
                r,
                new GrantResource(this.handler, this.Uri, r.links));
        }

        /// <summary>
        /// A deep link into the provider resource.
        /// </summary>
        /// <param name="key">The provider key. </param>
        public async Task<Link<Types.ProviderDetails, Resources.IProviderResource>> ToProviderAsync(
            string key)
        {
            var r = await handler.GetFromJsonAsync<Types.ProviderDetails>(
                $"{this.Uri}/provider/{ Uri.EscapeDataString(key.ToString()) }"
            ) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The no label available. service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
            return new Link<Types.ProviderDetails, Resources.IProviderResource>(
                r,
                new GrantResource(this.handler, this.Uri, r.links));
        }

        /// <summary>
        /// A deep link into the account resource.
        /// </summary>
        /// <param name="accountId">The account identifier. </param>
        public async Task<Link<Types.AccountDetails, Resources.IAccountResource>> ToAccountAsync(
            Guid accountId)
        {
            var r = await handler.GetFromJsonAsync<Types.AccountDetails>(
                $"{this.Uri}/account/{ Uri.EscapeDataString(accountId.ToString()) }"
            ) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The no label available. service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
            return new Link<Types.AccountDetails, Resources.IAccountResource>(
                r,
                new GrantResource(this.handler, this.Uri, r.links));
        }

        /// <summary>
        /// A deep link into the policy resource.
        /// </summary>
        /// <param name="policyGroupId">The policy group name for this role. </param>
        /// <param name="accountId">The account identifier. </param>
        public async Task<Link<Types.PolicyDetails, Resources.IPolicyResource>> ToPolicyAsync(
            Guid policyGroupId,
            Guid accountId)
        {
            var r = await handler.GetFromJsonAsync<Types.PolicyDetails>(
                $"{this.Uri}/policy/{ Uri.EscapeDataString(policyGroupId.ToString()) }/{ Uri.EscapeDataString(accountId.ToString()) }"
            ) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The no label available. service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
            return new Link<Types.PolicyDetails, Resources.IPolicyResource>(
                r,
                new GrantResource(this.handler, this.Uri, r.links));
        }

        /// <summary>
        /// A deep link into the assertion resource.
        /// </summary>
        /// <param name="accountId">The account identifier. </param>
        /// <param name="claimId">The claim identifier. </param>
        public async Task<Link<Types.AssertionDetails, Resources.IAssertionResource>> ToAssertionAsync(
            Guid accountId,
            Guid claimId)
        {
            var r = await handler.GetFromJsonAsync<Types.AssertionDetails>(
                $"{this.Uri}/assertion/{ Uri.EscapeDataString(accountId.ToString()) }/{ Uri.EscapeDataString(claimId.ToString()) }"
            ) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The no label available. service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
            return new Link<Types.AssertionDetails, Resources.IAssertionResource>(
                r,
                new GrantResource(this.handler, this.Uri, r.links));
        }

        /// <summary>
        /// A deep link into the organization resource.
        /// </summary>
        /// <param name="organizationId">The organization identifier. </param>
        public async Task<Link<Types.OrganizationDetails, Resources.IOrganizationResource>> ToOrganizationAsync(
            Guid organizationId)
        {
            var r = await handler.GetFromJsonAsync<Types.OrganizationDetails>(
                $"{this.Uri}/organization/{ Uri.EscapeDataString(organizationId.ToString()) }"
            ) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The no label available. service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
            return new Link<Types.OrganizationDetails, Resources.IOrganizationResource>(
                r,
                new GrantResource(this.handler, this.Uri, r.links));
        }

    }

    /// <summary>
    /// All resource accessors and schema elements for the Grant portal.
    /// </summary>
    /// <remarks>
    /// The resource interfaces restrict the visible portion of this portal based
    /// on the current context. However, dealing with the entirety becomes
    /// unavoidable when performing open ended navigation or deep linking.
    /// </remarks>
    public readonly struct GrantResource : Resources.IGrantResource,
        Resources.IArtifactResource,
        Resources.IServiceResource,
        Resources.IRoleResource,
        Resources.IClaimResource,
        Resources.IProviderResource,
        Resources.IAccountResource,
        Resources.IPolicyResource,
        Resources.IAssertionResource,
        Resources.IOrganizationResource
    {

        private readonly IHttpHandler? handler;

        private readonly Uri? origin;

        private readonly IReadOnlyDictionary<string, Uri>? links;

        /// <summary>
        /// The public constructor, which acts as an all-in-one navigation service and deep link for the remote API.
        /// </summary>
        /// <remarks>
        /// The service can be cast to one of the relevant service interfaces, or
        /// used directly to access all local resources.
        /// </remarks>
        public GrantResource(
            IHttpHandler? handler,
            Uri? origin,
            IReadOnlyDictionary<string, Uri>? links)
        {
            this.handler = handler;
            this.origin = origin;
            this.links = links;
        }

        /// <summary>
        /// The root entrypoint into the service group.
        /// </summary>
        /// <remarks>
        /// A utility method for a cached link collection.
        /// </remarks>
        public static bool TryLoad(
            IHttpHandler handler,
            Dictionary<string, Uri> links,
            out GrantResource service)
        {
            if (links.TryGetValue("grant", out var origin))
            {
                service = new GrantResource(handler, origin, links);
                return true;
            }
            service = default;
            return false;
        }

        /// <inheritdoc/>
        public bool TrySave(out Dictionary<string, Uri>? links)
        {
            if (origin != null)
            {
                links = this.links == null ? new Dictionary<string, Uri>() : new Dictionary<string, Uri>(this.links);
                links["grant"] = origin;
                return true;
            }
            links = null;
            return false;
        }

        /// <inheritdoc/>
        public async Task<Link<Types.ArtifactDetails, Resources.IArtifactResource>> ToArtifactAsync(CancellationToken token = default)
        {
            var data = await AsArtifactUri(token: token).GetAsync();
            return new Link<Types.ArtifactDetails, Resources.IArtifactResource>(
                data,
                new GrantResource(this.handler, this.origin, data.links));
        }

        /// <inheritdoc/>
        public async Task<Link<Types.ServiceDetails, Resources.IServiceResource>> ToServiceAsync(CancellationToken token = default)
        {
            var data = await AsServiceUri(token: token).GetAsync();
            return new Link<Types.ServiceDetails, Resources.IServiceResource>(
                data,
                new GrantResource(this.handler, this.origin, data.links));
        }

        /// <inheritdoc/>
        public async Task<Link<Types.RoleDetails, Resources.IRoleResource>> ToRoleAsync(CancellationToken token = default)
        {
            var data = await AsRoleUri(token: token).GetAsync();
            return new Link<Types.RoleDetails, Resources.IRoleResource>(
                data,
                new GrantResource(this.handler, this.origin, data.links));
        }

        /// <inheritdoc/>
        public async Task<Link<Types.ClaimDetails, Resources.IClaimResource>> ToClaimAsync(CancellationToken token = default)
        {
            var data = await AsClaimUri(token: token).GetAsync();
            return new Link<Types.ClaimDetails, Resources.IClaimResource>(
                data,
                new GrantResource(this.handler, this.origin, data.links));
        }

        /// <inheritdoc/>
        public async Task<Link<Types.ProviderDetails, Resources.IProviderResource>> ToProviderAsync(CancellationToken token = default)
        {
            var data = await AsProviderUri(token: token).GetAsync();
            return new Link<Types.ProviderDetails, Resources.IProviderResource>(
                data,
                new GrantResource(this.handler, this.origin, data.links));
        }

        /// <inheritdoc/>
        public async Task<Link<Types.AccountDetails, Resources.IAccountResource>> ToAccountAsync(CancellationToken token = default)
        {
            var data = await AsAccountUri(token: token).GetAsync();
            return new Link<Types.AccountDetails, Resources.IAccountResource>(
                data,
                new GrantResource(this.handler, this.origin, data.links));
        }

        /// <inheritdoc/>
        public async Task<Link<Types.PolicyDetails, Resources.IPolicyResource>> ToPolicyAsync(CancellationToken token = default)
        {
            var data = await AsPolicyUri(token: token).GetAsync();
            return new Link<Types.PolicyDetails, Resources.IPolicyResource>(
                data,
                new GrantResource(this.handler, this.origin, data.links));
        }

        /// <inheritdoc/>
        public async Task<Link<Types.AssertionDetails, Resources.IAssertionResource>> ToAssertionAsync(CancellationToken token = default)
        {
            var data = await AsAssertionUri(token: token).GetAsync();
            return new Link<Types.AssertionDetails, Resources.IAssertionResource>(
                data,
                new GrantResource(this.handler, this.origin, data.links));
        }

        /// <inheritdoc/>
        public async Task<Link<Types.OrganizationDetails, Resources.IOrganizationResource>> ToOrganizationAsync(CancellationToken token = default)
        {
            var data = await AsOrganizationUri(token: token).GetAsync();
            return new Link<Types.OrganizationDetails, Resources.IOrganizationResource>(
                data,
                new GrantResource(this.handler, this.origin, data.links));
        }

        /// <inheritdoc/>
        public bool HasArtifactUri => this.links != null
            && links.TryGetValue("artifact", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ArtifactUri AsArtifactUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("artifact", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ArtifactUri(resolved, handler, token);
            }
            else throw new SecurityException("The artifact endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasServiceUri => this.links != null
            && links.TryGetValue("service", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ServiceUri AsServiceUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("service", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ServiceUri(resolved, handler, token);
            }
            else throw new SecurityException("The service endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasRoleUri => this.links != null
            && links.TryGetValue("role", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.RoleUri AsRoleUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("role", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.RoleUri(resolved, handler, token);
            }
            else throw new SecurityException("The role endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasClaimUri => this.links != null
            && links.TryGetValue("claim", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ClaimUri AsClaimUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("claim", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ClaimUri(resolved, handler, token);
            }
            else throw new SecurityException("The claim endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasProviderUri => this.links != null
            && links.TryGetValue("provider", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ProviderUri AsProviderUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("provider", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ProviderUri(resolved, handler, token);
            }
            else throw new SecurityException("The provider endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasAccountUri => this.links != null
            && links.TryGetValue("account", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.AccountUri AsAccountUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("account", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.AccountUri(resolved, handler, token);
            }
            else throw new SecurityException("The account endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasPolicyUri => this.links != null
            && links.TryGetValue("policy", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.PolicyUri AsPolicyUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("policy", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.PolicyUri(resolved, handler, token);
            }
            else throw new SecurityException("The policy endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasAssertionUri => this.links != null
            && links.TryGetValue("assertion", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.AssertionUri AsAssertionUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("assertion", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.AssertionUri(resolved, handler, token);
            }
            else throw new SecurityException("The assertion endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasOrganizationUri => this.links != null
            && links.TryGetValue("organization", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.OrganizationUri AsOrganizationUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("organization", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.OrganizationUri(resolved, handler, token);
            }
            else throw new SecurityException("The organization endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUserAssertionUri => this.links != null
            && links.TryGetValue("user_assertion", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UserAssertionUri AsUserAssertionUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("user_assertion", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UserAssertionUri(resolved, handler, token);
            }
            else throw new SecurityException("The user assertion endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasArtifactsUri => this.links != null
            && links.TryGetValue("artifacts", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ArtifactsUri AsArtifactsUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("artifacts", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ArtifactsUri(resolved, handler, token);
            }
            else throw new SecurityException("The artifacts endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasServicesUri => this.links != null
            && links.TryGetValue("services", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ServicesUri AsServicesUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("services", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ServicesUri(resolved, handler, token);
            }
            else throw new SecurityException("The services endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasResourcesUri => this.links != null
            && links.TryGetValue("resources", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ResourcesUri AsResourcesUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("resources", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ResourcesUri(resolved, handler, token);
            }
            else throw new SecurityException("The resources endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasReleasesUri => this.links != null
            && links.TryGetValue("releases", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ReleasesUri AsReleasesUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("releases", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ReleasesUri(resolved, handler, token);
            }
            else throw new SecurityException("The releases endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasPermissionsUri => this.links != null
            && links.TryGetValue("permissions", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.PermissionsUri AsPermissionsUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("permissions", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.PermissionsUri(resolved, handler, token);
            }
            else throw new SecurityException("The permissions endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasRolesUri => this.links != null
            && links.TryGetValue("roles", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.RolesUri AsRolesUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("roles", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.RolesUri(resolved, handler, token);
            }
            else throw new SecurityException("The roles endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasClaimsUri => this.links != null
            && links.TryGetValue("claims", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ClaimsUri AsClaimsUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("claims", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ClaimsUri(resolved, handler, token);
            }
            else throw new SecurityException("The claims endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasAccountsUri => this.links != null
            && links.TryGetValue("accounts", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.AccountsUri AsAccountsUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("accounts", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.AccountsUri(resolved, handler, token);
            }
            else throw new SecurityException("The accounts endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasPoliciesUri => this.links != null
            && links.TryGetValue("policies", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.PoliciesUri AsPoliciesUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("policies", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.PoliciesUri(resolved, handler, token);
            }
            else throw new SecurityException("The policies endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasAssertionsUri => this.links != null
            && links.TryGetValue("assertions", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.AssertionsUri AsAssertionsUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("assertions", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.AssertionsUri(resolved, handler, token);
            }
            else throw new SecurityException("The assertions endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasManifestUri => this.links != null
            && links.TryGetValue("manifest", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ManifestUri AsManifestUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("manifest", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ManifestUri(resolved, handler, token);
            }
            else throw new SecurityException("The manifest endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasProvidersUri => this.links != null
            && links.TryGetValue("providers", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ProvidersUri AsProvidersUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("providers", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ProvidersUri(resolved, handler, token);
            }
            else throw new SecurityException("The providers endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasAvailableResourcesUri => this.links != null
            && links.TryGetValue("available_resources", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.AvailableResourcesUri AsAvailableResourcesUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("available_resources", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.AvailableResourcesUri(resolved, handler, token);
            }
            else throw new SecurityException("The available resources endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasLookupAccountUri => this.links != null
            && links.TryGetValue("lookup_account", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.LookupAccountUri AsLookupAccountUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("lookup_account", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.LookupAccountUri(resolved, handler, token);
            }
            else throw new SecurityException("The lookup account endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasAvailablePoliciesUri => this.links != null
            && links.TryGetValue("available_policies", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.AvailablePoliciesUri AsAvailablePoliciesUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("available_policies", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.AvailablePoliciesUri(resolved, handler, token);
            }
            else throw new SecurityException("The available policies endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasOrganizationsUri => this.links != null
            && links.TryGetValue("organizations", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.OrganizationsUri AsOrganizationsUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("organizations", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.OrganizationsUri(resolved, handler, token);
            }
            else throw new SecurityException("The organizations endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasStatusUri => this.links != null
            && links.TryGetValue("status", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.StatusUri AsStatusUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("status", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.StatusUri(resolved, handler, token);
            }
            else throw new SecurityException("The status endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasRefreshUri => this.links != null
            && links.TryGetValue("refresh", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.RefreshUri AsRefreshUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("refresh", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.RefreshUri(resolved, handler, token);
            }
            else throw new SecurityException("The refresh endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasReleaseUri => this.links != null
            && links.TryGetValue("release", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ReleaseUri AsReleaseUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("release", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ReleaseUri(resolved, handler, token);
            }
            else throw new SecurityException("The release endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUpdateArtifactUri => this.links != null
            && links.TryGetValue("update_artifact", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UpdateArtifactUri AsUpdateArtifactUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("update_artifact", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UpdateArtifactUri(resolved, handler, token);
            }
            else throw new SecurityException("The update artifact endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasCreateServiceUri => this.links != null
            && links.TryGetValue("create_service", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.CreateServiceUri AsCreateServiceUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("create_service", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.CreateServiceUri(resolved, handler, token);
            }
            else throw new SecurityException("The create service endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUpdateServiceUri => this.links != null
            && links.TryGetValue("update_service", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UpdateServiceUri AsUpdateServiceUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("update_service", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UpdateServiceUri(resolved, handler, token);
            }
            else throw new SecurityException("The update service endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasCreateResourceUri => this.links != null
            && links.TryGetValue("create_resource", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.CreateResourceUri AsCreateResourceUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("create_resource", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.CreateResourceUri(resolved, handler, token);
            }
            else throw new SecurityException("The create resource endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUpdateResourceUri => this.links != null
            && links.TryGetValue("update_resource", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UpdateResourceUri AsUpdateResourceUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("update_resource", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UpdateResourceUri(resolved, handler, token);
            }
            else throw new SecurityException("The update resource endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasCreateRoleUri => this.links != null
            && links.TryGetValue("create_role", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.CreateRoleUri AsCreateRoleUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("create_role", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.CreateRoleUri(resolved, handler, token);
            }
            else throw new SecurityException("The create role endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasCreateClaimUri => this.links != null
            && links.TryGetValue("create_claim", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.CreateClaimUri AsCreateClaimUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("create_claim", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.CreateClaimUri(resolved, handler, token);
            }
            else throw new SecurityException("The create claim endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUpdateClaimUri => this.links != null
            && links.TryGetValue("update_claim", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UpdateClaimUri AsUpdateClaimUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("update_claim", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UpdateClaimUri(resolved, handler, token);
            }
            else throw new SecurityException("The update claim endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSetAccountAccessUri => this.links != null
            && links.TryGetValue("set_account_access", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SetAccountAccessUri AsSetAccountAccessUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("set_account_access", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SetAccountAccessUri(resolved, handler, token);
            }
            else throw new SecurityException("The set account access endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasCreateAccountUri => this.links != null
            && links.TryGetValue("create_account", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.CreateAccountUri AsCreateAccountUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("create_account", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.CreateAccountUri(resolved, handler, token);
            }
            else throw new SecurityException("The create account endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasCreateArtifactUri => this.links != null
            && links.TryGetValue("create_artifact", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.CreateArtifactUri AsCreateArtifactUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("create_artifact", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.CreateArtifactUri(resolved, handler, token);
            }
            else throw new SecurityException("The create artifact endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSetGeneralAccessUri => this.links != null
            && links.TryGetValue("set_general_access", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SetGeneralAccessUri AsSetGeneralAccessUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("set_general_access", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SetGeneralAccessUri(resolved, handler, token);
            }
            else throw new SecurityException("The set general access endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUpdateAccountUri => this.links != null
            && links.TryGetValue("update_account", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UpdateAccountUri AsUpdateAccountUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("update_account", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UpdateAccountUri(resolved, handler, token);
            }
            else throw new SecurityException("The update account endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUpdateRoleUri => this.links != null
            && links.TryGetValue("update_role", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UpdateRoleUri AsUpdateRoleUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("update_role", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UpdateRoleUri(resolved, handler, token);
            }
            else throw new SecurityException("The update role endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasRegisterUri => this.links != null
            && links.TryGetValue("register", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.RegisterUri AsRegisterUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("register", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.RegisterUri(resolved, handler, token);
            }
            else throw new SecurityException("The register endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasCreateProviderUri => this.links != null
            && links.TryGetValue("create_provider", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.CreateProviderUri AsCreateProviderUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("create_provider", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.CreateProviderUri(resolved, handler, token);
            }
            else throw new SecurityException("The create provider endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUpdateProviderUri => this.links != null
            && links.TryGetValue("update_provider", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UpdateProviderUri AsUpdateProviderUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("update_provider", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UpdateProviderUri(resolved, handler, token);
            }
            else throw new SecurityException("The update provider endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSetResourceAccessUri => this.links != null
            && links.TryGetValue("set_resource_access", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SetResourceAccessUri AsSetResourceAccessUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("set_resource_access", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SetResourceAccessUri(resolved, handler, token);
            }
            else throw new SecurityException("The set resource access endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasCreateTenantUri => this.links != null
            && links.TryGetValue("create_tenant", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.CreateTenantUri AsCreateTenantUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("create_tenant", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.CreateTenantUri(resolved, handler, token);
            }
            else throw new SecurityException("The create tenant endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSetIdentityUri => this.links != null
            && links.TryGetValue("set_identity", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SetIdentityUri AsSetIdentityUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("set_identity", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SetIdentityUri(resolved, handler, token);
            }
            else throw new SecurityException("The set identity endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUpdateIdentityUri => this.links != null
            && links.TryGetValue("update_identity", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UpdateIdentityUri AsUpdateIdentityUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("update_identity", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UpdateIdentityUri(resolved, handler, token);
            }
            else throw new SecurityException("The update identity endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasResetUri => this.links != null
            && links.TryGetValue("reset", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ResetUri AsResetUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("reset", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ResetUri(resolved, handler, token);
            }
            else throw new SecurityException("The reset endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSetServiceStatusUri => this.links != null
            && links.TryGetValue("set_service_status", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SetServiceStatusUri AsSetServiceStatusUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("set_service_status", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SetServiceStatusUri(resolved, handler, token);
            }
            else throw new SecurityException("The set service status endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSetArtifactStatusUri => this.links != null
            && links.TryGetValue("set_artifact_status", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SetArtifactStatusUri AsSetArtifactStatusUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("set_artifact_status", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SetArtifactStatusUri(resolved, handler, token);
            }
            else throw new SecurityException("The set artifact status endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSetAccountStatusUri => this.links != null
            && links.TryGetValue("set_account_status", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SetAccountStatusUri AsSetAccountStatusUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("set_account_status", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SetAccountStatusUri(resolved, handler, token);
            }
            else throw new SecurityException("The set account status endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSetReleaseStatusUri => this.links != null
            && links.TryGetValue("set_release_status", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SetReleaseStatusUri AsSetReleaseStatusUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("set_release_status", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SetReleaseStatusUri(resolved, handler, token);
            }
            else throw new SecurityException("The set release status endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasRefreshProfileUri => this.links != null
            && links.TryGetValue("refresh_profile", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.RefreshProfileUri AsRefreshProfileUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("refresh_profile", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.RefreshProfileUri(resolved, handler, token);
            }
            else throw new SecurityException("The refresh profile endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasRegisterAppUri => this.links != null
            && links.TryGetValue("register_app", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.RegisterAppUri AsRegisterAppUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("register_app", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.RegisterAppUri(resolved, handler, token);
            }
            else throw new SecurityException("The register app endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasOverrideAccountAccessUri => this.links != null
            && links.TryGetValue("override_account_access", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.OverrideAccountAccessUri AsOverrideAccountAccessUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("override_account_access", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.OverrideAccountAccessUri(resolved, handler, token);
            }
            else throw new SecurityException("The override account access endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasOverrideAccountUri => this.links != null
            && links.TryGetValue("override_account", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.OverrideAccountUri AsOverrideAccountUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("override_account", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.OverrideAccountUri(resolved, handler, token);
            }
            else throw new SecurityException("The override account endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasAssignIdentityUri => this.links != null
            && links.TryGetValue("assign_identity", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.AssignIdentityUri AsAssignIdentityUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("assign_identity", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.AssignIdentityUri(resolved, handler, token);
            }
            else throw new SecurityException("The assign identity endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasUpdateOrganizationUri => this.links != null
            && links.TryGetValue("update_organization", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.UpdateOrganizationUri AsUpdateOrganizationUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("update_organization", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.UpdateOrganizationUri(resolved, handler, token);
            }
            else throw new SecurityException("The update organization endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSendInvitationUri => this.links != null
            && links.TryGetValue("send_invitation", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SendInvitationUri AsSendInvitationUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("send_invitation", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SendInvitationUri(resolved, handler, token);
            }
            else throw new SecurityException("The send invitation endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasSetupUri => this.links != null
            && links.TryGetValue("setup", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.SetupUri AsSetupUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("setup", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.SetupUri(resolved, handler, token);
            }
            else throw new SecurityException("The setup endpoint is unavailable");
        }

        /// <inheritdoc/>
        public bool HasConfigureUri => this.links != null
            && links.TryGetValue("configure", out var uri)
            && this.origin?.IsBaseOf(uri) == true;

        /// <inheritdoc/>
        public Resources.ConfigureUri AsConfigureUri(CancellationToken token = default)
        {
            if (this.handler != null
                && this.links != null
                && links.TryGetValue("configure", out var resolved)
                && this.origin?.IsBaseOf(resolved) == true)
            {
                return new Resources.ConfigureUri(resolved, handler, token);
            }
            else throw new SecurityException("The configure endpoint is unavailable");
        }

        /// <inheritdoc/>
        public GrantResource From(INavigationAware? src)
        {
            return new GrantResource(this.handler, this.origin, src?.links);
        }

        /// <inheritdoc/>
        public Task<meta::IAttachment> GetAttachmentAsync(
            Uri uri,
            CancellationToken token = default)
        {
            if (this.handler != null)
            {
                return this.handler.GetAttachmentAsync(uri, token);
            }
            else throw new SecurityException("Attempted to use an uninitialized service");
        }

        /// <inheritdoc/>
        public Task<meta::IAttachment> GetAttachmentAsync(
            string uri,
            CancellationToken token = default)
        {
            if (this.handler != null)
            {
                return this.handler.GetAttachmentAsync(uri, token);
            }
            else throw new SecurityException("Attempted to use an uninitialized service");
        }

        /// <inheritdoc/>
        public Task<T?> GetFromJsonAsync<T>(
            Uri uri,
            CancellationToken token = default) where T : class
        {
            if (this.handler != null)
            {
                return this.handler.GetFromJsonAsync<T>(uri, token);
            }
            else throw new SecurityException("Attempted to use an uninitialized service");
        }

        /// <inheritdoc/>
        public Task<T?> GetFromJsonAsync<T>(
            string uri,
            CancellationToken token = default) where T : class
        {
            if (this.handler != null)
            {
                return this.handler.GetFromJsonAsync<T>(uri, token);
            }
            else throw new SecurityException("Attempted to use an uninitialized service");
        }

        /// <inheritdoc/>
        public Task<meta::IResultSet<T>> ScrollFromJsonAsync<T>(
            string uri,
            CancellationToken token = default,
            bool lazy = default)
        {
            if (this.handler != null)
            {
                return this.handler.ScrollFromJsonAsync<T>(uri, token, lazy);
            }
            else throw new SecurityException("Attempted to use an uninitialized service");
        }

        /// <inheritdoc/>
        public Task<meta::IResultSet<T>> ScrollFromJsonAsync<T>(
            Uri uri,
            CancellationToken token = default,
            bool lazy = default)
        {
            if (this.handler != null)
            {
                return this.handler.ScrollFromJsonAsync<T>(uri, token, lazy);
            }
            else throw new SecurityException("Attempted to use an uninitialized service");
        }

    }

}