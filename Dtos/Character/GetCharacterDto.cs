using series_dotnet.Dtos.Weapon;
using series_dotnet.Models;

namespace series_dotnet.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public ERpgClass Class { get; set; } = ERpgClass.Knight;
        public GetWeaponDto Weapon { get; set; }
    }
}