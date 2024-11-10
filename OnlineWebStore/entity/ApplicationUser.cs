using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebStore.entity
{
    public class ApplicationUser:IdentityUser
    {
        public string Name {  get; set; }

        [Column(TypeName = "date")]
        public DateTime DOB {  get; set; }

        public Store Store { get; set; }
        [ForeignKey(nameof(Store))]
        public int? StoreId { get; set; }    
    }
}
