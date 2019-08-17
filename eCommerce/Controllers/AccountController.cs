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
        //Never create a new context. 
        //Make it once and then pass it around

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
                    TempData["MessageHeader"] = "Logged in successfully.";

                    //Create session for user
                    _httpAccessor.HttpContext.Session.SetInt32("MemberID", member.MemberID);
                    _httpAccessor.HttpContext.Session.SetString("Username", member.Username);


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
    }
}