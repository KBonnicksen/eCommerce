using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    /// <summary>
    /// DB helper class for VideoGames
    /// </summary>
    public static class VideoGameDB
    {
        /// <summary>
        /// Returns one page worth of products. Products are sorted 
        /// alphabetically by Title
        /// </summary>
        /// <param name="context">the db context</param>
        /// <param name="pageNum">The page number for the products</param>
        /// <param name="pageSize">The number of products per page</param>
        /// <returns></returns>
        public static async Task<List<VideoGame>> 
            GetGamesByPage(GameContext context, int pageNum, int pageSize)
        {
            List<VideoGame> games = await context.VideoGames
                                                 .OrderBy(vg => vg.Title)
                                                 .Skip((pageNum - 1) * pageSize)
                                                 .Take(pageSize)
                                                 .ToListAsync();
            return games;
        }

        /// <summary>
        /// Returns the total number of pages needed to have 
        /// <paramref name="pageSize"/> amount of products per page
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<int> GetTotalPages(GameContext context, int pageSize)
        {
            int totalNumGames = await context.VideoGames.CountAsync();
            double pages = (double) totalNumGames / pageSize;
            return (int) Math.Ceiling(pages);

        }

        /// <summary>
        /// Adds a video game to the data store. Sets the ID value
        /// </summary>
        /// <param name="game">The game to add</param>
        /// <param name="context">The context used</param>
        public static async Task<VideoGame> AddAsync(VideoGame game, GameContext context)
        {
            // NEEDS THE CONTEXT PASSED IN AS WELL
            // must add await keyword
            await context.AddAsync(game);
            await context.SaveChangesAsync();
            return game;
        }

        /// <summary>
        /// Retrieves all games sorted in alphabetical order by title
        /// </summary>
        /// <param name="context"></param>
        public static async Task<List<VideoGame>> GetAllGames(GameContext context)
        {
            List<VideoGame> games = await context.VideoGames
                                                 .OrderBy(g => g.Title)
                                                 .ToListAsync();
            return games;
        }

        /// <summary>
        /// Gets a game with a specified ID. If no game is found null is returned
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        public static async Task<VideoGame> GetGameByID(int id, GameContext context)
        {   //Single or default async grabs a single entity from the database. If more than one item
            //is pulled an exception is thrown
            VideoGame g = await (context.VideoGames.Where(m => m.ID == id)).SingleOrDefaultAsync();
            return g;
        }

        //Task<whateverYourReturnTypeIs>
        public static async Task<VideoGame> UpdateGame(VideoGame g, GameContext context)
        {
            context.Update(g);
            await context.SaveChangesAsync();
            return g;
        }

        public static async Task DeleteByID(int ID, GameContext context)
        {
            // Create video game object with the id of the game that we want to remove from the database
            VideoGame g = new VideoGame()
            {
                ID = ID
            };
            context.Entry(g).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
