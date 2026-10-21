namespace BotCreator.Api.ViewModels
{
    public class UserDto : UserWithoutPasswordDto
    {
        public string Password { get; set; }
    }
}
