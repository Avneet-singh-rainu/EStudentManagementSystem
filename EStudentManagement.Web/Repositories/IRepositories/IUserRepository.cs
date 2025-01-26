namespace EStudentManagement.Web.Repositories.IRepositories {
    using System.Threading.Tasks;
    using EStudentManagement.Web.Models;

    public interface IUserRepository {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> GetByIdAsync(int id);
    }

}
