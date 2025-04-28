using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    [Table("Users")]
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        public string? UserType { get; set; }

    }
}
