using eCommerce.Data;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce
{

    public static class MemberDB
    {
        /// <summary>
        /// Adds a member to the database. Returns the member with their memberID
        /// </summary>
        /// <param name="context">The database context used</param>
        /// <param name="m">The member being added to the databaase</param>
        /// <returns></returns>
        public async static Task<Member> Add(GameContext context, Member m)
        {
            context.Members.Add(m);
            await context.SaveChangesAsync();
            return m;
        }

        /// <summary>
        /// Checks if credentials are found in the database.
        /// The matching member is returned for valid credentials. Null is
        /// returned if there are no matches.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async static Task<Member> IsLoginValid(GameContext context, LoginViewModel model)
        {
            // Returns true if the member logging in is in the database
            // False if there is no record of user
            return await (from m in context.Members
                    where( m.Username == model.UserNameOrEmail
                        || m.EmailAddress == model.UserNameOrEmail)
                        && m.Password == model.Password
                    select m).SingleOrDefaultAsync();
        }
    }
}
