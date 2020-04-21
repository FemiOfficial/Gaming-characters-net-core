using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using series_dotnet.Services.WeaponService;
using System.Threading.Tasks;
using series_dotnet.Dtos.Weapon;

namespace series_dotnet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("controller")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService) 
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWeapon(AddWeaponDto newWeapon) 
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }
    }
}