using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DTO
{
    public class OrderDTO
    {

        public DateTime OrderStartDate { get; set; }
        public DateTime OrderEndDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderName { get; set; }
        public string BusinessYear { get; set; }
        public int ContractNumber { get; set; }
        public string CodeOfEmployeeInCharge { get; set; }
        public IList<OrderItemDTO> OrderItems { get; set; }
        public string CurrencyISO { get; set; }

        public OrderDTO()
        {
            OrderItems = new List<OrderItemDTO>();
        }
    }
}
