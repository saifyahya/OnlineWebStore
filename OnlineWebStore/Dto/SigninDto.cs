using System.ComponentModel.DataAnnotations;

namespace OnlineWebStore.Dto
{
    public class SigninDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
