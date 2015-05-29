using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ssWeb;
using ssWeb.Repositories;

namespace ssWeb.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRoleRepository _roleRepo;
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo, IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
            _userRepo = userRepo;
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = _userRepo.GetAll(); // db.Users.Include(u => u.Role1);
            foreach (var user in users)
            {
                user.Role1 = _roleRepo.Get(user.Role);
            }
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userRepo.Get(id.Value);// _userRepo.GetAll().FirstOrDefault(x => x.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {

            ViewBag.Role = new SelectList(_roleRepo.GetAll() as List<Role>, "ID", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,UserName,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _userRepo.Add(user);
                //db.Users.Add(user);
                // db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role = new SelectList(_roleRepo.GetAll() as List<Role>, "ID", "Name", user.Role);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = _userRepo.GetAll();// db.Users.Include(u => u.Role1);
            foreach (var u in users)
            {
                u.Role1 = _roleRepo.Get(u.Role);
            }

            var user = users.FirstOrDefault(x => x.ID == id);
            var role = user.Role1;
            if (user == null)
            {
                return HttpNotFound();
            }

            var roles = _roleRepo.GetAll() as List<Role>;
            var selectListHelper = new ssWeb.Models.SelectListHelper(_userRepo, _roleRepo);
            ViewBag.MultiSelectListRoles = selectListHelper.GetRoles(roles, new[] { user.Role.ToString() });
            ViewBag.Role = new SelectList(roles, "ID", "Name", user.Role);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,UserName,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _userRepo.Update(user);
                //db.Entry(user).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role = new SelectList(_roleRepo.GetAll(), "ID", "Name", user.Role);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userRepo.Get(id.Value);// db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // User user = _userRepo.Get(id);// db.Users.Find(id);

            _userRepo.Delete(id);

            //db.Users.Remove(user);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
