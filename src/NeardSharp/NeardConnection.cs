using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NeardSharp.Internal;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]

namespace NeardSharp
{
    /// <summary>
    /// Represents connection with Neard DBus.
    /// </summary>
    public sealed class NeardConnection : IDisposable, IAsyncDisposable
    {
        internal const string _serviceName = "org.neard";
        private const string _rootObjectPath = "/";
        private const string _tagInterface = _serviceName + ".Tag";
        private const string _recordInterface = _serviceName + ".Record";
        private const string _deviceInterface = _serviceName + ".Device";

        /// <summary>
        /// Initializes a new instance of <see cref="NeardConnection"/>. 
        /// </summary>
        /// <param name="scheduler">Scheduler to use for callbacks. If not specified, fallbacks to <see cref="Scheduler.Default"/>.</param>
        /// <param name="constantPollEnabled">Whether neard daemon config file specifies 'ConstantPool' as <see langword="true"/></param>
        public NeardConnection(IScheduler? scheduler = null, bool constantPollEnabled = false)
        {
            _scheduler = scheduler ?? Scheduler.Default;
            _shouldPollOnLost = !constantPollEnabled;
            _tagFound = new Subject<NfcTag>();
            _recordFound = new Subject<NfcRecord>();
            _deviceFound = new Subject<NfcDevice>();
            _neardManager = Connection.System.CreateProxy<INeardManager>(_serviceName, _rootObjectPath);
            _objectManager = Connection.System.CreateProxy<INeardObjectManager>(_serviceName, _rootObjectPath);
        }

        /// <summary>
        /// Push based collection for publishing newly found tags.
        /// </summary>
        public IObservable<NfcTag> WhenTagFound => _tagFound.ObserveOn(_scheduler);
        /// <summary>
        /// Push based collection for publishing newly found records.
        /// </summary>
        public IObservable<NfcRecord> WhenRecordFound => _recordFound.ObserveOn(_scheduler);
        /// <summary>
        /// Push based collection for publishing newly found devices.
        /// </summary>
        public IObservable<NfcDevice> WhenDeviceFound => _deviceFound.ObserveOn(_scheduler);

        private async Task Poll()
        {
            foreach (var adapter in await _neardManager.GetAdaptersAsync().ConfigureAwait(false))
            {
                var name = await adapter.GetNameAsync().ConfigureAwait(false);
                await adapter.StartPollLoopAsync(name).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Initializes callbacks and puts adapter in the polling mode.
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            _interfaceAddedHandler = await _objectManager.WatchInterfacesAddedAsync(OnAddedInterface, OnError).ConfigureAwait(false);
            if(_shouldPollOnLost)
                _interfaceRemovedHandler = await _objectManager.WatchInterfacesRemovedAsync(OnRemovedInterface, OnError).ConfigureAwait(false);

            await Poll().ConfigureAwait(false);
        }

        private async void OnRemovedInterface((ObjectPath objectPath, string[] interfaces) obj)
        {
            if (obj.interfaces.Any(x => x == _tagInterface || x == _deviceInterface))
                await Poll().ConfigureAwait(false);
        }
        private void OnAddedInterface((ObjectPath objectPath, IDictionary<string, IDictionary<string, object>> interfacesAndProperties) obj)
        {
            var dict = obj.interfacesAndProperties;
            if (dict.TryGetValue(_recordInterface, out var recordData))
                _recordFound.OnNext(new NfcRecord(recordData, obj.objectPath));
            if (dict.TryGetValue(_tagInterface, out var tagData))
                _tagFound.OnNext(new NfcTag(tagData, obj.objectPath));
            if (dict.TryGetValue(_deviceInterface, out var deviceData))
                _deviceFound.OnNext(new NfcDevice(deviceData, obj.objectPath));
        }

        private void OnError(Exception obj)
        {
            _tagFound.OnError(obj);
            _recordFound.OnError(obj);
            _deviceFound.OnError(obj);
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            _interfaceAddedHandler?.Dispose();
            _interfaceRemovedHandler?.Dispose();
            var adapters = _neardManager.GetAdaptersAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            foreach (var adapter in adapters)
            {
                adapter.StopPollLoopAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            _tagFound.Dispose();
            _recordFound.Dispose();
            _deviceFound.Dispose();
        }
        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            _interfaceAddedHandler?.Dispose();
            _interfaceRemovedHandler?.Dispose();
            foreach (var adapter in await _neardManager.GetAdaptersAsync().ConfigureAwait(false))
            {
                await adapter.StopPollLoopAsync().ConfigureAwait(false);
            }
            _tagFound.Dispose();
            _recordFound.Dispose();
            _deviceFound.Dispose();
        }

        private IDisposable? _interfaceAddedHandler;
        private IDisposable? _interfaceRemovedHandler;

        private readonly IScheduler _scheduler;
        private readonly bool _shouldPollOnLost;
        private readonly Subject<NfcTag> _tagFound;
        private readonly Subject<NfcRecord> _recordFound;
        private readonly Subject<NfcDevice> _deviceFound;
        private readonly INeardManager _neardManager;
        private readonly INeardObjectManager _objectManager;
    }
}
