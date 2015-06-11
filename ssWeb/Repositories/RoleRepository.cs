using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ssWeb.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private simpleSurvey1Entities _db = new simpleSurvey1Entities();
        private IQueryable<Role> _queryRoles;
        private List<Role> _roles = new List<Role>();
        private readonly User _role;
        private int _nextId = 1;

        public IEnumerable GetAll()
        {

            // TODO : Code to get the list of all the records in database

            // test get values from db
            using (_db = new simpleSurvey1Entities())
            {
                _db.Configuration.ProxyCreationEnabled = false;
                _queryRoles = from u in _db.Roles
                              // Response Filled By
                              select u;


                _roles = new List<Role>(_queryRoles);
            }
            _nextId = _roles.LastOrDefault().ID + 1;
            return _roles;
        }

        public Role Add(Role item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.ID = _nextId++;
            // TO DO : Code to save record into database
            using (_db = new simpleSurvey1Entities())
            {
                _db.Roles.Add(item);
                _db.SaveChanges();
            }

            _roles.Add(item);
            return item;
        }


        public Role Get(int id)
        {
            // TO DO : Code to find a record in database
            _roles = GetAll() as List<Role>;
            return _roles.FirstOrDefault(p => p.ID == id);//.Find(p => p.ID == id);
        }

        public bool Update(Role item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database
            _roles = GetAll() as List<Role>;
            int index = _roles.FindIndex(p => p.ID == item.ID);
            if (index == -1)
            {
                return false;
            }

            using (_db = new simpleSurvey1Entities())
            {
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
            }

            _roles.RemoveAt(index);
            _roles.Add(item);
            return true;
        }
        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database
            _roles = GetAll() as List<Role>;
            int index = _roles.FindIndex(p => p.ID == id);
            if (index == -1)
            {
                return false;
            }
            var currentRole = _roles.FirstOrDefault(x => x.ID == id);
            using (_db = new simpleSurvey1Entities())
            {
                _db.Roles.Remove(currentRole);//.Entry(item).State = EntityState.Deleted;
                _db.SaveChanges();
            }

            _roles.RemoveAll(p => p.ID == id);
            return true;
        }
    }
}