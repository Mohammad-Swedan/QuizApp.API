using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using QuizForAndroid.DAL.DTOs;
using QuizForAndroid.DAL.Entities;

namespace QuizForAndroid.BLL.MappingProfiles
{


    namespace eCampus_Prototype.BLL
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<User, UserDTO>().ReverseMap();
                CreateMap<Role, RoleDTO>().ReverseMap();
                CreateMap<University, UniversityDTO>().ReverseMap();
                CreateMap<College, CollegeDTO>().ReverseMap();
                CreateMap<Specialization, SpecializationDTO>().ReverseMap();
                CreateMap<Material, MaterialDTO>().ReverseMap();
                CreateMap<Quiz, QuizDTO>().ReverseMap();
                CreateMap<Question, QuestionDTO>().ReverseMap();
                CreateMap<Choice, ChoiceDTO>().ReverseMap();
                CreateMap<QuizLikesDislike, QuizLikesDislikesDTO>().ReverseMap();
                CreateMap<WriterApplication, WriterApplicationDTO>().ReverseMap();

                // Additional mappings
            }
        }
    }

}
