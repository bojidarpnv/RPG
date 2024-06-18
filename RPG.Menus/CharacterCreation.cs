using RPG.Core.Entities;
using RPG.Data;
using RPG.Data.Models;

namespace RPG.Menus
{
    public class CharacterCreation
    {
        private Entity player;
        private int pointsToBeDistributed = 3;
        private readonly GameDbContext dbContext;

        public CharacterCreation(GameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Hero CreateCharacter()
        {
            ShowCharacterSelect();
            var hero = SetupHero();
            SaveHero(hero); // Save hero to the database
            return hero;
        }

        private void ShowCharacterSelect()
        {
            Console.Clear();
            Console.WriteLine("Choose character type:");
            Console.WriteLine("Options:");
            Console.WriteLine("1) Warrior");
            Console.WriteLine("2) Archer");
            Console.WriteLine("3) Mage");
            Console.Write("Your pick: ");
            ConsoleKeyInfo raceKeyInfo = Console.ReadKey(true);

            switch (raceKeyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    player = new Warrior();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    player = new Archer();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    player = new Mage();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Try again.");
                    Thread.Sleep(1500);
                    ShowCharacterSelect(); // Recursively call until a valid choice is made
                    break;
            }

            Console.Clear();
            if (player != null)
            {
                bool validResponse = false;
                while (!validResponse)
                {
                    Console.WriteLine("Would you like to buff up your stats before starting? (Limit: 3 points total) (Y/N): ");
                    ConsoleKeyInfo response = Console.ReadKey(true);
                    if (response.Key == ConsoleKey.Y)
                    {
                        DistributePoints();
                        validResponse = true;
                        Console.Clear();
                        Console.WriteLine("Loading...");
                    }
                    else if (response.Key == ConsoleKey.N)
                    {
                        validResponse = true;
                        Console.Clear();
                        Console.WriteLine("Loading...");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Try again.");
                        Thread.Sleep(1500);
                    }
                }

            }
        }

        private void DistributePoints()
        {
            while (pointsToBeDistributed > 0)
            {
                Console.Clear();
                Console.WriteLine("Remaining Points: " + pointsToBeDistributed);
                Console.Write("Add to Strength: ");
                int strengthPoints;
                bool isStrengthValid = int.TryParse(Console.ReadLine(), out strengthPoints);
                if (!isStrengthValid || strengthPoints < 0)
                {
                    InvalidInput();
                    continue;
                }
                else if (strengthPoints > pointsToBeDistributed)
                {
                    NotEnoughPoints();
                    continue;
                }

                pointsToBeDistributed -= strengthPoints;
                player.Strength += strengthPoints;

                if (pointsToBeDistributed == 0)
                {
                    break;
                }

                Console.Clear();
                Console.WriteLine("Remaining Points: " + pointsToBeDistributed);
                Console.Write("Add to Agility: ");
                int agilityPoints;
                bool isAgilityValid = int.TryParse(Console.ReadLine(), out agilityPoints);

                if (!isAgilityValid || agilityPoints < 0)
                {
                    InvalidInput();
                    continue;
                }
                else if (agilityPoints > pointsToBeDistributed)
                {
                    NotEnoughPoints();
                    continue;
                }

                pointsToBeDistributed -= agilityPoints;
                player.Agility += agilityPoints;

                if (pointsToBeDistributed == 0)
                {
                    break;
                }

                Console.Clear();
                Console.WriteLine("Remaining Points: " + pointsToBeDistributed);
                Console.Write("Add to Intelligence: ");
                int intelligencePoints;
                bool isIntelligenceValid = int.TryParse(Console.ReadLine(), out intelligencePoints);
                if (!isIntelligenceValid || intelligencePoints < 0)
                {
                    InvalidInput();
                    continue;
                }
                else if (intelligencePoints > pointsToBeDistributed)
                {
                    NotEnoughPoints();
                    continue;
                }

                pointsToBeDistributed -= intelligencePoints;
                player.Intelligence += intelligencePoints;
                if (pointsToBeDistributed == 0)
                {
                    break;
                }
            }
            player.Setup();
        }

        private Hero SetupHero()
        {
            return new Hero
            {
                Race = player.GetType().Name.ToString(),
                Strength = player.Strength,
                Agility = player.Agility,
                Intelligence = player.Intelligence,
                Health = player.Health,
                Range = player.Range,
                Damage = player.Damage,
                Mana = player.Mana,
                Symbol = player.Symbol,
                MonstersKilled = 0,
                CreatedAt = DateTime.Now,
                PositionX = 1,
                PositionY = 1,
            };
        }

        private void SaveHero(Hero hero)
        {
            try
            {
                dbContext.Heroes.Add(hero);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void NotEnoughPoints()
        {
            Console.Clear();
            Console.WriteLine("Not enough points. Try again.");
            Thread.Sleep(1200);
        }

        private void InvalidInput()
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Try again.");
            Thread.Sleep(1200);
        }
    }
}
