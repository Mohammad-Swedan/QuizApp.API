using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.DAL.Abstracts;
using QuizForAndroid.DAL.DTOs;
using QuizForAndroid.DAL.Entities;
using QuizForAndroid.DAL.Repositories;
using QuizForAndroid.DAL.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChoiceRepository _choiceRepository;
        private readonly IQuizLikesDislikesRepository _quizLikesDislikesRepository;

        public QuizService(IQuizRepository quizRepository,IQuizLikesDislikesRepository quizLikesDislikesRepository ,IChoiceRepository choiceRepository ,IUnitOfWork unitOfWork ,IMapper mapper)
            : base(quizRepository, mapper)
        {
            _choiceRepository = choiceRepository;
            _unitOfWork = unitOfWork;
            _quizRepository = quizRepository;
            _quizLikesDislikesRepository = quizLikesDislikesRepository;
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

        public async Task<FullQuizDTO> AddFullQuizAsync(FullQuizDTO model)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Map and add the quiz
                var quizEntity = _mapper.Map<Quiz>(model.Quiz);
                await _unitOfWork.Quizzes.AddAsync(quizEntity);
                await _unitOfWork.SaveChangesAsync();

                // Prepare the list of questions
                var questionEntities = new List<Question>();

                foreach (var fullQuestionDto in model.Questions)
                {
                    // Map and add the question
                    var questionEntity = _mapper.Map<Question>(fullQuestionDto.Question);
                    questionEntity.QuizId = quizEntity.QuizId; // Associate question with the quiz

                    await _unitOfWork.Questions.AddAsync(questionEntity);
                    await _unitOfWork.SaveChangesAsync();

                    // Map and add the choices
                    foreach (var choiceDto in fullQuestionDto.Choices)
                    {
                        var choiceEntity = _mapper.Map<Choice>(choiceDto);
                        choiceEntity.QuestionId = questionEntity.QuestionId; // Associate choice with the question

                        await _unitOfWork.Choices.AddAsync(choiceEntity);
                    }

                    await _unitOfWork.SaveChangesAsync();

                    // Optionally load choices into the question entity | make it on choice repo
                    //questionEntity.Choices =
                    //    await _choiceRepository.GetChoicesByQuestionAsync()//do it later
                        
                        

                    questionEntities.Add(questionEntity);
                }

                await _unitOfWork.CommitTransactionAsync();

                // Prepare the result DTO
                var result = new FullQuizDTO
                {
                    Quiz = _mapper.Map<QuizDTO>(quizEntity),
                    LikeStatus = model.LikeStatus,
                    Questions = questionEntities.Select(q => new FullQuestionDTO
                    {
                        Question = _mapper.Map<QuestionDTO>(q),
                        Choices = q.Choices.Select(c => _mapper.Map<ChoiceDTO>(c)).ToList()
                    }).ToList()
                };

                return result;
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<FullQuizDTO> GetFullQuizByIdAsync(int quizId,int userId)
        {
            // Get quiz include Questions and Choices
            var quizEntity = await _quizRepository.GetFull(quizId);

            if (quizEntity == null)
                return null;

            short likeStatus = await _quizLikesDislikesRepository
                    .GetLikeStatusAsync(quizEntity.QuizId,userId);

            var result = new FullQuizDTO
            {
                Quiz = _mapper.Map<QuizDTO>(quizEntity),
                LikeStatus = likeStatus, // Default value; adjust based on user action if needed
                Questions = quizEntity.Questions.Select(q => new FullQuestionDTO
                {
                    Question = _mapper.Map<QuestionDTO>(q),
                    Choices = q.Choices.Select(c => _mapper.Map<ChoiceDTO>(c)).ToList()
                }).ToList()
            };

            return result;
        }



        
    }
}
