using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class VideoGameController : Controller
    {
        // Readonly makes it so that only the constructor can modify its value
        private readonly GameContext context;
        public VideoGameController(GameContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VideoGame> allGames = await VideoGameDB.GetAllGames(context);
            return View(allGames);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(VideoGame game)
        {
            if (ModelState.IsValid)
            {
                // Add to Database (await must be called before you call an async method)
                await VideoGameDB.AddAsync(game, context);

                return RedirectToAction("Index");
            };

            // So that all of the errors that you encounter are sent with the view
            return View(game);
        }
    }
}