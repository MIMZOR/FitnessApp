using System.Linq;
using FitnessApp.Data;
using FitnessApp.Models;

namespace FitnessApp.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? Authenticate(string login, string password)
        {
            string hashedPassword = PasswordHasher.ComputeSha256Hash(password);
            return _context.Users.FirstOrDefault(u => u.login == login && u.password_hash == hashedPassword);
        }

        public bool Register(User user, string rawPassword)
        {
            if (_context.Users.Any(u => u.login == user.login)) return false;
            
            user.password_hash = PasswordHasher.ComputeSha256Hash(rawPassword);
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}