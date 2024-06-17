
using Entities.Entites;

namespace Repository.Interfaces
{
    public interface IEmailConfigurationRepository
    {
        Task SendVerificationEmail(User user);
    }
}
