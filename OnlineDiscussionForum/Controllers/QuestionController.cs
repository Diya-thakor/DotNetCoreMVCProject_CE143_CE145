using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineDiscussionForum.Models;
using System.Collections;
using System.Collections.Generic;

namespace OnlineDiscussionForum.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly IAnswerRepository _answerRepo;
        private readonly IUserRepository _userRepo;
        public QuestionController(IQuestionRepository questionRepo, IAnswerRepository answerRepo, IUserRepository userRepo)
        {
            _questionRepo = questionRepo;
            _answerRepo = answerRepo;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");
            CollectionDataModel model = new CollectionDataModel(_userRepo);
            model.Questions = _questionRepo.GetQuestionsByUser(userId);
            model.answers = _answerRepo.GetAllAnswer();
            return View(model);
        }
        public ViewResult Details(int id)
        {
            QuestionHistory question = _questionRepo.GetQuestion(id);
            if (question == null)
            {
                TempData["message"] = "Question is not present";
                return View();
            }
            return View(question);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(QuestionHistory question)
        {
            QuestionHistory newQuestion = _questionRepo.Add(question, (int)HttpContext.Session.GetInt32("userId"));
            return RedirectToAction("details", new { id = newQuestion.Id });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            QuestionHistory question = _questionRepo.GetQuestion(id);
            return View(question);
        }
        [HttpPost]
        public IActionResult Edit(QuestionHistory question)
        {
            QuestionHistory question1 = _questionRepo.GetQuestion(question.Id);
            question1.text = question.text;
            QuestionHistory updatedQuestion = _questionRepo.Update(question1);
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            QuestionHistory question = _questionRepo.GetQuestion(id);
            _questionRepo.Delete(question.Id);
            return RedirectToAction("index");

        }
        [HttpDelete, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var question = _questionRepo.GetQuestion(id);
            _questionRepo.Delete(id);
            return RedirectToAction("index");
        }
    }
}
