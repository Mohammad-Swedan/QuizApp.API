// BLL/ServiceInterfaces/ITokenService.cs
using eCampus_Prototype.DAL.Entities;
using System.Threading.Tasks;

namespace eCampus_Prototype.BLL.ServiceInterfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(User user ,IEnumerable<string> roles);
    }
}
