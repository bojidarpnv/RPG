namespace RPG.Entities
{
    public class Monster : Entity
    {
        private static Random random = new Random();
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Monster()
        {
            Strength = random.Next(1, 4);
            Agility = random.Next(1, 4);
            Intelligence = random.Next(1, 4);
            Range = 1;
            Symbol = 'O'; //'◙'
            Setup();
        }

    }
}
