﻿using eCommerce.Models;
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
            /* List<VideoGame> games = await (from vidGame in context.VideoGames
                                      orderby vidGame.Title ascending
                                      select vidGame).ToListAsync();*/
            List<VideoGame> games = await context.VideoGames
                                                 .OrderBy(g => g.Title)
                                                 .ToListAsync();
            return games;
        }
    }
}
