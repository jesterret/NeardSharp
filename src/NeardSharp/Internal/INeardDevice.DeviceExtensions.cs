using System.Threading.Tasks;
using Tmds.DBus;

namespace NeardSharp.Internal
{
    internal static class DeviceExtensions
    {
        public static Task<string> GetNameAsync(this INeardDevice o) => o.GetAsync<string>("Name");
        public static Task<INeardAdapter> GetAdapterAsync(this INeardDevice o) => o.GetAsync<INeardAdapter>("Adapter");
        public static Task<INeardRecord[]> GetRecordsAsync(this INeardDevice o) => o.GetAsync<INeardRecord[]>("Records");
    }

}
