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
    public class RolesController : Controller
    {

        private readonly IRoleRepository _rolesRepo;
      //  private simpleSurvey1Entities db = new simpleSurvey1Entities();

        public RolesController(IRoleRepository rolesRepo)
        {
            _rolesRepo = rolesRepo;
        }

        // GET: Roles
        public ActionResult Index()
        {
            return View(_rolesRepo.GetAll() as List<Role>);//.Roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _rolesRepo.Get(id.Value);// db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                _rolesRepo.Add(role);
                //  db.Roles.Add(role);
                //  db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _rolesRepo.Get(id.Value);// db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                _rolesRepo.Update(role);
                //  db.Entry(role).State = EntityState.Modified;
                // db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _rolesRepo.Get(id.Value);// db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //   Role role = _rolesRepo.Get(id);// db.Roles.Find(id);
            _rolesRepo.Delete(id);
            // db.Roles.Remove(role);
            // db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //     db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
