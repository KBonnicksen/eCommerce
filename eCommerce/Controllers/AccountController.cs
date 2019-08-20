using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Provides access to session data for the current user.
        /// </summary>
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly GameContext _context;

        public AccountController(GameContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _httpAccessor = accessor;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Member m)
        {
            await MemberDB.Add(_context, m);
            SessionHelper.LogUserIn(_httpAccessor, m.MemberID, m.Username);

            TempData["Message"] = "The registration thing that you just did was a HUGE success. Go you.";
            TempData["MessageHeader"] = "You're registered!";

            //How you redirect to another page!!
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = await MemberDB.IsLoginValid(_context, model);
                if (member != null)
                {
                    SessionHelper.LogUserIn(_httpAccessor, member.MemberID, member.Username);
                    TempData["MessageHeader"] = "Logged in successfully.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Adding model error with no key will display error message in the validation summary
                    ModelState.AddModelError(string.Empty, "I'm sorry, your credentials did not match anything in our database");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            SessionHelper.LogUserOut(_httpAccessor);
            TempData["Message"] = "You have been logged out";
            return RedirectToAction("Index", "Home");
        }
    }
}