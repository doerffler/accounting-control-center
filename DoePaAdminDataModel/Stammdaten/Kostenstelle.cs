﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Kostenstelle
    {

        public int KostenstelleID { get; set; }

        public string Kostenstellenbezeichnung { get; set; }

        public DateTime GueltigAb { get; set; }

        public int ZugehoerigeKostenstellenartId { get; set; }

        public Kostenstellenart ZugehoerigeKostenstellenart { get; set; }

        public Kostenstelle ()
        {

        }

    }
}
