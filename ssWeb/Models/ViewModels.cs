using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ssWeb.Repositories;

namespace ssWeb.Models
{

    public class SelectListHelper
    {
        private readonly IUserRepository _userRepo;

        public SelectListHelper(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public MultiSelectList GetRoles(string[] selectedValues)
        {
            IEnumerable<Role> Roles = new List<Role>();

            var userEnum = _userRepo.GetAll() as IEnumerable<User>;
            if (userEnum != null) Roles = userEnum.Select(x => x.Role1);


            //using (_db = new simpleSurvey1Entities())
            //{
            //    var query = from r in _db.Roles select r;
            //    foreach (var role in query)
            //    {
            //        Roles.Add(new Role() { ID = role.ID, Name = role.Name });
            //    }
            //}

            return new MultiSelectList(Roles, "ID", "Name", selectedValues);

        }
    }

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