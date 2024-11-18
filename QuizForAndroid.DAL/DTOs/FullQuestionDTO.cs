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
        public QuestionDTO Quiz { get; set; }

        /// <summary>
        /// The list of questions associated with the quiz.
        /// </summary>
        public List<ChoiceDTO> choices { get; set; }
    }
}
