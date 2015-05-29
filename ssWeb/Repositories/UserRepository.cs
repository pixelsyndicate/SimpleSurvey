using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace ssWeb.Repositories
{

    public class UserRepository : IUserRepository
    {
        private simpleSurvey1Entities _db = new simpleSurvey1Entities();
        private List<User> _users = new List<User>();
        private User _user;
        private int _nextId = 1;

        public IEnumerable GetAll()
        {
            // TODO : Code to get the list of all the records in database
            using (_db = new simpleSurvey1Entities())
            {

                var userModels = from u in _db.Users
                                 join r in _db.Roles on u.Role equals r.ID
                                 // Response Filled By
                                 select u;

                _users = new List<User>(userModels);

                return userModels;
            }
            return _users;
        }

        public User Get(int id)
        {
            // TO DO : Code to find a record in database
            var all = GetAll();
            _users = new List<User>(all);
            _user = _users.Find(p => p.ID == id);
            return _user;

        }
        public User Add(User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // get current users
            _users = GetAll().ToList();

            // TODO : Code to save record into database
            _nextId = _users.FindLastIndex(x => x.ID >= 1) + 1;
            item.ID = _nextId++;
            using (_db = new simpleSurvey1Entities())
            {
                _db.Users.Add(item);
                _db.Entry(item).State = EntityState.Modified;
                var newId = _db.SaveChanges();
            }
            _users.Add(item);

            return item;
        }

        private int SaveEntity(DbContext db, User item)
        {
            int newId = 0;
            User objUsr;
            using (_db = new simpleSurvey1Entities())
            {
                var usrQuery = from usr in _db.Users
                               where usr.ID == item.ID
                               select usr;

                objUsr = usrQuery.Single();
                objUsr.UserName = item.UserName;
                objUsr.FirstName = item.FirstName;
                objUsr.LastName = item.LastName;
                objUsr.Password = item.Password;
                objUsr.Role = item.Role;

                db.Entry(item).State = EntityState.Modified;
                newId = db.SaveChanges();
            }
            return newId;
        }

        private int AddEntity(DbContext db, User item)
        {
            int newId = 0;
            using (_db = new simpleSurvey1Entities())
            {
                _db.Users.Add(item);
                newId = _db.SaveChanges();
            }
            return newId;
            ;

        }



        public bool Update(User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database

            _users = GetAll() as List<User>;
            int index = _users.FindIndex(p => p.ID == item.ID);
            if (index == -1)
            {
                return false;
            }
            using (_db = new simpleSurvey1Entities())
            {
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
            }

            _users.RemoveAt(index);
            _users.Add(item);
            return true;
        }
        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database

            _users = GetAll() as List<User>;
            int index = _users.FindIndex(p => p.ID == id);
            if (index == -1)
            {
                return false;
            }

            var entityToDelete = Get(id);
            using (_db = new simpleSurvey1Entities())
            {
                _db.Entry(entityToDelete).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            _users.RemoveAll(p => p.ID == id);
            return true;
        }
    }
}