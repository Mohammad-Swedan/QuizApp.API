using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizForAndroid.DAL.GenericBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BAL.GenericBase
{
    public class GenericServiceAsync<TEntity, TDto> : IGenericServiceAsync<TDto>
        where TEntity : class
        where TDto : class
    {
        protected readonly IGenericRepositoryAsync<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericServiceAsync(IGenericRepositoryAsync<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetTableNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found.");

            await _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();
        }
    }
}
