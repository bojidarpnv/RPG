using RPG.Entities;
using RPG.Models;

namespace RPG.GameLogic
{
    public class GameBoard
    {
        private const int BoardSize = 10;
        private char[,] board;
        private readonly Random random = new Random();
        public ICollection<Monster> Monsters = new List<Monster>();

        public GameBoard()
        {
            board = new char[BoardSize, BoardSize];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    board[row, col] = '▒';
                }
            }
        }

        public void PrintBoard(Hero hero)
        {

            Console.Clear();
            Console.WriteLine($"Health: {hero.Health}  Mana: {hero.Mana}");
            Console.Write(Environment.NewLine);
            for (int row = BoardSize - 1; row >= 0; row--)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    Console.Write(board[row, col]);
                }
                Console.Write(Environment.NewLine);
            }
        }

        public void PlaceEntity(int x, int y, char symbol)
        {
            if (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize)
            {
                board[y, x] = symbol;
            }
        }



        public void RemoveEntity(int x, int y)
        {
            if (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize)
            {
                board[y, x] = '▒';
            }
        }


        public void InitializeMonster()
        {
            int x = random.Next(0, BoardSize);
            int y = random.Next(0, BoardSize);
            while (!IsPositionEmpty(x, y))
            {
                x = random.Next(0, BoardSize);
                y = random.Next(0, BoardSize);
            }
            Monster monster = new Monster();
            monster.PositionX = x;
            monster.PositionY = y;
            Monsters.Add(monster);
            PlaceEntity(x, y, monster.Symbol); // Use the method to place the monster
        }

        public void InitializePlayer(Hero player)
        {
            PlaceEntity(player.PositionX, player.PositionY, player.Symbol);
        }

        public bool IsPositionEmpty(int x, int y)
        {
            if (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize)
            {
                return board[y, x] == '▒';
            }
            return false;
        }
        public bool IsPositionOutOfBounds(int x, int y)
        {
            if (x < 0 || x >= BoardSize || y < 0 || y >= BoardSize)
            {
                return true;
            }
            return false;
        }
        public Monster? GetMonster(int x, int y)
        {
            if (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize)
            {
                return Monsters.FirstOrDefault(m => m.PositionX == x && m.PositionY == y);
            }
            return null;
        }
    }
}
