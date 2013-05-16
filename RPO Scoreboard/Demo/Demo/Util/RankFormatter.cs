using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Util
{
    public class RankFormatter
    {
        public static string Format(int rank)
        {
            int mod10 = rank % 10;
            int mod100 = rank % 100;
            string suffix;
            if (mod100 == 11 || mod100 == 12 || mod100 == 13)
            {
                suffix = "th";
            }
            else
            {
                switch (mod10)
                {
                    case 1:
                        suffix = "st";
                        break;
                    case 2:
                        suffix = "nd";
                        break;
                    case 3:
                        suffix = "rd";
                        break;
                    default:
                        suffix = "th";
                        break;
                }
            }

            return rank + suffix;
        }
    }
}