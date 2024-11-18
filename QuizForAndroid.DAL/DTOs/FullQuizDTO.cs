using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class FullQuizDTO
    {
        /// <summary>
        /// The quiz details.
        /// </summary>
        public QuizDTO Quiz { get; set; }

        /// <summary>
        ///  The Status of the quiz like / dislike based on user action
        /// </summary>
        public short LikeStatus { get; set; } = 0; // 0 : normal (not liked or disliked) / 1 : liked / 2 :disliked

        /// <summary>
        /// The list of questions associated with the quiz.
        /// </summary>
        public List<FullQuestionDTO> Questions { get; set; }
    }

}
