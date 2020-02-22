using System;
using System.Threading.Tasks;
using Tmds.DBus;

namespace NeardSharp.Internal
{
    [DBusInterface("org.neard.Manager")]
    internal interface INeardManager : INeardAgentManager
    {
        Task<IDisposable> WatchPropertyChangedAsync(Action<(string name, object value)> handler, Action<Exception>? onError = null);
        Task<IDisposable> WatchAdapterAddedAsync(Action<INeardAdapter> handler, Action<Exception>? onError = null);
        Task<IDisposable> WatchAdapterRemovedAsync(Action<INeardAdapter> handler, Action<Exception>? onError = null);
        Task<T> GetAsync<T>(string prop);
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }
}
