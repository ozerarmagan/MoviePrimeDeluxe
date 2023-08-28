using MoviePrimeDeluxe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePrimeDeluxe.Business.Abstract
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExist(string username);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<bool>> ChangePassword(string oldPassword, string newPassword, string confirmPassword);
        int GetUserId();
        string GetUsername();
        Task<ServiceResponse<bool>> DeleteAccount(string password);
    }
}
