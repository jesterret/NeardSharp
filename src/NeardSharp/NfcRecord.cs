using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using NeardSharp.Internal;
using Tmds.DBus;

namespace NeardSharp
{
    /// <summary>
    /// Represents Record encoded on NFC device or NFC tag.
    /// </summary>
    public sealed class NfcRecord : NfcObject<INeardRecord?>
    {
        [DoesNotReturn]
        private static void ThrowInvalidRecord() => throw new InvalidRecordTypeException();
        [DoesNotReturn]
        private static void ThrowArgumentNull(string paramName) => throw new ArgumentNullException(paramName);

        private NfcRecord(RecordType recordType, string? name = null, string? encoding = null, string? language = null, string? representation = null, string? uri = null, string? mimeType = null, uint size = 0, string? action = null, string? androidPackage = null) : base()
        {
            if (recordType == RecordType.Invalid)
                ThrowInvalidRecord();

            Name = name ?? string.Empty;
            Type = recordType;
            Encoding = encoding ?? string.Empty;
            Language = language ?? string.Empty;
            Representation = representation ?? string.Empty;
            URI = uri ?? string.Empty;
            MIMEType = mimeType ?? string.Empty;
            Size = size;
            Action = action ?? string.Empty;
            AndroidPackage = androidPackage ?? string.Empty;
        }
        internal NfcRecord(IDictionary<string, object> keyValues, ObjectPath objectPath) : base(objectPath)
        {
            if(keyValues.Count > 10)
            {
                foreach (var kv in keyValues)
                {
                    Debug.WriteLine($"{kv.Key} : {kv.Value}");
                }
            }
            Name = (string)keyValues[nameof(Name)];
            var typeString = (string)keyValues[nameof(Type)];
            if (Enum.TryParse<RecordType>(typeString, out var type))
                Type = type;
            Encoding = (string)keyValues[nameof(Encoding)];
            Language = (string)keyValues[nameof(Language)];
            Representation = (string)keyValues[nameof(Representation)];
            URI = (string)keyValues[nameof(URI)];
            MIMEType = (string)keyValues[nameof(MIMEType)];
            Size = (uint)keyValues[nameof(Size)];
            Action = (string)keyValues[nameof(Action)];
            AndroidPackage = (string)keyValues[nameof(AndroidPackage)];
        }

        /// <summary>
        /// Name of the record object.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The NDEF record type.
        /// </summary>
        public RecordType Type { get; }
        /// <summary>
        /// The character encoding of the data.
        /// </summary>
        /// <remarks>
        /// Possible values are "UTF-8" or "UTF-16".
        /// This property is only valid for <see cref="RecordType.Text"/> and <see cref="RecordType.SmartPoster"/>'s title records.
        /// </remarks>
        public string Encoding { get; }
        /// <summary>
        /// The ISO/IANA language code.
        /// </summary>
        /// <remarks>
        /// This property is only valid for <see cref="RecordType.Text"/> and <see cref="RecordType.SmartPoster"/>'s title records.
        /// </remarks>
        public string Language { get; }
        /// <summary>
        /// The human readable representation of a text or title record.
        /// </summary>
        /// <remarks>
        /// This property is only valid for <see cref="RecordType.Text"/> and <see cref="RecordType.SmartPoster"/>'s title records.
        /// </remarks>
        public string Representation { get; }
        /// <summary>
        /// Uri encoded into the record.
        /// </summary>
        /// <remarks>
        /// This is the complete URI, including the scheme and the resource. 
        /// This property is only valid for <see cref="RecordType.SmartPoster"/>'s URI records.
        /// </remarks>
        public string URI { get; }
        /// <summary>
        /// The URI object MIME type.
        /// </summary>
        /// <remarks>Actual MIME data is not sent via dbus. Needs investigating how to obtain it.</remarks>
        public string MIMEType { get; }
        /// <summary>
        /// Size of URI object.
        /// </summary>
        public uint Size { get; }
        /// <summary>
        /// The suggested course of action.
        /// </summary>
        /// <remarks>
        /// This one is only valid for <see cref="RecordType.SmartPoster"/> and is a suggestion only. It can be ignored, and the possible values are "Do" (for example launch the browser), "Save" (for example save the URI in the bookmarks folder, or "Edit" (for example open the URI in an URI editor for the user to modify it.
        /// </remarks>
        public string Action { get; }
        /// <summary>
        /// This is the Android Package Name contained in an Android Application Record(AAR). It hints the reader towards launching an Android specific application.
        /// </summary>
        /// <remarks>It is only valid for <see cref="RecordType.AAR"/> NDEFs.</remarks>
        public string AndroidPackage { get; }

