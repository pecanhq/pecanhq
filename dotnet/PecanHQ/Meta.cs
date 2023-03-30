// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable
using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace PecanHQ
{

    /// <summary>
    /// A client HTTP handler, responsible for authentication and retries.
    /// </summary>
    public interface IHttpHandler
    {

        /// <summary>
        /// The default serialization options for JSON.
        /// </summary>
        JsonSerializerOptions Json => HttpClientExtension.JSON;

        /// <summary>
        /// Decorate an API request with authentication, then submit asynchronously.
        /// </summary>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token);

    }

    /// <summary>
    /// Utility methods for HttpClient requests.
    /// </summary>
    public static class HttpClientExtension
    {

        /// <summary>
        /// A singleton value for the default json options.
        /// </summary>
        internal static readonly JsonSerializerOptions JSON = new JsonSerializerOptions()
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            PropertyNamingPolicy = null,
            PropertyNameCaseInsensitive = false,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            AllowTrailingCommas = true,
            MaxDepth = 64,
            IncludeFields = false,
            IgnoreReadOnlyProperties = true,
            IgnoreReadOnlyFields = true,
            WriteIndented = false,
            Converters = {
                new TimeConverter(),
            },
        };

        /// <summary>
        /// JSON API request method, where failures are transformed into ServiceExceptions
        /// </summary>
        public static Task<T?> GetFromJsonAsync<T>(
            this IHttpHandler handler,
            Uri uri,
            CancellationToken token = default) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("accept", "application/json");
            return handler.SendAsync(request, token).GetFromJsonAsync<T>(handler.Json, token);
        }

        /// <summary>
        /// JSON API request method, where failures are transformed into ServiceExceptions
        /// </summary>
        /// <remarks>
        /// A utility method for string URIs.
        /// </remarks>
        public static Task<T?> GetFromJsonAsync<T>(
            this IHttpHandler handler,
            string uri,
            CancellationToken token = default) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("accept", "application/json");
            return handler.SendAsync(request, token).GetFromJsonAsync<T>(handler.Json, token);
        }

        /// <summary>
        /// JSON API request method, where failures are transformed into ServiceExceptions
        /// </summary>
        /// <remarks>
        /// A utility method for in-flight requests.
        /// </remarks>
        public static async Task<T?> GetFromJsonAsync<T>(
            this Task<HttpResponseMessage> request,
            JsonSerializerOptions options,
            CancellationToken token = default) where T : class
        {
            var response = await request;
            if (!response.IsSuccessStatusCode)
            {
                throw await ServiceException.CreateAsync(response, options: options);
            }
            else if (response.Content.Headers.ContentLength == 0)
            {
                return default(T);
            }
            else
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream, options);
                }
            }
        }

        /// <summary>
        /// JSON API request method, where failures are transformed into ServiceExceptions
        /// </summary>
        /// <remarks>
        /// A utility method for string URIs.
        /// </remarks>
        public static async Task<IResultSet<T>> ScrollFromJsonAsync<T>(
            this IHttpHandler handler,
            string uri,
            CancellationToken token = default,
            bool lazy = default)
        {
            var page = await handler.GetFromJsonAsync<ResultPage<T>>(uri, token);
            return (page ?? new ResultPage<T>()).AsResultSet(
                handler,
                token,
                lazy);
        }

        /// <summary>
        /// JSON API request method, where failures are transformed into ServiceExceptions
        /// </summary>
        public static async Task<IResultSet<T>> ScrollFromJsonAsync<T>(
            this IHttpHandler handler,
            Uri uri,
            CancellationToken token = default,
            bool lazy = default)
        {
            var page = await handler.GetFromJsonAsync<ResultPage<T>>(uri, token);
            return (page ?? new ResultPage<T>()).AsResultSet(
                handler,
                token,
                lazy);
        }

        /// <summary>
        /// An attachment API request, where header data is collated into the attachment metadata
        /// </summary>
        /// <remarks>
        /// A utility method for string URIs.
        /// </remarks>
        public static async Task<IAttachment> GetAttachmentAsync(
            this IHttpHandler handler,
            string uri,
            CancellationToken token = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await handler.SendAsync(request, token);
            if (!response.IsSuccessStatusCode)
            {
                throw await ServiceException.CreateAsync(response, options: handler.Json);
            }

            ContentType contentType;
            try
            {
                contentType = new ContentType(response.Content.Headers.ContentType?.ToString() ?? string.Empty);
            }
            catch (Exception e)
            {
                throw new ServiceException(response.StatusCode, new ServiceError(
                        "evaluation.3000",
                        "Invalid attachment",
                        "The attachment was returned without a valid content type."), e);
            }

            return new StreamAttachment(
                response.Content.ReadAsStreamAsync,
                contentType,
                response.Content.Headers.ContentDisposition?.FileName ?? throw new ServiceException(response.StatusCode, new ServiceError(
                    "evaluation.3000",
                    "Invalid attachment",
                    "The attachment was returned without an associated file name")));
        }

        /// <summary>
        /// An attachment API request, where header data is collated into the attachment metadata
        /// </summary>
        public static async Task<IAttachment> GetAttachmentAsync(
            this IHttpHandler handler,
            Uri uri,
            CancellationToken token = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await handler.SendAsync(request, token);
            if (!response.IsSuccessStatusCode)
            {
                throw await ServiceException.CreateAsync(response, options: handler.Json);
            }

            ContentType contentType;
            try
            {
                contentType = new ContentType(response.Content.Headers.ContentType?.ToString() ?? string.Empty);
            }
            catch (Exception e)
            {
                throw new ServiceException(response.StatusCode, new ServiceError(
                        "evaluation.3000",
                        "Invalid attachment",
                        "The attachment was returned without a valid content type."), e);
            }

            return new StreamAttachment(
                response.Content.ReadAsStreamAsync,
                contentType,
                response.Content.Headers.ContentDisposition?.FileName ?? throw new ServiceException(response.StatusCode, new ServiceError(
                    "evaluation.3000",
                    "Invalid attachment",
                    "The attachment was returned without an associated file name")));
        }

        /// <summary>
        /// Map each element in this result set while retaining the page structure.
        /// </summary>
        public static IResultSet<T> Map<S, T>(this IResultSet<S> results, Func<S, T> fn)
        {
            return new MappingResultSet<S, T>(results, fn);
        }

        /// <summary>
        /// Map each element in this result set while retaining the page structure.
        /// </summary>
        public static async Task<IResultSet<T>> Map<S, T>(this Task<IResultSet<S>> task, Func<S, T> fn)
        {
            var results = await task;
            return results.Map(fn);
        }

        /// <summary>
        /// Map each element in this result set into a dictionary.
        /// </summary>
        public static async Task<Dictionary<K, V>> ToDictionaryAsync<K, V>(this Task<IResultSet<V>> task, Func<V, K> key) where K : notnull
        {
            var values = new Dictionary<K, V>();
            await foreach (var item in await task)
            {
                values.Add(key(item), item);
            }
            return values;
        }

        /// <summary>
        /// Map each element in this result set into a dictionary.
        /// </summary>
        public static async Task<Dictionary<K, V>> ToDictionaryAsync<K, V, R>(this Task<IResultSet<R>> task, Func<R, K> key, Func<R, V> value) where K : notnull
        {
            var values = new Dictionary<K, V>();
            await foreach (var item in await task)
            {
                values.Add(key(item), value(item));
            }
            return values;
        }

        /// <summary>
        /// Map each element in this result set into a hash set.
        /// </summary>
        public static async Task<HashSet<V>> ToHashSetAsync<V>(this Task<IResultSet<V>> task)
        {
            var values = new HashSet<V>();
            await foreach (var item in await task)
            {
                values.Add(item);
            }
            return values;
        }

        /// <summary>
        /// Map each element in this result set into a list.
        /// </summary>
        public static async Task<List<V>> ToListAsync<V>(this Task<IResultSet<V>> task)
        {
            var values = new List<V>();
            await foreach (var item in await task)
            {
                values.Add(item);
            }
            return values;
        }

        /// <summary>
        /// Fetch the first entry in the result set, or null if empty.
        /// </summary>
        public static async Task<V?> FirstOrDefault<V>(this Task<IResultSet<V>> task)
        {
            var results = await task;
            return results.Rows.FirstOrDefault();
        }

    }

    /// <summary>
    /// A primitive converter for TimeSpan values.
    /// </summary>
    public class TimeConverter : JsonConverter<TimeSpan>
    {

        /// <inheritdoc/>
        public override bool HandleNull => false;

        /// <inheritdoc/>
        public override TimeSpan Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (TimeSpan.TryParse(reader.GetString(), out var result))
            {
                return result;
            }
            else throw new JsonException();
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

    }

    /// <summary>
    /// The base interface for a navigation into a linked collection of resources.
    /// </summary>
    public interface INavigationState
    {

        /// <summary>
        /// Attempt to save the navigation state to a dictonary.
        /// </summary>
        bool TrySave(out Dictionary<string, Uri>? links);

    }

    /// <summary>
    /// The core interface for HATEOS linking between resources.
    /// </summary>
    public interface INavigationAware
    {

        /// <summary>
        /// All links available at this node.
        /// </summary>
        /// <value>
        /// Resource urls keyed by the authoritative <c>snake_cased</c> resource names.
        /// </value>
        /// <remarks>
        /// Presence of a key implies the caller had permissions to access the
        /// resource at the time of retrieval.
        /// </remarks>
        Dictionary<string, Uri>? links { get; }

    }

    /// <summary>
    /// A marker interface encapsulating the type of a linkable service.
    /// </summary>
    /// <typeparam name="T">The base data type returned by the service</typeparam>
    public interface ILinkable<T> where T : INavigationAware
    {

    }

    /// <summary>
    /// A collection of file metadata.
    /// </summary>
    /// <remarks>
    /// Contains all information required for fully-formed HTTP requests.
    /// </remarks>
    public interface IAttachmentMetadata
    {

        /// <summary>
        /// The HTTP ContentType header.
        /// </summary>
        /// <value>
        /// The fully qualified content type (mimetype + encoding etc) as a plain string.
        /// </value>
        ContentType ContentType { get; }

        /// <summary>
        /// The file name.
        /// </summary>
        /// <value>
        /// The original filename for the referenced attachment.
        /// </value>
        string FileName { get; }

    }

    /// <summary>
    /// The base interface for different file attachment types.
    /// </summary>
    /// <remarks>
    /// The only guarantee provided by this class is that
    /// on opening the stream all file metadata will be made
    /// available.
    /// </remarks>
    public interface IAttachment
    {

        /// <summary>
        /// The base URI for the attachment, if stored remotely.
        /// </summary>
        Uri? Uri { get; }

        /// <summary>
        /// Open the file stream, and load all associated metadata.
        /// </summary>
        /// <remarks>
        /// Depending on the type of attachment, this may be everything
        /// from a web request to a no-op.
        /// </remarks>
        /// <exception cref="IOException">Thrown when opening the file fails.</exception>
        ValueTask<AttachmentStream> LoadAsync(CancellationToken token = default);

    }

    /// <summary>
    /// A unit of data storage in the underlying acorn domain layer.
    /// </summary>
    public interface IAggregateRoot
    {

        /// <value>The primary key of the entity</value>
        object Id { get; }

    }

    /// <summary>
    /// A unit of data storage in the underlying acorn domain layer.
    /// </summary>
    /// <typeparam name="K">A primitive key type.</typeparam>
    public interface IAggregateRoot<K> : IAggregateRoot
    {

        /// <value>The primary key of the entity</value>
        new K Id { get; }

    }

    /// <summary>
    /// A unidirectional scrolling result set.
    /// </summary>
    public interface IResultSet<out T> : IAsyncEnumerable<T>
    {

        /// <value>All rows in the local page.</value>
        IEnumerable<T> Rows { get; }

        /// <value>True iff more results might be available.</value>
        bool HasNext { get; }

        /// <value>A pagination token for the next page in the result set.</value>
        string? Cursor { get; }

        /// <summary>
        /// Load the next page, and notify the caller if there are more rows to be loaded.
        /// </summary>
        ValueTask<bool> LoadMoreAsync(CancellationToken token = default);

    }

    /// <summary>
    /// A read-only stream with attachment metadata.
    /// </summary>
    public sealed class AttachmentStream : Stream, IAttachmentMetadata
    {

        private Stream stream;

        /// <summary>
        /// Construct a new attachment stream by decorating an existing stream.
        /// </summary>
        public AttachmentStream(Stream stream, ContentType contentType, string fileName)
        {
            this.stream = stream;
            this.ContentType = contentType;
            this.FileName = fileName;
        }

        /// <inheritdoc/>
        public ContentType ContentType { get; }

        /// <inheritdoc/>
        public string FileName { get; }

        /// <inheritdoc/>
        public override long Length { get => stream.Length; }

        /// <inheritdoc/>
        public override long Position { get => stream.Position; set => stream.Position = value; }

        /// <inheritdoc/>
        public override bool CanWrite { get => false; }

        /// <inheritdoc/>
        public override bool CanTimeout { get => stream.CanTimeout; }

        /// <inheritdoc/>
        public override bool CanSeek { get => stream.CanSeek; }

        /// <inheritdoc/>
        public override bool CanRead { get => stream.CanRead; }

        /// <inheritdoc/>
        public override int ReadTimeout { get => throw new InvalidOperationException(); set => throw new InvalidOperationException(); }

        /// <inheritdoc/>
        public override int WriteTimeout { get => throw new InvalidOperationException(); set => throw new InvalidOperationException(); }

        /// <inheritdoc/>
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state) => stream.BeginRead(
            buffer,
            offset,
            count,
            callback,
            state);

        /// <inheritdoc/>
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state) => throw new NotSupportedException();

        /// <inheritdoc/>
        public override void Close() => stream.Close();

        /// <inheritdoc/>
        public override void CopyTo(Stream destination, int bufferSize) => stream.CopyTo(destination, bufferSize);

        /// <inheritdoc/>
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken) => stream.CopyToAsync(
            destination,
            bufferSize,
            cancellationToken);

        /// <inheritdoc/>
        public override ValueTask DisposeAsync() => stream.DisposeAsync();

        /// <inheritdoc/>
        public override int EndRead(IAsyncResult asyncResult) => stream.EndRead(asyncResult);

        /// <inheritdoc/>
        public override void EndWrite(IAsyncResult asyncResult) => throw new NotSupportedException();

        /// <inheritdoc/>
        public override void Flush() => stream.Flush();

        /// <inheritdoc/>
        public override Task FlushAsync(CancellationToken cancellationToken) => stream.FlushAsync(cancellationToken);

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count) => stream.Read(buffer, offset, count);

        /// <inheritdoc/>
        public override int Read(Span<byte> buffer) => stream.Read(buffer);

        /// <inheritdoc/>
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) => stream.ReadAsync(
            buffer,
            cancellationToken);

        /// <inheritdoc/>
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => stream.ReadAsync(
            buffer,
            offset,
            count,
            cancellationToken);

        /// <inheritdoc/>
        public override int ReadByte() => stream.ReadByte();

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin) => stream.Seek(offset, origin);

        /// <inheritdoc/>
        public override void SetLength(long value) => throw new NotSupportedException();

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        /// <inheritdoc/>
        public override void Write(ReadOnlySpan<byte> buffer) => throw new NotSupportedException();

        /// <inheritdoc/>
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => throw new NotSupportedException();

        /// <inheritdoc/>
        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default) => throw new NotSupportedException();

        /// <inheritdoc/>
        public override void WriteByte(byte value) => throw new NotSupportedException();

        /// <inheritdoc/>
        protected override void Dispose(bool disposing) => stream.Dispose();

    }

    /// <summary>
    /// A utility class that combines a stream-generating function
    /// with its metadata.
    /// </summary>
    public class StreamAttachment : IAttachment, IAttachmentMetadata
    {

        private readonly Func<Task<Stream>> open;

        /// <summary>
        /// Construct a new attachment by providing a stream source.
        /// </summary>
        public StreamAttachment(
            Func<Task<Stream>> open,
            ContentType contentType,
            string fileName)
        {
            this.open = open;

            this.ContentType = contentType;
            this.FileName = fileName;
        }

        /// <inheritdoc/>
        public Uri? Uri { get; }

        /// <inheritdoc/>
        public ContentType ContentType { get; }

        /// <inheritdoc/>
        public string FileName { get; }

        /// <inheritdoc/>
        public async ValueTask<AttachmentStream> LoadAsync(CancellationToken token = default)
        {
            return new AttachmentStream(
                await this.open(),
                ContentType,
                FileName);
        }

    }

    /// <summary>
    /// A utility class that creates a MemoryStream from local data
    /// when loaded.
    /// </summary>
    public class MemoryAttachment : IAttachment, IAttachmentMetadata
    {

        private readonly byte[] data;

        /// <summary>
        /// Construct a new attachment by providing an in-memory source.
        /// </summary>
        public MemoryAttachment(
            byte[] data,
            ContentType contentType,
            string fileName)
        {
            this.data = data;

            this.ContentType = contentType;
            this.FileName = fileName;
        }

        /// <inheritdoc/>
        public Uri? Uri { get; }

        /// <inheritdoc/>
        public ContentType ContentType { get; }

        /// <inheritdoc/>
        public string FileName { get; }

        /// <inheritdoc/>
        public ValueTask<AttachmentStream> LoadAsync(CancellationToken token = default)
        {
            var stream = new AttachmentStream(
                new MemoryStream(data),
                ContentType,
                FileName);
            return new ValueTask<AttachmentStream>(Task.FromResult(stream));
        }

    }

    /// <summary>
    /// A service node coupled with its associated data.
    /// </summary>
    /// <remarks>
    /// While the data is mutable, the service is immutable, and data mutations may
    /// lead to the associated links being outdated.
    /// </remarks>
    /// <typeparam name="T">The base data type returned by the service</typeparam>
    /// <typeparam name="S">The local service type</typeparam>
    public readonly struct Link<T, S>
        where T : INavigationAware
        where S : ILinkable<T>
    {

        internal Link(T data, S service)
        {
            this.Data = data;
            this.Service = service;
        }

        /// <summary>
        /// The data used to create the service.
        /// </summary>
        /// <remarks>
        /// No effort has been made to prevent this object being mutated. Treat
        /// this link as a one-off coupling between the service and its data
        /// </remarks>
        public T Data { get; }

        /// <summary>
        /// The local node as a service.
        /// </summary>
        /// <remarks>
        /// This service takes its permission structure from the associated object.
        /// </remarks>
        public S Service { get; }

        /// <summary>
        /// A utility method for deconstructing the link
        /// </summary>
        public void Deconstruct(out T data, out S service)
        {
            data = this.Data;
            service = this.Service;
        }

    }

    /// <summary>
    /// A single page in a scrollable result set.
    /// </summary>
    public sealed class ResultPage<T>
    {

        internal ResultPage()
        {
            this.Rows = Array.Empty<T>();
            this.Cursor = null;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        [JsonConstructor]
        public ResultPage(T[] rows, string cursor)
        {
            this.Rows = rows;
            this.Cursor = cursor;
        }

        /// <value>All rows in the local page.</value>
        [JsonPropertyName("rows")]
        public T[] Rows { get; }

        /// <value>The opaque token for the next page in the sequence.</value>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@next")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Uri? next { get; set; }

        /// <summary>
        /// Construct a result set by eagerly fetching the next page.
        /// </summary>
        public IResultSet<T> AsResultSet(
            IHttpHandler handler,
            CancellationToken token = default,
            bool lazy = default)
        {
            var next = lazy || this.next == null ? null : handler.GetFromJsonAsync<ResultPage<T>>(this.next, token);
            return new ResultSet<T>(handler, this, next);
        }

    }

    /// <summary>
    /// A raw result set that scrolls directly through pages, performing requests as required.
    /// </summary>
    internal sealed class ResultSet<T> : IResultSet<T>
    {

        private readonly IHttpHandler handler;

        private ResultPage<T> page;

        private Task<ResultPage<T>?>? next;

        internal ResultSet(
            IHttpHandler handler,
            ResultPage<T> page,
            Task<ResultPage<T>?>? next)
        {
            this.handler = handler;
            this.page = page;
            this.next = next;
        }

        /// <inheritdoc/>
        public IEnumerable<T> Rows { get { return this.page.Rows; } }

        /// <inheritdoc/>
        public bool HasNext { get { return this.page.next != null; } }

        /// <inheritdoc/>
        public string? Cursor { get { return this.page.next?.Query; } }

        /// <inheritdoc/>
        public async ValueTask<bool> LoadMoreAsync(CancellationToken token = default)
        {
            var current = next;
            if (current != null)
            {
                this.page = (await current) ?? new ResultPage<T>();
                this.next = this.page.next == null ? null : handler.GetFromJsonAsync<ResultPage<T>>(
                    this.page.next,
                    token);
                return true;
            }
            else if (this.page.next != null)
            {
                this.page = (await handler.GetFromJsonAsync<ResultPage<T>>(
                    this.page.next,
                    token)) ?? new ResultPage<T>();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return new ResultPageEnumerator<T>(cancellationToken, this.handler, this.page, this.next);
        }

    }

    /// <summary>
    /// A eagerly-fetching enumerator that operates on the underlying result pages.
    /// </summary>
    internal sealed class ResultPageEnumerator<T> : IAsyncEnumerator<T>
    {

        private readonly CancellationToken cancellationToken;

        private readonly IHttpHandler handler;

        private ResultPage<T> page;

        private Task<ResultPage<T>?>? next;

        private int index = -1;

        internal ResultPageEnumerator(
            CancellationToken cancellationToken,
            IHttpHandler handler,
            ResultPage<T> page,
            Task<ResultPage<T>?>? next)
        {
            this.cancellationToken = cancellationToken;
            this.handler = handler;
            this.page = page;
            this.next = next;
        }

        /// <inheritdoc/>
        public T Current { get { return this.page.Rows[this.index]; } }

        /// <inheritdoc/>
        public async ValueTask<bool> MoveNextAsync()
        {
            this.index++;
            if (this.index < this.page.Rows.Length)
            {
                return true;
            }
            else if (this.next != null)
            {
                this.page = (await this.next) ?? new ResultPage<T>();
                this.next = this.page.next == null ? null : handler.GetFromJsonAsync<ResultPage<T>>(
                    this.page.next,
                    this.cancellationToken);
                this.index = 0;
                return this.page.Rows.Length > 0;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.CompletedTask);
        }

    }

    /// <summary>
    /// A derived result set that consumes a source result set and maps each item.
    /// </summary>
    internal sealed class MappingResultSet<T, S> : IResultSet<S>
    {

        private readonly IResultSet<T> src;

        private readonly Func<T, S> fn;

        internal MappingResultSet(IResultSet<T> src, Func<T, S> fn)
        {
            this.src = src;
            this.fn = fn;
        }

        /// <inheritdoc/>
        public IEnumerable<S> Rows { get => this.src.Rows.Select(this.fn); }

        /// <inheritdoc/>
        public bool HasNext { get { return this.src.HasNext; } }

        /// <inheritdoc/>
        public string? Cursor { get { return this.src.Cursor; } }

        /// <inheritdoc/>
        public ValueTask<bool> LoadMoreAsync(CancellationToken token = default)
        {
            return this.src.LoadMoreAsync(token);
        }

        /// <inheritdoc/>
        IAsyncEnumerator<S> IAsyncEnumerable<S>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return new MappingEnumerator<T, S>(this.src.GetAsyncEnumerator(cancellationToken), this.fn);
        }

    }

    /// <summary>
    /// A mapping enumerator that pulls from an underlying source.
    /// </summary>
    internal sealed class MappingEnumerator<T, S> : IAsyncEnumerator<S>
    {

        private readonly IAsyncEnumerator<T> src;

        private readonly Func<T, S> fn;

        internal MappingEnumerator(IAsyncEnumerator<T> src, Func<T, S> fn)
        {
            this.src = src;
            this.fn = fn;
        }

        /// <inheritdoc/>
        public S Current { get { return this.fn(this.src.Current); } }

        /// <inheritdoc/>
        public ValueTask<bool> MoveNextAsync()
        {
            return this.src.MoveNextAsync();
        }

        /// <inheritdoc/>
        public ValueTask DisposeAsync()
        {
            return this.src.DisposeAsync();
        }

    }

    /// <summary>
    /// A bundle of links for all top-level resources in a portal.
    /// </summary>
    public sealed class ServiceEntrypoint : INavigationAware
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        [JsonConstructor]
        public ServiceEntrypoint(LookupDefinition[] lookups)
        {
            this.Lookups = lookups;
        }

        /// <value>All requested lookup definitions.</value>
        [JsonPropertyName("lookups")]
        public LookupDefinition[] Lookups { get; }

        /// <value>A utility property required due to JSON parsing limitations.</value>
        [JsonInclude]
        [JsonPropertyName("@links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, Uri>? links { get; private set; }

    }

    /// <summary>
    /// A namespaced key-value pair.
    /// </summary>
    public sealed class LookupDefinition
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        [JsonConstructor]
        public LookupDefinition(string category, string value, string display)
        {
            this.Category = category;
            this.Value = value;
            this.Display = display;
        }

        /// <value>The namespace of the lookup value.</value>
        [JsonPropertyName("category")]
        public string Category { get; }

        /// <value>The canonical value for the lookup, unique within the category.</value>
        [JsonPropertyName("value")]
        public string Value { get; }

        /// <value>A descriptive label for the lookup.</value>
        [JsonPropertyName("display")]
        public string Display { get; }

    }

    /// <summary>
    /// An individual problem with an API request, keyed to a specific source path
    /// </summary>
    /// See <a href="https://wiki.squirreltechnologies.nz/Errors">here</a> for more details.
    public sealed class DataError
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        [JsonConstructor]
        public DataError(
            string code,
            string title,
            string detail,
            string source)
        {
            this.Code = code;
            this.Title = title;
            this.Detail = detail;
            this.Source = source;
        }

        /// <value>The error code.</value>
        [JsonPropertyName("code")]
        public string Code { get; }

        /// <value>The translated title, shared by all similar errors.</value>
        [JsonPropertyName("title")]
        public string Title { get; }

        /// <value>The translated detail message, specific to this error.</value>
        [JsonPropertyName("detail")]
        public string Detail { get; }

        /// <value>The <c>snake_cased</c> JSON path of the error in the request, e.g. /here.</value>
        [JsonPropertyName("source")]
        public string Source { get; }

    }

    /// <summary>
    /// A summary problem with an API request.
    /// </summary>
    /// See <a href="https://wiki.squirreltechnologies.nz/Errors">here</a> for more details.
    public sealed class ServiceError
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        [JsonConstructor]
        public ServiceError(
            string code,
            string title,
            string detail,
            params DataError[] errors)
        {
            this.Code = code;
            this.Title = title;
            this.Detail = detail;
            this.Errors = errors;
        }

        /// <value>The error code.</value>
        [JsonPropertyName("code")]
        public string Code { get; }

        /// <value>The translated title, shared by all similar errors.</value>
        [JsonPropertyName("title")]
        public string Title { get; }

        /// <value>The translated detail message, specific to this error.</value>
        [JsonPropertyName("detail")]
        public string Detail { get; }

        /// <value>All data errors that contributed to the service error.</value>
        [JsonPropertyName("errors")]
        public DataError[] Errors { get; }

    }


    /// <summary>
    /// An exception type used to pass <see cref="ServiceError">service errors</see> up the stack.
    /// </summary>
    public sealed class ServiceException : Exception
    {

        internal ServiceException(HttpStatusCode statusCode, ServiceError error, Exception? cause = null) : base(error.Title, cause)
        {
            this.StatusCode = statusCode;
            this.Error = error;
        }

        /// <value>The status code for the HTTP request.</value>
        public HttpStatusCode StatusCode { get; }

        /// <value>The error that initiated the exception.</value>
        public ServiceError Error { get; }

        /// <summary>
        /// A utility method for constructing service exceptions
        /// </summary>
        public static async Task<ServiceException> CreateAsync(HttpResponseMessage response, JsonSerializerOptions? options = null)
        {
            ServiceError? error = null;
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                try
                {
                    error = await JsonSerializer.DeserializeAsync<ServiceError>(stream, options);
                }
                catch (JsonException) { }
            }
            return new ServiceException(response.StatusCode, error ?? new ServiceError(
                "evaluation.3000",
                "An unexpected response was received from the server",
                await response.Content.ReadAsStringAsync()));
        }

    }

}