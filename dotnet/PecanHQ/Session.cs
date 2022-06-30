// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable
using System;
using System.Security.Claims;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PecanHQ
{

    /// <summary>
    /// A stateful cache of authorization data, used to memoize typed authorization data items.
    /// </summary>
    public struct Session
    {

        private static readonly Dictionary<string, bool> TRUTHY = new(StringComparer.OrdinalIgnoreCase)
        {
            { "true", true },
            { "t", true },
            { "1", true },
            { "yes", true },
            { "false", false },
            { "f", false },
            { "0", false },
            { "no", false },
        };

        private readonly Pecan pecan;

        private readonly ClaimsPrincipal? principal;

        private readonly ConcurrentDictionary<string, ServiceRegistration?> registrations;

        private readonly ConcurrentDictionary<string, object?> values;

        internal IDictionary<string, Permissions> cache;

        /// <summary>
        /// The default constructor.
        /// </summary>
        public Session(Pecan pecan, ClaimsPrincipal? principal)
        {
            this.pecan = pecan;
            this.principal = principal;
            this.registrations = new();
            this.values = new();
            this.cache = new ConcurrentDictionary<string, Permissions>();
        }

        /// <summary>
        /// Check whether the active user has a specified level of access to a resource.
        /// </summary>
        public bool HasPermissions(string service, string resource, string access)
        {
            if (this.pecan == null)
            {
                return false;
            }

            if (!this.registrations.TryGetValue(service, out var claim))
            {
                if (!this.pecan.Services.TryGetValue(service, out var active)
                    || (active.Subject != null && this.AsString(active.Subject) == null)
                    || (active.Tenant != null && this.AsString(active.Tenant) == null))
                {
                    this.registrations[service] = null;
                    return false;
                }

                claim = active;
                this.registrations[service] = claim;
            }

            if (claim != null
                && claim.Resources.TryGetValue(resource, out var resourceId)
                && this.TryGetPermissions(claim.Claim, out var permissions))
            {
                return this.pecan.CheckAccess(permissions, access, resourceId);
            }

            return false;
        }

        /// <summary>
        /// Escalate privileges to the all active resources for the active user.
        /// </summary>
        /// <remarks>
        /// Assign the original struct to a new variable to revert to old permissions values.
        /// </remarks>
        public void EscalatePrivileges()
        {
            this.cache = this.pecan.permissions;
        }

        /// <summary>
        /// Fetch a single string-valued claim.
        /// </summary>
        public string? AsString(string claim)
        {
            var issuer = pecan?.Issuer;
            return this.principal?.FindFirst(x => x.Issuer == issuer && x.Type == claim)?.Value;
        }

        /// <summary>
        /// Fetch a single boolean-valued claim.
        /// </summary>
        /// <remarks>
        /// The result will be null if the claim does not exist or cannot be parsed as a boolean.
        /// </remarks>
        public bool? AsBool(string claim)
        {
            if (this.pecan == null)
            {
                return null;
            }

            if (values.TryGetValue(claim, out var cached))
            {
                var cast = cached as bool?;
                if (object.Equals(cast, cached))
                {
                    return cast;
                }
            }

            var selected = this.AsString(claim);
            if (selected == null)
            {
                values[claim] = null;
            }
            else if (TRUTHY.TryGetValue(selected, out var value))
            {
                values[claim] = value;
                return value;
            }
            return null;
        }

        /// <summary>
        /// Fetch a single Guid-valued claim.
        /// </summary>
        /// <remarks>
        /// The result will be null if the claim does not exist or cannot be parsed as a Guid.
        /// </remarks>
        public Guid? AsGuid(string claim)
        {
            if (this.pecan == null)
            {
                return null;
            }

            if (values.TryGetValue(claim, out var cached))
            {
                var cast = cached as Guid?;
                if (object.Equals(cast, cached))
                {
                    return cast;
                }
            }

            var selected = this.AsString(claim);
            if (Guid.TryParse(selected, out var value))
            {
                values[claim] = value;
                return value;
            }
            else if (selected == null)
            {
                values[claim] = null;
            }
            return null;
        }

        /// <summary>
        /// Fetch a single int-valued claim.
        /// </summary>
        /// <remarks>
        /// The result will be null if the claim does not exist or cannot be parsed as an int.
        /// </remarks>
        public int? AsInt(string claim)
        {
            if (this.pecan == null)
            {
                return null;
            }

            if (values.TryGetValue(claim, out var cached))
            {
                var cast = cached as int?;
                if (object.Equals(cast, cached))
                {
                    return cast;
                }
            }

            var selected = this.AsString(claim);
            if (int.TryParse(selected, out var value))
            {
                values[claim] = value;
                return value;
            }
            else if (selected == null)
            {
                values[claim] = null;
            }
            return null;
        }

        /// <summary>
        /// Fetch a single long-valued claim.
        /// </summary>
        /// <remarks>
        /// The result will be null if the claim does not exist or cannot be parsed as a long.
        /// </remarks>
        public long? AsLong(string claim)
        {
            if (this.pecan == null)
            {
                return null;
            }

            if (values.TryGetValue(claim, out var cached))
            {
                var cast = cached as long?;
                if (object.Equals(cast, cached))
                {
                    return cast;
                }
            }

            var selected = this.AsString(claim);
            if (long.TryParse(selected, out var value))
            {
                values[claim] = value;
                return value;
            }
            else if (selected == null)
            {
                values[claim] = null;
            }
            return null;
        }

        /// <summary>
        /// Fetch a single decimal-valued claim.
        /// </summary>
        /// <remarks>
        /// The result will be null if the claim does not exist or cannot be parsed as a decimal.
        /// </remarks>
        public decimal? AsDecimal(string claim)
        {
            if (this.pecan == null)
            {
                return null;
            }

            if (values.TryGetValue(claim, out var cached))
            {
                var cast = cached as decimal?;
                if (object.Equals(cast, cached))
                {
                    return cast;
                }
            }

            var selected = this.AsString(claim);
            if (decimal.TryParse(selected, out var value))
            {
                values[claim] = value;
                return value;
            }
            else if (selected == null)
            {
                values[claim] = null;
            }
            return null;
        }

        /// <summary>
        /// Fetch a single float-valued claim.
        /// </summary>
        /// <remarks>
        /// The result will be null if the claim does not exist or cannot be parsed as a float.
        /// </remarks>
        public float? AsFloat(string claim)
        {
            if (this.pecan == null)
            {
                return null;
            }

            if (values.TryGetValue(claim, out var cached))
            {
                var cast = cached as float?;
                if (object.Equals(cast, cached))
                {
                    return cast;
                }
            }

            var selected = this.AsString(claim);
            if (float.TryParse(selected, out var value))
            {
                values[claim] = value;
                return value;
            }
            else if (selected == null)
            {
                values[claim] = null;
            }
            return null;
        }

        /// <summary>
        /// Fetch a single double-valued claim.
        /// </summary>
        /// <remarks>
        /// The result will be null if the claim does not exist or cannot be parsed as a double.
        /// </remarks>
        public double? AsDouble(string claim)
        {
            if (this.pecan == null)
            {
                return null;
            }

            if (values.TryGetValue(claim, out var cached))
            {
                var cast = cached as double?;
                if (object.Equals(cast, cached))
                {
                    return cast;
                }
            }

            var selected = this.AsString(claim);
            if (double.TryParse(selected, out var value))
            {
                values[claim] = value;
                return value;
            }
            else if (selected == null)
            {
                values[claim] = null;
            }
            return null;
        }

        /// <summary>
        /// Fetch a single datetime-valued claim.
        /// </summary>
        /// <remarks>
        /// The result will be null if the claim does not exist or cannot be parsed as a datetime.
        /// </remarks>
        public DateTimeOffset? AsDateTimeOffset(string claim)
        {
            if (this.pecan == null)
            {
                return null;
            }

            if (values.TryGetValue(claim, out var cached))
            {
                var cast = cached as DateTimeOffset?;
                if (object.Equals(cast, cached))
                {
                    return cast;
                }
            }

            var selected = this.AsString(claim);
            if (DateTimeOffset.TryParse(selected, out var value))
            {
                values[claim] = value;
                return value;
            }
            else if (selected == null)
            {
                values[claim] = null;
            }
            return null;
        }

        private bool TryGetPermissions(string claim, out Permissions permissions)
        {
            if (cache.TryGetValue(claim, out permissions))
            {
                return true;
            }
            else if (values.TryGetValue(claim, out var cached))
            {
                var bytes = cached as byte[];
                if (bytes?.Length >= 4)
                {
                    permissions = new Permissions(bytes);
                    cache.TryAdd(claim, permissions);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            var selected = this.AsString(claim);
            if (selected != null)
            {
                try
                {
                    var bytes = Convert.FromBase64String(selected);
                    if (bytes.Length >= 4)
                    {
                        permissions = new Permissions(bytes);
                        values[claim] = bytes;
                        cache.TryAdd(claim, permissions);
                        return true;
                    }
                }
                catch (FormatException) { }
                catch (ArgumentOutOfRangeException) { }
            }
            else
            {
                values[claim] = null;
            }

            return false;
        }

    }

}