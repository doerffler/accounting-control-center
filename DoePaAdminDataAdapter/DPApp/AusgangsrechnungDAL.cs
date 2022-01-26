using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using DoePaAdminDataModel.Kostenrechnung;

namespace DoePaAdminDataAdapter.DPApp
{
    public class AusgangsrechnungDAL : BaseDAL
    {

        public AusgangsrechnungDAL(string connectionString) : base(connectionString)
        {

        }

        private async Task<Ausgangsrechnung> ReadAusgangsrechnungFromReaderAsync(SqlDataReader rdr)
        {
            Ausgangsrechnung newAusgangsrechnung = new()
            {
                RechnungID = rdr.GetInt32("id"),
                RechnungsNummer = rdr.GetString("invoice_no"),
                BezahltDatum = rdr.GetDateTime("date_paid"),
                RechnungsDatum = rdr.GetDateTime("date_document"),
                //ZugehoerigerVertrag
            };

            newAusgangsrechnung.Rechnungspositionen = await this.ReadAusgangsrechnungspositionenForAusgangsrechnungAsync(newAusgangsrechnung);
            newAusgangsrechnung.KorrekturRechnung = await this.ReadAusgangsrechnungAsync(rdr.GetInt32("outgoing_invoice_id_replaced_by"));

            return newAusgangsrechnung;
        }

        public async Task<ICollection<Ausgangsrechnung>> ReadAusgangsrechnungenAsync()
        {

            IList<Ausgangsrechnung> listAusgangsrechnung = new List<Ausgangsrechnung>();

            SqlCommand cmd = new(Properties.Resources.ReadAusgangsrechnungen)
            {
                CommandType = CommandType.Text
            };

            using (SqlDataReader rdr = await this.CreateReaderFromCommandAsync(cmd))
            {

                while (rdr.Read())
                {
                    listAusgangsrechnung.Add(await this.ReadAusgangsrechnungFromReaderAsync(rdr));
                }

            }

            return listAusgangsrechnung;
        }

        public async Task<Ausgangsrechnung> ReadAusgangsrechnungAsync(int korrekturRechnungsId)
        {
            
            SqlCommand cmd = new(Properties.Resources.ReadAusgangsrechnung)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.Add("@AUSGANGSRECHNUNGID", SqlDbType.Int);
            cmd.Parameters["@AUSGANGSRECHNUNGID"].Value = korrekturRechnungsId;

            using (SqlDataReader rdr = await this.CreateReaderFromCommandAsync(cmd))
            {
                while (rdr.Read())
                {
                    return await this.ReadAusgangsrechnungFromReaderAsync(rdr);
                }
            }

            return null;
        }

        public async Task<ICollection<Ausgangsrechnungsposition>> ReadAusgangsrechnungspositionenForAusgangsrechnungAsync(Ausgangsrechnung zugehoerigeRechnung)
        {
            IList<Ausgangsrechnungsposition> listAusgangsrechnungsposition = new List<Ausgangsrechnungsposition>();

            SqlCommand cmd = new(Properties.Resources.ReadAusgangsrechnungspositionen);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@AUSGANGSRECHNUNGID", SqlDbType.Int);
            cmd.Parameters["@AUSGANGSRECHNUNGID"].Value = zugehoerigeRechnung.RechnungID;

            using (SqlDataReader rdr = await this.CreateReaderFromCommandAsync(cmd))
            {

                while (rdr.Read())
                {
                    listAusgangsrechnungsposition.Add(new Ausgangsrechnungsposition
                    {
                        ZugehoerigeRechnung = zugehoerigeRechnung,
                        AnzahlStunden = rdr.GetDecimal("hours"),
                        LeistungszeitraumBis = rdr.GetDateTime("date_service_to"),
                        LeistungszeitraumVon = rdr.GetDateTime("date_service_from"), 
                        Nettobetrag = rdr.GetDecimal("netto"),
                        NettobetragWaehrungISO = rdr.GetString("currency"),
                        Positionsbeschreibung = rdr.GetString("position_text"),
                        PositionsNummer = rdr.GetInt32("sequence"),
                        RechnungspositionID = rdr.GetInt32("id"),
                        Steuersatz = rdr.GetDecimal("tax_percent")/100m,
                        Stundensatz = rdr.GetDecimal("hourly_rate"),
                        //todo: Hier noch zugehörige Eingangsrechnungen einlesen:
                        ZugehoerigeFremdleistungen = null,
                        //ZugehoerigeKostenstelle = ,
                        //ZugehoerigesProjekt
                    });
                }

            }

            return listAusgangsrechnungsposition;
        }
    }

}