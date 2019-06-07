using InformationApp.Models;
using InformationApp.Models.AccountViewModel;
using InformationApp.Models.AuthViewModel;
using InformationApp.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InformationApp.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtSettings jwtSettings
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResultModel> RegisterAsync(RegisterViewModel model)
        {
            ApplicationUser signedUser = await _userManager.FindByEmailAsync(model.Email);

            if (signedUser.Email != null)
            {
                return new AuthenticationResultModel
                {
                    Errors = new[] { "User with this email Exist" }
                };
            }

            var user = new ApplicationUser { Name = model.Name, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new AuthenticationResultModel
                {
                    Errors = result.Errors.Select(x => x.Description)
                };
            }

            return GenerateAuthResultForUser(user);

        }


        public async Task<AuthenticationResultModel> LoginAsync(LoginViewModel model)
        {
           ApplicationUser signedUser = await _userManager.FindByEmailAsync(model.Email);

            if (signedUser.Email == null)
            {
                return new AuthenticationResultModel
                {
                    Errors = new[] { "User does not Exist" }
                };
            }

            var result = await _userManager.CheckPasswordAsync(signedUser, model.Password);

            if (!result)
            {
                return new AuthenticationResultModel
                {
                    Errors = new[] { "user or Password is wrong" }
                };
            }

            return GenerateAuthResultForUser(signedUser);

        }


        private AuthenticationResultModel GenerateAuthResultForUser(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: new[]
                {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Email),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email),
                    new Claim(type: "id", value: user.ID),
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), algorithm: SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResultModel
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }

}