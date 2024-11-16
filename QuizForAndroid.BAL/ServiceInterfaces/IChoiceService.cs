using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.ServiceInterfaces
{
    public interface IChoiceService : IGenericServiceAsync<ChoiceDTO>
    {
        Task<IEnumerable<ChoiceDTO>> GetChoicesByQuestionAsync(int questionId);
    }
}
