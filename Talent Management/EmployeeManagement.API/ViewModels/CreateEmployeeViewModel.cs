using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.ViewModels
{
    public class CreateEmployeeViewModel
    {
        public string Username { get; set; }
        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }
        [JsonProperty("phoneNumber")]
        [Required]
        public string PhoneNumber { get; set; }
        [JsonProperty("skillSet")]
        [Required]
        public string SkillSet { get; set; }
        [JsonProperty("hobbies")]
        public string Hobbies { get; set; }
        [JsonProperty("createdBy")]
        public Guid CreatedBy { get; set; }

    }
}
