using MoonlightShadow.Models;
using MoonlightShadow.Services;
using MoonlightShadow.ViewModels;
using MoonlightShadow.ViewModels.Account;

namespace MoonlightShadow.ValidModels
{
    public class UserValidModels
    {
        private readonly User _user;
        private readonly UserService _userService;

        public UserValidModels(User user, UserService userService)
        {
            _user = user;
            _userService = userService;
        }

        public bool IsValid()
        {
            return true;
        }

        public bool IsValid(ChangePasswordViewModel changePasswordForm)
        {
            return true;
        }

        public bool IsValid(LoginViewModel loginForm)
        {
            return true;
        }

        public bool IsValid(SignUpViewModel signUpForm)
        {
            return true;
        }

        // private bool IsPasswordValid(ChangePasswordViewModel changePasswordForm)
        // {
        //     if (user.Password.Hash != Hasher.Encrypt(changePasswordForm.Password, user.Password.Salt).hash)
        //     {
        //         ModelState.AddModelError("Password", "Hasło niepoprawne");

        //         return false
        //     }
        // }
    }
}