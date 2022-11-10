using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineDiscussionForum.Models;

namespace OnlineDiscussionForum.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IQuestionRepository _questionRepo;
        private readonly IAnswerRepository _answerRepo;
        public UserController(IUserRepository userRepo, IQuestionRepository questionRepo, IAnswerRepository answerRepo)
        {
            _userRepo = userRepo;
            _questionRepo = questionRepo;
            _answerRepo = answerRepo;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("userId") == null)
            {
                return RedirectToAction("Login");
            }
            CollectionDataModel model = new CollectionDataModel(_userRepo);
            model.Questions = _questionRepo.GetAllQuestions();
            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(User user)
        {
            User loginUser = _userRepo.GetUser(user.Name, user.password);
            if(loginUser == null)
            {
                TempData["message"] = "user name Or password is invalid.";
                return View(user);
            }
            HttpContext.Session.SetInt32("userId", loginUser.Id);
            HttpContext.Session.SetString("userName", loginUser.Name);

            return RedirectToAction("details");

        }

        [HttpGet]
        public IActionResult LogOut()
        {
            int id = (int)HttpContext.Session.GetInt32("userId");
            return View();
        }

        [HttpPost, ActionName("LogOut")]
        public IActionResult LogOutUser()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("userName");
            return RedirectToAction("login");
        }

        public IActionResult GetAnswers(int id)
        {
            
            CollectionDataModel model = new CollectionDataModel(_userRepo);
            model.answers = _answerRepo.GetAnswerByQuestion(id);
            TempData["Question"] = _questionRepo.GetQuestion(id).text;
            TempData["id"] = id;
            //var model = _answerRepo.GetAnswerByQuestion(id);
            return View(model);
        }

        public IActionResult Like(int id, int qId)
        {
            Answer answer = _answerRepo.GetAnswer(id);
            answer.noOfLike = answer.noOfLike + 1;
            _answerRepo.update(answer);
            return RedirectToAction("GetAnswers", new { id = qId });
        }

        public IActionResult UnLike(int id, int qId)
        {
            Answer answer = _answerRepo.GetAnswer(id);
            answer.noOfLike = answer.noOfLike - 1;
            _answerRepo.update(answer);
            return RedirectToAction("GetAnswers", new { id = qId });
        }

        public ViewResult Details()
        {
            int id = (int)HttpContext.Session.GetInt32("userId");
            User user = _userRepo.GetUser(id);
            if(user == null)
            {
                Response.StatusCode = 404;
                return View("userNotFound", id);
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (_userRepo.GetUser(user.Name) == null)
                {
                    User newUser = _userRepo.Add(user);
                    return RedirectToAction("details", new { id = newUser.Id });
                }
                else
                {
                    TempData["message"] = "User name is already taken.";
                    user.Name = "";
                    return View(user);
                }
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            User user = _userRepo.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                User userChanges = _userRepo.GetUser(user.Id);
                userChanges.Name = user.Name;
                userChanges.password = user.password;
                userChanges.Email = user.Email;

                User updatedUser = _userRepo.Update(userChanges);

                return RedirectToAction("details");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            User user = _userRepo.GetUser(id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _userRepo.GetUser(id);
            _userRepo.Delete(user.Id);

            return RedirectToAction("index");
        }
    }
}
