using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Model;
using Web.API.Resources;

namespace Web.API.Mapping
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserResource>();
            CreateMap<UserResource, User>();
        }
    }
}
