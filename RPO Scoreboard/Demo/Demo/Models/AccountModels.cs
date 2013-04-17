using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class AccountModels
    {
        /// <summary>
        /// Contains user's data like username and real name.
        /// </summary>
        public class UserProfile
        {
            public int ID { get; set; }

            /// <summary>
            /// Username on our site.
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// Display name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The player's data.
            /// </summary>
            public virtual PlayerProfile PlayerProfile { get; set; }
        }
    }
}