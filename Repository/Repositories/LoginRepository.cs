
using Entities.Entites;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;


namespace Repository.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly Context _context;

        public LoginRepository(Context context)
           => _context = context;

        public async Task<bool> EmailExists(string email)
            => await _context
                .Users
                .AsNoTracking()
                .AnyAsync(x => x.Email.Address == email);

        public async Task SaveUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email) =>
        await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email);

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
