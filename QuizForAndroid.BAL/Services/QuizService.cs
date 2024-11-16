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
    public class QuizService : GenericServiceAsync<Quiz, QuizDTO>, IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository, IMapper mapper)
            : base(quizRepository, mapper)
        {
            _quizRepository = quizRepository;
        }

        public async Task<IEnumerable<QuizDTO>> GetQuizzesByMaterialAsync(int materialId)
        {
            var quizzes = await _quizRepository.GetQuizzesByMaterialAsync(materialId);
            return _mapper.Map<IEnumerable<QuizDTO>>(quizzes);
        }

        public async Task<IEnumerable<QuizDTO>> GetQuizzesByWriterAsync(int writerId)
        {
            var quizzes = await _quizRepository.GetQuizzesByWriterAsync(writerId);
            return _mapper.Map<IEnumerable<QuizDTO>>(quizzes);
        }

        //public async Task<IEnumerable<QuizDTO>> GetTopQuizzesAsync(int materialId, int count)
        //{
        //    var quizzes = await _quizRepository.GetTopQuizzesAsync(materialId, count);
        //    return _mapper.Map<IEnumerable<QuizDTO>>(quizzes);
        //}

        public async Task<IEnumerable<QuizDTO>> GetQuizzesByStatusAsync(bool isDraft)
        {
            var quizzes = await _quizRepository.GetQuizzesByStatusAsync(isDraft);
            return _mapper.Map<IEnumerable<QuizDTO>>(quizzes);
        }
    }
}
