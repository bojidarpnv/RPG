namespace RPG.GameLogic
{
    public class GameActions
    {
        public delegate void AttackHandler();
        public delegate void MoveHandler();

        public static event AttackHandler? OnAttack;
        public static event MoveHandler? OnMove;

        public static void Attack() => OnAttack?.Invoke();
        public static void Move() => OnMove?.Invoke();
    }
}
