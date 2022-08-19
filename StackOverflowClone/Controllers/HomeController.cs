using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.ServiceLayer;
using StackOverflowClone.ViewModels;


namespace StackOverflowClone.Controllers
{
    public class HomeController : Controller
    {

        IQuestionsService qs;

        public HomeController(IQuestionsService qs)
        {
            this.qs = qs;
        }
        // GET: Home
        public ActionResult Index()
        {
            List<QuestionViewModel> top10Questions = this.qs.GetAllQuestions().Take(10).ToList();
            return View(top10Questions);
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

    }
}