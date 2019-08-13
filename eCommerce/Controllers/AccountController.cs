using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly GameContext _context;
        //Never create a new context. 
        //Make it once and then pass it around
        public AccountController(GameContext context)
        {
            _context = context;
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
                bool isMember = await MemberDB.IsLoginValid(_context, model);
                if (isMember)
                {
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
    }
}