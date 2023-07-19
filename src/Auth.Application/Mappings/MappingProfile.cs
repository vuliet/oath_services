using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Application.Features.Users.Command;

namespace Auth.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, CreateUserModel>().ReverseMap();
        }
    }
}