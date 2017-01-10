using System;
using System.Text;

namespace RandomClass
{
   static public class passRandom
    {
        static public string RanPass(byte length)
        {
            string inst = "0123456789abcdef";
            //Hexadecimal symbols

            Random Ran = new Random();

            StringBuilder SB = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                SB.Append(inst[Ran.Next(inst.Length)]);
            }

            return SB.ToString();
        }
    }
}
