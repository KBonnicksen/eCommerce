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
    }
}
