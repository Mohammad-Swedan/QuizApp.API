using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UniversityID { get; set; }
        public int CollegeID { get; set; }
        public int SpecializationID { get; set; }
        public int RoleID { get; set; } = 1; // default --> 1:User
        public bool IsTrusted { get; set; } = false;
        public bool IsDoctor { get; set; } = false;
        public string PasswordHash { get; set; }
    }
}