        internal Dictionary<string, object> ToDictionary() 
            => new Dictionary<string, object>()
            {
                { nameof(Name), Name },
                { nameof(Type), Type.ToString() },
                { nameof(Encoding), Encoding },
                { nameof(Language), Language },
                { nameof(Representation), Representation },
                { nameof(URI), URI },
                { nameof(MIMEType), MIMEType },
                { nameof(Size), Size },
                { nameof(Action), Action },
                { nameof(AndroidPackage), AndroidPackage }
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static NfcRecord UriBase(RecordType recordType, string? uri)
        {
            if (uri is null)
                ThrowArgumentNull(nameof(uri));

            return new NfcRecord(recordType, uri: uri, encoding: "UTF-8");
        }

        /// <summary>
        /// Creates <see cref="NfcRecord"/> from specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">Uri to save in this record.</param>
        /// <returns>Valid <see cref="NfcRecord"/> of type <see cref="RecordType.URI"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <see langword="null"/>.</exception>
        public static NfcRecord FromUri(string uri) => UriBase(RecordType.URI, uri);
        /// <summary>
        /// Creates <see cref="NfcRecord"/> from specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">Uri to save in this record.</param>
        /// <returns>Valid <see cref="NfcRecord"/> of type <see cref="RecordType.URI"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <see langword="null"/>.</exception>
        public static NfcRecord FromUri(Uri uri) => UriBase(RecordType.URI, uri?.ToString());
        /// <summary>
        /// Creates <see cref="NfcRecord"/> from specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">Uri to save in this record.</param>
        /// <returns>Valid <see cref="NfcRecord"/> of type <see cref="RecordType.SmartPoster"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <see langword="null"/>.</exception>
        public static NfcRecord FromSmartPoster(string uri) => UriBase(RecordType.SmartPoster, uri);
        /// <summary>
        /// Creates <see cref="NfcRecord"/> from specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">Uri to save in this record.</param>
        /// <returns>Valid <see cref="NfcRecord"/> of type <see cref="RecordType.SmartPoster"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <see langword="null"/>.</exception>
        public static NfcRecord FromSmartPoster(Uri uri) => UriBase(RecordType.SmartPoster, uri?.ToString());
        /// <summary>
        /// Creates <see cref="NfcRecord"/> from specified <paramref name="appPackage"/>.
        /// </summary>
        /// <param name="appPackage">Android package identifier.</param>
        /// <returns>Valid <see cref="NfcRecord"/> of type <see cref="RecordType.AAR"/>.</returns>
        public static NfcRecord FromAndroidPackage(string appPackage) => new NfcRecord(RecordType.AAR, androidPackage: appPackage);
        /// <summary>
        /// Creates <see cref="NfcRecord"/> from specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Text to save in this record.</param>
        /// <param name="culture">Culture of the language of saved text.</param>
        /// <returns>Valid <see cref="NfcRecord"/> of type <see cref="RecordType.Text"/>.</returns>
        public static NfcRecord FromText(string text, CultureInfo? culture = null) => new NfcRecord(RecordType.Text, encoding: "UTF-8", language: culture?.TwoLetterISOLanguageName ?? "en", representation: text);
    }
}
