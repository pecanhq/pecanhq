// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using PecanHQ.Grant.Types;
#nullable enable

namespace PecanHQ.Util
{

    /// <summary>
    /// State information for the service.
    /// </summary>
    public class ServiceState : PecanHQ.INavigationAware
    {

        /// <summary>
        /// Default constructor setting all required fields
        /// </summary>
        [JsonConstructor]
        public ServiceState(
            Uri uri,
            string artifact,
            int version,
            AppManifest manifest,
            Dictionary<string, string> user,
            Guid accountId)
        {
            this.Uri = uri;
            this.Artifact = artifact;
            this.Version = version;
            this.Manifest = manifest;
            this.User = user;
            this.AccountId = accountId;
        }

        /// <summary>
        /// The manifest state.
        /// </summary>
        [JsonPropertyName("uri")]
        public Uri Uri { get; set; }

        /// <summary>
        /// The name of this artifact.
        /// </summary>
        [JsonPropertyName("artifact")]
        public string Artifact { get; set; }

        /// <summary>
        /// The artifact schema version string.
        /// </summary>
        [JsonPropertyName("version")]
        public int Version { get; set; }

        /// <summary>
        /// The manifest state.
        /// </summary>
        [JsonPropertyName("manifest")]
        public AppManifest Manifest { get; set; }

        /// <summary>
        /// The system user.
        /// </summary>
        [JsonPropertyName("user")]
        public Dictionary<string, string> User { get; set; }

        /// <summary>
        /// The system user.
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
            if (obj is ServiceState that)
            {
                return Equals(this.Uri, that.Uri)
                    && Equals(this.Artifact, that.Artifact)
                    && Equals(this.Version, that.Version)
                    && Equals(this.Manifest, that.Manifest);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Uri, Artifact, Version, Manifest);
        }

    }
}