using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.ViewModels
{
    public class EmployeeViewModel
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }
        [JsonProperty("username")]
        [Required]
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
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updatedBy")]
        public Guid? UpdatedBy { get; set; }
        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}
