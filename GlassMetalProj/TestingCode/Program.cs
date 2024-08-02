using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlzEx.Theming;
using GlassMetalProj;

namespace TestingCode
{
    public class Program
    {
        static List<string> glassStandards = new List<string>()
        {
            "Vitrage recuit NF EN 572-2",
            "Vitrage recuit armé NF EN 572-3",
            "Vitrage étiré NF EN 572-4",
            "Vitrage imprimé NF EN 572-5",
            "Vitrage imprimé armé NF EN 572-6",
            "Vitrage trempé NF EN 12150 ou NF EN 14179",
            "Vitrage émaillé trempé NF EN 12150",
            "Vitrage imprimé trempé NF EN 12150",
            "Vitrage durci NF EN 1863",
            "Vitrage borosilicate NF EN 1748-1",
            "Vitrage borosilicate trempé NF EN 13024",
            "Vitrage émaillé durci NF EN 1863",
            "Vitrage alcalino-terreux recuit NF EN 1748-1",
            "Vitrage alcalino-terreux trempé NF EN 14321",
            "Vitrocéramique NF EN 1748-2",
            "Vitrage trempé chimique NF EN 12337",
            "Vitrage recuit dépoli acide industriellement",
            "Vitrage recuit dépoli par sablage",
            "Vitrage recuit dépoli par grenaillage",
            "Vitrage gravé"
        };
        static List<string> glassFeuilleté = new List<string>()
        {
            "Vitrage feuilleté de sécurité à deux composants",
            "Vitrage feuilleté de sécurité à trois composants",
            "Vitrage feuilleté de sécurité à quatre composants",
            "Vitrage feuilleté à deux composants",
            "Vitrage feuilleté à trois composants",
        };

        public static FilledInfos fi;

        static void Main(string[] args)
        {
            fi = new FilledInfos("", "");
            MathsHelper mathsHelper = new MathsHelper(fi);

            //// Starting the Tests; 
            //string[] testTokens = { "123.456", "1.2", "9999.0", "12.34a", "abc.def", "12.34.56" };

            //foreach (string token in testTokens)
            //{
            //    Console.WriteLine($"Token: {token} - Valid: {MathsHelper.IsTokenValid(token)}");
            //}

            //fi.L = 3.05;
            //fi.l = 0.6;
            //string str = "En appui sur toute la périphérie";
            //fi.Pressure = 2000;

            //Console.WriteLine("L / l is : " + fi.L / fi.l);
            //mathsHelper.e1Calculatation(str, -1, -1);
            //Console.WriteLine("e1 is : " + mathsHelper.e1);

            //fi.L = 2.05;
            //fi.l = 1.12;
            //fi.Pressure = 1500;
            //Console.WriteLine("L / l is : " + fi.L / fi.l);

            //mathsHelper.e1Calculatation("En appui sur 2 côtés opposés", 1, -1);
            //Console.WriteLine("e1 is : " + mathsHelper.e1);
            //Console.ReadKey();


            //TEST FOR eR Calculation
            //Monolithique :
            double thickness = 5;
            int indexEpsilon = 0;
            double résultat = 5;
            mathsHelper.eRCalculation(0, thickness, null, indexEpsilon, null, null);

            Console.Write("Monolithique - " + "épaisseur : " + thickness + " || type du vitrage : " + glassStandards[indexEpsilon] + " || Résultat - [");
            if (mathsHelper.eR == résultat)
                Console.Write("True]");
            else Console.Write("False]");

            Console.WriteLine("\n");
            Console.WriteLine("---------------------------------------------------------------------------\n");

            //Feuilleté :
            double[] thicknesses = new double[4] { 8, 8, 0, 0 };
            int[] indexEpsilons = new int[4] { 5, 5, 0, 0 };
            int[] indexEpsilons2 = new int[3] { 0, 0, 0 };
            résultat = 22.4;

            mathsHelper.eRCalculation(1, 0, thicknesses, -1, indexEpsilons, indexEpsilons2);
            Console.WriteLine("Feuilleté : ");
            int i = 0;
            while (i < 4 && thicknesses[i] != 0)
            {
                Console.WriteLine("épaisseur" + i + " : " + thicknesses[i] + " || type du vitrage : " + glassStandards[indexEpsilons[i]]);
                i++;
            }
            Console.WriteLine("Type de Feuilleté : " + glassFeuilleté[indexEpsilons2[0]]);
            Console.Write("Résultat : [");
            if (mathsHelper.eR == résultat)
                Console.Write("True]");
            else Console.Write("False] || eR is : " + mathsHelper.eR);


            Console.WriteLine("\n");
            Console.WriteLine("---------------------------------------------------------------------------\n");

            //Isolant 2 faces : double Monolithiques
            fi.GlazingTypeMonolithiqueIndex = 0;
            fi.thicknessformonolithiques = 0;
            résultat = 0;

            thickness = 0;
            indexEpsilon = 0;
            mathsHelper.eRCalculation(2, thickness, null, indexEpsilon, null, null);

            Console.WriteLine("Isolant 2 faces : 2M");
            Console.WriteLine("épaisseur0 : " + fi.thicknessformonolithiques + " || + type du vitrage : " + glassStandards[fi.GlazingTypeMonolithiqueIndex]);
            Console.WriteLine("épaisseur1 : " + thickness + " || + type du vitrage : " + glassStandards[indexEpsilon]);

            Console.WriteLine("\n--------------------------------------------------------------------------------------------------------");

            //-------------------------------------------------------------------------------------------------------------------------------------------------
            //Test alpha for fleche -- Type 0
            double[] ltest = new double[12] { 1, 0.92, 0.67, 1.03, 1.02, 0.65, 0.2, 0.48, 1.28, 4.2,0.72,2.7 };
            double[] Ltest = new double[12] { 2.9, 2.4, 2.8, 3.87, 1.14, 1.567, 1.2, 0.7, 3.21, 5.4, 1.58, 4.53};

            for(int j = 0; j < ltest.Length; j++) 
            {
                ChangelandL(ltest[j], Ltest[j]);
                Console.WriteLine();
                Console.WriteLine("l / L :" + fi.l / fi.L);
                Console.WriteLine("The value of alpha is : " + fi.findAlpha(0,0));
            }
            Console.WriteLine("\n-----------------------------Test for Type 1 with b = l------------------------------------");
            //Test alpha for fleche -- Type 1
            //Test when b is l 
            for (int j = 0; j < ltest.Length; j++) 
            {
                ChangelandL(ltest[j], Ltest[j]);
                Console.WriteLine();
                Console.WriteLine("L / b :" + fi.L / fi.l);
                Console.WriteLine("The value of alpha is : " + fi.findAlpha(1, fi.l));

            }
            Console.WriteLine("\n-----------------------------Test for Type 1 with b = L------------------------------------");


            //Test alpha for fleche -- Type 1
            //Test when b is L 
            for (int j = 0; j < ltest.Length; j++)
            {
                ChangelandL(ltest[j], Ltest[j]);
                Console.WriteLine();
                Console.WriteLine("L / b :" + fi.l / fi.L);
                Console.WriteLine("The value of alpha is : " + fi.findAlpha(1, fi.L));

            }

            Console.ReadKey();

        }
        public static void ChangelandL(double newl,double newL) 
        {
            fi.l = newl; 
            fi.L = newL;
        }
    }
}
