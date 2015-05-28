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
        private simpleSurvey1Entities db = new simpleSurvey1Entities();
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
            foreach (var obj in surveys)
            {
                obj.User = _userRepo.Get(obj.CreatedBy);
            }
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
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "FirstName");
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,CreatedOn,ExpiresOn,CreatedBy,Publish")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                var thisSurvey = survey;

                // add a user to this survey if it's empty
                if (thisSurvey.CreatedBy == 0)
                {
                    var user = db.Users.FirstOrDefault(x => x.ID == thisSurvey.CreatedBy);

                    if (user != null)
                    {
                        thisSurvey.CreatedBy = user.ID;
                    }
                    else
                    {
                        // add new user to the table
                        ssWeb.User newUser = new User()
                        {
                            
                        };

                        _userRepo.Add(newUser);
                    }
                }
                db.Surveys.Add(thisSurvey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "FirstName", survey.CreatedBy);
            return View(survey);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "FirstName", survey.CreatedBy);
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
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "FirstName", survey.CreatedBy);
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
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
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
