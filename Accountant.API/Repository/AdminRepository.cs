using Accountant.API.Data;
using Accountant.API.Entities;
using Accountant.API.Migrations;
using Accountant.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accountant.API.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AccountantContext _context;

        public AdminRepository(AccountantContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteUser(string Email)
        {
            try
            {
                var deleteUser = await _context.Users.Where(x => x.Email == Email || x.Email == Email).FirstOrDefaultAsync();
                if (deleteUser != null)
                {
                    _context.Users.Remove(deleteUser);
                    if (await Save())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                // log exception
                throw;
            }
        }

        public async Task<Admin> GetAdminById(int id)
        {
            try
            {
                var Admin = await _context.Admins.Where(ad => ad.Id == id).FirstOrDefaultAsync();
                return new Admin();
            }
            catch (Exception)
            {
                //log exception
                throw;
            }
        }

        public async Task<ICollection<Admin>> GetAdmins()
        {
            try
            {
                var Admins = await _context.Admins.ToListAsync();
                return Admins;
            }
            catch (Exception)
            {
                // log exception
                throw;
            }
        }

        public async Task<User> GetUserByUsernameOrEmail(string spec)
        {
            try
            {
                var user = await _context.Users.Where(sp => sp.UserName.ToLower() == spec 
                                        || sp.Email.ToLower() == spec).FirstOrDefaultAsync();
                if(user != null)
                    return user;

                return new User();
            }
            catch (Exception)
            {
                //log exception
                throw;
            }
        }

        public async Task<Admin> loginAdmin(string AdminSpec, string password)
        {
            try
            {
                var admin = await _context.Admins.Where(af => (af.AdminFirstname == AdminSpec || af.AdminAlias == AdminSpec)
                                                && (af.AdminPassword == password)).FirstOrDefaultAsync();
                if (admin != null)
                    return admin;
               
                return new Admin();
            }
            catch
            {
                // lox exception
                throw;
            }
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateUserPassword(string spec, string password)
        {
            try
            {
                var user = await _context.Users.Where(sp =>sp.Email == spec).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.Password = password;
                    _context.Users.Update(user);
                    if (await Save())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                //log exception
                throw;
            }
        }
    }
}
