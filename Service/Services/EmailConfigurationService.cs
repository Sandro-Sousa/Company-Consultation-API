using Entities.Entites;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class EmailConfigurationService : IEmailConfigurationService
    {
        private readonly IEmailConfigurationRepository _emailConfigurationRepository;
        public EmailConfigurationService(IEmailConfigurationRepository emailConfigurationRepository)
        {
           _emailConfigurationRepository = emailConfigurationRepository;
        }
        public async Task SendVerificationEmail(User user)
        {
            try
            {
                var sendEmail = await Task.FromResult(_emailConfigurationRepository.SendVerificationEmail(user));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
