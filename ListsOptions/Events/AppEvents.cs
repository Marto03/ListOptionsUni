namespace ListsOptionsUI.Events
{
    public static class AppEvents
    {
        public static event Action? UsersChanged;

        public static void RaiseUsersChanged()
        {
            UsersChanged?.Invoke();
        }
    }
}
