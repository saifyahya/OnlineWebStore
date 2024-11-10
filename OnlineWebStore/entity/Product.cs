using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebStore.entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int StockLevel { get; set; }

        [ForeignKey(nameof(Store))]
        public int StoreId;
        public Store Store { get; set; }

        public List<ProductOrder> Orders { get; set;}
    }
}
