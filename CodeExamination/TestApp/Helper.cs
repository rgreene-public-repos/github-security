using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    internal static class Helper
    {
        /// <summary>
        /// This is an example of bad code as it will fail for negative values.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        internal static bool IsOdd(this int x)
        {
            return x % 2 == 1;
        }
    }
}
