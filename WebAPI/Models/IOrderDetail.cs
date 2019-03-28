using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessModel;
using BusinessModel.DTO;

namespace WebAPI.Models
{
    public interface IOrderDetail
    {
        OrderDetailViewModel GetOrderDetailsByOrderID(int id, string uid);
        void UpdateOrderDetail(OrderDetail detail, int id);
        void CreateOrderDetail(OrderDetail detail, int id);
    }
}