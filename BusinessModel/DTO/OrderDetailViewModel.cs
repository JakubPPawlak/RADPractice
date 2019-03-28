using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.DTO
{
    public class OrderDetailViewModel
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public List<OrderDetail> details { get; set; }
    }
}
