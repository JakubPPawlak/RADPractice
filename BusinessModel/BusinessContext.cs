using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessModel
{
    public class BusinessContext : DbContext
    {
        public BusinessContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new BusinessContextInitializer());
            Database.Initialize(true);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> Details { get; set; }
    }

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm", ApplyFormatInEditMode = true)]
        public DateTime? OrderPlacedDate { get; set; }

        public double Total { get; set; }

        public string OrderedBy { get; set; }

    }

    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { get; set; }
        public string ProductOdered { get; set; }
        public int Qauntity { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public virtual Order Order { get; set; }
    }
}
