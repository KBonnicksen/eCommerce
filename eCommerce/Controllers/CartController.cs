using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private const int DaysInWeek = 7;
        private readonly GameContext _context;
        private readonly IHttpContextAccessor _httpAccessor;

        public CartController(GameContext context, IHttpContextAccessor httpAccessor)
        {
            _context = context;
            _httpAccessor = httpAccessor;
        }

        public async Task<IActionResult> Add(int ID)
        {
            //Get the game with the corresponding ID
            VideoGame g = await VideoGameDB.GetGameByID(ID, _context);

            //Convert game to string
            string data = JsonConvert.SerializeObject(g);

            //Set up cookie
            CookieOptions options = new CookieOptions()
            {
                Secure = true,
                MaxAge = TimeSpan.FromDays(2 * DaysInWeek)
            };

            //THIS IS HOW YOU CREATE A COOKIE
            _httpAccessor.HttpContext.Response.Cookies.Append("CartCookie", data, options);

            //Redirect to catalog
            return RedirectToAction("Index", "VideoGame");
        }
    }
}