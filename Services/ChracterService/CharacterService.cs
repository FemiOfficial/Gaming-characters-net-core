using series_dotnet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using series_dotnet.Dtos.Character;
using AutoMapper;
using System;
using series_dotnet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace series_dotnet.Services.ChracterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httContextAccessor;
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            Character character = _mapper.Map<Character>(newCharacter);

            character.User = await _context.Users.FirstOrDefaultAsync(c => c.Id == GetUserId());

            await _context.Characters.AddAsync(character);
            
            await _context.SaveChangesAsync();

            serviceResponse.Data = (_context.Characters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacters = await _context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            Character dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());

            if (dbCharacter == null) 
            {
                serviceResponse.Message = "Character not found";
                serviceResponse.Success = false;
            }

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {

                Character character = await _context.Characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id && c.User.Id == GetUserId());

                if (character != null)
                {
                    if (updatedCharacter.Name != null)
                    {
                        character.Name = updatedCharacter.Name;
                    }

                    if (updatedCharacter.Class != 0)
                    {
                        character.Class = updatedCharacter.Class;
                    }

                    if (updatedCharacter.Defense != 0)
                    {
                        character.Defense = updatedCharacter.Defense;
                    }

                    if (updatedCharacter.HitPoints != 0)
                    {
                        character.HitPoints = updatedCharacter.HitPoints;
                    }

                    if (updatedCharacter.Strength != 0)
                    {
                        character.Strength = updatedCharacter.Strength;
                    }

                    if (updatedCharacter.Intelligence != 0)
                    {
                        character.Intelligence = updatedCharacter.Intelligence;
                    }

                    _context.Characters.Update(character);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                } 
                else 
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.Characters.FirstAsync(c => c.Id == id && c.User.Id == GetUserId());

                if(character != null) 
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = (_context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
                }
                else 
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}