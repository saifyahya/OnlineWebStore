using AutoMapper;
using OnlineWebStore.entity;

namespace OnlineWebStore.Dto
{
    [AutoMap(typeof(Product),ReverseMap=true)]
    public class ProductDto
    {
        public string Code { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockLevel { get; set; }

        public int StoreId { get; set; }

    }
}
