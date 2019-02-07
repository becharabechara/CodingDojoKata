using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BankOCRKata
{
    public class BankOCRTransformer
    {
        private static List<char> charDigital = new List<char>()
        {
            ' ','_','|'
        };

        private static readonly Dictionary<string, string> dict_digital =
            new Dictionary<string, string>()
            {
                {" _ "+
                 "| |"+
                 "|_|", "0"},
                {"   "+
                 "  |"+
                 "  |", "1"},
                {" _ "+
                 " _|"+
                 "|_ ", "2"},
                {" _ "+
                 " _|"+
                 " _|", "3"},
                {"   "+
                 "|_|"+
                 "  |", "4"},
                {" _ "+
                 "|_ "+
                 " _|", "5"},
                {" _ "+
                 "|_ "+
                 "|_|", "6"},
                {" _ "+
                 "  |"+
                 "  |", "7"},
                {" _ "+
                 "|_|"+
                 "|_|", "8"},
                {" _ "+
                 "|_|"+
                 " _|", "9"}
            };
        internal static string Transform(string str)
        {
            var res = "";
            var digital = "";
            var number = "";
            for (int i = 0; i < 9; i++)
            {
                digital = str.Substring(i * 3, 3) + str.Substring(i * 3 + 27, 3) + str.Substring(i * 3 + 27 + 27, 3);
                if (dict_digital.TryGetValue(digital, out number))
                    res += number;
                else
                    res += "?";
            }

            return res;
        }

        internal static int CalculSum(string str)
        {
            var ret = 0;
            var b = 0;
            var res = Transform(str);
            for(int i = 0; i < 9; i++)
            {
                //bool success =  Int32.TryParse(res[i].ToString(),out b);
                Int32.TryParse(res[i].ToString(), out b);
                //if (success)
                //{
                ret += b * (9 - i);
                //}
            }
            return ret;
        }

        public static string WriteToFile(string str)
        {
            var ret = "";
            bool isLegal = false;
            var output = 0;
            var res = Transform(str);
            for (int i =0; i < 9; i++)
            {
                isLegal = Int32.TryParse(res[i].ToString(), out output);
                if (!isLegal)
                {
                    var possCombList = GetPossibleCombsIndexed(str, i);
                    if(possCombList.Count != 0)
                    {
                        ret = Transform(possCombList[0]);
                        File.AppendAllText(@"./BankOCR.txt", ret + "\n");
                        return ret;
                    }
                    break;
                }
            }
            if(isLegal)
            {
                if(CalculSum(str) % 11 == 0)
                {
                    ret = (res);
                    File.AppendAllText(@"./BankOCR.txt", ret + "\n");
                    return ret;
                }
                else
                {
                    //THIS IS ERR
                    var possCombList = GetPossibleCombs(str);
                    if (possCombList.Count != 0)
                    {
                        ret += res + " AMB ['";
                        for (var t = 0; t < possCombList.Count - 1; t++)
                        {
                            ret += Transform(possCombList[t]) + "', '";
                        }
                        ret += Transform(possCombList[possCombList.Count - 1]) + "']";
                        File.AppendAllText(@"./BankOCR.txt", ret + "\n");
                        return ret;
                    }
                    else
                    {
                        ret = (res + " ERR");
                        File.AppendAllText(@"./BankOCR.txt", ret + "\n");
                        return ret;
                    }
                }
            }
            File.AppendAllText(@"./BankOCR.txt", ret+"\n");
            return ret;
        }

        private static List<string> ChangeOneDigit(string str, int n, string digital)
        {
            var res = new List<string>();
            for (int j = 0; j < 9; j++)
            {
                foreach (var c in charDigital)
                {
                    char[] letters = digital.ToCharArray();
                    letters[j] = c;
                    string tmp = string.Join("", letters);
                    if (dict_digital.ContainsKey(tmp))
                    {
                        string newString = str;
                        char[] newLetters = str.ToCharArray();
                        for (int k = n * 3; k < (n * 3 + 3); k++)
                        {
                            newLetters[k] = tmp[k - (n * 3)];
                        }
                        for (int k = (n * 3 + 27); k < ((n * 3 + 27) + 3); k++)
                        {
                            newLetters[k] = tmp[k - (n * 3 + 27) + 3];
                        }
                        for (int k = (n * 3 + 27 + 27); k < ((n * 3 + 27 + 27) + 3); k++)
                        {
                            newLetters[k] = tmp[k - (n * 3 + 27 + 27) + 3 + 3];
                        }
                        if (CalculSum(string.Join("", newLetters)) % 11 == 0) res.Add(string.Join("", newLetters));
                    }
                }
            }
            return res;
        }

        private static List<string> GetPossibleCombsIndexed(string str, int n)
        {
            var res = new List<string>();
            string digital = str.Substring(n * 3, 3) + str.Substring(n * 3 + 27, 3) + str.Substring(n * 3 + 27 + 27, 3);
            //var tmp = digital;
            return ChangeOneDigit(str, n, digital);
        }

        private static List<string> GetPossibleCombs(string str)
        {
            var res = new List<string>();
            string digital = "";
            for (int i = 0; i < 9; i++)
            {
                digital = str.Substring(i * 3, 3) + str.Substring(i * 3 + 27, 3) + str.Substring(i * 3 + 27 + 27, 3);
                //var tmp = digital;
                res.AddRange(ChangeOneDigit(str, i, digital));
            }
            return res;
        }
    }
}
