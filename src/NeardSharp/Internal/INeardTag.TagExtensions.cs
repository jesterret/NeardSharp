using System.Threading.Tasks;

namespace NeardSharp.Internal
{
    internal static class TagExtensions
    {
        public static Task<string> GetNameAsync(this INeardTag o) => o.GetAsync<string>("Name");
        public static Task<string> GetTypeAsync(this INeardTag o) => o.GetAsync<string>("Type");
        public static Task<string> GetProtocolAsync(this INeardTag o) => o.GetAsync<string>("Protocol");
        public static Task<bool> GetReadOnlyAsync(this INeardTag o) => o.GetAsync<bool>("ReadOnly");
        public static Task<INeardAdapter> GetAdapterAsync(this INeardTag o) => o.GetAsync<INeardAdapter>("Adapter");
        public static Task<INeardRecord[]> GetRecordsAsync(this INeardTag o) => o.GetAsync<INeardRecord[]>("Records");
        public static Task<byte[]> GetIso14443aUidAsync(this INeardTag o) => o.GetAsync<byte[]>("Iso14443aUid");
        public static Task<byte[]> GetIso14443aAtqaAsync(this INeardTag o) => o.GetAsync<byte[]>("Iso14443aAtqa");
        public static Task<byte[]> GetIso14443aSakAsync(this INeardTag o) => o.GetAsync<byte[]>("Iso14443aSak");
        public static Task<byte[]> GetFelicaManufacturerAsync(this INeardTag o) => o.GetAsync<byte[]>("FelicaManufacturer");
        public static Task<byte[]> GetFelicaCidAsync(this INeardTag o) => o.GetAsync<byte[]>("FelicaCid");
        public static Task<byte[]> GetFelicaIcAsync(this INeardTag o) => o.GetAsync<byte[]>("FelicaIc");
        public static Task<byte[]> GetFelicaMaxRespTimesAsync(this INeardTag o) => o.GetAsync<byte[]>("FelicaMaxRespTimes");
    }
}
