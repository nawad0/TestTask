using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using UserStatus = TestTask.Enums.UserStatus;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetUser()
        {
            return await _dbContext.Users.AsNoTracking().OrderByDescending(u => u.Orders.Count()).FirstOrDefaultAsync() ?? throw new Exception("Такого пользователя нет");

        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.AsNoTracking().Where(u => u.Status == UserStatus.Inactive).ToListAsync();
        }
    }
}
