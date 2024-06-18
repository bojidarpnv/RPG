using RPG.Data;
using RPG.Entities;
using RPG.Models;

namespace RPG.GameLogic
{
    public class Game
    {
        private readonly GameBoard gameBoard = new GameBoard();
        private readonly Hero player;
        private readonly GameDbContext dbContext;
        private readonly Random random = new Random();
        private readonly List<string> errorMessages = new List<string>();
        private readonly List<string> successMessages = new List<string>();
        private bool gameRunning = true;
        int monstersKilled = 0;

        public Game(Hero player)
        {
            this.player = player;

            gameBoard.PlaceEntity(player.PositionX, player.PositionY, player.Symbol);

            // Subscribe to events
            GameActions.OnAttack += DisplayAttackMenu;
            GameActions.OnMove += DisplayMoveMenu;
        }
        public Game(Hero player, GameDbContext dbContext) : this(player)
        {
            this.dbContext = dbContext;
        }

        public void Start()
        {
            while (gameRunning)
            {
                DisplayPlayerStatus();
                gameBoard.PrintBoard(player);
                DisplayMessages();
                DisplayMainMenu();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.WriteLine();

                try
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.D1: // Attack
                        case ConsoleKey.NumPad1:
                            HandleAttackAction();
                            break;

                        case ConsoleKey.D2: // Move
                        case ConsoleKey.NumPad2:
                            HandleMoveAction();
                            break;

                        default:
                            throw new InvalidOperationException("Invalid input. Please try again.");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    errorMessages.Add($"Error: {ex.Message}");
                }
            }

        }

        private void DisplayPlayerStatus()
        {
            Console.WriteLine($"Health: {player.Health}, Mana: {player.Mana}");
        }

        private void DisplayMessages()
        {
            foreach (var message in successMessages)
            {
                Console.WriteLine(message);
            }
            foreach (var message in errorMessages)
            {
                Console.WriteLine($"Error: {message}");
            }
            ClearMessages();
        }

