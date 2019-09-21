using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using activities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace activities.Controllers
{
    public class HomeController : Controller
    {
        private ActivityContext DbContext;

        public HomeController(ActivityContext context)
        {
            DbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (DbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use! Please log in or use a different email address.");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    DbContext.Add(newUser);
                    DbContext.SaveChanges();
                    HttpContext.Session.SetString("User", newUser.FirstName);
                    HttpContext.Session.SetInt32("UserId", newUser.UserId);
                    System.Console.WriteLine("************* Session User:" + newUser.FirstName + "***************");
                    return RedirectToAction("Home");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login userSubmission)
        {
            if (ModelState.IsValid)
            {
                User userInDb = DbContext.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
                if (userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email and/or Password");
                    return View("Index");
                }
                PasswordHasher<Login> hasher = new PasswordHasher<Login>();

                PasswordVerificationResult result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);

                if (result == 0)
                {
                    ModelState.AddModelError("Password", "Invalid Email and/or Password");
                    return View("Index");
                }
                else
                {
                    HttpContext.Session.SetString("User", userInDb.FirstName);
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return RedirectToAction("Home");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("home")]
        public IActionResult Home()
        {
            int? currentUserId = HttpContext.Session.GetInt32("UserId");
            string currentUserName = HttpContext.Session.GetString("User");
            if (currentUserName == null)
            {
                return RedirectToAction("Index");
            }
            HomeViewModel model = new HomeViewModel()
            {
                User = DbContext.Users.Where(u => u.UserId == (int)currentUserId).FirstOrDefault(),

                Events = DbContext.Events
                    .OrderByDescending(e => e.CreatedAt)
                    .Include(e => e.Attending)
                    .ThenInclude(a => a.User)
                    .ToList()
            };
            return View(model);
        }

        [HttpGet]
        [Route("new")]
        public IActionResult NewActivity()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateEvent(Event newEvent)
        {
            System.Console.WriteLine("******* made it to create method *********");
            string currentUserName = HttpContext.Session.GetString("User");
            int? currentUserId = HttpContext.Session.GetInt32("UserId");
            System.Console.WriteLine("******* adding coordinator ************");
            if (ModelState.IsValid)
            {
                newEvent.Coordinator = currentUserName;
                DbContext.Add(newEvent);
                DbContext.SaveChanges();
                return Redirect($"activity/{newEvent.EventId}");
            }
            return View("NewActivity");
        }

        [HttpGet]
        [Route("activity/{EventId}")]
        public IActionResult EventDetails(int EventId)
        {
            int? currentUserId = HttpContext.Session.GetInt32("UserId");
            EventViewModel model = new EventViewModel()
            {
                Event = DbContext.Events
                .Include(w => w.Attending)
                .ThenInclude(r => r.User)
                .FirstOrDefault(w => w.EventId == EventId),

                User = DbContext.Users.Where(u => u.UserId == (int)currentUserId).FirstOrDefault()
            };
            return View(model);
        }

        [HttpPost]
        [Route("attending")]
        public IActionResult Attending(HomeViewModel model)
        {
            DbContext.Add(model.Attending);
            DbContext.SaveChanges();
            return Redirect($"activity/{model.Attending.EventId}");
        }

        [HttpPost]
        [Route("leaving")]
        public IActionResult Leaving(HomeViewModel model)
        {
            System.Console.WriteLine("*************** AttendingID to be removed:" + model.Attending.UserId + "**************************");
            Attending leaving = DbContext.Attendings.FirstOrDefault(a => a.EventId == model.Attending.EventId && a.UserId == model.Attending.UserId);
            DbContext.Remove(leaving);
            DbContext.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult Remove(HomeViewModel model)
        {
            System.Console.WriteLine("*************** EventId to be removed:" + model.EventToDelete + "**************************");
            Event RetrievedEvent = DbContext.Events.SingleOrDefault(e => e.EventId == model.EventToDelete);
            DbContext.Events.Remove(RetrievedEvent);
            DbContext.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
