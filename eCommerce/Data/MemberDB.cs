using eCommerce.Data;
using eCommerce.Models;
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
    }
}
