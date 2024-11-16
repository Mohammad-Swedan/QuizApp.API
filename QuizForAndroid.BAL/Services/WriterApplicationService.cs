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
    public class WriterApplicationService : GenericServiceAsync<WriterApplication, WriterApplicationDTO>, IWriterApplicationService
    {
        private readonly IWriterApplicationRepository _repository;

        public WriterApplicationService(IWriterApplicationRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WriterApplicationDTO>> GetPendingApplicationsAsync()
        {
            var applications = await _repository.GetPendingApplicationsAsync();
            return _mapper.Map<IEnumerable<WriterApplicationDTO>>(applications);
        }

        public async Task<IEnumerable<WriterApplicationDTO>> GetApplicationsByUserAsync(string email)
        {
            var applications = await _repository.GetApplicationsByUserEmailAsync(email);
            return _mapper.Map<IEnumerable<WriterApplicationDTO>>(applications);
        }
    }
}
