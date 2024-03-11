using ACCDataModel.Kostenrechnung;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace ACCWebAPI.Services
{
    public class PdfService
    {
        private string _webRootPath;
        private string _stationeryPath;

        private MemoryStream _stream;
        private PdfWriter _writer;
        private PdfDocument _pdfDocument;
        private Document _document;

        private PdfFont btBoldFont = PdfFontFactory.CreateFont("wwwroot/fonts/Bitstream - Humnst777 BT Bold.ttf", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
        private PdfFont btItalicFont = PdfFontFactory.CreateFont("wwwroot/fonts/Bitstream - Humnst777 BT Italic.ttf", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
        private PdfFont btRomanFont = PdfFontFactory.CreateFont("wwwroot/fonts/Bitstream - Humnst777 BT Roman.ttf", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
        private PdfFont btFont = PdfFontFactory.CreateFont("wwwroot/fonts/Bitstream - Humnst777 Cn BT.ttf", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
        private PdfFont btLightFont = PdfFontFactory.CreateFont("wwwroot/fonts/Bitstream - Humnst777 Lt BT Light.ttf", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);

        public PdfService()
        {
            _webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            _stationeryPath = Path.Combine(_webRootPath, "briefpapier.pdf");

            _stream = new MemoryStream();
            _writer = new PdfWriter(_stream);
            _pdfDocument = new PdfDocument(_writer);
            _document = new Document(_pdfDocument);
        }

        private PdfDocument GetStationery()
        {
            PdfReader stationeryReader = new PdfReader(_stationeryPath);
            PdfDocument stationaryDocument = new PdfDocument(stationeryReader);
            return stationaryDocument;
        }

        private void ApplyStationery()
        {
            PdfDocument stationeryDocument = GetStationery();
            PdfPage stationeryPage = stationeryDocument.GetFirstPage();
            PdfFormXObject formXObject = stationeryPage.CopyAsFormXObject(_pdfDocument);

            for (int pageNumber = 1; pageNumber <= _pdfDocument.GetNumberOfPages(); pageNumber++)
            {
                PdfPage page = _pdfDocument.GetPage(pageNumber);
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), _pdfDocument);
                canvas.AddXObjectAt(formXObject, 0, 0);
            }

            stationeryDocument.Close();
        }

        private void AddHeaderRow(Table table, string col1, string col2, string col3, string col4)
        {
            Cell cell1 = new Cell().Add(new Paragraph(col1));
            cell1.SetTextAlignment(TextAlignment.LEFT);
            cell1.SetFont(btBoldFont);
            cell1.SetBorder(Border.NO_BORDER);
            cell1.SetBorderBottom(new SolidBorder(1f));
            table.AddHeaderCell(cell1);

            Cell cell2 = new Cell().Add(new Paragraph(col2));
            cell2.SetTextAlignment(TextAlignment.LEFT);
            cell2.SetFont(btBoldFont);
            cell2.SetBorder(Border.NO_BORDER);
            cell2.SetBorderBottom(new SolidBorder(1f));
            table.AddHeaderCell(cell2);

            Cell cell3 = new Cell().Add(new Paragraph(col3));
            cell3.SetTextAlignment(TextAlignment.RIGHT);
            cell3.SetFont(btBoldFont);
            cell3.SetBorder(Border.NO_BORDER);
            cell3.SetBorderBottom(new SolidBorder(1f));
            table.AddHeaderCell(cell3);

            Cell cell4 = new Cell().Add(new Paragraph(col4));
            cell4.SetTextAlignment(TextAlignment.RIGHT);
            cell4.SetFont(btBoldFont);
            cell4.SetBorder(Border.NO_BORDER);
            cell4.SetBorderBottom(new SolidBorder(1f));
            table.AddHeaderCell(cell4);
        }

        private void AddRow(Table table, int pos, string name, decimal tax, decimal sum, string currency, Border border)
        {
            Cell cell1 = new Cell().Add(new Paragraph(pos.ToString()));
            cell1.SetFont(btFont);
            cell1.SetTextAlignment(TextAlignment.CENTER);
            cell1.SetBorder(Border.NO_BORDER);
            cell1.SetBorderBottom(border);
            table.AddCell(cell1);

            Cell cell2 = new Cell().Add(new Paragraph(name));
            cell2.SetFont(btFont);
            cell2.SetTextAlignment(TextAlignment.LEFT);
            cell2.SetBorder(Border.NO_BORDER);
            cell2.SetBorderBottom(border);
            table.AddCell(cell2);

            Cell cell3 = new Cell().Add(new Paragraph(string.Format("{0:P2}", tax)));
            cell3.SetFont(btFont);
            cell3.SetTextAlignment(TextAlignment.RIGHT);
            cell3.SetBorder(Border.NO_BORDER);
            cell3.SetBorderBottom(border);
            table.AddCell(cell3);

            Cell cell4 = new Cell().Add(new Paragraph($"{sum.ToString("N2")} {currency}"));
            cell4.SetFont(btFont);
            cell4.SetTextAlignment(TextAlignment.RIGHT);
            cell4.SetBorder(Border.NO_BORDER);
            cell4.SetBorderBottom(border);
            table.AddCell(cell4);

        }

        private void AddFooterRow(Table table, string text, decimal value, string currency, Border borderTop, Border borderBottom)
        {
            Cell cell1 = new Cell(1, 3).Add(new Paragraph(text));
            cell1.SetFont(btFont);
            cell1.SetTextAlignment(TextAlignment.RIGHT);
            cell1.SetBorder(Border.NO_BORDER);
            table.AddCell(cell1);

            Cell cell2 = new Cell().Add(new Paragraph($"{value.ToString("N2")} {currency}"));
            cell2.SetFont(btFont);
            cell2.SetTextAlignment(TextAlignment.RIGHT);
            cell2.SetBorder(Border.NO_BORDER);
            cell2.SetBorderTop(borderTop);
            cell2.SetBorderBottom(borderBottom);
            table.AddCell(cell2);
        }

        public async Task<string> GeneratePdfAsync(Ausgangsrechnung ausgangsrechnung)
        {            
            _document.SetFontSize(9);
            _document.SetMargins(260, 80, 100, 65);

            string empfaenger = ausgangsrechnung.Rechnungsempfaenger.ZugehoerigerKunde == null ? ausgangsrechnung.Rechnungsempfaenger.Anschrift : ausgangsrechnung.Rechnungsempfaenger.ZugehoerigerKunde.Langname;

            Paragraph Adressfeld =
                new Paragraph(
                    $"{empfaenger}\n" +
                    $"{ausgangsrechnung.Rechnungsempfaenger.ZugehoerigeAdresse.Strasse} {ausgangsrechnung.Rechnungsempfaenger.ZugehoerigeAdresse.Hausnummer}\n" +
                    $"{ausgangsrechnung.Rechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl.PLZ} {ausgangsrechnung.Rechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl.Ortsname}\n" +
                    $"{ausgangsrechnung.Rechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl.Land}"
                )
                .SetFont(btFont)
                .SetMarginTop(-115);
            _document.Add(Adressfeld);

            Paragraph Datum =
                new Paragraph(ausgangsrechnung.RechnungsDatum.ToString("dd. MMMM yyyy"))
                .SetMarginLeft(350)
                .SetMarginTop(50)
                .SetFont(btFont);
            _document.Add(Datum);

            Paragraph Rechnungsnummer =
                new Paragraph($"Rechnung Nr. {ausgangsrechnung.RechnungsNummer}")
                .SetMarginBottom(30)
                .SetFont(btBoldFont);
            _document.Add(Rechnungsnummer);

            Paragraph Einleitungstext =
                new Paragraph(ausgangsrechnung.Rechnungstext ?? "")
                .SetFont(btFont);
            _document.Add(Einleitungstext);

            Table Positionen =
                new Table(new float[] { 1, 4, 3, 3 });

            Positionen.SetBorder(Border.NO_BORDER);
            Positionen.UseAllAvailableWidth();
            Positionen.SetMarginTop(10);

            AddHeaderRow(Positionen, "Pos.", "Posten", "MwSt. Satz", "Betrag");

            decimal Zwischensumme = 0;
            decimal Rabattsumme = 0;
            decimal Mwstsumme = 0;

            int index = 0;
            int totalItems = ausgangsrechnung.Rechnungspositionen.Count;

            foreach (Ausgangsrechnungsposition position in ausgangsrechnung.Rechnungspositionen)
            {
                index++;
                bool lastItem = index == totalItems;

                AddRow(
                    Positionen, 
                    position.PositionsNummer, 
                    $"{position.Stueckzahl} {position.ZugehoerigeAbrechnungseinheit.Abkuerzung} á {position.StueckpreisNetto.ToString("N2")} {ausgangsrechnung.ZugehoerigeWaehrung.WaehrungZeichen} {position.Positionsbeschreibung}", 
                    position.Steuersatz, 
                    position.Nettobetrag,
                    ausgangsrechnung.ZugehoerigeWaehrung.WaehrungZeichen,
                    lastItem ? new SolidBorder(0.5f) : Border.NO_BORDER
                );

                Zwischensumme += position.Nettobetrag;
                Rabattsumme += position.Nettobetrag * ausgangsrechnung.RabattPct.GetValueOrDefault();
                Mwstsumme += (position.Nettobetrag - (position.Nettobetrag * ausgangsrechnung.RabattPct.GetValueOrDefault())) * position.Steuersatz;
            }

            if(ausgangsrechnung.RabattPct != null)
            {
                AddFooterRow(
                    Positionen, 
                    "Zwischensumme (Netto)", 
                    Zwischensumme,
                    ausgangsrechnung.ZugehoerigeWaehrung.WaehrungZeichen,
                    Border.NO_BORDER,
                    Border.NO_BORDER
                );
                AddFooterRow(
                    Positionen, 
                    $"Rabatt ({string.Format("{0:P2}", ausgangsrechnung.RabattPct)}):", 
                    Rabattsumme,
                    ausgangsrechnung.ZugehoerigeWaehrung.WaehrungZeichen,
                    Border.NO_BORDER, 
                    Border.NO_BORDER
                );
            }

            decimal Nettosumme = Zwischensumme - Rabattsumme;
            decimal Bruttosumme = Nettosumme + Mwstsumme;

            AddFooterRow(
                Positionen, 
                "Gesamtsumme (Netto)", 
                Nettosumme,
                ausgangsrechnung.ZugehoerigeWaehrung.WaehrungZeichen,
                Border.NO_BORDER, 
                Border.NO_BORDER
            );
            AddFooterRow(
                Positionen, 
                "Mehrwertsteuer", 
                Mwstsumme,
                ausgangsrechnung.ZugehoerigeWaehrung.WaehrungZeichen,
                Border.NO_BORDER, 
                Border.NO_BORDER
            );
            AddFooterRow(
                Positionen, 
                "Gesamtsumme (Brutto)",            
                Bruttosumme,
                ausgangsrechnung.ZugehoerigeWaehrung.WaehrungZeichen,
                new SolidBorder(0.5f), 
                new SolidBorder(0.5f)
            );

            _document.Add(Positionen);

            Paragraph Abschlusstext =
                new Paragraph("Wir bitten Sie die Rechnung innerhalb von 30 Tagen nach Erhalt zu begleichen."
                )
                .SetFont(btFont)
                .SetMarginTop(30);
            _document.Add(Abschlusstext);

            GiroCodeService giroCodeService = new(
                Bruttosumme,
                $"Rechnungsnummer: {ausgangsrechnung.RechnungsNummer}",
                ausgangsrechnung.ZugehoerigeWaehrung.WaehrungISO
            );

            Image girocode = new Image(
                ImageDataFactory.Create(giroCodeService.GenerateGiroCode())
            ).ScaleToFit(75, 75);
            _document.Add(girocode);

            Paragraph Grussformel =
                new Paragraph("Mit freundlichen Grüßen"
                )
                .SetFont(btFont);
            _document.Add(Grussformel);

            ApplyStationery();

            _document.Close();
            _pdfDocument.Close();

            byte[] pdfBytes = _stream.ToArray();

            string pdfBase64 = Convert.ToBase64String(pdfBytes);

            return pdfBase64;
        }
    }
}
