using System;
using System.Collections.Generic;
using System.Linq;

namespace PotterKata
{
    public class PotterKataManager
    {
        private static int[] CountBook(List<int> bookNumb)
        {
            int[] res = new int[5];
            foreach (var i in bookNumb)
                res[i]++;

            return res;
        }

        private static decimal GetMin(params Decimal [] numbers)
        {
            return numbers.Min();
        }

        private static decimal FindSolution(int x0, int x1, int x2, int x3, int x4)
        {
            List<int> list = new List<int>() { x0, x1, x2, x3, x4 };
            list.Sort();
            x0 = list[0];
            x1 = list[1];
            x2 = list[2];
            x3 = list[3];
            x4 = list[4];
            if (x0 > 0)
                return GetMin(8 + FindSolution(x0, x1, x2, x3, x4 - 1),
                    2 * 8 * 0.95m + FindSolution(x0, x1, x2, x3 - 1, x4 - 1),
                    3 * 8 * 0.9m + FindSolution(x0, x1, x2 - 1, x3 - 1, x4 - 1),
                    4 * 8 * 0.8m + FindSolution(x0, x1 - 1, x2 - 1, x3 - 1, x4 - 1),
                    5 * 8 * 0.75m + FindSolution(x0 - 1, x1 - 1, x2 - 1, x3 - 1, x4 - 1));
            else if (x1 > 0)
                return GetMin(8 + FindSolution(x0, x1, x2, x3, x4 - 1),
                    2 * 8 * 0.95m + FindSolution(x0, x1, x2, x3 - 1, x4 - 1),
                    3 * 8 * 0.9m + FindSolution(x0, x1, x2 - 1, x3 - 1, x4 - 1),
                    4 * 8 * 0.8m + FindSolution(x0, x1 - 1, x2 - 1, x3 - 1, x4 - 1));
            else if (x2 > 0)
                return GetMin(8 + FindSolution(x0, x1, x2, x3, x4 - 1),
                    2 * 8 * 0.95m + FindSolution(x0, x1, x2, x3 - 1, x4 - 1),
                    3 * 8 * 0.9m + FindSolution(x0, x1, x2 - 1, x3 - 1, x4 - 1));
            else if (x3 > 0)
                return GetMin(8 + FindSolution(x0, x1, x2, x3, x4 - 1),
                    2 * 8 * 0.95m + FindSolution(x0, x1, x2, x3 - 1, x4 - 1));
            else if (x4 > 0)
                return (8 + FindSolution(x0, x1, x2, x3, x4 - 1));
            else
                return 0;
        }

        public static Decimal CalculPrix(List<int> bookNumb)
        {
            var arrBook = CountBook(bookNumb);
            return FindSolution(arrBook[0], arrBook[1], arrBook[2], arrBook[3], arrBook[4]);
        }
    }
}