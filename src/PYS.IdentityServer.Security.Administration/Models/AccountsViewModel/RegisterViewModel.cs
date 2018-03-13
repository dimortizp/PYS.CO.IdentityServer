using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspIdAndEF.Models.AccountsViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 7)]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        public string Names { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string Address { get; set; }
    }
}
