namespace Auth.Application.Features.Users.Command
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}