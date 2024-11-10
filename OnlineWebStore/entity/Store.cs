using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebStore.entity
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }

       // [ForeignKey(nameof(ApplicationUser))]
       // public string? UserId;
        public ApplicationUser ApplicationUser { get; set; } 

    }
}
