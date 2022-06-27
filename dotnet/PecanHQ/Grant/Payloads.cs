// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable

using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using meta = PecanHQ;

namespace PecanHQ.Grant.Types
{

    /// <summary>
    /// Refresh the user's session.
    /// </summary>
    public class Refresh
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Refresh()
        {
        }

        /// <summary>
        /// The specific session.
        /// </summary>
        [JsonPropertyName("session")]
        public string? Session { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Refresh that)
            {
                return Equals(this.Session, that.Session);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Session);
        }

    }

    /// <summary>
    /// Update artifact metadata.
    /// </summary>
    public class UpdateArtifact
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UpdateArtifact(
            string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// Full-text information about the artifact.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UpdateArtifact that)
            {
                return Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Description);
        }

    }

    /// <summary>
    /// Create a new service.
    /// </summary>
    public class CreateService
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public CreateService(
            string name,
            string claim,
            string description,
            string provider)
        {
            this.Name = name;
            this.Claim = claim;
            this.Description = description;
            this.Provider = provider;
        }

        /// <summary>
        /// The resource group name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The permissions claim key.
        /// </summary>
        [JsonPropertyName("claim")]
        public string Claim { get; set; }

        /// <summary>
        /// Full-text information about the resource group.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The provider name.
        /// </summary>
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        /// <summary>
        /// An OpenAPI specification.
        /// </summary>
        [JsonPropertyName("openapi")]
        public meta::IAttachment? Openapi { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is CreateService that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Claim, that.Claim)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Provider, that.Provider)
                    && Equals(this.Openapi, that.Openapi);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Claim, Description, Provider, Openapi);
        }

    }

    /// <summary>
    /// Update an existing resource group.
    /// </summary>
    public class UpdateService
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UpdateService(
            string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// Full-text information about the resource group.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UpdateService that)
            {
                return Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Description);
        }

    }

    /// <summary>
    /// Create a new resource.
    /// </summary>
    public class CreateResource
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public CreateResource(
            string name,
            int mask,
            int restricted,
            string description)
        {
            this.Name = name;
            this.Mask = mask;
            this.Restricted = restricted;
            this.Description = description;
        }

        /// <summary>
        /// The name of this resource.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The unix permissions associated with this resource (7:rwx,
        /// 6:rw-, 5:r-x, 4:r--, 3:-wx, 2:-w-, 1:--x).
        /// </summary>
        [JsonPropertyName("mask")]
        public int Mask { get; set; }

        /// <summary>
        /// The restriction applied to the mask for normal user
        /// accounts. System accountabilities have full access to the
        /// unmodified mask.
        /// </summary>
        [JsonPropertyName("restricted")]
        public int Restricted { get; set; }

        /// <summary>
        /// A human-readable description of the resource.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is CreateResource that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Restricted, that.Restricted)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Mask, Restricted, Description);
        }

    }

    /// <summary>
    /// Update a resource.
    /// </summary>
    public class UpdateResource
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UpdateResource(
            Guid resourceId,
            string name,
            int mask,
            int restricted,
            string description,
            bool archived)
        {
            this.ResourceId = resourceId;
            this.Name = name;
            this.Mask = mask;
            this.Restricted = restricted;
            this.Description = description;
            this.Archived = archived;
        }

        /// <summary>
        /// The resource identifier.
        /// </summary>
        [JsonPropertyName("resource_id")]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// The name of this resource.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The unix permissions associated with this resource (7:rwx,
        /// 6:rw-, 5:r-x, 4:r--, 3:-wx, 2:-w-, 1:--x).
        /// </summary>
        [JsonPropertyName("mask")]
        public int Mask { get; set; }

        /// <summary>
        /// The restriction applied to the mask for normal user
        /// accounts. System accountabilities have full access to the
        /// unmodified mask.
        /// </summary>
        [JsonPropertyName("restricted")]
        public int Restricted { get; set; }

        /// <summary>
        /// A human-readable description of the resource.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// A flag indicating the resource should currently be archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UpdateResource that)
            {
                return Equals(this.ResourceId, that.ResourceId)
                    && Equals(this.Name, that.Name)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Restricted, that.Restricted)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(ResourceId, Name, Mask, Restricted, Description, Archived);
        }

    }

    /// <summary>
    /// Create a child policy group.
    /// </summary>
    public class CreateRole
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public CreateRole(
            string name,
            string description)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// The name of this policy group.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this policy group.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The maximum permissions that can be granted within this
        /// group.
        /// </summary>
        [JsonPropertyName("mask")]
        public int? Mask { get; set; }

        /// <summary>
        /// The name of the externally managed scope for the policy
        /// group, used by the authentication provider to expand access
        /// tokens.
        /// </summary>
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is CreateRole that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Scope, that.Scope);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Mask, Scope);
        }

    }

    /// <summary>
    /// Create a new security claim.
    /// </summary>
    public class CreateClaim
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public CreateClaim(
            string claim,
            string description,
            bool unique)
        {
            this.Claim = claim;
            this.Description = description;
            this.Unique = unique;
        }

        /// <summary>
        /// The claim key.
        /// </summary>
        [JsonPropertyName("claim")]
        public string Claim { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this claim.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// A flag indicating the claim is a unique key for an account.
        /// </summary>
        [JsonPropertyName("unique")]
        public bool Unique { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is CreateClaim that)
            {
                return Equals(this.Claim, that.Claim)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Unique, that.Unique);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Claim, Description, Unique);
        }

    }

    /// <summary>
    /// Update a security claim.
    /// </summary>
    public class UpdateClaim
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UpdateClaim(
            string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// A description of the semantic meaning of this claim.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UpdateClaim that)
            {
                return Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Description);
        }

    }

    /// <summary>
    /// Configure an access policy for an account.
    /// </summary>
    public class SetAccountAccess
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SetAccountAccess(
            List<ClaimValue> claims)
        {
            this.Claims = claims;
        }

        /// <summary>
        /// All claim values to be modified.
        /// </summary>
        [JsonPropertyName("claims")]
        public List<ClaimValue> Claims { get; set; }

        /// <summary>
        /// The permissions granted to the user for this group.
        /// </summary>
        [JsonPropertyName("mask")]
        public int? Mask { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SetAccountAccess that)
            {
                return Equals(this.Claims, that.Claims)
                    && Equals(this.Mask, that.Mask);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Claims, Mask);
        }

    }

    /// <summary>
    /// Create a new account.
    /// </summary>
    public class CreateAccount
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public CreateAccount(
            string display,
            List<ClaimValue> claims)
        {
            this.Display = display;
            this.Claims = claims;
        }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <summary>
        /// All claims associated with the account.
        /// </summary>
        [JsonPropertyName("claims")]
        public List<ClaimValue> Claims { get; set; }

        /// <summary>
        /// A description of the account.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The tenant organization account, if a multi-tenanted
        /// account.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public Guid? OrganizationId { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is CreateAccount that)
            {
                return Equals(this.Display, that.Display)
                    && Equals(this.Claims, that.Claims)
                    && Equals(this.Description, that.Description)
                    && Equals(this.OrganizationId, that.OrganizationId);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Display, Claims, Description, OrganizationId);
        }

    }

    /// <summary>
    /// Create a new  artifact.
    /// </summary>
    public class CreateArtifact
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public CreateArtifact(
            string name,
            string description)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// The artifact name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Full-text information about the artifact.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is CreateArtifact that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }

    }

    /// <summary>
    /// Mark a policy group with general access status.
    /// </summary>
    public class SetGeneralAccess
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SetGeneralAccess(
            bool enabled)
        {
            this.Enabled = enabled;
        }

        /// <summary>
        /// A flag indicating the policy group should be made general
        /// access.
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SetGeneralAccess that)
            {
                return Equals(this.Enabled, that.Enabled);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Enabled);
        }

    }

    /// <summary>
    /// Update an account's metadata.
    /// </summary>
    public class UpdateAccount
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UpdateAccount(
            string display)
        {
            this.Display = display;
        }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <summary>
        /// A description of the account.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UpdateAccount that)
            {
                return Equals(this.Display, that.Display)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Display, Description);
        }

    }

    /// <summary>
    /// Update an existing policy group.
    /// </summary>
    public class UpdateRole
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UpdateRole(
            string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// Full-text information about the policy group.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UpdateRole that)
            {
                return Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Description);
        }

    }

    /// <summary>
    /// Register an artifact from an application manifest.
    /// </summary>
    public class Register
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Register(
            meta::IAttachment manifest)
        {
            this.Manifest = manifest;
        }

        /// <summary>
        /// The application manifest.
        /// </summary>
        [JsonPropertyName("manifest")]
        public meta::IAttachment Manifest { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Register that)
            {
                return Equals(this.Manifest, that.Manifest);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Manifest);
        }

    }

    /// <summary>
    /// Create a new authentication provider.
    /// </summary>
    public class CreateProvider
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public CreateProvider(
            string name,
            string description)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// The provider name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Full-text information about the provider.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The subject identifier claim key.
        /// </summary>
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        /// <summary>
        /// The tenant identifier claim key, if multi-tenanted.
        /// </summary>
        [JsonPropertyName("tenant")]
        public string? Tenant { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is CreateProvider that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Subject, that.Subject)
                    && Equals(this.Tenant, that.Tenant);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Subject, Tenant);
        }

    }

    /// <summary>
    /// Update provider metadata.
    /// </summary>
    public class UpdateProvider
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UpdateProvider(
            string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// Full-text information about the provider.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UpdateProvider that)
            {
                return Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Description);
        }

    }

    /// <summary>
    /// Update the active status for a resource.
    /// </summary>
    public class SetResourceAccess
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SetResourceAccess(
            List<Guid> enabled,
            List<Guid> disabled)
        {
            this.Enabled = enabled;
            this.Disabled = disabled;
        }

        /// <summary>
        /// The resource identifiers to enable.
        /// </summary>
        [JsonPropertyName("enabled")]
        public List<Guid> Enabled { get; set; }

        /// <summary>
        /// The resource identifiers to disable.
        /// </summary>
        [JsonPropertyName("disabled")]
        public List<Guid> Disabled { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SetResourceAccess that)
            {
                return Equals(this.Enabled, that.Enabled)
                    && Equals(this.Disabled, that.Disabled);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Enabled, Disabled);
        }

    }

    /// <summary>
    /// Register a system tenant.
    /// </summary>
    public class CreateTenant
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public CreateTenant(
            string name,
            List<ClaimValue> claims)
        {
            this.Name = name;
            this.Claims = claims;
        }

        /// <summary>
        /// The organization's display name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// All claims associated with the account.
        /// </summary>
        [JsonPropertyName("claims")]
        public List<ClaimValue> Claims { get; set; }

        /// <summary>
        /// A description of the organization.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The tenant organization account, if a multi-tenanted
        /// account.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public Guid? OrganizationId { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is CreateTenant that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Claims, that.Claims)
                    && Equals(this.Description, that.Description)
                    && Equals(this.OrganizationId, that.OrganizationId);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Claims, Description, OrganizationId);
        }

    }

    /// <summary>
    /// Set the identity for a account within a provider.
    /// </summary>
    public class SetIdentity
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SetIdentity(
            string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// The claim key for the identity claim.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The identifier value, if the account should be granted
        /// access.
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SetIdentity that)
            {
                return Equals(this.Key, that.Key)
                    && Equals(this.Value, that.Value);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Value);
        }

    }

    /// <summary>
    /// Update an identity claim with a secret value.
    /// </summary>
    public class UpdateIdentity
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UpdateIdentity(
            string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// The claim key for the identity claim.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The secret value for the identity claim.
        /// </summary>
        [JsonPropertyName("secret")]
        public byte[]? Secret { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UpdateIdentity that)
            {
                return Equals(this.Key, that.Key)
                    && Equals(this.Secret, that.Secret);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Secret);
        }

    }

    /// <summary>
    /// Update the service status.
    /// </summary>
    public class SetServiceStatus
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SetServiceStatus(
            bool archived)
        {
            this.Archived = archived;
        }

        /// <summary>
        /// A flag indicating the service should be archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SetServiceStatus that)
            {
                return Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Archived);
        }

    }

    /// <summary>
    /// A flag indicating the artifact should be archived.
    /// </summary>
    public class SetArtifactStatus
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SetArtifactStatus(
            bool archived)
        {
            this.Archived = archived;
        }

        /// <summary>
        /// A flag indicating the resource group should currently be
        /// archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SetArtifactStatus that)
            {
                return Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Archived);
        }

    }

    /// <summary>
    /// Update the account status.
    /// </summary>
    public class SetAccountStatus
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SetAccountStatus(
            bool archived)
        {
            this.Archived = archived;
        }

        /// <summary>
        /// A flag indicating the account should be archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SetAccountStatus that)
            {
                return Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Archived);
        }

    }

    /// <summary>
    /// Activate a specific artifact version.
    /// </summary>
    public class SetReleaseStatus
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SetReleaseStatus(
            string artifact,
            decimal schema,
            bool published)
        {
            this.Artifact = artifact;
            this.Schema = schema;
            this.Published = published;
        }

        /// <summary>
        /// The name of this artifact.
        /// </summary>
        [JsonPropertyName("artifact")]
        public string Artifact { get; set; }

        /// <summary>
        /// The artifact schema version string.
        /// </summary>
        [JsonPropertyName("schema")]
        public decimal Schema { get; set; }

        /// <summary>
        /// A flag indicating whether the artifact should be the
        /// currently published version of the schema.
        /// </summary>
        [JsonPropertyName("published")]
        public bool Published { get; set; }

        /// <summary>
        /// Optionally override the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SetReleaseStatus that)
            {
                return Equals(this.Artifact, that.Artifact)
                    && Equals(this.Schema, that.Schema)
                    && Equals(this.Published, that.Published)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Artifact, Schema, Published, Description);
        }

    }

    /// <summary>
    /// Refresh all profile assertions.
    /// </summary>
    public class RefreshProfile
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public RefreshProfile(
            Guid accountId)
        {
            this.AccountId = accountId;
        }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is RefreshProfile that)
            {
                return Equals(this.AccountId, that.AccountId);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(AccountId);
        }

    }

    /// <summary>
    /// Register a new application identity.
    /// </summary>
    public class RegisterApp
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public RegisterApp(
            string display)
        {
            this.Display = display;
        }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <summary>
        /// A description of the account.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is RegisterApp that)
            {
                return Equals(this.Display, that.Display)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Display, Description);
        }

    }

    /// <summary>
    /// Use delegated authority to override the account access level
    /// for a policy.
    /// </summary>
    public class OverrideAccountAccess
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public OverrideAccountAccess(
            Guid policyGroupId,
            Guid accountId,
            Guid administratorId,
            List<ClaimValue> claims)
        {
            this.PolicyGroupId = policyGroupId;
            this.AccountId = accountId;
            this.AdministratorId = administratorId;
            this.Claims = claims;
        }

        /// <summary>
        /// The policy group name for this role.
        /// </summary>
        [JsonPropertyName("policy_group_id")]
        public Guid PolicyGroupId { get; set; }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The administrator account controlling the action.
        /// </summary>
        [JsonPropertyName("administrator_id")]
        public Guid AdministratorId { get; set; }

        /// <summary>
        /// All claim values to be modified.
        /// </summary>
        [JsonPropertyName("claims")]
        public List<ClaimValue> Claims { get; set; }

        /// <summary>
        /// The permissions granted to the user for this group.
        /// </summary>
        [JsonPropertyName("mask")]
        public int? Mask { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is OverrideAccountAccess that)
            {
                return Equals(this.PolicyGroupId, that.PolicyGroupId)
                    && Equals(this.AccountId, that.AccountId)
                    && Equals(this.AdministratorId, that.AdministratorId)
                    && Equals(this.Claims, that.Claims)
                    && Equals(this.Mask, that.Mask);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(PolicyGroupId, AccountId, AdministratorId, Claims, Mask);
        }

    }

    /// <summary>
    /// Override account metadata.
    /// </summary>
    public class OverrideAccount
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public OverrideAccount(
            Guid accountId,
            Guid administratorId,
            string display,
            List<ClaimValue> claims)
        {
            this.AccountId = accountId;
            this.AdministratorId = administratorId;
            this.Display = display;
            this.Claims = claims;
        }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The administrator account controlling the action.
        /// </summary>
        [JsonPropertyName("administrator_id")]
        public Guid AdministratorId { get; set; }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <summary>
        /// All claim values to be modified.
        /// </summary>
        [JsonPropertyName("claims")]
        public List<ClaimValue> Claims { get; set; }

        /// <summary>
        /// A description of the account.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is OverrideAccount that)
            {
                return Equals(this.AccountId, that.AccountId)
                    && Equals(this.AdministratorId, that.AdministratorId)
                    && Equals(this.Display, that.Display)
                    && Equals(this.Claims, that.Claims)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(AccountId, AdministratorId, Display, Claims, Description);
        }

    }

    /// <summary>
    /// Assign an externally managed identity, optionally creating
    /// the profile if it does not exist.
    /// </summary>
    public class AssignIdentity
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public AssignIdentity(
            string key,
            string value,
            List<string> scopes)
        {
            this.Key = key;
            this.Value = value;
            this.Scopes = scopes;
        }

        /// <summary>
        /// The claim key for the identity claim.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The asserted claim value.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// All external scopes assigned to this user. Any existing
        /// roles belonging to a scope not in this collection will be
        /// disabled.
        /// </summary>
        [JsonPropertyName("scopes")]
        public List<string> Scopes { get; set; }

        /// <summary>
        /// The secret value for the identity claim.
        /// </summary>
        [JsonPropertyName("secret")]
        public byte[]? Secret { get; set; }

        /// <summary>
        /// The value of the associated tenant claim, if the key claim
        /// is associated with a multi-tenanted identity.
        /// </summary>
        [JsonPropertyName("tenant")]
        public string? Tenant { get; set; }

        /// <summary>
        /// The display name for the account, if it should be
        /// overwritten.
        /// </summary>
        [JsonPropertyName("display")]
        public string? Display { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is AssignIdentity that)
            {
                return Equals(this.Key, that.Key)
                    && Equals(this.Value, that.Value)
                    && Equals(this.Scopes, that.Scopes)
                    && Equals(this.Secret, that.Secret)
                    && Equals(this.Tenant, that.Tenant)
                    && Equals(this.Display, that.Display);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Value, Scopes, Secret, Tenant, Display);
        }

    }

    /// <summary>
    /// Update organization details.
    /// </summary>
    public class UpdateOrganization
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UpdateOrganization(
            string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// The organization name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the organization.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UpdateOrganization that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }

    }

    /// <summary>
    /// Send an invitation for a user to take ownership of an
    /// account.
    /// </summary>
    public class SendInvitation
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SendInvitation(
            string email)
        {
            this.Email = email;
        }

        /// <summary>
        /// The user's email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SendInvitation that)
            {
                return Equals(this.Email, that.Email);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Email);
        }

    }

    /// <summary>
    /// Perform initial setup actions for an account.
    /// </summary>
    public class Setup
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Setup(
            string name,
            decimal version,
            string claim,
            string description,
            string idp)
        {
            this.Name = name;
            this.Version = version;
            this.Claim = claim;
            this.Description = description;
            this.Idp = idp;
        }

        /// <summary>
        /// The application name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The schema version.
        /// </summary>
        [JsonPropertyName("version")]
        public decimal Version { get; set; }

        /// <summary>
        /// The permissions claims.
        /// </summary>
        [JsonPropertyName("claim")]
        public string Claim { get; set; }

        /// <summary>
        /// A description of the application.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The identity provider name.
        /// </summary>
        [JsonPropertyName("idp")]
        public string Idp { get; set; }

        /// <summary>
        /// The user id claim, if relevant for the identity provider.
        /// </summary>
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        /// <summary>
        /// The tenant id claim, if relevant for the identity provider.
        /// </summary>
        [JsonPropertyName("tenant")]
        public string? Tenant { get; set; }

        /// <summary>
        /// The OpenAPI document for the application.
        /// </summary>
        [JsonPropertyName("openapi")]
        public meta::IAttachment? Openapi { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Setup that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Version, that.Version)
                    && Equals(this.Claim, that.Claim)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Idp, that.Idp)
                    && Equals(this.Subject, that.Subject)
                    && Equals(this.Tenant, that.Tenant)
                    && Equals(this.Openapi, that.Openapi);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Version, Claim, Description, Idp, Subject, Tenant, Openapi);
        }

    }

    /// <summary>
    /// Perform manual configuration of the account.
    /// </summary>
    public class Configure
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Configure(
            List<PermissionDefinition> permissions)
        {
            this.Permissions = permissions;
        }

        /// <summary>
        /// All permissions associated with the issuer.
        /// </summary>
        [JsonPropertyName("permissions")]
        public List<PermissionDefinition> Permissions { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Configure that)
            {
                return Equals(this.Permissions, that.Permissions);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Permissions);
        }

    }

}