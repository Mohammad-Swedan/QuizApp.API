using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class QuizLikesDislikesDTO
    {
        public int UserID { get; set; }
        public int QuizID { get; set; }
        public bool IsLike { get; set; } // True for Like, False for Dislike
    }
}
