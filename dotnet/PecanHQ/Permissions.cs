// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable
using System;
using System.Collections;

namespace PecanHQ
{

    /// <summary>
    /// An indexable versioned permissions array.
    /// </summary>
    public readonly struct Permissions
    {

        private readonly BitArray bits;

        /// <summary>
        /// The default constructor.
        /// </summary>
        public Permissions(byte[] bytes)
        {
            this.Version = BitConverter.ToInt32(bytes);
            this.bits = new BitArray(bytes);
        }

        /// <summary>
        /// The pre-constructed value constructor.
        /// </summary>
        public Permissions(int version, BitArray bits)
        {
            this.Version = version;
            this.bits = bits;
        }

        /// <summary>
        /// The claim version.
        /// </summary>
        public int Version { get; }

        /// <summary>
        /// Check whether the masked bits are flipped at a specific position in the claim.
        /// </summary>
        public bool HasPermissions(int version, int position, int mask)
        {
            bool valid = version == Version;
            int i = 0;
            int m = mask;
            while (valid && m > 0)
            {
                m = mask >> i;
                valid &= ((m&1) == 0) || (position + i < this.bits?.Length && this.bits[position + i]);
                i++;
            }
            return valid;
        }

        /// <summary>
        /// Convert the permission claim to a base 64 encoded string.
        /// </summary>
        public string? AsBase64String()
        {
            if (bits == null)
            {
                return null;
            }

            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return Convert.ToBase64String(ret);
        }

        /// <summary>
        /// Convert the permission claim to a raw byte array.
        /// </summary>
        public byte[]? AsByteArray()
        {
            if (bits == null)
            {
                return null;
            }

            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

    }

}