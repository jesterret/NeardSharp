using System.Threading.Tasks;

namespace NeardSharp.Internal
{
    internal static class AdapterExtensions
    {
        public static Task<string> GetNameAsync(this INeardAdapter o) => o.GetAsync<string>("Name");
        public static Task<string> GetModeAsync(this INeardAdapter o) => o.GetAsync<string>("Mode");
        public static Task<bool> GetPoweredAsync(this INeardAdapter o) => o.GetAsync<bool>("Powered");
        public static Task<bool> GetPollingAsync(this INeardAdapter o) => o.GetAsync<bool>("Polling");
        public static Task<string[]> GetProtocolsAsync(this INeardAdapter o) => o.GetAsync<string[]>("Protocols");
        public static Task<INeardTag[]> GetTagsAsync(this INeardAdapter o) => o.GetAsync<INeardTag[]>("Tags");
        public static Task<INeardDevice[]> GetDevicesAsync(this INeardAdapter o) => o.GetAsync<INeardDevice[]>("Devices");
        public static Task SetPoweredAsync(this INeardAdapter o, bool val) => o.SetAsync("Powered", val);
    }
}
