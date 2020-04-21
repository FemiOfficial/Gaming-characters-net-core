using AutoMapper;
using series_dotnet.Dtos.Character;
using series_dotnet.Dtos.Weapon;
using series_dotnet.Models;

namespace series_dotnet
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
        }
    }
}