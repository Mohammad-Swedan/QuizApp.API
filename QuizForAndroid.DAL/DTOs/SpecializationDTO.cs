﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class SpecializationDTO
    {
        public int SpecializationID { get; set; }
        public string SpecializationName { get; set; }
        public int CollegeID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}