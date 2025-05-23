using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastracture.Repositories
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IGenerateToken generateToken;

        public AuthRepository(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken generateToken)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.generateToken = generateToken;
        }
        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            if (registerDTO is null)
            {
                return null;
            }
            if (await userManager.FindByNameAsync(registerDTO.UserName) is not null)
            {
                return "this UserName is already registered";
            }
            if (await userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return "this Email is already registered";
            }
            AppUser user = new AppUser()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                DisplayName=registerDTO.DisplayName

            };
            var result = await userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded is not true)
            {
                return result.Errors.ToList()[0].Description;
            }
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendEmail(user.Email, token, "active", "ActiveEmail", "Please Active your Email,click on button to active ");
            return "Done";
        }
        public async Task SendEmail(string email, string code, string component, string subject, string message)
        {
            var result = new EmailDTO(email, "kameladel1117@gmail.com", subject,
                EmailStringBody.Send(email, code, component, message));
            await emailService.SendEmail(result);
        }
        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            if (loginDTO is null)
            {
                return null;
            }
            var findUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (!findUser.EmailConfirmed)
            {
                string token = await userManager.GenerateEmailConfirmationTokenAsync(findUser);
                await SendEmail(findUser.Email, token, "active", "ActiveEmail", "Please Active your Email,click on button to active ");
                return "Please confirm Your Email First, we have send activat to your E-mail";
            }
            var result = await signInManager.CheckPasswordSignInAsync(findUser, loginDTO.Password, true);
            if (result.Succeeded)
            {
                return generateToken.GetAndCteateToken(findUser);
            }
            return "Please Check your email and password, somthing went wrong";
        }
        public async Task<bool> SendEmailForForgetPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return false;
            }
            var token=await userManager.GeneratePasswordResetTokenAsync(user);
            await SendEmail(user.Email, token, "Reset-Password", "Reset Password", "Click on button to reset your password");
            return true;

        }
        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO) 
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user is null)
            {
                return null;
            }
            var result=await userManager.ResetPasswordAsync(user,resetPasswordDTO.Token, resetPasswordDTO.Password);
            if (result.Succeeded)
            {
                return "Password Change success";
            }
            return result.Errors.ToList()[0].Description;

        }
        public async Task<bool> ActiveAccount(ActiveAccountDTO activeAccountDTO)
        {
            var user=await userManager.FindByEmailAsync(activeAccountDTO.Email);
            if (user is null)
            {
                return false;
            }
            var result=await userManager.ConfirmEmailAsync(user,activeAccountDTO.Token);
            if (result.Succeeded) 
            {
                return true;
            }
            var token=await userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendEmail(user.Email, token, "Reset-Password", "Reset Password", "Click on button to reset your password");
            return false;
        }

    }
}
