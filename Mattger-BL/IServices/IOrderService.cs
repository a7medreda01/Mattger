using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;
using Mattger_DAL.Entities.Enums;

namespace Mattger_BL.IServices
{
    public interface IOrderService
    {


        IEnumerable<Order> GetOrders(string userId);
        IEnumerable<Order> GetAllOrders();

        Order GetOrderById(int id);

        void CreateOrder(string userId,Order order);

        void CancelOrder(int orderId);
        void UpdateOrder(int orderId, OrderStatus newStatus);
    }
}
