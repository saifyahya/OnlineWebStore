using AutoMapper;
using Microsoft.Data.SqlClient.DataClassification;
using OnlineWebStore.entity;

namespace OnlineWebStore.Dto
{
    [AutoMap(typeof(Order), ReverseMap = true)]
    public class OrderDto
    {
        public string Code { get; set; }    
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }

        public string ShippingAddress { get; set; }

        public string PaymentMethod { get; set; }

        public string CustomerDetails { get; set; }
        public string Status { get; set; }
        public int StoreId { get; set; }

        public Dictionary<string,int> Order { get; set; }
        public List<ProductOrderDto> Products { get; set; }

    }
}
