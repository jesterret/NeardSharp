using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeardSharp.Internal;
using Tmds.DBus;

namespace NeardSharp
{
    /// <summary>
    /// Represents NFC device.
    /// </summary>
    public class NfcDevice : NfcObject<INeardDevice>
    {
        /// <summary>
        /// Name of the device object.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The object path of the adapter the device belongs to.
        /// </summary>
        public string Adapter { get; }
        /// <summary>
        /// List of NDEF records object paths.
        /// </summary>
        public List<ObjectPath> Records { get; }

        internal NfcDevice(IDictionary<string, object> keyValues, ObjectPath objectPath) : base(objectPath)
        {
            Name = (string)keyValues[nameof(Name)];
            Adapter = (string)keyValues[nameof(Adapter)];
            Records = ((ObjectPath[])keyValues[nameof(Records)]).ToList();
        }

        /// <summary>
        /// Pushes specified <see cref="NfcRecord"/> to the device.
        /// </summary>
        /// <param name="record">Record to push to device.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="record"/> is <see langword="null"/>.</exception>
        public Task PushAsync(NfcRecord record)
        {
            if (record is null)
                return Task.FromException(new ArgumentNullException(nameof(record)));

            return ObjectImpl.PushAsync(record.ToDictionary());
        }
        /// <summary>
        /// Reads raw bytes of NDEF record received from the device.
        /// </summary>
        /// <returns>Raw NDEF bytes.</returns>
        public Task<byte[]> GetRawNDEFAsync() => ObjectImpl.GetRawNDEFAsync();
    }
}
