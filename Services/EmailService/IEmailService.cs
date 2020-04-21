using System.Threading.Tasks;

namespace series_dotnet.Services.EmailService
{
    public interface IEmailService
    {
        void SendMail(Message message);
        Task SendMailAsync(Message message);
    }
}