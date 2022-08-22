using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.ViewModels;
using StackOverflowClone.ServiceLayer;

namespace StackOverflowClone.Controllers
{
    public class QuestionController : Controller
    {
        IQuestionsService qs;
        ICategoriesService cs;
        IAnswersService ans;

        public QuestionController(IQuestionsService qs)
        {
            this.qs = qs;
        }
        public QuestionController(ICategoriesService cs)
        {
            this.cs = cs;
        }
        public QuestionController(IAnswersService ans)
        {
            this.ans = ans;
        }

        // GET: Question
        public ActionResult View(int id)
        {
            this.qs.UpdateQuestionViewCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.qs.GetQuestionByQuestionID(id, uid);
            return View(qvm);


        }


        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {

            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;



            if (ModelState.IsValid)
            {
                this.ans.InsertAnswer(navm);
                return RedirectToAction("View", "Question", new { id = navm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                QuestionViewModel qvm = this.qs.GetQuestionByQuestionID(navm.QuestionID, navm.UserID);
                return View("View", qvm);
            }


        }
    }
}