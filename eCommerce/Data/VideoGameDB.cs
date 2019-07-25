using eCommerce.Models;
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
        public static VideoGame Add(VideoGame game, GameContext context)
        {
            // NEEDS THE CONTEXT PASSED IN AS WELL
            context.Add(game);
            context.SaveChanges();
            return game;
        }
    }
}