        private void ClearMessages()
        {
            successMessages.Clear();
            errorMessages.Clear();
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1) Attack");
            Console.WriteLine("2) Move");
        }

        private void HandleAttackAction()
        {
            try
            {
                List<Monster> monstersInRange = FindMonstersInRange();
                if (monstersInRange.Count > 0)
                {
                    GameActions.Attack();

                }
                else
                {
                    throw new InvalidOperationException("No monsters in range to attack.");
                }
            }
            catch (InvalidOperationException ex)
            {
                errorMessages.Add(ex.Message);
                return;
            }
            MoveMonsters();
            gameBoard.InitializeMonster();
        }

        private void HandleMoveAction()
        {
            try
            {
                GameActions.Move();

            }
            catch (InvalidOperationException ex)
            {
                errorMessages.Add(ex.Message);
                return;
            }
            MoveMonsters();
            gameBoard.InitializeMonster();
        }

        private void DisplayMoveMenu()
        {
            Console.WriteLine("Choose a direction to move:");
            Console.WriteLine("W - Move up");
            Console.WriteLine("S - Move down");
            Console.WriteLine("D - Move right");
            Console.WriteLine("A - Move left");
            Console.WriteLine("E - Move diagonally up & right");
            Console.WriteLine("X - Move diagonally down & right");
            Console.WriteLine("Q - Move diagonally up & left");
            Console.WriteLine("Z - Move diagonally down & left");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.WriteLine();


            HandleMoveInput(keyInfo);


        }

        private void HandleMoveInput(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                    MovePlayer(0, player.Range);
                    break;
                case ConsoleKey.S:
                    MovePlayer(0, -player.Range);
                    break;
                case ConsoleKey.D:
                    MovePlayer(player.Range, 0);
                    break;
                case ConsoleKey.A:
                    MovePlayer(-player.Range, 0);
                    break;
                case ConsoleKey.E:
                    MovePlayer(player.Range, player.Range);
                    break;
                case ConsoleKey.X:
                    MovePlayer(player.Range, -player.Range);
                    break;
                case ConsoleKey.Q:
                    MovePlayer(-player.Range, player.Range);
                    break;
                case ConsoleKey.Z:
                    MovePlayer(-player.Range, -player.Range);
                    break;
                default:
                    throw new InvalidOperationException("Invalid input. Please try again.");
            }
        }

        private void DisplayAttackMenu()
        {
            List<Monster> monstersInRange = FindMonstersInRange();
            Dictionary<int, Monster> monsterDict = new Dictionary<int, Monster>();

            for (int i = 0; i < monstersInRange.Count; i++)
            {
                Monster monster = monstersInRange[i];
                monsterDict.Add(i + 1, monster);
                Console.WriteLine($"{i + 1}: Target at ({monster.PositionX}, {monster.PositionY}) with {monster.Health} health");
            }

            Console.WriteLine("Enter the number of the monster you want to attack:");


            if (int.TryParse(Console.ReadLine(), out int choice) && monsterDict.ContainsKey(choice))
            {
                AttackMonster(monsterDict[choice]);
            }
            else
            {
                throw new InvalidOperationException("Invalid choice.");
            }

        }

        private void AttackMonster(Monster monster)
        {
            int damage = player.Damage;
            monster.Health -= damage;
            successMessages.Add($"You attacked the monster for {damage} damage!");

            if (monster.Health <= 0)
            {
                successMessages.Add("You defeated the monster!");
                gameBoard.RemoveEntity(monster.PositionX, monster.PositionY);
                gameBoard.Monsters.Remove(monster);
                monstersKilled++;

            }
        }

        public void InitializeGame()
        {
            gameBoard.InitializeBoard();
            gameBoard.InitializePlayer(player);
            gameBoard.InitializeMonster();
            gameBoard.PrintBoard(player);
        }

        private void MovePlayer(int offsetX, int offsetY)
        {
            int newX = player.PositionX + offsetX;
            int newY = player.PositionY + offsetY;

            if (gameBoard.IsPositionEmpty(newX, newY))
            {
                gameBoard.RemoveEntity(player.PositionX, player.PositionY);
                player.PositionX = newX;
                player.PositionY = newY;
                gameBoard.PlaceEntity(player.PositionX, player.PositionY, player.Symbol);
            }
            else if (gameBoard.IsPositionOutOfBounds(newX, newY))
            {
                throw new InvalidOperationException("Can't move there, index out of bounds.");
            }
            else if (!gameBoard.IsPositionEmpty(newX, newY))
            {
                throw new InvalidOperationException("Can't move there. Position is occupied.");
            }
        }

        private List<Monster> FindMonstersInRange()
        {
            List<Monster> monstersInRange = new List<Monster>();
            int maxSize = 10;

            for (int x = -player.Range; x <= player.Range; x++)
            {
                for (int y = -player.Range; y <= player.Range; y++)
                {
                    int checkX = player.PositionX + x;
                    int checkY = player.PositionY + y;

                    if (IsWithinBounds(checkX, checkY, maxSize) && IsNotPlayerPosition(checkX, checkY))
                    {
                        Monster? monster = gameBoard.GetMonster(checkX, checkY) as Monster;
                        if (monster != null)
                        {
                            monstersInRange.Add(monster);
                        }
                    }
                }
            }

            return monstersInRange;
        }

        private bool IsWithinBounds(int x, int y, int maxSize)
        {
            return x >= 0 && x < maxSize && y >= 0 && y < maxSize;
        }

        private bool IsNotPlayerPosition(int x, int y)
        {
            return !(x == player.PositionX && y == player.PositionY);
        }

        private void MoveMonsters()
        {
            foreach (var monster in gameBoard.Monsters)
            {
                MoveMonsterTowardsPlayer(monster);
            }
        }

        private void MoveMonsterTowardsPlayer(Monster monster)
        {
            int x = Math.Sign(player.PositionX - monster.PositionX);
            int y = Math.Sign(player.PositionY - monster.PositionY);

            int newX = monster.PositionX + x;
            int newY = monster.PositionY + y;

            if (gameBoard.IsPositionEmpty(newX, newY))
            {
                gameBoard.RemoveEntity(monster.PositionX, monster.PositionY);
                monster.PositionX = newX;
                monster.PositionY = newY;
                gameBoard.PlaceEntity(monster.PositionX, monster.PositionY, monster.Symbol);
            }
            else if (newX == player.PositionX && newY == player.PositionY)
            {
                PerformMonsterAttack(monster);
            }
        }

        private void PerformMonsterAttack(Monster monster)
        {
            int damageDealt = monster.Damage;
            player.Health -= damageDealt;
            successMessages.Add($"Monster attacked you for {damageDealt} damage!");

            if (player.Health <= 0)
            {
                GameActions.OnAttack -= DisplayAttackMenu;
                GameActions.OnMove -= DisplayMoveMenu;
                DisplayMessages();
                gameRunning = false;

                try
                {
                    this.player.MonstersKilled = monstersKilled;
                    dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
