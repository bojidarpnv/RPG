namespace RPG.Entities
{
    public class Archer : Entity
    {
        public Archer()
        {
            Strength = 2;
            Agility = 4;
            Intelligence = 0;
            Range = 2;
            Symbol = '#';
            Setup();
        }
    }
}
