using System;
using System.Collections.Generic;

namespace Range
{
    class Range : Ensemble
    {
        private int LowerBound { get; set; }
        private int UpperBound { get; set; }
        public Range(string input)
        {
            var indexComma = input.IndexOf(',');
            int numbStart = Int32.Parse(input.Substring(1, indexComma - 1));
            LowerBound = input[0] == '[' ? numbStart : numbStart + 1;
            int numbEnd = Int32.Parse(input.Substring(indexComma + 1, (input.Length-1) - (indexComma + 1)));
            UpperBound = input[input.Length - 1] == ']' ? numbEnd : numbEnd - 1;
            var tmp = new List<int>();
            for(int i = LowerBound; i <= UpperBound; i++) { tmp.Add(i);}
            Elements = tmp;
        }
    }
}