using System.Collections.Generic;
using System.Threading.Tasks;
using series_dotnet.Models;

namespace series_dotnet.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task <bool> UserExists(string username, string email);
    }
}