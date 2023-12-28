namespace UserService.Service
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string content);

    }
    public class SendMailService : IMailService
    {
        public Task SendEmailAsync(string email, string subject, string content)
        {
            throw new NotImplementedException();
        }
    }
}
