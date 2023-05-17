using E_commerce.Models;
using E_commerce.Repository.Interfaces;
using E_commerce.Services.Interfaces;
using E_commerce.SqlHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace E_commerce.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDataHelper _dataHelper;

        public OrderServices(IOrderRepository orderRepository, IDataHelper dataHelper)
        {
            _orderRepository = orderRepository;
            _dataHelper = dataHelper;
        }
        public int Create(OrderRequest request)
        {
            ValidateRequest(request);
            var Orders = new Orders
            {
                CustomerID = request.CustomerID,
                OrderDate = request.OrderDate,
                TotalAmount = request.ItemLists.Sum(item => item.Quantity * item.Price)
            };
            var id = _orderRepository.Create(Orders);
            foreach (var Items in request.ItemLists)
            {
                var orderItems = new OrderItems
                {
                    OrderID = id,
                    ProductID = Items.ProductId,
                    Quantity = Items.Quantity,  
                    ProductPrice = Items.Price
                };
                _dataHelper.PopulateOrderItemsTable(orderItems);
            }
            return id;
        }

        private void ValidateRequest(OrderRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("Order request is null.");
            }
            if (_dataHelper.CheckIfCustomerExist(request.CustomerID) == 0)
            {
                throw new KeyNotFoundException($"There is no Customer with ID {request.CustomerID}");
            }

            if (request.OrderDate > DateTime.Now)
            {
                throw new ArgumentException("Order cannot be greater than current date");
            }
            if (request.ItemLists.Count == 0)
            {
                throw new ArgumentException("Item lists are null or empty.");
            }
        }
    }
 }

