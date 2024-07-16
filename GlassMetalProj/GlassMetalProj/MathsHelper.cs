using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

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


        public void e1Calculatation(string type,int tag, int tag2) 
        {
            // if tag is 0 : "le bord libre est le plus grand côté L"
            // tag != 0 : "le bord libre est le plus petit côté l;

            // tag2 is only for the "en appui sur 4 côtés avec maintiens ponctuels sur les hauteurs" case
            // then, tag is for "un maitien ponctuel au milieu du bord libre" when equals to 0
            // or "deux maintiens ponctuels équidistants when not equal to 0"
            // tag2 is then doing the same job as tag for this case.
            switch (type) 
            {
                case "En appui sur toute la périphérie":
                    if (fi.L / fi.l <= 2.5)
                        e1 = Math.Sqrt((fi.Pressure * fi.L * fi.l) / 100);
                    else
                        e1 = (fi.l * Math.Sqrt(fi.Pressure)/6.3);
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
            e1 = Math.Round(e1, 3);
        }

        public bool eRisValid() 
        {
            return eR >= e1 * fi.c;
        }

        public void eRCalculation(int index, int casesforIsolation, string epaisseur) 
        {
            switch (index) 
            {
                //Monolithique
                case 0:
                    double e = 0;
                    if (Double.TryParse(epaisseur, out e))
                    {
                        if (e <= 0)
                        {
                            eR = Math.Round(e / fi.epsilon3, 2);
                        }
                        else
                            MessageBox.Show("l'épaisseur ne peut pas être négative ou nulle");
                    }
                    else
                        MessageBox.Show("Veuillez entrer une valeur correcte");
                    break;
                //Feuilleté
                case 1:
                    double sum = 0;
                    foreach(char c in epaisseur) 
                    {
                        sum += Double.Parse(c.ToString());
                    }
                    eR = sum / 0.9 * fi.epsilon2 * fi.epsilon3;
                    break;
                case 2:
                    //DOUBLE monolithique
                    if (casesforIsolation == 0) 
                    {
                        sum = 0;
                        sum += Double.Parse(epaisseur[0].ToString()) + Double.Parse(epaisseur[1].ToString());
                    }
                    //Un monolithique, un feuilleté
                    if (casesforIsolation == 1) 
                    {
                        sum = 0;
                        sum += Double.Parse(epaisseur[0].ToString());

                    }


                    break;
            }
        }
    }
}
