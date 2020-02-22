using System;
using System.Collections.Generic;
using System.Text;
using Tmds.DBus;
using System.Threading.Tasks;

namespace NeardSharp.Internal
{
    [DBusInterface("org.neard.Adapter")]
    internal interface INeardAdapter : IDBusObject
    {
        Task StartPollLoopAsync(string Name);
        Task StopPollLoopAsync();
        Task<IDisposable> WatchTagFoundAsync(Action<INeardTag> handler, Action<Exception>? onError = null);
        Task<IDisposable> WatchTagLostAsync(Action<INeardTag> handler, Action<Exception>? onError = null);
        Task<T> GetAsync<T>(string prop);
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }
}
