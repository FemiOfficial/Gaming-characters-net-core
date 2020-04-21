using System.Threading.Tasks;
using series_dotnet.Dtos.Character;
using series_dotnet.Dtos.Weapon;
using series_dotnet.Models;

namespace series_dotnet.Services.WeaponService

{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}