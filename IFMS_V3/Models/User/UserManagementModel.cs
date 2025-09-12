using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace IFMS_V3.Models.User.UserManagement
{
    public class UsermanagementApiResponse
    {
        public string Status { get; set; }  
        public string Message { get; set; }  
        public UsermanagementData Data { get; set; }
    }

    public class UsermanagementData
    {
        public List<UserModel> UserList { get; set; } 
        public int TotalRecords { get; set; }         
    }

    public class UserModel
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int Id { get; set; } // Unique identifier of the user

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; } // User’s first name

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; } // User’s last name

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } // User’s email (validated as a proper email)

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters.")]
        public string Role { get; set; } // Role assigned to the user (e.g., Admin, Supervisor)        
        public bool Disabled { get; set; } 
        public string updatedByName { get; set; } 

        [Required(ErrorMessage = "CreatedAt date is required.")]
        public DateTime CreatedAt { get; set; } 

    }
}
