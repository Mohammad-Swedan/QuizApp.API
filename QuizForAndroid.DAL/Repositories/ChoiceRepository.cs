﻿using Microsoft.EntityFrameworkCore;
using QuizForAndroid.DAL.Abstracts;
using QuizForAndroid.DAL.Contexts;
using QuizForAndroid.DAL.Entities;
using QuizForAndroid.DAL.GenericBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.Repositories
{
    public class ChoiceRepository : GenericRepositoryAsync<Choice>, IChoiceRepository
    {
        private readonly DbSet<Choice> _choices;

        public ChoiceRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            _choices = dbContext.Set<Choice>();
        }

        public async Task<IEnumerable<Choice>> GetChoicesByQuestionAsync(int questionId)
        {
            return await _choices.AsNoTracking()
                .Where(c => c.QuestionId == questionId)
                .ToListAsync();
        }

    }
}
