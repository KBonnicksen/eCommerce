using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Contains helper methods to manage the users shopping cart
    /// </summary>
    public static class CartHelper
    {
        private const string CartCookie = "Cart";

        /// <summary>
        /// Gets the current users video games fom their shopping cart.
        /// If there are no items in the cart an empty list is returned.
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static List<VideoGame> GetGames(IHttpContextAccessor accessor)
        {
            // Get data out of cookie
            string data = accessor.HttpContext.Request.Cookies[CartCookie];

            if (string.IsNullOrEmpty(data))
                return new List<VideoGame>();

            List<VideoGame> games = JsonConvert.DeserializeObject<List<VideoGame>>(data);

            return games;
        }

        /// <summary>
        /// Gets total number of video games in the cart
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static int GetGameCount(IHttpContextAccessor accessor)
        {
            List<VideoGame> allGames = GetGames(accessor);
            return allGames.Count;
        }

        /// <summary>
        /// Adds video game to the cart.
        /// If no cart cookie exists, it will be created.
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="g">Game being added</param>
        public static void Add(IHttpContextAccessor accessor, VideoGame g)
        {
            List<VideoGame> games = GetGames(accessor);
            games.Add(g);

            // Rewrite the cookie with the new game added
            string data = JsonConvert.SerializeObject(games);
            accessor.HttpContext.Response.Cookies.Append(CartCookie, data);
        }
    }
}
