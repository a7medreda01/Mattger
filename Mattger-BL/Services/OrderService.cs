using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Mattger_DAL.Entities.Enums;
using Mattger_DAL.IRepos;

namespace Mattger_BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepo<Order> _repo;
        private readonly IGenericRepo<OrderItem> _orderItemRepo;
        private readonly IGenericRepo<Cart> _cartRepo;

        public OrderService(
            IGenericRepo<Order> orderRepo,
            IGenericRepo<OrderItem> orderItemRepo,
            IGenericRepo<Cart> cartRepo)
        {
            _repo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _cartRepo = cartRepo;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _repo.GetAll(o => o.OrderItems).ToList();
        }
        public IEnumerable<Order> GetOrders(string UId)
        {
            return _repo.GetOrdersByUser(UId);

        }
        public Order GetOrderById(int id)
        {
            return _repo.GetOrderById(id);
        }
        public void CreateOrder(string userId, Order newOrder)
        {
            var cart = _cartRepo
                .GetCart(userId);

            if (cart == null || cart.Items.Count == 0)
                throw new Exception("Cart is empty");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                TotalPrice = 0,
                OrderItems = new List<OrderItem>(),
                Phone= newOrder.Phone,
                Address= newOrder.Address
            };

            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                order.TotalPrice += item.Quantity * item.Product.Price;

                order.OrderItems.Add(orderItem);
            }

            _repo.Add(order);
            _repo.Save();
        }
        public void CancelOrder(int orderId)
        {
            var order = _repo.GetById(orderId);

            if (order == null)
                return;

            order.Status = OrderStatus.Cancelled;

            _repo.Update(order);
            _repo.Save();
        }
        public void UpdateOrder(int orderId, OrderStatus newStatus)
        {
            var order = _repo
                .GetOrderById(orderId);
            
            if (order == null)
                throw new Exception("Order not found");

            order.Status = newStatus;

            _repo.Update(order);
            _repo.Save();
        }

    }
}
