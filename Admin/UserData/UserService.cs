
using Admin.DTO;
using Admin.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.UserData
{
    //public class UserService : IUserService
    //{
    //    private readonly UserContext _context;
    //    public UserService(UserContext context)
    //    {
    //        _context = context;
    //    }
    //    //private List<User> users = new List<User>()
    //    //{
    //    //    new User()
    //    //    {
    //    //        ID=Guid.NewGuid(),
    //    //        Name="Selim",
    //    //        SurName="Agamaliyev",
    //    //        Age=20,
    //    //        Genre="Kisi",
    //    //        Number=0506403840,
    //    //        Email="agamaliyev.selim@gmail.com",
    //    //        Pasword="Selim-2003",
    //    //        ConfirmPasword="Selim-2003"
    //    //    },
    //    //    new User()
    //    //    {
    //    //        ID=Guid.NewGuid(),
    //    //        Name="Agali",
    //    //        SurName="Ehmedov",
    //    //        Age=20,
    //    //        Genre="Kisi",
    //    //        Number=0506403840,
    //    //        Email="agamaliyev.selim@gmail.com",
    //    //        Pasword="Selim-2003",
    //    //        ConfirmPasword="Selim-2003"
    //    //    }
    //    //};


    //    //public User AddUser(User user)
    //    //{
    //    //    user.ID = Guid.NewGuid();
    //    //    _context.Users.Add(user);
    //    //    _context.SaveChanges();
    //    //    return user;
    //    //}

    //    public async Task<User> AddUser(User user)
    //    {
    //        user.ID=Guid.NewGuid();
    //        await _context.Users.AddAsync(user);
    //        await _context.SaveChangesAsync();
    //        return user;
    //    }

    //    public void DeleteUser(User user)
    //    {
    //        _context.Users.Remove(user);
    //        _context.SaveChanges();
    //    }

    //    public User EditUser(User user)
    //    {
    //        var existinguser = _context.Users.Find(user.ID);
    //        if(existinguser != null)
    //        {
    //            _context.Users.Update(user);
    //            _context.SaveChanges();
    //        }
    //        return user;            
    //    }

    //    public User GetUser(Guid id)
    //    {
    //        var user = _context.Users.Find(id);
    //        return user;
    //    }

    //    public List<User> GetUsers()
    //    {
    //        return _context.Users.ToList();
    //    }
    //}

    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private UserContext userContext;
        private IMapper mapper;
        public UserService(UserContext _userContext, IMapper _mapper, ILogger<UserService> logger)
        {
            userContext = _userContext;
            mapper = _mapper;
            _logger = logger;
        }

        public async Task<User> AddUser(User user)
        {
            try
            {
                user.ID = Guid.NewGuid();
                var userr = mapper.Map<User>(user);
                userContext.Users.Add(user);
                await userContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<User> DeleteUser(Guid id)
        {
            try
            {
                User user = await userContext.Users.FirstOrDefaultAsync(x => x.ID == id);
                userContext.Users.Remove(user);
                await userContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }


        public async Task<List<User>> GetAllUser()
        {
            try
            {
                var User = await userContext.Users.ToListAsync();
                return User;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public Task<List<User>> GetAllUser(Guid id)
        {
            try
            {
                var UserById = userContext.Users.Where(m => m.ID == id).ToListAsync();
                return UserById;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }


        public async Task<UserDTO> UpdateUser(UserDTO userDTO, Guid id)
        {
            try
            {
                var User = mapper.Map<User>(userDTO);
                User.ID = id;
                userContext.Users.Update(User);
                await userContext.SaveChangesAsync();
                return userDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        
    }
}
