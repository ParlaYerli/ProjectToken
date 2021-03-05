using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Responses;
using Web.API.Domain.Services.Abstract;
using Web.API.Security.Token;

namespace Web.API.Domain.Services.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }
        public AccessTokenResponse CreateAccessToken(string email, string password)
        {
            UserResponse userResponse = _userService.FindEmailAndPassword(email, password);
            if (userResponse.Success)
            {
                AccessToken accessToken = _tokenHandler.CreateAccessToken(userResponse.User);
                _userService.SaveRefreshToken(userResponse.User.Id, accessToken.RefreshToken);
                return new AccessTokenResponse(accessToken);
            }
            else
            {
                return new AccessTokenResponse(userResponse.Message);
            }
        }

        public AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken)
        {
            UserResponse userResponse= _userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                if (userResponse.User.RefreshTokenEndDate>DateTime.Now)
                {
                    AccessToken accessToken = _tokenHandler.CreateAccessToken(userResponse.User);
                    _userService.SaveRefreshToken(userResponse.User.Id, accessToken.RefreshToken);
                    return new AccessTokenResponse(accessToken);
                }
                else
                {
                    return new AccessTokenResponse("refresh token süresi dolmuş.");
                }
            }
            else
            {
                return new AccessTokenResponse("refresh token bulunamadı.");
            }
        }

        public AccessTokenResponse RevokeRefreshToken(string refreshToken)
        {
            UserResponse userResponse = _userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                _userService.RemoveRefreshToken(userResponse.User);
                return new AccessTokenResponse(new AccessToken());
            }
            else
            {
                return new AccessTokenResponse("refresh token bulunamadı");
            }
        }
    }
}
