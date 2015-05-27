using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ssWeb
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

    public interface ISurveyViewModel
    {
        int ID { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        System.DateTime CreatedOn { get; set; }
        DateTime? ExpiresOn { get; set; }
        int CreatedBy { get; set; }
        bool Publish { get; set; }

        ICollection<Survey_Questions> Survey_Questions { get; set; }
        ICollection<SurveyResponse> SurveyResponses { get; set; }
        User User { get; set; }
    }
}