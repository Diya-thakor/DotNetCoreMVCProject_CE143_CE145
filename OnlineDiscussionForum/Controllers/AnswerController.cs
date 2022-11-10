using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineDiscussionForum.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OnlineDiscussionForum.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerRepository _answerRepo;
        private readonly IQuestionRepository _questionRepo;
        private readonly IUserRepository _userRepo;
        public AnswerController(IAnswerRepository answerRepo, IQuestionRepository questionRepo, IUserRepository userRepo)
        {
            _answerRepo = answerRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");
            IEnumerable<Answer> answerList = _answerRepo.GetAllAnswer();
            return View(answerList);
        }

        public IActionResult answerIndex()
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");
            CollectionDataModel model = new CollectionDataModel(_userRepo);
            model.answers = _answerRepo.getAnswerByUser(userId);
            model.Questions = _questionRepo.GetAllQuestions();
            return View(model);
        }

        public ViewResult Details(int Id)
        {
            var model = _answerRepo.GetAnswer(Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Answer answer)
        {
            answer.UserId = (int)HttpContext.Session.GetInt32("userId");
            Answer newAnswer = _answerRepo.Add(answer);
            return RedirectToAction("index", "User");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Answer answer = _answerRepo.GetAnswer(Id);
            return View(answer);
        }

        [HttpPost]
        public IActionResult Edit(Answer answerChanges)
        {
            Answer answer = _answerRepo.GetAnswer(answerChanges.Id);
            answer.text = answerChanges.text;
            _answerRepo.update(answer);
            return RedirectToAction("Index", new {id = answer.QuestionId});
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Answer answer = _answerRepo.GetAnswer(id);
            return View(answer);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Answer answer = _answerRepo.GetAnswer(id);
            _answerRepo.delete(id);
            return RedirectToAction("answerIndex");
        }

        [HttpGet, ActionName("markStatus")]
        public IActionResult MarkStatus(int id)
        {
            Answer answer = _answerRepo.GetAnswer(id);
            answer.status = "accepted";
            _answerRepo.update(answer);
            return RedirectToAction("Index","Question");
        }
    }
}
