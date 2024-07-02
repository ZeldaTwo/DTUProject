using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GlassMetalProj
{
    public class MathsHelper
    {
        public double e1;

        public double eR;
        public double eF;

        FilledInfos fi;

        public MathsHelper(FilledInfos filledInfos) 
        {
            fi = filledInfos;
        }

        public static bool IsTokenValid(string token)
        {
            // Regex to match the pattern (nn.X)
            string pattern = @"^(\d+)\.(\d+)$";
            Match match = Regex.Match(token, pattern);

            // Check if the pattern matches
            if (!match.Success)
            {
                return false;
            }

            // Extracting the parts of the string
            string nPart = match.Groups[1].Value;
            string xPart = match.Groups[2].Value;

            // Validate that all n's and X are integers
            if (IsAllDigits(nPart) && IsAllDigits(xPart))
            {
                return true;
            }

            return false;
        }


        // Helper method to check if a string contains only digits
        private static bool IsAllDigits(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public void e1Calculatation(string type,int tag, int tag2) 
        {
            // if tag is 0 : "le bord libre est le plus grand côté L"
            // tag != 0 : "le bord libre est le plus petit côté l;

            // tag2 is only for the 4th case
            // then, tag is for "un maitien ponctuel au milieu du bord libre" 
            // or "deux maintiens ponctuels équidistants"
            // tag2 is then doing the same job as tag for this case.
            switch (type) 
            {
                case "En appui sur toute la périphérie":
                    if (fi.L / fi.l <= 2.5)
                        e1 = Math.Sqrt((fi.Pressure * fi.L * fi.l) / 100);
                    else
                        e1 = (fi.l / Math.Sqrt(fi.Pressure)/6.3);
                    break;

                case "En appui sur 3 côtés":
                    if (tag == 0)
                    {
                        if (fi.L / fi.l <= 7.5)
                        {
                            e1 = Math.Sqrt((fi.L * 3 * fi.l * fi.Pressure) / 100);
                        }
                        else
                        {
                            e1 = (3 * fi.l * Math.Sqrt(fi.Pressure)) / 6.3;
                        }
                    }
                    else
                        e1 = (fi.l * Math.Sqrt(fi.Pressure)) / 6.3;
                    break;

                case "En appui sur 2 côtés opposés":
                    if (tag == 0)
                        e1 = (fi.L * Math.Sqrt(fi.Pressure)) / 6.3;
                    else
                        e1 = (fi.l * Math.Sqrt(fi.Pressure)) / 6.3;
                    break;

                case "En appui sur 2 côtés opposés avec maintien(s) ponctuel(s) sur les hauteurs":
                    if (tag == 0) 
                    {
                        if (tag2 == 0)
                            e1 = ((fi.L * Math.Sqrt(fi.Pressure)) / 6.3) * 0.625;
                        else
                            e1 = ((fi.l * Math.Sqrt(fi.Pressure)) / 6.3) * 0.625;
                    }
                    else
                    {
                        if (tag2 == 0)
                            e1 = ((fi.L * Math.Sqrt(fi.Pressure)) / 6.3) * 0.588;
                        else
                            e1 = ((fi.l * Math.Sqrt(fi.Pressure)) / 6.3) * 0.588;
                    }
                    break;

                default:
                    throw new Exception("Error in the code, the string is not passed");

            }
        }

        public bool eRisValid() 
        {
            return eR >= e1 * fi.c;
        }

        public void eRCalculation(string type,string epaisseur) 
        {
            switch (type) 
            {
                case "Vitrage simple monolithique":
                    break;
                case "Vitrage simple feuilleté":
                    if (IsTokenValid(epaisseur)) 
                    {

                    }
                    break;
            }
        }
    }
}
