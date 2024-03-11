using System.Drawing;
using System.Globalization;
using QRCoder;

namespace ACCWebAPI.Services
{
    public class GiroCodeService
    {
        private string _recipientName;
        private string _bic;
        private string _iban;
        private decimal _amount;
        private string _purpose;
        private string _currency;

        private string giroCodeContent;

        public GiroCodeService(decimal amount, string purpose, string currency)
        {
            _amount = amount;
            _purpose = purpose;
            _currency = currency;
            _recipientName = "Dörffler & Partner GmbH";
            _iban = "DE89501900000000783064";
            _bic = "FFVBDEFFXXX";
            giroCodeContent = 
                $"BCD\n" +
                $"001\n" +
                $"1\n" +
                $"SCT\n" +
                $"{_bic}\n" +
                $"{_recipientName}\n" +
                $"{_iban}\n" +
                $"{_currency}{_amount.ToString("F2", CultureInfo.InvariantCulture)}\n" +
                $"\n" +
                $"\n" +
                $"{_purpose}\n" +
                $"\n";
        }

        public byte[] GenerateGiroCode()
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(giroCodeContent, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCodeByte = new(qrCodeData);
            byte[] qrCodeBytes = qrCodeByte.GetGraphic(20);

            return qrCodeBytes;
        }
    }
}
