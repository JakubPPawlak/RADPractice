using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace BusinessModel
{
    class BusinessContextInitializer : DropCreateDatabaseIfModelChanges<BusinessContext>
    {
        public BusinessContextInitializer()
        {

        }

        protected override void Seed(BusinessContext context)
        {

            context.Details.AddOrUpdate(new OrderDetail[]
            {
                new OrderDetail { ProductOdered = "Saw", Qauntity = 52, OrderID = 1 },
                new OrderDetail { ProductOdered = "Hammer", Qauntity = 14, OrderID = 1 },
                new OrderDetail { ProductOdered = "Bucket", Qauntity = 45, OrderID = 1 },
                new OrderDetail { ProductOdered = "Saw2", Qauntity = 52, OrderID = 2 },
                new OrderDetail { ProductOdered = "Hammer2", Qauntity = 21, OrderID = 2 },
                new OrderDetail { ProductOdered = "Bucket2", Qauntity = 33, OrderID = 2 },
                new OrderDetail { ProductOdered = "Saw3", Qauntity = 20, OrderID = 3 },
                new OrderDetail { ProductOdered = "Hammer3", Qauntity = 14, OrderID = 3 },
                new OrderDetail { ProductOdered = "Bucket3", Qauntity = 15, OrderID = 3 }
            });

            context.Orders.AddOrUpdate(new Order[]
            {
                new Order { OrderPlacedDate = new DateTime(1965,12,02), Total = 255.4 },
                new Order { OrderPlacedDate = new DateTime(2001,12,02), Total = 5645.5 },
                new Order { OrderPlacedDate = new DateTime(2005,12,02), Total = 1450.4 }

            });

            base.Seed(context);
        }
    }
}
