using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BusinessModel;
using WebAPI.Models;
using BusinessModel.DTO;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/OrderManager")]
    public class OrdersController : ApiController
    {
        public OrderManagerRepository db;

        public OrdersController(OrderManagerRepository repo)
        {
            this.db = repo;
        }
        public OrdersController()
        {
            db = new OrderManagerRepository();
        }

        [Route("getOrders")]
        public OrderViewModel getOrders()
        {
            var currentuser = db.getUserByName(User.Identity.Name);
            if(currentuser != null)
            {
                var CurrentUserPortfolio = db.GetOrdersByUser(currentuser.Id);
                if(CurrentUserPortfolio.orders.Count() < 1)
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.OK)
                        { ReasonPhrase = "There are no orders to manage for this user" };
                    throw new HttpResponseException(msg);
                }
                return CurrentUserPortfolio;
            }
            var msg1 = new HttpResponseMessage(HttpStatusCode.BadRequest)
                { ReasonPhrase = "User is not a customer" };
            throw new HttpResponseException(msg1);
        }

        [Route("getOrderDetailsForOrder/{id}")]
        public OrderDetailViewModel getOrderDetails(int id)
        {
            var currentUser = db.getUserByName(User.Identity.Name);
            if(currentUser != null)
            {
                var currentUserOrderDetails = db.GetOrderDetailsByOrderID(id, currentUser.Id);
                if(currentUserOrderDetails.details.Count() < 1)
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.OK)
                        { ReasonPhrase = "There are no order details for this order" };
                    throw new HttpResponseException(msg);
                }
                return currentUserOrderDetails;
            }
            var msg1 = new HttpResponseMessage(HttpStatusCode.BadRequest)
                { ReasonPhrase = "User is not a customer" };
            throw new HttpResponseException(msg1);
        }

        [Route("getOrdersForCurrentCustomer/{id}")]
        public List<Order> getOrders(string id)
        {
            return db.GetOrdersByUser(id).orders;
        }

        [Route("createOrder")]
        public IHttpActionResult craeteOrder(Order order)
        {
            var currentUser = db.getUserByName(User.Identity.Name);

            if (order == null)
            {
                BadRequest();
            }
            if(currentUser == null)
            {
                NotFound();
            }

            db.CreateOrder(order, currentUser.Id);

            return Ok();
        }
    }
}