using System;
using System.Collections.Generic;

namespace ssWeb.Models
{
    public class SurveyViewModel : ISurveyViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public int CreatedBy { get; set; }
        public bool Publish { get; set; }
        public ICollection<Survey_Questions> Survey_Questions { get; set; }
        public ICollection<SurveyResponse> SurveyResponses { get; set; }
        public User User { get; set; }
    }
}