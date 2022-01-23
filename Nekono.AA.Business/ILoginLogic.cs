using Nekono.AA.Domain.Login;
using System.Threading.Tasks;

namespace Nekono.AA.Business
{
    public interface ILoginLogic
    {
        Task<LoginResponse> Authenticate(LoginRequest loginRequest);
    }
}