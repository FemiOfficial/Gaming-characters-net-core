
namespace series_dotnet.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public ERpgClass Class { get; set; }
        public User User  { get; set; }

        public Weapon weapon { get; set; }
    }
}