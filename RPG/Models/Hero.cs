using System.ComponentModel.DataAnnotations;

namespace RPG.Models
{
    public class Hero
    {
        [Key]
        public int Id { get; set; }
        public string? Race { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Range { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public char Symbol { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int MonstersKilled { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
