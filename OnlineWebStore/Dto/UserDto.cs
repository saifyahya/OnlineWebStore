using AutoMapper;
using OnlineWebStore.entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineWebStore.Dto
{
    [AutoMap(typeof(ApplicationUser), ReverseMap = true)]

    public class UserDto
    {

        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public StoreDto Store { get; set; }
    }
}
