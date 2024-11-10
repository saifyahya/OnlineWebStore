using OnlineWebStore.Dto;

namespace OnlineWebStore.service
{
    public interface IOrderService
    {
        void addStoreOrder(OrderDto orderDto);
        bool deleteOrder(string orderCode);
        bool editOrderStatus(string orderCode, string newStatus);
        List<OrderDto> getAllStoresOrders();
        List<OrderDto> getStoreOrders(int storeId);
    }
}