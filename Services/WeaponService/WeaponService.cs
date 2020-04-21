using series_dotnet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using series_dotnet.Dtos.Character;
using series_dotnet.Dtos.Weapon;
using AutoMapper;
using System;
using series_dotnet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace series_dotnet.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper) 
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon) 
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User.Id == GetUserId());
                if (character == null) 
                {
                    response.Message = "Character not found";
                    response.Success = false;
                    return response;
                }

                Weapon weapon = _mapper.Map<Weapon>(newWeapon);
                weapon.character = character;
                await _context.Weapons.AddAsync(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    }
}