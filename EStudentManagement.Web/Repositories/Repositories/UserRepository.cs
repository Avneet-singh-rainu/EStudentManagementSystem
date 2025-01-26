namespace EStudentManagement.Web.Repositories.Repositories {
    using System.Threading.Tasks;
    using EStudentManagement.Web.Data;
    using EStudentManagement.Web.Models;
    using EStudentManagement.Web.Repositories.IRepositories;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password) {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User> GetByIdAsync(int id) =>
            await _context.Users.Include(u => u.Student).FirstOrDefaultAsync(u => u.Id == id);
    }

}
