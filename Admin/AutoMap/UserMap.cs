using Admin.DTO;
using Admin.Models;
using AutoMapper;

namespace Admin.AutoMap
{
    public class UserMap:Profile
    {
        public UserMap()
        {
            CreateMap<UserDTO, User>();
        }
    }
}
