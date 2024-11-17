using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class EditUserDTO
    {
        /// <summary>
        /// New first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// New last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// New email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Old password for verification when changing password.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// New password to set.
        /// </summary>
        public string NewPassword { get; set; }
    }

}
