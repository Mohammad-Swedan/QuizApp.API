using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public class QuizLikesDislikesService : GenericServiceAsync<QuizLikesDislike, QuizLikesDislikesDTO>, IQuizLikesDislikesService
    {
        private readonly IQuizLikesDislikesRepository _repository;

        public QuizLikesDislikesService(IQuizLikesDislikesRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
        }

        public async Task<int> GetLikesCountAsync(int quizId)
        {
            return await _repository.GetLikesCountAsync(quizId);
        }

        public async Task<int> GetDislikesCountAsync(int quizId)
        {
            return await _repository.GetDislikesCountAsync(quizId);
        }

        public async Task<QuizLikesDislikesDTO> GetUserLikeDislikeAsync(int userId, int quizId)
        {
            var likeDislike = await _repository.GetUserLikeDislikeAsync(userId, quizId);
            return _mapper.Map<QuizLikesDislikesDTO>(likeDislike);
        }

        public async Task AddLikeOrDislikeAsync(int quizId, int userId, bool likeOrDislike)
        {
            await _repository.AddLikeOrDislikeAsync(quizId, userId, likeOrDislike);
        }

        public async Task<short> GetLikeStatusAsync(int quizId, int userId)
        {
            if (quizId <= 0)
                throw new ArgumentException("Invalid QuizId.", nameof(quizId));

            if (userId <= 0)
                throw new ArgumentException("Invalid UserId.", nameof(userId));

            short status = await _repository.GetLikeStatusAsync(quizId, userId);
            return status;
        }

    }
}
