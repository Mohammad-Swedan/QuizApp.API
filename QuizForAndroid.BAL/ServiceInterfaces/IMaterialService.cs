using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.ServiceInterfaces
{
    public interface IMaterialService : IGenericServiceAsync<MaterialDTO>
    {
        Task<IEnumerable<MaterialDTO>> GetMaterialsByCollegeAsync(int collegeId);
    }
}
