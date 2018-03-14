using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerWithAspIdAndEF.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Documento Requerido")]
        [StringLength(15)]
        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Required (ErrorMessage = "Nombres Requeridos")]
        [Display(Name = "Nombres")]
        public string Names { get; set; }

        [Required(ErrorMessage = "Dirección Requerida")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }
    }
}
