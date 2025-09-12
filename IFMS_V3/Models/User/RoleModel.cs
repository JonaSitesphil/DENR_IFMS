using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IFMS_V3.Models.User.Roles
{
    public class RoleModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role name is required.")]
        [MaxLength(50, ErrorMessage = "Role name cannot exceed 50 characters.")]
        [MinLength(3, ErrorMessage = "Role name must be at least 3 characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9][a-zA-Z0-9\s\-]*[a-zA-Z0-9]$",
            ErrorMessage = "Role name can only contain letters, numbers, spaces, and dashes, and must start and end with a letter or number.")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("disabled")]
        public bool Disabled { get; set; }
    }
}
