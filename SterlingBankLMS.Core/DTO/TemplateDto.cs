﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class TemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public int SurveyCount { get; set; }
    }
}
