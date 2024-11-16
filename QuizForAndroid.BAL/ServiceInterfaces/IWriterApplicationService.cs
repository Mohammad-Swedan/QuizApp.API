using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.ServiceInterfaces
{
    public interface IWriterApplicationService : IGenericServiceAsync<WriterApplicationDTO>
    {
        Task<IEnumerable<WriterApplicationDTO>> GetPendingApplicationsAsync();
        Task<IEnumerable<WriterApplicationDTO>> GetApplicationsByUserAsync(string email);
    }
}
