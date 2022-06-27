// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
#nullable enable

namespace PecanHQ
{

    /// <summary>
    /// A JSON-serializable claim response.
    /// </summary>
    public readonly struct ClaimResponse
    {

        /// <summary>
        /// The default constructor.
        /// </summary>
        [JsonConstructor]
        public ClaimResponse(
            bool success,
            string issuer,
            Guid accountability,
            string display,
            IEnumerable<KeyValuePair<string, string>> claims)
        {
            this.Success = success;
            this.Issuer = issuer;
            this.Accountability = accountability;
            this.Display = display;
            this.Claims = claims;
        }

        /// <summary>
        /// The result of the operation.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; }

        /// <summary>
        /// The issuing authority.
        /// </summary>
        [JsonPropertyName("issuer")]
        public string Issuer { get; }

        /// <summary>
        /// The accountability identifier.
        /// </summary>
        [JsonPropertyName("accountability")]
        public Guid Accountability { get; }

        /// <summary>
        /// The display name for the accountability.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; }

        /// <summary>
        /// All claims associated with the user.
        /// </summary>
        [JsonPropertyName("claims")]
        public IEnumerable<KeyValuePair<string, string>> Claims { get; }

    }

}
