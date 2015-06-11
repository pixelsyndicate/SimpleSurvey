using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using ssWeb.Repositories;

namespace ssWeb.Controllers
{
    public class QuestionsController : Controller
    {
        //private simpleSurvey1Entities db = new simpleSurvey1Entities();
        private readonly IQuestionRepository _questionRepo;

        public QuestionsController(IQuestionRepository questionRepo)
        {
            _questionRepo = questionRepo;
        }

        // GET: Questions
        public ActionResult Index()
        {
            var questions = (IList<Question>)_questionRepo.GetAll();
            return View(questions);
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = _questionRepo.Get(id.Value); // db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Text,QuestionType,Options")] Question question)
        {
            if (ModelState.IsValid)
            {
                _questionRepo.Add(question);
                // db.Questions.Add(question);
                // db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = _questionRepo.Get(id.Value); // db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Text,QuestionType,Options")] Question question)
        {
            if (ModelState.IsValid)
            {
                _questionRepo.Update(question);
                // db.Entry(question).State = EntityState.Modified;
                // db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = _questionRepo.Get(id.Value); // db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //  Question question = _questionRepo.Get(id);// db.Questions.Find(id);
            _questionRepo.Delete(id);
            // db.Questions.Remove(question);
            //  db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //  db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}