using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessModel;
using BusinessModel.DTO;

namespace WebAPI.Models
{
    public interface IOrder
    {
        OrderViewModel GetOrdersByUser(string uid);
        void UpdateOrder(Order oreder, string uid);
        void CreateOrder(Order oreder, string uid);
    }
}