using System.Collections.Generic;
using Tmds.DBus;

namespace NeardSharp
{
    /// <summary>
    /// Base class for all DBus NFC objects.
    /// </summary>
    /// <typeparam name="T">Type of managed DBus object</typeparam>
    public class NfcObject<T> where T : IDBusObject?
    {
        /// <summary>
        /// Implementation of DBus communication for the object.
        /// </summary>
        protected T ObjectImpl { get; }
        /// <summary>
        /// Initializes DBus connection for the implementation of the object <typeparamref name="T"/> located at <paramref name="objectPath"/>.
        /// </summary>
        /// <param name="objectPath">Object path of the object to be created</param>
        protected NfcObject(ObjectPath objectPath) => ObjectImpl = Connection.System.CreateProxy<T>(NeardConnection._serviceName, objectPath);
        internal NfcObject() => ObjectImpl = default!;
    }
}
