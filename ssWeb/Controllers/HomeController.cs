﻿using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using ssWeb.Models;
using ssWeb.Repositories;

namespace ssWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISurveyRepository _surveyRepo;
        private readonly IUserRepository _userRepo;

        public HomeController(ISurveyRepository surveyRepo, IUserRepository userRepo)
        {
            _surveyRepo = surveyRepo;
            _userRepo = userRepo;
        }

        public ActionResult Index()
        {
          //  IEnumerable data = _surveyRepo.GetAll();
            SurveyManager sm = new SurveyManager(_surveyRepo);
           var data = sm.GetSurveyViewModelBySurveyId(1);
            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}