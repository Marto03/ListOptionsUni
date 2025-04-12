namespace HotelApp.Common.CustomExceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string userName)
            : base($"Потребителското име \"{userName}\" вече съществува.") { }
    }
}
