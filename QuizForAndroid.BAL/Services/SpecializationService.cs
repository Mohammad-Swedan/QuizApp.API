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
    public class SpecializationService : GenericServiceAsync<Specialization, SpecializationDTO>, ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationService(ISpecializationRepository specializationRepository, IMapper mapper)
            : base(specializationRepository, mapper)
        {
            _specializationRepository = specializationRepository;
        }

        public async Task<IEnumerable<SpecializationDTO>> GetSpecializationsByCollegeAsync(int collegeId)
        {
            var specializations = await _specializationRepository.GetSpecializationsByCollegeAsync(collegeId);
            return _mapper.Map<IEnumerable<SpecializationDTO>>(specializations);
        }
    }
}
