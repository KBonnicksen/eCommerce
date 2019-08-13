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
    }
}