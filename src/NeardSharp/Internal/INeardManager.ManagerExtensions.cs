using System.Threading.Tasks;

namespace NeardSharp.Internal
{
    internal static class ManagerExtensions
    {
        public static Task<INeardAdapter[]> GetAdaptersAsync(this INeardManager o) => o.GetAsync<INeardAdapter[]>("Adapters");
    }
}
