using System;
using System.Collections.Generic;
using System.Linq;

namespace Range
{
    public class Ensemble
    {
        public List<int> Elements { get; set; }
        public Ensemble(){ Elements = new List<int>(); }
        public override string ToString()
        {
            string ret = "{";
            foreach (var el in Elements){ret += el + ",";}
            ret = ret.Remove(ret.Length - 1, 1);
            ret += "}";
            return ret;
        }

        public Ensemble(string input)
        {
            Elements = new List<int>();
            var str = input;
            str = str.Remove(0,1);
            str = str.Remove(str.Length - 1,1);
            string[] inputNumbs = str.Split(',');
            foreach (var num in inputNumbs)
            {
                Elements.Add(Int32.Parse(num));
            }
        }
        public bool Contains(Ensemble toCompare)
        {
            return Elements.Intersect(toCompare.Elements).Any();
        }
    }
}