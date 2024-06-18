namespace RPG.Interfaces
{
    public interface IEntity
    {
        int Strength { get; set; }
        int Agility { get; set; }
        int Intelligence { get; set; }
        int Range { get; set; }
        int Health { get; set; }
        int Mana { get; set; }
        int Damage { get; set; }
        char Symbol { get; set; }
        public void Setup();
    }
}
