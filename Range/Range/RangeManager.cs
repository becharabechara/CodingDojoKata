using System;
using System.Collections.Generic;

namespace Range
{
    public class RangeManager
    {
        public static bool VerifyContains(string input)
        {
            string[] strArr = input.Split(' ');
            var listEl = TransformInput(input);
            return strArr.Length == 3 
                ? listEl[0].Contains(listEl[1]) 
                : !listEl[0].Contains(listEl[1]);
        }
        public static List<Ensemble> TransformInput(string input)
        {
            var ret = new List<Ensemble>();
            string[] strArr = input.Split(' ');
            var firstEl = new Range(strArr[0]);
            var secondEl = input[input.Length - 1] == '}'
                ? new Ensemble(strArr[strArr.Length - 1])
                : new Range(strArr[strArr.Length - 1]);
            ret.Add(firstEl);
            ret.Add(secondEl);
            return ret;
        }

        public static string GetEndPoints(string input)
        {
            throw new NotImplementedException();
        }

        public static string GetAllPoints(string input)
        {
            string[] strArr = input.Split(' ');
            var listEl = TransformInput(input);
            return listEl[0].ToString();
        }
    }
}