using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.DTO;
using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using System.Net;

namespace ACCWebAPI.Controllers
{
    [ApiController]
    [Route("outgoing_invoices")]
    public class OutgoingInvoiceController : ControllerBase
    {
        private readonly ILogger<OutgoingInvoiceController> _logger;
        private readonly IACCService _accService;

        public OutgoingInvoiceController(ILogger<OutgoingInvoiceController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOutgoingInvoices(int? currentPage = 0, int? pageSize = 0)
        {
            try
            {
                IEnumerable<Ausgangsrechnung> ausgangsrechnungen = await _accService.GetAusgangsrechnungenAsync(default, currentPage, pageSize);
                int totalCount = await _accService.GetAusgangsrechnungenCountAsync();

                var response = new ApiResponseDTO<Ausgangsrechnung>
                {
                    Items = ausgangsrechnungen,
                    TotalCount = totalCount
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> PostOutgoingInvoice(Ausgangsrechnung ausgangsrechnung)
        {
            try
            {
                ausgangsrechnung.AusgangsrechnungID = 0;

                Ausgangsrechnung neueAusgangsrechnung = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Ausgangsrechnungen, ausgangsrechnung, default);

                await _accService.SaveChangesAsync();

                return Ok(neueAusgangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{OutgoingInvoiceID}")]
        public async Task<IActionResult> GetOutgoingInvoice(int OutgoingInvoiceID)
        {
            try
            {
                Ausgangsrechnung ausgangsrechnung = (await _accService.GetAusgangsrechnungAsync(OutgoingInvoiceID)).FirstOrDefault();
                return Ok(ausgangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{OutgoingInvoiceID}")]
        public async Task<IActionResult> PutOutgoingInvoice(int OutgoingInvoiceID, Ausgangsrechnung ausgangsrechnung)
        {
            try
            {
                Ausgangsrechnung ausgangsrechnungUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Ausgangsrechnungen, OutgoingInvoiceID, ausgangsrechnung);

                await _accService.SaveChangesAsync();

                return Ok(ausgangsrechnungUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{OutgoingInvoiceID}")]
        public async Task<IActionResult> DeleteOutgoingInvoice(int OutgoingInvoiceID)
        {
            try
            {
                Ausgangsrechnung ausgangsrechnung = (await _accService.GetAusgangsrechnungAsync(OutgoingInvoiceID)).FirstOrDefault();
                _accService.RemoveAusgangsrechnung(ausgangsrechnung);

                await _accService.SaveChangesAsync();

                return Ok(ausgangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{OutgoingInvoiceID}/pdf")]
        public async Task<IActionResult> PostPdf(int OutgoingInvoiceID)
        {
            Ausgangsrechnung ausgangsrechnung = (await _accService.GetAusgangsrechnungAsync(OutgoingInvoiceID)).First();

            string OutgoingInvoiceDirectory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "documents/outgoing_invoices");

            byte[] pdfBytes;

            if (!Directory.Exists(OutgoingInvoiceDirectory))
            {
                Directory.CreateDirectory(OutgoingInvoiceDirectory);
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new PdfWriter(memoryStream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);

                        string webRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        string letterheadPdfFile = System.IO.Path.Combine(webRootPath, "briefpapier.pdf");

                        var letterheadReader = new PdfReader(letterheadPdfFile);
                        var letterheadPdf = new PdfDocument(letterheadReader);
                        var letterheadPage = letterheadPdf.GetFirstPage();

                        var formXObject = letterheadPage.CopyAsFormXObject(pdf);

                        // Weitere Inhalte zum PDF hinzufügen...
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));
                        document.Add(new Paragraph($"Zeile"));

                        // Schließen des PDF-Readers für das Briefpapier
                        letterheadPdf.Close();

                        // Durchlaufe die Seiten des PDF-Dokuments und füge das Briefpapier als Hintergrund hinzu
                        for (int pageNumber = 1; pageNumber <= pdf.GetNumberOfPages(); pageNumber++)
                        {
                            PdfPage page = pdf.GetPage(pageNumber);
                            PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);
                            canvas.AddXObjectAt(formXObject, 0, 0);
                        }
                    }
                }

                pdfBytes = memoryStream.ToArray();
            }


            await System.IO.File.WriteAllBytesAsync($"{OutgoingInvoiceDirectory}/INV_{ausgangsrechnung.RechnungsNummer}.pdf", pdfBytes);


            return Ok("Outgoing invoice generated successfully.");
        }

        [HttpGet("download/{Filename}")]
        public IActionResult DownloadFile(string Filename)
        {
            string Filepath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "documents/outgoing_invoices", Filename);

            if (!System.IO.File.Exists(Filepath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(Filepath, FileMode.Open, FileAccess.Read);

            return File(fileStream, "application/octet-stream");
        }
    }
}
