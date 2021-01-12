using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using DoePaAdminDataModel.Kostenrechnung;

namespace DoePaAdminDataAdapter.DPApp
{
    public class AusgangsrechnungDAL
    {

        private string ConnectionString { get; set; }

        public AusgangsrechnungDAL(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public async Task<ICollection<Ausgangsrechnung>> ReadAusgangsrechnungenAsync()
        {
            
            var listAusgangsrechnung = new List<Ausgangsrechnung>();
                        
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(Properties.Resources.ReadAusgangsrechnungen, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    
                    SqlDataReader rdr = cmd.ExecuteReader();
                    
                    while (rdr.Read())
                    {
                        listAusgangsrechnung.Add(new Ausgangsrechnung
                        {
                            RechnungID = Convert.ToInt32(rdr[0]),
                            RechnungsNummer = rdr[3].ToString()
                        });
                    }
                }

            return listAusgangsrechnung;
        }
    }

}
