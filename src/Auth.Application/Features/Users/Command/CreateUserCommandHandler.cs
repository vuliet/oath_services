

namespace Auth.Application.Features.Users.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommand> _logger;


        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<CreateUserCommand> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var rq = _mapper.Map<CreateUserModel>(request);
            var result = await _userRepository.CreateUserAsync(rq);

            if (result.Succeeded == false)
            {
                _logger.LogError($"Failed to create user: {@result}");
                return false;
            }

            return true;
        }
    }
}