using EmployeeManagement.API.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Models
{
    [Table("Employees")]
    public class Employee : IEntity
    {
        public Guid Id { get; set; }
        [Range(5, 20, ErrorMessage = "Username length must be between {0} and {1} characters.")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [MinLength(5, ErrorMessage = "The email length must be greater then {0} characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Skill set is required")]
        public string SkillSet { get; set; }
        public string Hobbies { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
