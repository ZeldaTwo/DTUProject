using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlassMetalProj;

namespace TestingCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            FilledInfos fi = new FilledInfos("", "");
            MathsHelper mathsHelper = new MathsHelper(fi);

            // Starting the Tests; 
            string[] testTokens = { "123.456", "1.2", "9999.0", "12.34a", "abc.def", "12.34.56" };

            foreach (string token in testTokens)
            {
                Console.WriteLine($"Token: {token} - Valid: {MathsHelper.IsTokenValid(token)}");
            }
            Console.ReadKey();
        }
    }
}
