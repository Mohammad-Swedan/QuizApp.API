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
    public class CollegeService : GenericServiceAsync<College, CollegeDTO>, ICollegeService
    {
        private readonly ICollegeRepository _collegeRepository;

        public CollegeService(ICollegeRepository collegeRepository, IMapper mapper)
            : base(collegeRepository, mapper)
        {
            _collegeRepository = collegeRepository;
        }

        public async Task<IEnumerable<CollegeDTO>> GetCollegesByUniversityAsync(int universityId)
        {
            var colleges = await _collegeRepository.GetCollegesByUniversityAsync(universityId);
            return _mapper.Map<IEnumerable<CollegeDTO>>(colleges);
        }
    }
}
