using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ssWeb.Models
{

    public class SurveyQuestionAnswerViewModel
    {
        public Survey Survey { get; set; }
        public SurveyResponse Response { get; set; }
        public User SurveyCreatedBy { get; set; }
        public User ResponseBy { get; set; }
        public Role UserRole { get; set; }
    }
}