using Accountant.API.Data;
using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace Accountant.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AccountantContext _context;
        public UserRepository(AccountantContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllUser()
        {
            var users = await _context.Users.OrderBy(u => u.Id).ToListAsync();
            return users;

        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return await Save();
            }
            return false;
        }

        public async Task<User> Login(string username, string password)
        {
            try
            {
                var user = await _context.Users.Where(un => un.UserName.Trim().ToLower() == username.Trim().ToLower()).FirstOrDefaultAsync();
                if (user == null)
                {
                    return new User();
                }
                else
                {
                    if (user.Password.ToLower() == password.ToLower())      // Do in Controller
                    {
                        return user;
                    }
                    else
                    {
                        return new User();
                    }
                }
            }

            catch (Exception)
            {
                return new User();
            }
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<User> SignUp(User users)
        {
            var newuser = await _context.AddAsync(users);

            if (newuser != null)
            {
                await Save();
                return users;
            }
            else
            {
                return new User();
            }
        }

        public async Task<bool> UpdateUser(int userid, User updateuser)
        {
            var finduser = await _context.Users.FindAsync(userid);

            if (finduser != null)
            {
                finduser.Id = userid;
                finduser.UserName = updateuser.UserName;
                finduser.Password = updateuser.Password;
                finduser.Email = updateuser.Email;
                finduser.ImgURL = updateuser.ImgURL;
                _context.Update(finduser);
                return await Save();
            }
            return false;

        }

        public Task<bool> UserExists(int userid)
        {
            return _context.Users.AnyAsync(U => U.Id == userid);
        }

        public async Task<User> GetByUserName(string username)
        {
            var user = await _context.Users.Where(us => us.UserName.ToLower() == username.ToLower()).FirstOrDefaultAsync();
            return user != null ? user : new User();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            return user != null ? user : new User();
        }
    }// Class
}

