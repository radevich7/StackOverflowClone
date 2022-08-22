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
        ICategoriesService cs;

        public HomeController(IQuestionsService qs, ICategoriesService cs)
        {
            this.qs = qs;
            this.cs = cs;
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
        public ActionResult Categories()
        {
            List<CategoriesViewModel> categories = this.cs.GetAllCategories();
            return View(categories);
        }

        [Route("allquestions")]
        public ActionResult Questions()
        {
            List<QuestionViewModel> questions = this.qs.GetAllQuestions();
            return View(questions);
        }

        public ActionResult Search(string str)
        {
            List<QuestionViewModel> questions = this.qs.GetAllQuestions().Where(temp => temp.QuestionName.ToLower().Contains(str.ToLower()) || temp.Category.CategoryName.ToLower().Contains(str.ToLower())
            ).ToList();

            ViewBag.SearchValue=str;
            return View(questions);
        }
    }
}