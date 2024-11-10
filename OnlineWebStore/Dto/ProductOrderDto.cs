using AutoMapper;
using OnlineWebStore.entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebStore.Dto
{
    [AutoMap(typeof(ProductOrder), ReverseMap = true)]
    public class ProductOrderDto
    {
        public int OrderId { get; set; }


        public int ProductId { get; set; }
        public ProductDto Product { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
