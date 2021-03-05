using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Model;
using Web.API.Domain.Repositories;
using Web.API.Domain.Responses;
using Web.API.Domain.Services.Abstract;
using Web.API.Domain.UnitOfWork;

namespace Web.API.Domain.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public UserResponse AddUser(User user)
        {
            try
            {
                _repository.AddUser(user);
                _unitOfWork.Complete();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı eklenirken bir hata oluştu:{ex.Message}");
            }
        }

        public UserResponse FindById(int userId)
        {
            try
            {
                User user = _repository.FindById(userId);
                if (user == null)
                {
                    return new UserResponse("Kullanıcı bulunamadı");
                }
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi: {ex.Message}");
            }
        }

        public UserResponse FindEmailAndPassword(string email, string password)
        {
            User user = _repository.FindByEmailandPassword(email, password);
            try
            {
                if (user == null)
                {
                    return new UserResponse("Kullanıcı bulunamadı");
                }
                else
                {
                    return new UserResponse(user);
                }
            }
            catch (Exception exp)
            {
                return new UserResponse($"Kullanıcı bulunamadı:{exp.Message}");
            }
        }

        public UserResponse GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                User user = _repository.GetUserWithRefreshToken(refreshToken);
                if (user == null)
                {
                    return new UserResponse("Kullanıcı bulunamadı");
                }
                else
                {
                    return new UserResponse(user);
                }
            }
            catch (Exception exp)
            {
                return new UserResponse($"Kullanıcı bulunamadı:{exp.Message}");
            }
        }

        public void RemoveRefreshToken(User user)
        {
            try
            {
                _repository.RemoveRefreshToken(user);
                _unitOfWork.Complete();
            }

            catch (Exception)
            {
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                _repository.SaveRefreshToken(userId, refreshToken);
                _unitOfWork.Complete();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
