using series_dotnet.Models;

namespace series_dotnet.Dtos.Character
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public ERpgClass Class { get; set; } = ERpgClass.Knight;
    }
}