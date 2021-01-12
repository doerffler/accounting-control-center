using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Bankverbindung
    {

        public int BankverbindungID { get; set; }

        public string IBAN { get; set; }

        public string BIC { get; set; }

        public string Kreditinstitut { get; set; }

    }
}
