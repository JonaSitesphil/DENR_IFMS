using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using IFMS_V3.Models.Ors;
using IFMS_V3.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace IFMS_V3.Services.Pdf
{
    public class ORSPdfMock
    {
        private readonly HttpClient _http;

        public ORSPdfMock(HttpClient http)
        {
            _http = http;

            if (GlobalFontSettings.FontResolver == null)
                GlobalFontSettings.FontResolver = new CustomFontResolver();
        }

        public async Task<byte[]> GenerateORSAsync(OrsModel data)
        {
            await CustomFontResolver.LoadFontAsync(_http);

            var stream = await _http.GetStreamAsync("https://localhost:7007/Sample-data/Ors.pdf");
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;

            PdfDocument pdf = PdfReader.Open(ms, PdfDocumentOpenMode.Modify);
            PdfPage page = pdf.Pages[0];
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XRect responsibilityCenterRect = new XRect(30, 205, 76, 190); // (x, y, width, height)
            XRect particularsRect = new XRect(111, 205, 147, 190); // (x, y, width, height)
            XRect mfoPapRect = new XRect(262, 205, 85, 190); // (x, y, width, height)
            XRect uacsCodeRect = new XRect(350, 205, 70, 190); // (x, y, width, height)

            //// Draw the border of the rectangle (red outline, no fill)
            //gfx.DrawRectangle(XPens.Red, responsibilityCenterRect);
            //gfx.DrawRectangle(XPens.DarkViolet, particularsRect);
            //gfx.DrawRectangle(XPens.GreenYellow, mfoPapRect);
            //gfx.DrawRectangle(XPens.HotPink, uacsCodeRect);

            XTextFormatter tf = new XTextFormatter(gfx);

            var font = new XFont("Arial", 8, XFontStyleEx.Regular);
            var bold = new XFont("Arial", 8, XFontStyleEx.Bold);

            // Header
            gfx.DrawString(data.OrsNumber, bold, XBrushes.Black, new XPoint(325, 55));
            gfx.DrawString(data.OrsDate?.ToString("MM/dd/yyyy") ?? "", font, XBrushes.Black, new XPoint(330, 75));
            gfx.DrawString(data.fundClusterName ?? "", font, XBrushes.Black, new XPoint(353, 96));

            // Payee
            gfx.DrawString(data.Payee ?? "", font, XBrushes.Black, new XPoint(110, 137));
            gfx.DrawString(data.Office ?? "", font, XBrushes.Black, new XPoint(110, 155));
            gfx.DrawString(data.Address ?? "", font, XBrushes.Black, new XPoint(110, 170));

            // Details
            //gfx.DrawString(data.responsibilityCenterName ?? "", font, XBrushes.Black, new XPoint(30, 210));
            tf.DrawString(data.responsibilityCenterName ?? "", font, XBrushes.Black, responsibilityCenterRect, XStringFormats.TopLeft);
            tf.DrawString(data.Particulars ?? "", font, XBrushes.Black, particularsRect, XStringFormats.TopLeft);
            tf.DrawString(data.mfoPapName ?? "", font, XBrushes.Black, mfoPapRect, XStringFormats.TopLeft);
            tf.DrawString(data.uacsCodeName.ToString() ?? "", font, XBrushes.Black, uacsCodeRect, XStringFormats.TopLeft);
            gfx.DrawString($"{data.Amount:n2}", bold, XBrushes.Black, new XPoint(520, 210));

            // Total
            gfx.DrawString($"{data.Total:n2}", bold, XBrushes.Black, new XPoint(520, 392));

            // Signatories
            gfx.DrawString(data.SignatoryA ?? "", font, XBrushes.Black, new XPoint(115, 465));
            gfx.DrawString(data.PositionA ?? "", font, XBrushes.Black, new XPoint(115, 485));   
            gfx.DrawString(data.SignatoryB ?? "", font, XBrushes.Black, new XPoint(400, 465));
            gfx.DrawString(data.PositionB ?? "", font, XBrushes.Black, new XPoint(400, 485));

            using var outStream = new MemoryStream();
            pdf.Save(outStream, false);
            return outStream.ToArray();
        }
    }

}
