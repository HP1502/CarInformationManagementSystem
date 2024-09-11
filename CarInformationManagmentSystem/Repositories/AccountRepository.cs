using CarInformationManagmentSystem.Data;
using CarInformationManagmentSystem.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CarInformationManagmentSystem.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Context _context;

        public AccountRepository(Context context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string userName, string password)
        {
            try
            {
                var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Create(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(string userName, string password)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(c => c.UserName == userName && c.Password == password);

                if (user == null)
                {
                    return false;
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Authorize]
        public async Task<User> GetUserByUserName(string userName)
        {
            try
            {
                var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UsernameExists(string username)
        {
            try {
                var existingUser = await _context.Users.FindAsync(username);
                if (existingUser == null) return false;
                return true;
            }
            catch {
                return true;
            }
        }

        public async Task<bool> Update(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserName);
            if (existingUser == null) return false;

            try
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
