using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Helper class to provide session management
    /// </summary>
    public static class SessionHelper
    {
        private const string MemberIDKey = "MemberID";
        private const string UsernameKey = "Username";

        //log in
        //log out
        //check if logged in
        public static void LogUserIn(IHttpContextAccessor context, int memberID, string username)
        {
            context.HttpContext.Session.SetInt32(MemberIDKey, memberID);
            context.HttpContext.Session.SetString(UsernameKey, username);
        }

        /// <summary>
        /// Returns true if the user is logged in
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool isUserLoggedIn(IHttpContextAccessor context)
        {
            if (context.HttpContext.Session.GetInt32(MemberIDKey).HasValue)
                return true;

            return false;
        }

        /// <summary>
        /// Destroys the current users session
        /// </summary>
        /// <param name="context"></param>
        public static void LogUserOut(IHttpContextAccessor context)
        {
            context.HttpContext.Session.Clear();
        }

        /// <summary>
        /// Gets the username of the current user if they are logged in.
        /// Null is returned if the user is not logged in
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserName(IHttpContextAccessor context)
        {
            return context.HttpContext.Session.GetString(UsernameKey);
        }

        /// <summary>
        /// Returns the MemberID of the currently logged in user.
        /// MemberID will be null if they are not logged in.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int? GetMemberID(IHttpContextAccessor context)
        {
            return context.HttpContext.Session.GetInt32(MemberIDKey);
        }
    }
}
