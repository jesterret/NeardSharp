using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus;

namespace NeardSharp.Internal
{
    [DBusInterface("org.neard.Record")]
    public interface INeardRecord : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }
}
