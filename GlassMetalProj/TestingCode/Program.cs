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

            fi.L = 3.05;
            fi.l = 0.6;
            string str = "En appui sur toute la périphérie";
            fi.Pressure = 2000;

            Console.WriteLine("L / l is : " + fi.L / fi.l);
            mathsHelper.e1Calculatation(str, -1, -1);
            Console.WriteLine("e1 is : " + mathsHelper.e1);

            fi.L = 2.05;
            fi.l = 1.12;
            fi.Pressure = 1500;
            Console.WriteLine("L / l is : " + fi.L / fi.l);

            mathsHelper.e1Calculatation("En appui sur 2 côtés opposés", 1, -1);
            Console.WriteLine("e1 is : " + mathsHelper.e1);
            Console.ReadKey();
        }
    }
}
