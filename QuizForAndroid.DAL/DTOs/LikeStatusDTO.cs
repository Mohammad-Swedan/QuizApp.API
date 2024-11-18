using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class LikeStatusDTO
    {
        /// <summary>
        /// The ID of the quiz.
        /// </summary>
        public int QuizId { get; set; }

        /// <summary>
        /// The like status: 1 for like, 0 for dislike, null for neither.
        /// </summary>
        public short? LikeStatus { get; set; }
    }
}
