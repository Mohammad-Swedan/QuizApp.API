using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class CollegeDTO
    {
        public int CollegeID { get; set; }
        public string CollegeName { get; set; }
        public int UniversityID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
