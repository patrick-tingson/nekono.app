using Nekono.AA.Domain.Login;
using System.Threading.Tasks;

namespace Nekono.AA.Data
{
    public interface ILoginServices
    {
        Task<bool> Authenticate(string userName, string password);
        Task<LoginResponse> GetToken(string userName, string password);
    }
}