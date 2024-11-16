using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class MaterialDTO
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int CollegeID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
