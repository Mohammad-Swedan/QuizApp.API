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
    public class ChoiceService : GenericServiceAsync<Choice, ChoiceDTO>, IChoiceService
    {
        private readonly IChoiceRepository _choiceRepository;

        public ChoiceService(IChoiceRepository choiceRepository, IMapper mapper)
            : base(choiceRepository, mapper)
        {
            _choiceRepository = choiceRepository;
        }

        public async Task<IEnumerable<ChoiceDTO>> GetChoicesByQuestionAsync(int questionId)
        {
            var choices = await _choiceRepository.GetChoicesByQuestionAsync(questionId);
            return _mapper.Map<IEnumerable<ChoiceDTO>>(choices);
        }
    }
}
