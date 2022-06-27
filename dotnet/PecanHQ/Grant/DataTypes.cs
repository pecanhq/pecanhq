// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable

using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace PecanHQ.Grant.Types
{

    /// <summary>
    /// Information about an artifact.
    /// </summary>
    public class ArtifactMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ArtifactMetadata(
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

        /// <summary>
        /// The published version of the artifact.
        /// </summary>
        [JsonPropertyName("published")]
        public decimal? Published { get; set; }

        /// <summary>
        /// The instant the artifact was last modified.
        /// </summary>
        [JsonPropertyName("modified")]
        public DateTimeOffset? Modified { get; set; }

        /// <summary>
        /// The instant the artifact was archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public DateTimeOffset? Archived { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ArtifactMetadata that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Published, that.Published)
                    && Equals(this.Modified, that.Modified)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Published, Modified, Archived);
        }

    }

    /// <summary>
    /// Full details about an artifact.
    /// </summary>
    public class ArtifactDetails : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ArtifactDetails(
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

        /// <summary>
        /// The published version of the artifact.
        /// </summary>
        [JsonPropertyName("published")]
        public decimal? Published { get; set; }

        /// <summary>
        /// The instant the artifact was last modified.
        /// </summary>
        [JsonPropertyName("modified")]
        public DateTimeOffset? Modified { get; set; }

        /// <summary>
        /// The instant the artifact was archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public DateTimeOffset? Archived { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ArtifactDetails that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Published, that.Published)
                    && Equals(this.Modified, that.Modified)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Published, Modified, Archived);
        }

    }

    /// <summary>
    /// Summary information about a resource group.
    /// </summary>
    public class ServiceMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ServiceMetadata(
            string name,
            string claim,
            string description)
        {
            this.Name = name;
            this.Claim = claim;
            this.Description = description;
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
        /// The instant the resource group was last modified.
        /// </summary>
        [JsonPropertyName("modified")]
        public DateTimeOffset? Modified { get; set; }

        /// <summary>
        /// The instant the resource group was archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public DateTimeOffset? Archived { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ServiceMetadata that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Claim, that.Claim)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Modified, that.Modified)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Claim, Description, Modified, Archived);
        }

    }

    /// <summary>
    /// Information about a user group.
    /// </summary>
    public class ServiceDetails : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ServiceDetails(
            string name,
            string claim,
            string description,
            ProviderMetadata provider)
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
        /// Information about the resource group provider.
        /// </summary>
        [JsonPropertyName("provider")]
        public ProviderMetadata Provider { get; set; }

        /// <summary>
        /// The instant the resource group was last modified.
        /// </summary>
        [JsonPropertyName("modified")]
        public DateTimeOffset? Modified { get; set; }

        /// <summary>
        /// The instant the resource group was archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public DateTimeOffset? Archived { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ServiceDetails that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Claim, that.Claim)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Provider, that.Provider)
                    && Equals(this.Modified, that.Modified)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Claim, Description, Provider, Modified, Archived);
        }

    }

    /// <summary>
    /// Summary information about a resource.
    /// </summary>
    public class ResourceMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ResourceMetadata(
            Guid resourceId,
            string name,
            int mask,
            int restricted,
            string description,
            DateTimeOffset modified)
        {
            this.ResourceId = resourceId;
            this.Name = name;
            this.Mask = mask;
            this.Restricted = restricted;
            this.Description = description;
            this.Modified = modified;
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
        /// The instant the resource was last modified.
        /// </summary>
        [JsonPropertyName("modified")]
        public DateTimeOffset Modified { get; set; }

        /// <summary>
        /// The initial claim version with which this resource was
        /// released.
        /// </summary>
        [JsonPropertyName("version")]
        public int? Version { get; set; }

        /// <summary>
        /// The instant the resource was archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public DateTimeOffset? Archived { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ResourceMetadata that)
            {
                return Equals(this.ResourceId, that.ResourceId)
                    && Equals(this.Name, that.Name)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Restricted, that.Restricted)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Modified, that.Modified)
                    && Equals(this.Version, that.Version)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(ResourceId, Name, Mask, Restricted, Description, Modified, Version, Archived);
        }

    }

    /// <summary>
    /// Information about a released schema.
    /// </summary>
    public class ReleaseMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ReleaseMetadata(
            string artifact,
            decimal schema,
            string description,
            DateTimeOffset created,
            bool published)
        {
            this.Artifact = artifact;
            this.Schema = schema;
            this.Description = description;
            this.Created = created;
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
        /// A human-readable description of the artifact.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The instant the release was created.
        /// </summary>
        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// A flag indicating this release is the currently active
        /// version of the artifact.
        /// </summary>
        [JsonPropertyName("published")]
        public bool Published { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ReleaseMetadata that)
            {
                return Equals(this.Artifact, that.Artifact)
                    && Equals(this.Schema, that.Schema)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Created, that.Created)
                    && Equals(this.Published, that.Published);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Artifact, Schema, Description, Created, Published);
        }

    }

    /// <summary>
    /// Information about a service within a schema.
    /// </summary>
    public class ServiceClaim : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ServiceClaim(
            string authority,
            string provider,
            string name,
            string claim,
            int version,
            int hwm,
            List<ServicePermission> permissions)
        {
            this.Authority = authority;
            this.Provider = provider;
            this.Name = name;
            this.Claim = claim;
            this.Version = version;
            this.Hwm = hwm;
            this.Permissions = permissions;
        }

        /// <summary>
        /// The issuing authority for the service.
        /// </summary>
        [JsonPropertyName("authority")]
        public string Authority { get; set; }

        /// <summary>
        /// The provider key.
        /// </summary>
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        /// <summary>
        /// The name of this service.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The permissions claim key.
        /// </summary>
        [JsonPropertyName("claim")]
        public string Claim { get; set; }

        /// <summary>
        /// The claim version.
        /// </summary>
        [JsonPropertyName("version")]
        public int Version { get; set; }

        /// <summary>
        /// The number of positions available in the permissions claim.
        /// </summary>
        [JsonPropertyName("hwm")]
        public int Hwm { get; set; }

        /// <summary>
        /// All service permissions available.
        /// </summary>
        [JsonPropertyName("permissions")]
        public List<ServicePermission> Permissions { get; set; }

        /// <summary>
        /// The subject claim, if the provider sets a subject
        /// identifier.
        /// </summary>
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        /// <summary>
        /// The tenant claim, if the provider sets a tenant identifier.
        /// </summary>
        [JsonPropertyName("tenant")]
        public string? Tenant { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ServiceClaim that)
            {
                return Equals(this.Authority, that.Authority)
                    && Equals(this.Provider, that.Provider)
                    && Equals(this.Name, that.Name)
                    && Equals(this.Claim, that.Claim)
                    && Equals(this.Version, that.Version)
                    && Equals(this.Hwm, that.Hwm)
                    && Equals(this.Permissions, that.Permissions)
                    && Equals(this.Subject, that.Subject)
                    && Equals(this.Tenant, that.Tenant);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Authority);
            hash.Add(Provider);
            hash.Add(Name);
            hash.Add(Claim);
            hash.Add(Version);
            hash.Add(Hwm);
            hash.Add(Permissions);
            hash.Add(Subject);
            hash.Add(Tenant);;
            return hash.ToHashCode();
        }

    }

    /// <summary>
    /// Information about a role.
    /// </summary>
    public class RoleMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public RoleMetadata(
            string name,
            int mask,
            bool generalAccess,
            string description,
            bool system)
        {
            this.Name = name;
            this.Mask = mask;
            this.GeneralAccess = generalAccess;
            this.Description = description;
            this.System = system;
        }

        /// <summary>
        /// The name of this policy group.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The maximum permissions that can be granted within this
        /// group.
        /// </summary>
        [JsonPropertyName("mask")]
        public int Mask { get; set; }

        /// <summary>
        /// A flag indicating this policy group grants access to all
        /// logged in users.
        /// </summary>
        [JsonPropertyName("general_access")]
        public bool GeneralAccess { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this policy group.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// A flag indicating whether the role is controlled by the
        /// system.
        /// </summary>
        [JsonPropertyName("system")]
        public bool System { get; set; }

        /// <summary>
        /// The name of the externally managed scope for the policy
        /// group, used by the authentication provider to expand access
        /// tokens.
        /// </summary>
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        /// <summary>
        /// The instant the role was archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public DateTimeOffset? Archived { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is RoleMetadata that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.GeneralAccess, that.GeneralAccess)
                    && Equals(this.Description, that.Description)
                    && Equals(this.System, that.System)
                    && Equals(this.Scope, that.Scope)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Mask, GeneralAccess, Description, System, Scope, Archived);
        }

    }

    /// <summary>
    /// Information about a role.
    /// </summary>
    public class RoleDetails : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public RoleDetails(
            string name,
            int mask,
            bool generalAccess,
            string description,
            ProviderMetadata provider)
        {
            this.Name = name;
            this.Mask = mask;
            this.GeneralAccess = generalAccess;
            this.Description = description;
            this.Provider = provider;
        }

        /// <summary>
        /// The name of this policy group.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The maximum permissions that can be granted within this
        /// group.
        /// </summary>
        [JsonPropertyName("mask")]
        public int Mask { get; set; }

        /// <summary>
        /// A flag indicating this policy group grants access to all
        /// logged in users.
        /// </summary>
        [JsonPropertyName("general_access")]
        public bool GeneralAccess { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this policy group.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Information about the authentication provider.
        /// </summary>
        [JsonPropertyName("provider")]
        public ProviderMetadata Provider { get; set; }

        /// <summary>
        /// The name of the externally managed scope for the policy
        /// group, used by the authentication provider to expand access
        /// tokens.
        /// </summary>
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        /// <summary>
        /// Details of the parent role.
        /// </summary>
        [JsonPropertyName("parent")]
        public RoleMetadata? Parent { get; set; }

        /// <summary>
        /// The instant the role was archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public DateTimeOffset? Archived { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is RoleDetails that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.GeneralAccess, that.GeneralAccess)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Provider, that.Provider)
                    && Equals(this.Scope, that.Scope)
                    && Equals(this.Parent, that.Parent)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Mask, GeneralAccess, Description, Provider, Scope, Parent, Archived);
        }

    }

    /// <summary>
    /// Information about a security claim.
    /// </summary>
    public class ClaimMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ClaimMetadata(
            string key,
            string description,
            bool unique)
        {
            this.Key = key;
            this.Description = description;
            this.Unique = unique;
        }

        /// <summary>
        /// The claim key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this claim.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// A flag indicating this claim acts as a unique key for an
        /// account.
        /// </summary>
        [JsonPropertyName("unique")]
        public bool Unique { get; set; }

        /// <summary>
        /// The role managing this claim, if not a system claim.
        /// </summary>
        [JsonPropertyName("role")]
        public ProviderRole? Role { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ClaimMetadata that)
            {
                return Equals(this.Key, that.Key)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Unique, that.Unique)
                    && Equals(this.Role, that.Role);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Description, Unique, Role);
        }

    }

    /// <summary>
    /// Detailed information about a claim.
    /// </summary>
    public class ClaimDetails : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ClaimDetails(
            string key,
            string description,
            bool unique)
        {
            this.Key = key;
            this.Description = description;
            this.Unique = unique;
        }

        /// <summary>
        /// The claim key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this claim.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// A flag indicating this claim acts as a unique key for an
        /// account.
        /// </summary>
        [JsonPropertyName("unique")]
        public bool Unique { get; set; }

        /// <summary>
        /// The role managing this claim, if not a system claim.
        /// </summary>
        [JsonPropertyName("role")]
        public ProviderRole? Role { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ClaimDetails that)
            {
                return Equals(this.Key, that.Key)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Unique, that.Unique)
                    && Equals(this.Role, that.Role);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Description, Unique, Role);
        }

    }

    /// <summary>
    /// Information about an account.
    /// </summary>
    public class AccountMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public AccountMetadata(
            Guid accountId,
            string display,
            List<SecurityClaim> claims)
        {
            this.AccountId = accountId;
            this.Display = display;
            this.Claims = claims;
        }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <summary>
        /// All requested security claims for the account.
        /// </summary>
        [JsonPropertyName("claims")]
        public List<SecurityClaim> Claims { get; set; }

        /// <summary>
        /// A description of the account.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The account category, from app to organization to user.
        /// </summary>
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        /// <summary>
        /// The claim value that was used to locate this account.
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; set; }

        /// <summary>
        /// The instant the account was archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public DateTimeOffset? Archived { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is AccountMetadata that)
            {
                return Equals(this.AccountId, that.AccountId)
                    && Equals(this.Display, that.Display)
                    && Equals(this.Claims, that.Claims)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Category, that.Category)
                    && Equals(this.Value, that.Value)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(AccountId, Display, Claims, Description, Category, Value, Archived);
        }

    }

    /// <summary>
    /// Information about an account.
    /// </summary>
    public class AccountDetails : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public AccountDetails(
            Guid accountId,
            string display,
            bool refresh,
            List<IdentityMetadata> identities,
            List<string> fingerprints,
            OrganizationInfo tenant)
        {
            this.AccountId = accountId;
            this.Display = display;
            this.Refresh = refresh;
            this.Identities = identities;
            this.Fingerprints = fingerprints;
            this.Tenant = tenant;
        }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <summary>
        /// A flag indicating the profile should be refreshed.
        /// </summary>
        [JsonPropertyName("refresh")]
        public bool Refresh { get; set; }

        /// <summary>
        /// All external identities claimed by this account.
        /// </summary>
        [JsonPropertyName("identities")]
        public List<IdentityMetadata> Identities { get; set; }

        /// <summary>
        /// All active keys associated with the account.
        /// </summary>
        [JsonPropertyName("fingerprints")]
        public List<string> Fingerprints { get; set; }

        /// <summary>
        /// Tenancy information for the user.
        /// </summary>
        [JsonPropertyName("tenant")]
        public OrganizationInfo Tenant { get; set; }

        /// <summary>
        /// A description of the account.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The user metadata for the account.
        /// </summary>
        [JsonPropertyName("user")]
        public UserMetadata? User { get; set; }

        /// <summary>
        /// The organization for the account, if not a user account.
        /// </summary>
        [JsonPropertyName("organization")]
        public OrganizationInfo? Organization { get; set; }

        /// <summary>
        /// The instant the account was archived.
        /// </summary>
        [JsonPropertyName("archived")]
        public DateTimeOffset? Archived { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is AccountDetails that)
            {
                return Equals(this.AccountId, that.AccountId)
                    && Equals(this.Display, that.Display)
                    && Equals(this.Refresh, that.Refresh)
                    && Equals(this.Identities, that.Identities)
                    && Equals(this.Fingerprints, that.Fingerprints)
                    && Equals(this.Tenant, that.Tenant)
                    && Equals(this.Description, that.Description)
                    && Equals(this.User, that.User)
                    && Equals(this.Organization, that.Organization)
                    && Equals(this.Archived, that.Archived);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(AccountId);
            hash.Add(Display);
            hash.Add(Refresh);
            hash.Add(Identities);
            hash.Add(Fingerprints);
            hash.Add(Tenant);
            hash.Add(Description);
            hash.Add(User);
            hash.Add(Organization);
            hash.Add(Archived);;
            return hash.ToHashCode();
        }

    }

    /// <summary>
    /// An asserted claim for an account.
    /// </summary>
    public class AssertionMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public AssertionMetadata(
            Guid claimId,
            string issuer,
            string key,
            string description)
        {
            this.ClaimId = claimId;
            this.Issuer = issuer;
            this.Key = key;
            this.Description = description;
        }

        /// <summary>
        /// The claim identifier for this assertion.
        /// </summary>
        [JsonPropertyName("claim_id")]
        public Guid ClaimId { get; set; }

        /// <summary>
        /// The issuing authority for the policy.
        /// </summary>
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// The claim key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this claim.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The claim value.
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; set; }

        /// <summary>
        /// The artifact name, if this assertion grants permissions.
        /// </summary>
        [JsonPropertyName("artifact")]
        public string? Artifact { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is AssertionMetadata that)
            {
                return Equals(this.ClaimId, that.ClaimId)
                    && Equals(this.Issuer, that.Issuer)
                    && Equals(this.Key, that.Key)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Value, that.Value)
                    && Equals(this.Artifact, that.Artifact);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(ClaimId, Issuer, Key, Description, Value, Artifact);
        }

    }

    /// <summary>
    /// Information about a policy grant.
    /// </summary>
    public class PolicyMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public PolicyMetadata(
            Guid policyGroupId,
            string issuer,
            string provider,
            string role,
            int mask,
            bool generalAccess,
            string description)
        {
            this.PolicyGroupId = policyGroupId;
            this.Issuer = issuer;
            this.Provider = provider;
            this.Role = role;
            this.Mask = mask;
            this.GeneralAccess = generalAccess;
            this.Description = description;
        }

        /// <summary>
        /// The policy group identifier.
        /// </summary>
        [JsonPropertyName("policy_group_id")]
        public Guid PolicyGroupId { get; set; }

        /// <summary>
        /// The issuing authority for the policy.
        /// </summary>
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// The authentication provider to which this policy belongs.
        /// </summary>
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        /// <summary>
        /// The role name.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// The permissions granted to the user for this group.
        /// </summary>
        [JsonPropertyName("mask")]
        public int Mask { get; set; }

        /// <summary>
        /// A flag indicating this policy group grants access to all
        /// logged in users.
        /// </summary>
        [JsonPropertyName("general_access")]
        public bool GeneralAccess { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this policy group.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The name of the externally managed scope for the policy
        /// group, used by the authentication provider to expand access
        /// tokens.
        /// </summary>
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        /// <summary>
        /// The policy group identifier for the parent group, if this
        /// role is nested.
        /// </summary>
        [JsonPropertyName("parent_id")]
        public Guid? ParentId { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is PolicyMetadata that)
            {
                return Equals(this.PolicyGroupId, that.PolicyGroupId)
                    && Equals(this.Issuer, that.Issuer)
                    && Equals(this.Provider, that.Provider)
                    && Equals(this.Role, that.Role)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.GeneralAccess, that.GeneralAccess)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Scope, that.Scope)
                    && Equals(this.ParentId, that.ParentId);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(PolicyGroupId);
            hash.Add(Issuer);
            hash.Add(Provider);
            hash.Add(Role);
            hash.Add(Mask);
            hash.Add(GeneralAccess);
            hash.Add(Description);
            hash.Add(Scope);
            hash.Add(ParentId);;
            return hash.ToHashCode();
        }

    }

    /// <summary>
    /// A single permissions entry for a service.
    /// </summary>
    public class ServicePermission : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ServicePermission(
            Guid resourceId,
            string name,
            int mask,
            int position,
            int restricted)
        {
            this.ResourceId = resourceId;
            this.Name = name;
            this.Mask = mask;
            this.Position = position;
            this.Restricted = restricted;
        }

        /// <summary>
        /// The resource identifier.
        /// </summary>
        [JsonPropertyName("resource_id")]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// The name of this service.
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
        /// The position of the service in the position claim.
        /// </summary>
        [JsonPropertyName("position")]
        public int Position { get; set; }

        /// <summary>
        /// The restriction applied to the mask for normal user
        /// accounts. System accountabilities have full access to the
        /// unmodified mask.
        /// </summary>
        [JsonPropertyName("restricted")]
        public int Restricted { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ServicePermission that)
            {
                return Equals(this.ResourceId, that.ResourceId)
                    && Equals(this.Name, that.Name)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Position, that.Position)
                    && Equals(this.Restricted, that.Restricted);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(ResourceId, Name, Mask, Position, Restricted);
        }

    }

    /// <summary>
    /// A single key-value pair for a claim.
    /// </summary>
    public class ClaimValue : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ClaimValue(
            string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// The claim key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The asserted claim value.
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ClaimValue that)
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
    /// Information about an authentication provider.
    /// </summary>
    public class ProviderMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ProviderMetadata(
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

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ProviderMetadata that)
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
    /// Detailed information about an authentication provider.
    /// </summary>
    public class ProviderDetails : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ProviderDetails(
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

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ProviderDetails that)
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
    /// A provider-role association object, with a description of
    /// the role.
    /// </summary>
    public class ProviderRole : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ProviderRole(
            string provider,
            string role,
            string description)
        {
            this.Provider = provider;
            this.Role = role;
            this.Description = description;
        }

        /// <summary>
        /// The provider key.
        /// </summary>
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        /// <summary>
        /// The role name.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// Detailed information about the role associated with this
        /// claim.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ProviderRole that)
            {
                return Equals(this.Provider, that.Provider)
                    && Equals(this.Role, that.Role)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Provider, Role, Description);
        }

    }

    /// <summary>
    /// Information about a resource available within a role.
    /// </summary>
    public class RoleResource : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public RoleResource(
            Guid resourceId,
            string artifact,
            string service,
            string resource,
            int mask,
            int restricted,
            string description,
            bool active,
            bool locked,
            DateTimeOffset modified)
        {
            this.ResourceId = resourceId;
            this.Artifact = artifact;
            this.Service = service;
            this.Resource = resource;
            this.Mask = mask;
            this.Restricted = restricted;
            this.Description = description;
            this.Active = active;
            this.Locked = locked;
            this.Modified = modified;
        }

        /// <summary>
        /// The resource identifier.
        /// </summary>
        [JsonPropertyName("resource_id")]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// The artifact name.
        /// </summary>
        [JsonPropertyName("artifact")]
        public string Artifact { get; set; }

        /// <summary>
        /// The name of this service.
        /// </summary>
        [JsonPropertyName("service")]
        public string Service { get; set; }

        /// <summary>
        /// The name of the resource.
        /// </summary>
        [JsonPropertyName("resource")]
        public string Resource { get; set; }

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
        /// A flag indicating the resource is currently available within
        /// the resource group.
        /// </summary>
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        /// <summary>
        /// A flag indicating the resource cannot be removed from the
        /// policy group, likely due to child policy groups requiring
        /// its presence or inability for the current principal to
        /// modify the association.
        /// </summary>
        [JsonPropertyName("locked")]
        public bool Locked { get; set; }

        /// <summary>
        /// The instant the resource access was last changed.
        /// </summary>
        [JsonPropertyName("modified")]
        public DateTimeOffset Modified { get; set; }

        /// <summary>
        /// The currently active claim details for this resource. If
        /// absent, users do not receive access to this resource even if
        /// they have been granted access to the role.
        /// </summary>
        [JsonPropertyName("published")]
        public ResourceClaim? Published { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is RoleResource that)
            {
                return Equals(this.ResourceId, that.ResourceId)
                    && Equals(this.Artifact, that.Artifact)
                    && Equals(this.Service, that.Service)
                    && Equals(this.Resource, that.Resource)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Restricted, that.Restricted)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Active, that.Active)
                    && Equals(this.Locked, that.Locked)
                    && Equals(this.Modified, that.Modified)
                    && Equals(this.Published, that.Published);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(ResourceId);
            hash.Add(Artifact);
            hash.Add(Service);
            hash.Add(Resource);
            hash.Add(Mask);
            hash.Add(Restricted);
            hash.Add(Description);
            hash.Add(Active);
            hash.Add(Locked);
            hash.Add(Modified);
            hash.Add(Published);;
            return hash.ToHashCode();
        }

    }

    /// <summary>
    /// Claim information about a resource.
    /// </summary>
    public class ResourceClaim : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ResourceClaim(
            string name,
            int mask,
            int restricted,
            bool compatible)
        {
            this.Name = name;
            this.Mask = mask;
            this.Restricted = restricted;
            this.Compatible = compatible;
        }

        /// <summary>
        /// The name of the resource.
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
        /// A flag indicating the current resource data is compatible
        /// with this release.
        /// </summary>
        [JsonPropertyName("compatible")]
        public bool Compatible { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ResourceClaim that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Restricted, that.Restricted)
                    && Equals(this.Compatible, that.Compatible);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Mask, Restricted, Compatible);
        }

    }

    /// <summary>
    /// Information about the currently active user.
    /// </summary>
    public class UserSession : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UserSession(
            Issuer issuer,
            Guid organizationId,
            Guid accountId,
            string display,
            List<UserClaim> assertions,
            List<Permission> permissions)
        {
            this.Issuer = issuer;
            this.OrganizationId = organizationId;
            this.AccountId = accountId;
            this.Display = display;
            this.Assertions = assertions;
            this.Permissions = permissions;
        }

        /// <summary>
        /// The issuing authority.
        /// </summary>
        [JsonPropertyName("issuer")]
        public Issuer Issuer { get; set; }

        /// <summary>
        /// The organization identifier.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <summary>
        /// All current claims asserted by this account.
        /// </summary>
        [JsonPropertyName("assertions")]
        public List<UserClaim> Assertions { get; set; }

        /// <summary>
        /// All available permissions.
        /// </summary>
        [JsonPropertyName("permissions")]
        public List<Permission> Permissions { get; set; }

        /// <summary>
        /// Tenancy information for a user.
        /// </summary>
        [JsonPropertyName("tenant")]
        public UserTenant? Tenant { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UserSession that)
            {
                return Equals(this.Issuer, that.Issuer)
                    && Equals(this.OrganizationId, that.OrganizationId)
                    && Equals(this.AccountId, that.AccountId)
                    && Equals(this.Display, that.Display)
                    && Equals(this.Assertions, that.Assertions)
                    && Equals(this.Permissions, that.Permissions)
                    && Equals(this.Tenant, that.Tenant);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Issuer, OrganizationId, AccountId, Display, Assertions, Permissions, Tenant);
        }

    }

    /// <summary>
    /// A valid claim asserted by an account.
    /// </summary>
    public class UserClaim : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UserClaim(
            Guid claimId,
            string issuer,
            string key,
            string value)
        {
            this.ClaimId = claimId;
            this.Issuer = issuer;
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// The claim identifier for this assertion.
        /// </summary>
        [JsonPropertyName("claim_id")]
        public Guid ClaimId { get; set; }

        /// <summary>
        /// The issuing authority for the claim.
        /// </summary>
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// The claim key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The claim value.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UserClaim that)
            {
                return Equals(this.ClaimId, that.ClaimId)
                    && Equals(this.Issuer, that.Issuer)
                    && Equals(this.Key, that.Key)
                    && Equals(this.Value, that.Value);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(ClaimId, Issuer, Key, Value);
        }

    }

    /// <summary>
    /// Tenancy information for a user.
    /// </summary>
    public class UserTenant : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UserTenant(
            Guid accountId,
            string display)
        {
            this.AccountId = accountId;
            this.Display = display;
        }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UserTenant that)
            {
                return Equals(this.AccountId, that.AccountId)
                    && Equals(this.Display, that.Display);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(AccountId, Display);
        }

    }

    /// <summary>
    /// Detailed policy information.
    /// </summary>
    public class PolicyDetails : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public PolicyDetails(
            string name,
            int mask,
            int editable,
            string description,
            List<AssertionMetadata> assertions)
        {
            this.Name = name;
            this.Mask = mask;
            this.Editable = editable;
            this.Description = description;
            this.Assertions = assertions;
        }

        /// <summary>
        /// The policy name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The permissions granted to the user for this group.
        /// </summary>
        [JsonPropertyName("mask")]
        public int Mask { get; set; }

        /// <summary>
        /// The permissions available to the calling account.
        /// </summary>
        [JsonPropertyName("editable")]
        public int Editable { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this policy group.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// All claim assertions that are within scope for this policy.
        /// </summary>
        [JsonPropertyName("assertions")]
        public List<AssertionMetadata> Assertions { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is PolicyDetails that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Editable, that.Editable)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Assertions, that.Assertions);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Mask, Editable, Description, Assertions);
        }

    }

    /// <summary>
    /// Reference information for an account identity.
    /// </summary>
    public class IdentityMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public IdentityMetadata(
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
        /// A description of the identity.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The identifying claim key.
        /// </summary>
        [JsonPropertyName("identifier")]
        public AssertionMetadata? Identifier { get; set; }

        /// <summary>
        /// The scoping claim key, if relevant.
        /// </summary>
        [JsonPropertyName("namespace")]
        public AssertionMetadata? Namespace { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is IdentityMetadata that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Identifier, that.Identifier)
                    && Equals(this.Namespace, that.Namespace);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Identifier, Namespace);
        }

    }

    /// <summary>
    /// Information about an organization.
    /// </summary>
    public class OrganizationMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public OrganizationMetadata(
            Guid organizationId,
            string name)
        {
            this.OrganizationId = organizationId;
            this.Name = name;
        }

        /// <summary>
        /// The organization identifier.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the account.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is OrganizationMetadata that)
            {
                return Equals(this.OrganizationId, that.OrganizationId)
                    && Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(OrganizationId, Name, Description);
        }

    }

    /// <summary>
    /// Summary information about an assertion.
    /// </summary>
    public class AccountAssertion : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public AccountAssertion(
            Guid accountId,
            DateTimeOffset modified)
        {
            this.AccountId = accountId;
            this.Modified = modified;
        }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The time the assertion was last modified.
        /// </summary>
        [JsonPropertyName("modified")]
        public DateTimeOffset Modified { get; set; }

        /// <summary>
        /// The current value of the claim secret.
        /// </summary>
        [JsonPropertyName("secret")]
        public byte[]? Secret { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is AccountAssertion that)
            {
                return Equals(this.AccountId, that.AccountId)
                    && Equals(this.Modified, that.Modified)
                    && Equals(this.Secret, that.Secret);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(AccountId, Modified, Secret);
        }

    }

    /// <summary>
    /// An issuing authority.
    /// </summary>
    public class Issuer : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Issuer(
            string authority,
            string name,
            string url,
            string register,
            string description)
        {
            this.Authority = authority;
            this.Name = name;
            this.Url = url;
            this.Register = register;
            this.Description = description;
        }

        /// <summary>
        /// The issuing authority.
        /// </summary>
        [JsonPropertyName("authority")]
        public string Authority { get; set; }

        /// <summary>
        /// The name of the authority, for use in account recovery
        /// settings.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The entrypoint url for the external service.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// The url for user signup/registration.
        /// </summary>
        [JsonPropertyName("register")]
        public string Register { get; set; }

        /// <summary>
        /// A description of the issuing authority.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The expiry date for the issuing authority.
        /// </summary>
        [JsonPropertyName("expiry")]
        public DateTimeOffset? Expiry { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Issuer that)
            {
                return Equals(this.Authority, that.Authority)
                    && Equals(this.Name, that.Name)
                    && Equals(this.Url, that.Url)
                    && Equals(this.Register, that.Register)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Expiry, that.Expiry);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Authority, Name, Url, Register, Description, Expiry);
        }

    }

    /// <summary>
    /// Detailed information about an asserted claim.
    /// </summary>
    public class AssertionDetails : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public AssertionDetails(
            string issuer,
            string key,
            string description)
        {
            this.Issuer = issuer;
            this.Key = key;
            this.Description = description;
        }

        /// <summary>
        /// The issuing authority for the policy.
        /// </summary>
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// The claim key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// A description of the semantic meaning of this claim.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The claim value.
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; set; }

        /// <summary>
        /// The service permissions granted by this claim.
        /// </summary>
        [JsonPropertyName("service")]
        public ServiceGrant? Service { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is AssertionDetails that)
            {
                return Equals(this.Issuer, that.Issuer)
                    && Equals(this.Key, that.Key)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Value, that.Value)
                    && Equals(this.Service, that.Service);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Issuer, Key, Description, Value, Service);
        }

    }

    /// <summary>
    /// Information about access granted to a service.
    /// </summary>
    public class ServiceGrant : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ServiceGrant(
            string name,
            string description,
            int version,
            bool invalidated,
            List<ResourceGrant> resources)
        {
            this.Name = name;
            this.Description = description;
            this.Version = version;
            this.Invalidated = invalidated;
            this.Resources = resources;
        }

        /// <summary>
        /// The name of this service.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// A detailed description of the service.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The claim version for the service.
        /// </summary>
        [JsonPropertyName("version")]
        public int Version { get; set; }

        /// <summary>
        /// A flag indicating the underlying data has been modified, and
        /// the claim will be refreshed next interaction.
        /// </summary>
        [JsonPropertyName("invalidated")]
        public bool Invalidated { get; set; }

        /// <summary>
        /// All resources granted to the user through these permissions.
        /// </summary>
        [JsonPropertyName("resources")]
        public List<ResourceGrant> Resources { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ServiceGrant that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Version, that.Version)
                    && Equals(this.Invalidated, that.Invalidated)
                    && Equals(this.Resources, that.Resources);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Version, Invalidated, Resources);
        }

    }

    /// <summary>
    /// Information about access granted to a resource.
    /// </summary>
    public class ResourceGrant : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ResourceGrant(
            string name,
            int mask,
            int restricted,
            int permissions,
            string description)
        {
            this.Name = name;
            this.Mask = mask;
            this.Restricted = restricted;
            this.Permissions = permissions;
            this.Description = description;
        }

        /// <summary>
        /// The name of this service.
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
        /// The permissions granted to the user.
        /// </summary>
        [JsonPropertyName("permissions")]
        public int Permissions { get; set; }

        /// <summary>
        /// A description of the service.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is ResourceGrant that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Restricted, that.Restricted)
                    && Equals(this.Permissions, that.Permissions)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Mask, Restricted, Permissions, Description);
        }

    }

    /// <summary>
    /// Information about an organization.
    /// </summary>
    public class OrganizationDetails : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public OrganizationDetails(
            Guid organizationId,
            string name)
        {
            this.OrganizationId = organizationId;
            this.Name = name;
        }

        /// <summary>
        /// The organization identifier.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public Guid OrganizationId { get; set; }

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

        /// <summary>
        /// Tenancy information for the user.
        /// </summary>
        [JsonPropertyName("tenant")]
        public UserTenant? Tenant { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is OrganizationDetails that)
            {
                return Equals(this.OrganizationId, that.OrganizationId)
                    && Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Tenant, that.Tenant);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(OrganizationId, Name, Description, Tenant);
        }

    }

    /// <summary>
    /// Profile information for a user.
    /// </summary>
    public class UserProfile : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UserProfile(
            string authority,
            Guid accountId,
            string display,
            List<UserClaim> assertions)
        {
            this.Authority = authority;
            this.AccountId = accountId;
            this.Display = display;
            this.Assertions = assertions;
        }

        /// <summary>
        /// The issuing authority.
        /// </summary>
        [JsonPropertyName("authority")]
        public string Authority { get; set; }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <summary>
        /// All current claims asserted by this account.
        /// </summary>
        [JsonPropertyName("assertions")]
        public List<UserClaim> Assertions { get; set; }

        /// <summary>
        /// Tenancy information for a user.
        /// </summary>
        [JsonPropertyName("tenant")]
        public UserTenant? Tenant { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UserProfile that)
            {
                return Equals(this.Authority, that.Authority)
                    && Equals(this.AccountId, that.AccountId)
                    && Equals(this.Display, that.Display)
                    && Equals(this.Assertions, that.Assertions)
                    && Equals(this.Tenant, that.Tenant);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Authority, AccountId, Display, Assertions, Tenant);
        }

    }

    /// <summary>
    /// Information about an signing key.
    /// </summary>
    public class AppRegistration : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public AppRegistration(
            Guid accountId,
            string fingerprint,
            byte[] secret)
        {
            this.AccountId = accountId;
            this.Fingerprint = fingerprint;
            this.Secret = secret;
        }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The identifying fingerprint for the key.
        /// </summary>
        [JsonPropertyName("fingerprint")]
        public string Fingerprint { get; set; }

        /// <summary>
        /// Secret data used to sign requests.
        /// </summary>
        [JsonPropertyName("secret")]
        public byte[] Secret { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is AppRegistration that)
            {
                return Equals(this.AccountId, that.AccountId)
                    && Equals(this.Fingerprint, that.Fingerprint)
                    && Equals(this.Secret, that.Secret);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(AccountId, Fingerprint, Secret);
        }

    }

    /// <summary>
    /// An asserted claim value.
    /// </summary>
    public class SecurityClaim : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public SecurityClaim(
            string claim,
            string value)
        {
            this.Claim = claim;
            this.Value = value;
        }

        /// <summary>
        /// The fully qualified claim key.
        /// </summary>
        [JsonPropertyName("claim")]
        public string Claim { get; set; }

        /// <summary>
        /// The asserted claim value.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is SecurityClaim that)
            {
                return Equals(this.Claim, that.Claim)
                    && Equals(this.Value, that.Value);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Claim, Value);
        }

    }

    /// <summary>
    /// A manifest for a schema.
    /// </summary>
    public class AppManifest : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public AppManifest(
            string authority,
            Guid accountId,
            List<Permission> permissions,
            List<ServiceClaim> services)
        {
            this.Authority = authority;
            this.AccountId = accountId;
            this.Permissions = permissions;
            this.Services = services;
        }

        /// <summary>
        /// The issuing authority.
        /// </summary>
        [JsonPropertyName("authority")]
        public string Authority { get; set; }

        /// <summary>
        /// The account identifier for the user that loaded the schema.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// All permissions understood by the application.
        /// </summary>
        [JsonPropertyName("permissions")]
        public List<Permission> Permissions { get; set; }

        /// <summary>
        /// All available services in the artifact.
        /// </summary>
        [JsonPropertyName("services")]
        public List<ServiceClaim> Services { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is AppManifest that)
            {
                return Equals(this.Authority, that.Authority)
                    && Equals(this.AccountId, that.AccountId)
                    && Equals(this.Permissions, that.Permissions)
                    && Equals(this.Services, that.Services);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Authority, AccountId, Permissions, Services);
        }

    }

    /// <summary>
    /// A permission string that can be granted for a resource.
    /// </summary>
    public class Permission : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Permission(
            string key,
            int mask,
            string description)
        {
            this.Key = key;
            this.Mask = mask;
            this.Description = description;
        }

        /// <summary>
        /// The stable key for this permission mask.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The bit-mask granted by this permission.
        /// </summary>
        [JsonPropertyName("mask")]
        public int Mask { get; set; }

        /// <summary>
        /// A human-readable description of the permission.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Permission that)
            {
                return Equals(this.Key, that.Key)
                    && Equals(this.Mask, that.Mask)
                    && Equals(this.Description, that.Description);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Mask, Description);
        }

    }

    /// <summary>
    /// Information about the user account.
    /// </summary>
    public class UserMetadata : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public UserMetadata(
            string display,
            bool pending)
        {
            this.Display = display;
            this.Pending = pending;
        }

        /// <summary>
        /// The display name for the user.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        /// <summary>
        /// A flag indicating the invitation is pending for this user.
        /// </summary>
        [JsonPropertyName("pending")]
        public bool Pending { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UserMetadata that)
            {
                return Equals(this.Display, that.Display)
                    && Equals(this.Pending, that.Pending);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Display, Pending);
        }

    }

    /// <summary>
    /// Detailed status summary for the parent account.
    /// </summary>
    public class StatusSummary : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public StatusSummary(
            long accounts,
            long users,
            long organizations,
            long apps)
        {
            this.Accounts = accounts;
            this.Users = users;
            this.Organizations = organizations;
            this.Apps = apps;
        }

        /// <summary>
        /// The total number of active accounts.
        /// </summary>
        [JsonPropertyName("accounts")]
        public long Accounts { get; set; }

        /// <summary>
        /// The total number of active user accounts.
        /// </summary>
        [JsonPropertyName("users")]
        public long Users { get; set; }

        /// <summary>
        /// The total number of active organization accounts.
        /// </summary>
        [JsonPropertyName("organizations")]
        public long Organizations { get; set; }

        /// <summary>
        /// The total number of active application accounts.
        /// </summary>
        [JsonPropertyName("apps")]
        public long Apps { get; set; }

        /// <summary>
        /// The most recently modified artifact.
        /// </summary>
        [JsonPropertyName("artifact")]
        public ArtifactMetadata? Artifact { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is StatusSummary that)
            {
                return Equals(this.Accounts, that.Accounts)
                    && Equals(this.Users, that.Users)
                    && Equals(this.Organizations, that.Organizations)
                    && Equals(this.Apps, that.Apps)
                    && Equals(this.Artifact, that.Artifact);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Accounts, Users, Organizations, Apps, Artifact);
        }

    }

    /// <summary>
    /// Summary information about an organization.
    /// </summary>
    public class OrganizationInfo : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public OrganizationInfo(
            string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the account.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The organization identifier, if the organization is locally
        /// managed.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public Guid? OrganizationId { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is OrganizationInfo that)
            {
                return Equals(this.Name, that.Name)
                    && Equals(this.Description, that.Description)
                    && Equals(this.OrganizationId, that.OrganizationId);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, OrganizationId);
        }

    }

    /// <summary>
    /// A permission definition.
    /// </summary>
    public class PermissionDefinition : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public PermissionDefinition(
            string key,
            string description,
            List<string> includes)
        {
            this.Key = key;
            this.Description = description;
            this.Includes = includes;
        }

        /// <summary>
        /// The natural key for the permission.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// A description of the permission.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// All permission keys included in this definition, if the
        /// permission is derived.
        /// </summary>
        [JsonPropertyName("includes")]
        public List<string> Includes { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is PermissionDefinition that)
            {
                return Equals(this.Key, that.Key)
                    && Equals(this.Description, that.Description)
                    && Equals(this.Includes, that.Includes);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Description, Includes);
        }

    }

    /// <summary>
    /// Fetch information about an artifact.
    /// </summary>
    public class Artifact : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Artifact(
            string claimGroup)
        {
            this.ClaimGroup = claimGroup;
        }

        /// <summary>
        /// The name of the claim group for this artifact.
        /// </summary>
        [JsonPropertyName("claim_group")]
        public string ClaimGroup { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Artifact that)
            {
                return Equals(this.ClaimGroup, that.ClaimGroup);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(ClaimGroup);
        }

    }

    /// <summary>
    /// Fetch information about a resource group.
    /// </summary>
    public class Service : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Service(
            string claimGroup,
            string resourceGroup)
        {
            this.ClaimGroup = claimGroup;
            this.ResourceGroup = resourceGroup;
        }

        /// <summary>
        /// The name of the claim group for this artifact.
        /// </summary>
        [JsonPropertyName("claim_group")]
        public string ClaimGroup { get; set; }

        /// <summary>
        /// The name of the resource group for this service.
        /// </summary>
        [JsonPropertyName("resource_group")]
        public string ResourceGroup { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Service that)
            {
                return Equals(this.ClaimGroup, that.ClaimGroup)
                    && Equals(this.ResourceGroup, that.ResourceGroup);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(ClaimGroup, ResourceGroup);
        }

    }

    /// <summary>
    /// Fetch details about a specific policy group.
    /// </summary>
    public class Role : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Role(
            string key,
            string policyGroup)
        {
            this.Key = key;
            this.PolicyGroup = policyGroup;
        }

        /// <summary>
        /// The provider key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The policy group name for this role.
        /// </summary>
        [JsonPropertyName("policy_group")]
        public string PolicyGroup { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Role that)
            {
                return Equals(this.Key, that.Key)
                    && Equals(this.PolicyGroup, that.PolicyGroup);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key, PolicyGroup);
        }

    }

    /// <summary>
    /// Fetch details about a security claim.
    /// </summary>
    public class Claim : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Claim(
            string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// The claim key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Claim that)
            {
                return Equals(this.Key, that.Key);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key);
        }

    }

    /// <summary>
    /// An authentication provider.
    /// </summary>
    public class Provider : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Provider(
            string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// The provider key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Provider that)
            {
                return Equals(this.Key, that.Key);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Key);
        }

    }

    /// <summary>
    /// Fetch details about an individual account.
    /// </summary>
    public class Account : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Account(
            Guid accountId)
        {
            this.AccountId = accountId;
        }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Account that)
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
    /// Information about a user policy.
    /// </summary>
    public class Policy : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Policy(
            Guid policyGroupId,
            Guid accountId)
        {
            this.PolicyGroupId = policyGroupId;
            this.AccountId = accountId;
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

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Policy that)
            {
                return Equals(this.PolicyGroupId, that.PolicyGroupId)
                    && Equals(this.AccountId, that.AccountId);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(PolicyGroupId, AccountId);
        }

    }

    /// <summary>
    /// A specific asserted claim.
    /// </summary>
    public class Assertion : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Assertion(
            Guid accountId,
            Guid claimId)
        {
            this.AccountId = accountId;
            this.ClaimId = claimId;
        }

        /// <summary>
        /// The account identifier.
        /// </summary>
        [JsonPropertyName("account_id")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The claim identifier.
        /// </summary>
        [JsonPropertyName("claim_id")]
        public Guid ClaimId { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Assertion that)
            {
                return Equals(this.AccountId, that.AccountId)
                    && Equals(this.ClaimId, that.ClaimId);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(AccountId, ClaimId);
        }

    }

    /// <summary>
    /// Information about an organization.
    /// </summary>
    public class Organization : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public Organization(
            Guid organizationId)
        {
            this.OrganizationId = organizationId;
        }

        /// <summary>
        /// The organization identifier.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public Guid OrganizationId { get; set; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Organization that)
            {
                return Equals(this.OrganizationId, that.OrganizationId);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(OrganizationId);
        }

    }

}