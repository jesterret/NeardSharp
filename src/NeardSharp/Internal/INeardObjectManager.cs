using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tmds.DBus;

namespace NeardSharp.Internal
{
    [DBusInterface("org.freedesktop.DBus.ObjectManager")]
    internal interface INeardObjectManager : IDBusObject
    {
        Task<IDictionary<ObjectPath, IDictionary<string, IDictionary<string, object>>>> GetManagedObjectsAsync();
        Task<IDisposable> WatchInterfacesAddedAsync(Action<(ObjectPath objectPath, IDictionary<string, IDictionary<string, object>> interfacesAndProperties)> handler, Action<Exception>? onError = null);
        Task<IDisposable> WatchInterfacesRemovedAsync(Action<(ObjectPath objectPath, string[] interfaces)> handler, Action<Exception>? onError = null);
    }
}
