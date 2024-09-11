using CarInformationManagmentSystem.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CarInformationManagmentSystem.Repositories
{
    public interface IAccountRepository
    {
        public Task<bool> Create(User user);
        [Authorize]
        public Task<bool> Update(User user);
        [Authorize]
        public Task<bool> Delete(string userName,string password);
        public Task<User> Authenticate(string userName,string password);
        [Authorize]
        public Task<User> GetUserByUserName(string userName);
        public Task<bool> UsernameExists(string username);
    }
}