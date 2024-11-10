using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebStore.entity
{
    public class Order
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }

        public string ShippingAddress { get; set; }

        public string PaymentMethod { get; set; }   

        public string CustomerDetails { get; set; }
        public string Status { get; set; }

        [ForeignKey(nameof(Store))]
        public int StoreId {  get; set; }
        public Store Store { get; set; }
        public List<ProductOrder> Products { get; set; }
    }
}
