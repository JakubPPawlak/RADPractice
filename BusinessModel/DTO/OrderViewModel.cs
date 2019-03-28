using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.DTO
{
    public class OrderViewModel
    {
        public string OrderedBy { get; set; }
        public int OrderDetailID { get; set; }
        public string CustomerName { get; set; }
        public List<Order> orders { get; set; }
    }
}
