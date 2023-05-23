using Admin.DTO;
using Admin.Models;

namespace Admin.UserData
{
    public interface IUserService
    {
        //List<User> GetUsers();
        //User GetUser(Guid id);
        //public Task<User> AddUser(User user);    
        //void DeleteUser(User user);
        //User EditUser(User user);

        public Task<List<User>> GetAllUser();
        public Task<List<User>> GetAllUser(Guid id);
        public Task<User> AddUser(User user);
        public Task<UserDTO> UpdateUser(UserDTO userDTO, Guid id);
        public Task<User> DeleteUser(Guid id);
    }
}
