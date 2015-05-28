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
        private readonly IRoleRepository _roleRepo;

        public SelectListHelper(IUserRepository userRepo, IRoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public MultiSelectList GetRoles(string[] selectedValues)
        {
            //using (_db = new simpleSurvey1Entities())
            //{
            //    var query = from r in _db.Roles select r;
            //    foreach (var role in query)
            //    {
            //        Roles.Add(new Role() { ID = role.ID, Name = role.Name });
            //    }
            //}
            var roles = _roleRepo.GetAll() as List<Role>;
            return new MultiSelectList(roles, "ID", "Name", selectedValues);

        }

        public object GetRoles(List<Role> possibleRoles, string[] selectedValues)
        {
            return new MultiSelectList(possibleRoles, "ID", "Name", selectedValues);
        }
    }




}