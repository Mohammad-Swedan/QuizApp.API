﻿using QuizForAndroid.DAL.Entities;
using QuizForAndroid.DAL.GenericBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.Abstracts
{
    public interface ICollegeRepository : IGenericRepositoryAsync<College>
    {
        Task<IEnumerable<College>> GetCollegesByUniversityAsync(int universityId);
    }
}
