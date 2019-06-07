using InformationApp.Models.AccountViewModel;
using InformationApp.Models.AuthViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResultModel> RegisterAsync(RegisterViewModel model);
        Task<AuthenticationResultModel> LoginAsync(LoginViewModel model);
    }
}
