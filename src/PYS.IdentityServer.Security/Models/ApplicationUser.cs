using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerWithAspIdAndEF.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(15)]
        public string Document { get; set; }

        [Required]
        public string Names { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
