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
    public class UniversityService : GenericServiceAsync<University, UniversityDTO>, IUniversityService
    {
        public UniversityService(IUniversityRepository universityRepository, IMapper mapper)
            : base(universityRepository, mapper)
        {
        }

    }
}
