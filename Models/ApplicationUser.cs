using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Online__Smart_Learning_System.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [StringLength(100)]
        [MaxLength(50)]
        public string? Name {  get; set; }
        public string? Address {  get; set; }
        public string? Role {  get; set; }

    }
}
