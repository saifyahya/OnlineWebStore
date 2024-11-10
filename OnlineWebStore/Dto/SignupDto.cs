using System.ComponentModel.DataAnnotations;

namespace OnlineWebStore.Dto
{
    public class SignupDto
    {
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]

        public string Username { get; set; }
        [Required]

        public string Name { get; set; }
        public DateTime DOB { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public int StoreId { get; set; }

    }
}
