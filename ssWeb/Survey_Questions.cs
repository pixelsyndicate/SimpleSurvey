//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ssWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Survey_Questions
    {
        public int ID { get; set; }
        public int SurveyID { get; set; }
        public int QuestionID { get; set; }
        public Nullable<int> OrderId { get; set; }
    
        public virtual Question Question { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
