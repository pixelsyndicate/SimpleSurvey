using System;
using System.Collections.Generic;

namespace ssWeb.Models
{
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