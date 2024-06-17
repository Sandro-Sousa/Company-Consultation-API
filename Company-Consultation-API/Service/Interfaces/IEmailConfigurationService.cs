using Entities.Entites;

namespace Service.Interfaces
{
    public interface IEmailConfigurationService
    {
        Task SendVerificationEmail(User user);
    }
}
