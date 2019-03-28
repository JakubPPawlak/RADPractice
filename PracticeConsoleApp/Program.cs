using System;
using System.Collections.Generic;
using System.Linq;
using BusinessModel;
using BusinessModel.DTO;
using System.Text;
using System.Threading.Tasks;
using WebAPIClient;

namespace PracticeConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientAuthentication.baseWebAddress = "http://localhost:55650/";
            if (ClientAuthentication.login("bowles.lionie@itsligo.ie", "LBowles$1"))
            {
                Console.WriteLine("Successful login Token acquired {0} user status is {1}", ClientAuthentication.AuthToken, ClientAuthentication.AuthStatus.ToString());
                try
                {
                    OrderViewModel acuvm = ClientAuthentication.getItem<OrderViewModel>("api/OrderManager/getOrders");
                    if (acuvm != null)
                    {
                        Console.WriteLine("Got {0} orders for current logged in user {1}", acuvm.orders.Count(), acuvm.CustomerName);
                        // Get account list using current UserID
                        List<Order> orders = ClientAuthentication.getList<Order>("api/OrderManager/getOrdersForCurrentCustomer/" + acuvm.OrderedBy);
                        foreach (var item in orders)
                        {
                            Console.WriteLine("Order ID {0}", item.OrderID);

                            OrderDetailViewModel odvm = ClientAuthentication.getItem<OrderDetailViewModel>("api/OrderManager/getOrderDetailsForOrder/" + item.OrderID);

                            Console.WriteLine("Got {0} order details for order with id {1}", odvm.details.Count(), item.OrderID);

                            foreach (var oDetail in odvm.details)
                            {
                                Console.WriteLine("Order ID: {0}, Order Detail ID: {1}, Product Ordered: {2}, Quantity Ordered: {3}", oDetail.OrderID, oDetail.OrderDetailID, oDetail.ProductOdered, oDetail.Qauntity);
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error {0} --> {1}", ex.Message, ex.InnerException.Message);
                }
                Console.ReadKey();
            }
        }
    }
}
