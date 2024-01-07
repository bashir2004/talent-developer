using EmployeeManagement.API.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Models
{
    [Table("Users")]
    public class AuthUser : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(100)]
        [Required]
        public string LastName { get; set; }
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Salt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
