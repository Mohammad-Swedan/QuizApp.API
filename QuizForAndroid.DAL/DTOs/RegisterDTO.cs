using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; }
        [Required]
        public int UniversityId { get; set; }
        [Required]
        public int CollegeId { get; set; }
        [Required]
        public int SpecializationId { get; set; }
        [Required]
        public int RoleId { get; set; } = 1; // default : User

        public bool isTrusted { get; set; } = false;
        public bool isDoctor { get; set; } = false;

    }
}
