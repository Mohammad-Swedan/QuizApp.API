using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class QuestionDTO
    {
        public int QuestionID { get; set; }
        public int QuizID { get; set; }
        public string QuestionText { get; set; }
        public int QuestionType { get; set; } // 1: Single Answer, 2: True or False, 3: Multiple Answer, 4: Short Answer
    }
}
