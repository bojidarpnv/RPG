namespace RPG.Menus
{
    public static class MainMenu
    {

        public static void Show()
        {

            Console.Clear();
            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play.");
            Console.ReadKey((true));
        }
    }
}
