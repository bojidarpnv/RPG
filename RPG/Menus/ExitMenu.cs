using RPG.Data;
using RPG.Models;

namespace RPG.Menus
{
    public class ExitMenu
    {
        private readonly GameDbContext dbContext;
        public ExitMenu(GameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public static void ShowRankings(GameDbContext dbContext, Hero player)
        {
            Console.WriteLine($"Game Over! You have been defeated. But you killed {player.MonstersKilled} monsters");

            Console.WriteLine("Top Players:");
            using (dbContext)
            {
                var topPlayers = dbContext.Heroes
                    .OrderByDescending(h => h.MonstersKilled)
                    .ThenByDescending(h => h.CreatedAt)
                    .Take(5)
                    .ToList();
                foreach (var player1 in topPlayers)
                {
                    Console.WriteLine($"{player1.Race} - Monsters Killed: {player1.MonstersKilled} - Created On: {player1.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss")}");
                }
                if (player.MonstersKilled == topPlayers.First().MonstersKilled)
                {
                    Console.WriteLine("You set a new record!");
                }
            }

        }
    }
}
