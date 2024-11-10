using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineWebStore.config;
using OnlineWebStore.Dto;
using OnlineWebStore.entity;

namespace OnlineWebStore.service
{
    public class OrderService : IOrderService
    {
        StoreContext context;
        IMapper mapper;
        public OrderService(StoreContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void addStoreOrder(OrderDto orderDto)
        {
            Store store = context.stores.Include("Products").FirstOrDefault(s => s.Id == orderDto.StoreId);
            if (store == null)
            {
                throw new Exception($"Store with id {orderDto.StoreId} Not Exists.");
            }
            orderDto.Code = Guid.NewGuid().ToString();
            Order orderEntity = mapper.Map<Order>(orderDto);
            context.orders.Add(orderEntity);
            context.SaveChanges();

            Dictionary<string, int> order = orderDto.Order;
            foreach (var item in order)
            {
                if (store.Products!=null && !store.Products.Any(p => p.Name == item.Key))
                {
                    context.orders.Remove(orderEntity);
                    throw new Exception($"Product {item.Key} Not Exists.");
                }
                Product storeProduct = store.Products.First(p => p.Name == item.Key);
                if (storeProduct!=null && storeProduct.StockLevel < item.Value)
                {
                    context.orders.Remove(orderEntity);
                    throw new Exception($"Product {storeProduct.Name} Stock level {storeProduct.StockLevel} is Not Enough.");
                }

                storeProduct.StockLevel -= item.Value;
                ProductOrder productOrderRecord = new ProductOrder();
                productOrderRecord.ProductId = storeProduct.Id;
                productOrderRecord.OrderId = orderEntity.Id;
                productOrderRecord.Quantity = item.Value;
                productOrderRecord.Price = storeProduct.Price;
                context.productsOrder.Add(productOrderRecord);
            }
            context.SaveChanges();
        }

        public Boolean deleteOrder(string orderCode)
        {
            bool isDeleted = false;

            Order order = context.orders.FirstOrDefault(o => o.Code == orderCode);
            if (order == null)
            {
                throw new Exception($"Order {orderCode} Not Exists.");
            }
            context.orders.Remove(order);
            // var productOrders = context.productsOrder.Where(po => po.OrderId == order.Id).ToList();
            // context.productsOrder.RemoveRange(productOrders); context.SaveChanges();
            isDeleted = true;
            return isDeleted;
        }

        public Boolean editOrderStatus(string orderCode, string newStatus)
        {
            bool isUpdated = false;
            Order order = context.orders.FirstOrDefault(o => o.Code == orderCode);
            if (order == null)
            {
                throw new Exception($"Order {orderCode} Not Exists.");
            }
            order.Status = newStatus;
            context.SaveChanges();
            isUpdated = true;
            return isUpdated;
        }

        public List<OrderDto> getStoreOrders(int storeId)
        {
            Store store = context.stores.Include("Orders").FirstOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                throw new Exception($"Store with id {storeId} Not Exists.");
            }
            List<Order> orders = store.Orders;
            List<OrderDto> orderDtos = new List<OrderDto>();
            foreach (Order order in orders)
            {
                OrderDto orderDto = new OrderDto();
                orderDto.TotalQuantity = order.TotalQuantity;
                orderDto.TotalPrice = order.TotalPrice;
                orderDto.PaymentMethod = order.PaymentMethod;
                orderDto.Status = order.Status;
                orderDto.Code = order.Code;
                orderDto.StoreId = storeId;
                orderDto.ShippingAddress= order.ShippingAddress;
                orderDto.CustomerDetails = order.CustomerDetails;
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                List<ProductOrder> retievedOrders = context.productsOrder.Include("Product").Where(po => po.OrderId == order.Id).ToList();
                orderDto.Products = mapper.Map<List<ProductOrderDto>>(retievedOrders);
                retievedOrders.ForEach(ro => dictionary.Add(ro.Product.Name, ro.Quantity));
                orderDto.Order = dictionary;
                orderDtos.Add(orderDto);
            }
            return orderDtos;
        }

        public List<OrderDto> getAllStoresOrders() //Manager Access
        {
            var stores = context.stores.Include("Orders").ToList();
            List<OrderDto> orderDtos = new List<OrderDto>();

            foreach (var store in stores)
            {
                foreach (Order order in store.Orders)
                {
                    OrderDto orderDto = new OrderDto
                    {
                        TotalQuantity = order.TotalQuantity,
                        TotalPrice = order.TotalPrice,
                        PaymentMethod = order.PaymentMethod,
                        Status = order.Status,
                        Code = order.Code,
                        StoreId = store.Id,
                        ShippingAddress = order.ShippingAddress,
                        CustomerDetails = order.CustomerDetails,
                    };

                    Dictionary<string, int> dictionary = new Dictionary<string, int>();

                    List<ProductOrder> retrievedOrders = context.productsOrder.Include("Product")
                        .Where(po => po.OrderId == order.Id)
                        .ToList();

                    retrievedOrders.ForEach(ro => dictionary.Add(ro.Product.Name, ro.Quantity));
                    orderDto.Order = dictionary;
                    orderDtos.Add(orderDto);
                }
            }

            return orderDtos;
        }

    }
}
