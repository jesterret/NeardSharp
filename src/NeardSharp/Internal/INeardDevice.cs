using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus;

namespace NeardSharp.Internal
{
    [DBusInterface("org.neard.Device")]
    public interface INeardDevice : IDBusObject
    {
        Task PushAsync(IDictionary<string, object> Attributes);
        Task<byte[]> GetRawNDEFAsync();
        Task<T> GetAsync<T>(string prop);
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

}
