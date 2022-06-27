// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable
using System;
using System.Collections.Generic;

namespace PecanHQ
{

    /// <summary>
    /// Information about a permissions claim for a service
    /// </summary>
    /// <param name="Name">The service name.</param>
    /// <param name="Claim">The permissions claim.</param>
    /// <param name="Resources">All resource identifiers by name.</param>
    public record ServiceRegistration(string Name, string Claim, IReadOnlyDictionary<string, Guid> Resources)
    {

        /// <summary>
        /// The subject claim, if the service has a primary subject identifier.
        /// </summary>
        public string? Subject { get; init; }

        /// <summary>
        /// The tenant claim, if the service is multi-tenanted.
        /// </summary>
        public string? Tenant { get; init; }

    }

}