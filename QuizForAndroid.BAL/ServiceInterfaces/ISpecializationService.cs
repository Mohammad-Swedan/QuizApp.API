using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.ServiceInterfaces
{
    public interface ISpecializationService : IGenericServiceAsync<SpecializationDTO>
    {
        Task<IEnumerable<SpecializationDTO>> GetSpecializationsByCollegeAsync(int collegeId);
    }
}
