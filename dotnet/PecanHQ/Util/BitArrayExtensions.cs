// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable
using System.Collections;

namespace PecanHQ.Util
{

    internal static class BitArrayExtensions
    {

        public static void SetVersion(this BitArray bits, int version)
        {
            for (int i = 0; i < 32; i++)
            {
                bits.Set(i, (version >> i & 1) == 1);
            }
        }

        public static void SetPermissions(this BitArray bits, int position, int mask)
        {
            int i = 0;
            int m = mask;
            while (position + i < bits.Length && m > 0)
            {
                m = mask >> i;
                if ((m&1) == 1)
                {
                    bits.Set(position + i, true);
                }
                i++;
            }
        }

    }

}