using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class FullQuestionDTO
    {
        /// <summary>
        /// The quiz details.
        /// </summary>
        public QuestionDTO Question { get; set; }

        /// <summary>
        /// The list of questions associated with the quiz.
        /// </summary>
        public List<ChoiceDTO> Choices { get; set; }
    }
}
