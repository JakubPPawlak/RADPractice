using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using BusinessModel;
using BusinessModel.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebAPI.Models
{
    public class OrderManagerRepository : IUser, IOrder, IOrderDetail
    {
        BusinessContext bctx = new BusinessContext();
        ApplicationDbContext actx = new ApplicationDbContext();

        public void CreateOrder(Order order, string uid)
        {
            //to do
        }

        public OrderViewModel GetOrdersByUser(string uid)
        {
            ApplicationUser AccountManager = getUserByID(uid);
            if( AccountManager != null )
            {
                var ManagerOrders = bctx.Orders.Where(a => a.OrderedBy == uid).ToList();
                return new OrderViewModel
                {
                    OrderedBy = AccountManager.Id,
                    CustomerName = AccountManager.FirstName + " " + AccountManager.SecondName,
                    orders = ManagerOrders
                };
            }
            return null;
        }

        public OrderDetailViewModel GetOrderDetailsByOrderID(int id, string uid)
        {
            Order order = getOrderByID(id);
            ApplicationUser user = getUserByID(uid);

            if(order != null)
            {
                var orderDetails = bctx.Details.Where(a => a.OrderID == order.OrderID).ToList();
                return new OrderDetailViewModel
                {
                    OrderID = order.OrderID,
                    CustomerName = user.FirstName + " " + user.SecondName,
                    details = orderDetails
                };
            }
            return null;
        }

        public Order getOrderByID(int id)
        {
            Order order = bctx.Orders.Find(id);

            return order;
        }

        public void UpdateOrderDetail(OrderDetail details, int id)
        {
            throw new NotImplementedException();
        }

        public void CreateOrderDetail(OrderDetail details, int id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser getUserByID(string id)
        {
            return actx.Users.Find(id);
        }

        public ApplicationUser getUserByName(string name)
        {
            return actx.Users.FirstOrDefault(u => u.UserName == name);
        }

        public void UpdateOrder(Order order, string uid)
        {
            throw new NotImplementedException();
        }
    }
}