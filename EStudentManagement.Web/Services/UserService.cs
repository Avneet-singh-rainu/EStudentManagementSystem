namespace EStudentManagement.Web.Services {
    using System.Threading.Tasks;
    using EStudentManagement.Web.Models;
    using EStudentManagement.Web.Repositories.IRepositories;

    public class UserService {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task<User> AuthenticateAsync(string username, string password) {
            return await _userRepository.AuthenticateAsync(username, password);
        }

        public bool CanModifyStudent(User user, int studentId) {
            if (user.Role == "Admin") return true;

            return false;
        }
    }

}
