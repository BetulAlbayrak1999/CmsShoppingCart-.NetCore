using Entity.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ViewModels.UserViewModels
{
    public class UpdateUserVM
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password), MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Password { get; set; }

        public UpdateUserVM() { }

        public UpdateUserVM(AppUser appUser)
        {
            Email = appUser.Email;
            Password = appUser.PasswordHash;
        }
    }
}
