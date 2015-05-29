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
    public class SurveysController : Controller
    {
        private readonly ISurveyRepository _surveyRepo;
        private readonly IUserRepository _userRepo;

        public SurveysController(ISurveyRepository surveyRepo, IUserRepository userRepo)
        {
            _surveyRepo = surveyRepo;
            _userRepo = userRepo;
        }

        // GET: Surveys
        public ActionResult Index()
        {
            // get surveys from repo
            var surveys = _surveyRepo.GetAll() as List<Survey>;
            //foreach (var obj in surveys)
            //{
            //    obj.User = _userRepo.Get(obj.CreatedBy);
            //}
            // var surveysWithUsers = db.Surveys.Include(s => s.User);
            return View(surveys);
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var surveys = _surveyRepo.GetAll() as List<Survey>;
            foreach (var obj in surveys)
            {
                obj.User = _userRepo.Get(obj.CreatedBy);
            }
            Survey survey = surveys.FirstOrDefault(x => x.ID == id);// db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {

            List<User> users = _userRepo.GetAll() as List<User>;
            ViewBag.CreatedBy = new SelectList(users, "ID", "FirstName");
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,CreatedOn,ExpiresOn,CreatedBy,Publish")] Survey survey)
        {
            var allUsers = _userRepo.GetAll().ToList();
            if (ModelState.IsValid)
            {
                var thisSurvey = survey;

                var newUser = new User();
                // add a user to this survey if it's empty
                if (thisSurvey.CreatedBy == 0)
                {

                    var user = _userRepo.Get(thisSurvey.ID);// db.Users.FirstOrDefault(x => x.ID == thisSurvey.CreatedBy);

                    if (user == null)
                    {
                        User nu = Services.Helpers.TranslateIdentityToUserRecord(User.Identity.Name);
                        user = _userRepo.Add(nu);
                    }

                    thisSurvey.CreatedBy = user.ID;

                    allUsers.Add(user);
                    _surveyRepo.Add(thisSurvey);

                }

                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(allUsers, "ID", "FirstName", survey.CreatedBy);
            return View(survey);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            var allUsers = _userRepo.GetAll().ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = _surveyRepo.Get(id.Value);// db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(allUsers, "ID", "FirstName", survey.CreatedBy);
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,CreatedOn,ExpiresOn,CreatedBy,Publish")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                _surveyRepo.Update(survey);
                // db.Entry(survey).State = EntityState.Modified;
                // db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(_userRepo.GetAll().ToList(), "ID", "FirstName", survey.CreatedBy);
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = _surveyRepo.Get(id.Value);// db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = _surveyRepo.Get(id);//db.Surveys.Find(id);
            _surveyRepo.Delete(survey.ID);
            // db.Surveys.Remove(survey);
            // db.SaveChanges();
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
