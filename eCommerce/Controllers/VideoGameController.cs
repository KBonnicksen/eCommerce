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

        [HttpGet] //ID is the page number coming in
        public async Task<IActionResult> Index(int? ID)
        {
            //Set to ID. If ID is null, set to 1
            int page = ID ?? 1;
            const int PageSize = 3;
            List<VideoGame> games = await VideoGameDB.GetGamesByPage(context, page, PageSize);

            int totalPages = await VideoGameDB.GetTotalPages(context, PageSize);
            ViewData["Pages"] = totalPages;
            ViewData["CurrentPage"] = page;
            return View(games);
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

        public async Task<IActionResult> Update(int id)
        {
            VideoGame game = await VideoGameDB.GetGameByID(id, context);
            return View(game);
        }

        [HttpPost] //After they fill out the form and submit it
        public async Task<IActionResult> Update(VideoGame g)
        {
            if (ModelState.IsValid)
            {
                await VideoGameDB.UpdateGame(g, context);
                return RedirectToAction("Index"); //Sends the user back to the video game index page
            }

            //If there are any errors, show the user the form again
            return View(g);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int ID)
        {
            VideoGame game = await VideoGameDB.GetGameByID(ID, context);

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int ID)
        {
            await VideoGameDB.DeleteByID(ID, context);
            return RedirectToAction("Index");
        }
    }
}