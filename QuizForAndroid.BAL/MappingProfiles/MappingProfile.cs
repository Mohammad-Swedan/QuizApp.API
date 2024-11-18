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
                CreateMap<User, AddUserDTO>().ReverseMap();
                CreateMap<User, GetUserDTO>().ReverseMap();
                CreateMap<User, RegisterDTO>().ReverseMap();


                // For FullQuizDto
                CreateMap<Quiz, FullQuizDTO>()
                    .ForMember(dest => dest.Quiz, opt => opt.MapFrom(src => src))
                    .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

                CreateMap<FullQuizDTO, Quiz>()
                    .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions.Select(q => q.Question)));

                // For FullQuestionDTO
                CreateMap<Question, FullQuestionDTO>()
                    .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src))
                    .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));

                CreateMap<FullQuestionDTO, Question>()
                    .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));


            }
        }
    }

}
