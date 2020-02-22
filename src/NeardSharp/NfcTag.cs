using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeardSharp.Internal;
using Tmds.DBus;

namespace NeardSharp
{
    /// <summary>
    /// Represents NFC tag.
    /// </summary>
    public sealed class NfcTag : NfcObject<INeardTag>
    {
        /// <summary>
        /// Name of the tag object.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The NFC tag type.
        /// </summary>
        /// <remarks>
        /// Possible values are "Type 1", "Type 2", "Type 3", "Type 4", "Type 5", and "NFC-DEP".
        /// </remarks>
        public string Type { get; }
        /// <summary>
        /// Protocol of the tag object.
        /// </summary>
        public string Protocol { get; }
        /// <summary>
        /// Give the current status of tag's read mode.
        /// </summary>
        public bool ReadOnly { get; }
        /// <summary>
        /// The object path of the adapter the tag belongs to.
        /// </summary>
        public ObjectPath Adapter { get; }
        /// <summary>
        /// List of NDEF records object paths.
        /// </summary>
        public List<ObjectPath> Records { get; }
        private byte[] Iso14443aUid { get; }
        private byte[] Iso14443aAtqa { get; }
        private byte[] Iso14443aSak { get; }
        private byte[] FelicaManufacturer { get; }
        private byte[] FelicaCid { get; }
        private byte[] FelicaIc { get; }
        private byte[] FelicaMaxRespTimes { get; }

        internal NfcTag(IDictionary<string, object> keyValues, ObjectPath objectPath) : base(objectPath)
        {
            Name = (string)keyValues[nameof(Name)]!;
            Type = (string)keyValues[nameof(Type)];
            Protocol = (string)keyValues[nameof(Protocol)];
            ReadOnly = (bool)keyValues[nameof(ReadOnly)];
            Adapter = (ObjectPath)keyValues[nameof(Adapter)];
            Records = ((ObjectPath[])keyValues[nameof(Records)]).ToList();
            Iso14443aUid = (byte[])keyValues[nameof(Iso14443aUid)];
            Iso14443aAtqa = (byte[])keyValues[nameof(Iso14443aAtqa)];
            Iso14443aSak = (byte[])keyValues[nameof(Iso14443aSak)];
            FelicaManufacturer = (byte[])keyValues[nameof(FelicaManufacturer)];
            FelicaCid = (byte[])keyValues[nameof(FelicaCid)];
            FelicaIc = (byte[])keyValues[nameof(FelicaIc)];
            FelicaMaxRespTimes = (byte[])keyValues[nameof(FelicaMaxRespTimes)];
        }

        /// <summary>
        /// Write the <paramref name="record"/> to the NFC tag.
        /// </summary>
        /// <param name="record"><see cref="NfcRecord"/> to write on the tag.</param>
        /// <returns></returns>
        public Task WriteAsync(NfcRecord record)
        {
            if (record is null)
                return Task.FromException(new ArgumentNullException(nameof(record)));

            return ObjectImpl.WriteAsync(record.ToDictionary());
        }
        /// <summary>
        /// Reads raw bytes of NDEF record.
        /// </summary>
        /// <returns>Raw NDEF bytes.</returns>
        public Task<byte[]> GetRawNDEFAsync() => ObjectImpl.GetRawNDEFAsync();
    }
}
