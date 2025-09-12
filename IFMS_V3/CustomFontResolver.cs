using PdfSharp.Fonts;
using System.Net.Http;
using System.Threading.Tasks;

namespace IFMS_V3.Pdf
{
    public class CustomFontResolver : IFontResolver
    {
        private static byte[]? _arialFont;

        // Called by PdfSharp when it needs the actual bytes
        public byte[] GetFont(string faceName)
        {
            if (faceName == "Arial#")
                return _arialFont ?? throw new InvalidOperationException("Arial font not loaded.");

            throw new ArgumentException($"Unknown font face: {faceName}");
        }

        // Called by PdfSharp when it maps font names → keys
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase) ||
                familyName.Equals("Helvetica", StringComparison.OrdinalIgnoreCase))
            {
                return new FontResolverInfo("Arial#");
            }

            return null;
        }

        // ✅ Preload font at startup
        public static async Task LoadFontAsync(HttpClient http)
        {
            if (_arialFont == null)
            {
                _arialFont = await http.GetByteArrayAsync("https://localhost:7007/fonts/ARIAL.TTF");
            }
        }
    }
}
