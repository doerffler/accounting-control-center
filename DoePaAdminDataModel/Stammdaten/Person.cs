using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.Stammdaten
{
    public abstract class Person
    {

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Zuname { get; set; }

        public string Anrede { get; set; }

        public string Titel { get; set; }

    }
}
