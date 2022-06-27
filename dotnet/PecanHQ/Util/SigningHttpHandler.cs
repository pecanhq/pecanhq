// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Globalization;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Security.Cryptography;

namespace PecanHQ.Util
{

    internal class SigningHttpHandler : IHttpHandler
    {

        private readonly HttpClient client;

        private readonly string key;

        private readonly byte[] secret;

        public SigningHttpHandler(HttpClient client, string key, byte[] secret)
        {
            this.client = client;
            this.key = key;
            this.secret = secret;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            request.Headers.Date = request.Headers.Date ?? DateTimeOffset.UtcNow;
            var date = request.Headers.Date.Value.ToString(
                "r",
                CultureInfo.InvariantCulture);
            var header = new StringBuilder("keyId=\"")
                .Append(Uri.EscapeDataString(key))
                .Append("\",algorithm=\"")
                .Append(Uri.EscapeDataString("hmac-sha256"))
                .Append("\",headers=\"(request-target) date");
            var summary = new StringBuilder("(request-target): ")
                .Append(request.Method.Method.ToLowerInvariant())
                .Append(" ");
            if (request.RequestUri.IsAbsoluteUri)
            {
                summary.Append(request.RequestUri.PathAndQuery);
            }
            else
            {
                summary.Append(request.RequestUri.OriginalString);
            }
            summary.Append("\n")
                .Append("date: ")
                .Append(date);

            if (request.Content != null)
            {
                var content = await request.Content.ReadAsByteArrayAsync(token);
                using (var sha256 = SHA256.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(content);
                    var digest = $"sha-256={Convert.ToBase64String(hashedBytes)}";
                    request.Headers.Add("Digest", digest);
                    summary.Append("\ndigest: ").Append(digest);
                    header.Append(" digest");
                }
            }

            using (var hmacsha256 = new HMACSHA256(secret))
            {
                var bytes = Encoding.ASCII.GetBytes(summary.ToString());
                var hashedBytes = hmacsha256.ComputeHash(bytes);
                var signature = Convert.ToBase64String(hashedBytes);
                header.Append("\",signature=\"")
                    .Append(signature)
                    .Append("\"");
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    "Signature",
                    header.ToString());
            }
            return await client.SendAsync(request, token);
        }

    }

}