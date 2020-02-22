using System.Threading.Tasks;

namespace NeardSharp.Internal
{
    internal static class RecordExtensions
    {
        public static Task<string> GetNameAsync(this INeardRecord o) => o.GetAsync<string>("Name");
        public static Task<string> GetTypeAsync(this INeardRecord o) => o.GetAsync<string>("Type");
        public static Task<string> GetEncodingAsync(this INeardRecord o) => o.GetAsync<string>("Encoding");
        public static Task<string> GetLanguageAsync(this INeardRecord o) => o.GetAsync<string>("Language");
        public static Task<string> GetRepresentationAsync(this INeardRecord o) => o.GetAsync<string>("Representation");
        public static Task<string> GetURIAsync(this INeardRecord o) => o.GetAsync<string>("URI");
        public static Task<string> GetMIMETypeAsync(this INeardRecord o) => o.GetAsync<string>("MIMEType");
        public static Task<uint> GetSizeAsync(this INeardRecord o) => o.GetAsync<uint>("Size");
        public static Task<string> GetActionAsync(this INeardRecord o) => o.GetAsync<string>("Action");
        public static Task<string> GetAndroidPackageAsync(this INeardRecord o) => o.GetAsync<string>("AndroidPackage");
    }
}
