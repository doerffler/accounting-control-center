using ACCDataModel.Stammdaten;
using System.Collections.Generic;

namespace ACCDataModel.DTO
{
    public class OrderResponseDTO
    {
        public IEnumerable<Auftrag> Auftraege { get; set; }
        public int TotalCount { get; set; }
    }
}
