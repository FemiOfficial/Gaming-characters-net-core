using series_dotnet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using series_dotnet.Dtos.Character;

namespace series_dotnet.Services.ChracterService
{
    public interface ICharacterService
    {

        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);

    }
}