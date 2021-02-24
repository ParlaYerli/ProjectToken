using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Model;

namespace Web.API.Domain.Responses
{
    public class UserResponse : BaseResponse
    {
        public User User { get; set; }

        public UserResponse(bool success, string message,User user) : base(success, message)
        {
            User = user;
        }

        public UserResponse(User user) : this(true, String.Empty, user) { }
        public UserResponse(string message) : this(false, message, null) { }
    }
}
