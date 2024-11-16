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
    public class MaterialService : GenericServiceAsync<Material, MaterialDTO>, IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository, IMapper mapper)
            : base(materialRepository, mapper)
        {
            _materialRepository = materialRepository;
        }

        public async Task<IEnumerable<MaterialDTO>> GetMaterialsByCollegeAsync(int collegeId)
        {
            var materials = await _materialRepository.GetMaterialsByCollegeAsync(collegeId);
            return _mapper.Map<IEnumerable<MaterialDTO>>(materials);
        }
    }
}
