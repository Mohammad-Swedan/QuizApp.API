using AutoMapper;
using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.DAL.Abstracts;
using QuizForAndroid.DAL.DTOs;
using QuizForAndroid.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.Services
{
    public class QuestionService : GenericServiceAsync<Question, QuestionDTO>, IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
            : base(questionRepository, mapper)
        {
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsByQuizAsync(int quizId)
        {
            var questions = await _questionRepository.GetQuestionsByQuizAsync(quizId);
            return _mapper.Map<IEnumerable<QuestionDTO>>(questions);
        }
    }
}
