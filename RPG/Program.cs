using RPG.Data;
using RPG.Enums;
using RPG.GameLogic;
using RPG.Menus;
using RPG.Models;

namespace RPG
{
    public class Program
    {
        private static Screen currentScreen = Screen.MainMenu;
        private static Hero player;
        private static Game game;
        public static void Main(string[] args)
        {
            var dbContext = new GameDbContext();

            while (currentScreen != Screen.Exit)
            {
                switch (currentScreen)
                {
                    case Screen.MainMenu:
                        ShowMainMenu();
                        break;
                    case Screen.CharacterSelect:
                        ShowCharacterSelect(dbContext);
                        break;
                    case Screen.InGame:
                        PlayGame();
                        break;

                }
            }
            ExitMenu.ShowRankings(dbContext, player);

        }


        private static void ShowMainMenu()
        {
            MainMenu.Show();
            currentScreen = Screen.CharacterSelect;
        }

        private static void ShowCharacterSelect(GameDbContext dbContext)
        {
            var characterCreation = new CharacterCreation(dbContext);
            player = characterCreation.CreateCharacter();
            game = new Game(player, dbContext);
            currentScreen = Screen.InGame;
        }

        private static void PlayGame()
        {
            Console.Clear();
            game.InitializeGame();
            game.Start();

            currentScreen = Screen.Exit;
            // make exit screen show how many monsters are killed and show ranking of top 5 players
        }
    }
}