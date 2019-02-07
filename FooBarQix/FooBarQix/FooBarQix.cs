using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FooBarQix
{
    public class FooBarQix
    {
        private static readonly Dictionary<int, string> DictFooBarQix = new Dictionary<int, string>
        {
            [0] =  "*",
            [3] = "Foo",
            [5] = "Bar",
            [7] = "Qix"
        };
        public static string Analyze(int input)
        {
            var res = string.Empty;
            res += ResultDivision(input);
            res = input.ToString().Aggregate(res,(current,t)=> current + ResultContains(Int32.Parse(t.ToString())));
            if (Regex.IsMatch(res, @"^[\*]*\*")) res = input.ToString().Replace('0', '*');
            return res == string.Empty? input.ToString():res;
        }

        private static string ResultDivision(int numb)
        {
            var result = string.Empty;
            foreach (var val in DictFooBarQix )
            {
                if (val.Key != 0)
                {
                    if (numb % val.Key == 0) result += val.Value;
                }
            }
            return result;
        }

        private static string ResultContains(int numb)
        {
            var result = string.Empty;
            if (DictFooBarQix.ContainsKey(numb)) result += DictFooBarQix[numb];
            return result;
        }
    }
}