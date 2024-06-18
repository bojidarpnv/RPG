namespace RPG.Entities
{
    public class Warrior : Entity
    {
        public Warrior()
        {
            Strength = 3;
            Agility = 3;
            Intelligence = 0;
            Range = 1;
            Symbol = '@';
            Setup();
        }
    }
}
