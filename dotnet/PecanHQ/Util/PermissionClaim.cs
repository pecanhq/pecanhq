// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable
using System.Collections.Concurrent;

namespace PecanHQ.Util
{

    internal record PermissionClaim(string Key, string Prefix, ConcurrentDictionary<string, int> Versions);

}