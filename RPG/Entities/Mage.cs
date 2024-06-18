namespace RPG.Entities
{
    public class Mage : Entity
    {
        public Mage()
        {
            Strength = 2;
            Agility = 1;
            Intelligence = 3;
            Range = 3;
            Symbol = '*';
            Setup();
        }
    }
}
