using AutoMapper;
using OnlineWebStore.entity;

namespace OnlineWebStore.Dto
{
    [AutoMap(typeof(Store), ReverseMap = true)]

    public class StoreDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public List<ProductDto> Products { get; set; }
        public List<OrderDto> Orders { get; set; }

    }
}
