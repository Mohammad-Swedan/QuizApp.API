using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class QuizDTO
    {
        public int QuizID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int WriterID { get; set; }
        public int MaterialID { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDraft { get; set; }
        public bool IsTrusted { get; set; }
        public bool IsDoctor { get; set; }
    }
}
