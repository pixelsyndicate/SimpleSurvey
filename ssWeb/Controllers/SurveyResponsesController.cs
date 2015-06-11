using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ssWeb;

namespace ssWeb.Controllers
{
    public class SurveyResponsesController : Controller
    {
        private simpleSurvey1Entities db = new simpleSurvey1Entities();

        // GET: SurveyResponses
        public ActionResult Index()
        {
            var surveyResponses = db.SurveyResponses.Include(s => s.Question).Include(s => s.Survey).Include(s => s.User);
            return View(surveyResponses.ToList());
        }

        // GET: SurveyResponses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyResponse surveyResponse = db.SurveyResponses.Find(id);
            if (surveyResponse == null)
            {
                return HttpNotFound();
            }
            return View(surveyResponse);
        }

        // GET: SurveyResponses/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text");
            ViewBag.SurveyID = new SelectList(db.Surveys, "ID", "Title");
            ViewBag.FilledBy = new SelectList(db.Users, "ID", "FirstName");
            return View();
        }

        // POST: SurveyResponses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SurveyID,QuestionID,Response,FilledBy")] SurveyResponse surveyResponse)
        {
            if (ModelState.IsValid)
            {
                db.SurveyResponses.Add(surveyResponse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", surveyResponse.QuestionID);
            ViewBag.SurveyID = new SelectList(db.Surveys, "ID", "Title", surveyResponse.SurveyID);
            ViewBag.FilledBy = new SelectList(db.Users, "ID", "FirstName", surveyResponse.FilledBy);
            return View(surveyResponse);
        }

        // GET: SurveyResponses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyResponse surveyResponse = db.SurveyResponses.Find(id);
            if (surveyResponse == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", surveyResponse.QuestionID);
            ViewBag.SurveyID = new SelectList(db.Surveys, "ID", "Title", surveyResponse.SurveyID);
            ViewBag.FilledBy = new SelectList(db.Users, "ID", "FirstName", surveyResponse.FilledBy);
            return View(surveyResponse);
        }

        // POST: SurveyResponses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SurveyID,QuestionID,Response,FilledBy")] SurveyResponse surveyResponse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyResponse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", surveyResponse.QuestionID);
            ViewBag.SurveyID = new SelectList(db.Surveys, "ID", "Title", surveyResponse.SurveyID);
            ViewBag.FilledBy = new SelectList(db.Users, "ID", "FirstName", surveyResponse.FilledBy);
            return View(surveyResponse);
        }

        // GET: SurveyResponses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyResponse surveyResponse = db.SurveyResponses.Find(id);
            if (surveyResponse == null)
            {
                return HttpNotFound();
            }
            return View(surveyResponse);
        }

        // POST: SurveyResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SurveyResponse surveyResponse = db.SurveyResponses.Find(id);
            db.SurveyResponses.Remove(surveyResponse);
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
