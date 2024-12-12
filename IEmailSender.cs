using System.Threading.Tasks;

namespace AllFunctionalityNetCore.Repository.Interface
{
    public interface IEmailSender
    { 
        Task<bool> EmailSendAsync(string email,string subject,string msg);
    }
}
