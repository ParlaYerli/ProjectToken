using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Responses;
using Web.API.Security.Token;

namespace Web.API.Domain.Services.Abstract
{
    public interface IAuthenticationService
    {
        AccessTokenResponse CreateAccessToken(string email, string password);
        AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken);
        AccessTokenResponse RevokeRefreshToken(string refreshToken);
    }
}
