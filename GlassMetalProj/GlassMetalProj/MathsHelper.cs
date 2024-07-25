using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace GlassMetalProj
{
    public class MathsHelper
    {
        public double e1;

        public double eR = 0;
        public double eF = 0;



        FilledInfos fi;

        public MathsHelper(FilledInfos filledInfos)
        {
            fi = filledInfos;
        }


        public void e1Calculatation(string type, int tag, int tag2)
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
                        e1 = (fi.l * Math.Sqrt(fi.Pressure) / 6.3);
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

        public void eRCalculation(int GlazingTypeIndex, double thickness, double[] thicknesses, int indexEpsilon, int[] indexEpsilons, int[] indexEpsilon2) 
        {
            eR = 0;
            switch (GlazingTypeIndex)
            {
                //Monolithique
                case 0:
                    double epsilon3 = fi.equivalencefactor3[indexEpsilon];
                    eR = thickness / epsilon3;
                    if (eR < thickness)
                        eR = thickness;
                    break;
                //Feuilleté
                case 1:
                    double epsilon2 = 0;
                    epsilon3 = 0;
                    //Find epsilon2
                    epsilon2 = fi.equivalencefactor2[indexEpsilon2[0]];
                    //Find MAX epsilon3
                    int i = 0;
                    while (i < thicknesses.Length && thicknesses[i] > 0) 
                    {
                        eR += thicknesses[i];
                        if (epsilon3 < fi.equivalencefactor3[indexEpsilons[i]])
                            epsilon3 = fi.equivalencefactor3[indexEpsilons[i]];
                        i++;
                    }
                    eR /= (0.9 * epsilon2 * epsilon3);
                    //Check if eR is not less than an other thickness
                    foreach(double d in thicknesses) 
                    {
                        if (eR < d)
                            eR = d;
                    }
                    break;
                //Isolant 2 faces
                case 2:
                    //Case where we have double Monolithique
                    if (thicknesses == null)
                    {
                        //Get the max of Epsilon3
                        epsilon3 = fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex];
                        double epsilon3v2 = fi.equivalencefactor3[indexEpsilon];
                        if (epsilon3 < epsilon3v2)
                            epsilon3 = epsilon3v2;
                        eR = (thickness + fi.thicknessformonolithiques) / (0.9 * fi.equivalencefactor1[0] * epsilon3);
                        if (eR < thickness)
                            eR = thickness;
                        if (eR < fi.thicknessformonolithiques)
                            eR = fi.thicknessformonolithiques;
                    }
                    //Case where we have Monolithique + feuilleté
                    else if (thicknesses != null && fi.GlazingTypeMonolithiqueIndex > -1)
                    {
                        epsilon2 = fi.equivalencefactor2[indexEpsilon2[0]];
                        epsilon3 = 0;
                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            eR += thicknesses[i];
                            if (epsilon3 < fi.equivalencefactor3[indexEpsilons[i]])
                                epsilon3 = fi.equivalencefactor3[indexEpsilons[i]];
                            i++;
                        }
                        if (epsilon3 < fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex])
                            epsilon3 = fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex];
                        eR /= 0.9 * epsilon2;
                        eR += fi.thicknessformonolithiques;

                        eR /= 0.9 * fi.equivalencefactor1[0] * epsilon3;

                        //check if eR is less than the other thicknesses 
                        if (eR < fi.thicknessformonolithiques)
                            eR = fi.thicknessformonolithiques;

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0) 
                        {
                            if (eR < thicknesses[i])
                                eR = thicknesses[i];
                            i++;
                        }

                    }
                    //Case where we have double Feuilleté 
                    else
                    {
                        double first_epsilon2 = fi.equivalencefactor2[indexEpsilon2[0]];
                        epsilon2 = fi.equivalencefactor2[indexEpsilon2[1]];
                        //We divide the calculation in 2 parts, with each feuilleté being calculated
                        double first_part_of_eR = 0;
                        epsilon3 = 0;
                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete.Length && fi.thicknessfromeachLayerfeuillete[i] > 0)
                        {
                            first_part_of_eR += fi.thicknessfromeachLayerfeuillete[i];
                            if (epsilon3 < fi.equivalencefactor3[fi.GlazingTypeFromLayersIndexes[i]])
                                epsilon3 = fi.equivalencefactor3[fi.GlazingTypeFromLayersIndexes[i]];
                            i++;
                        }
                        first_part_of_eR /= 0.9 * first_epsilon2;
                        //2nd part
                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            eR += thicknesses[i];
                            if (epsilon3 < fi.equivalencefactor3[indexEpsilons[i]])
                                epsilon3 = fi.equivalencefactor3[indexEpsilons[i]];
                            i++;
                        }
                        eR /= 0.9 * epsilon2;
                        eR += first_part_of_eR;
                        eR /= 0.9 * fi.equivalencefactor1[0] * epsilon3;

                        //check if er is less than the thicknesses
                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete.Length && fi.thicknessfromeachLayerfeuillete[i] > 0) 
                        {
                            if (eR < fi.thicknessfromeachLayerfeuillete[i])
                                eR = fi.thicknessfromeachLayerfeuillete[i];
                            i++;
                        }

                        i= 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0) 
                        {
                            if (eR < thicknesses[i])
                                eR = thicknesses[i];
                            i++;
                        }

                    }
                    break;
                //Isolant 3 faces
                case 3:
                    //Triple Monolithiques
                    if (thicknesses == null)
                    {
                        eR += fi.thicknessformonolithiques + fi.thicknessformonolithiques2 + thickness;
                        epsilon3 = fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex];
                        if (epsilon3 < fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex2])
                            epsilon3 = fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex2];
                        if (epsilon3 < fi.equivalencefactor3[indexEpsilon])
                            epsilon3 = fi.equivalencefactor3[indexEpsilon];

                        eR /= 0.9 * fi.equivalencefactor1[1] * epsilon3;

                        //TODO - check if eR is less than the thicknesses
                        if (eR < fi.thicknessformonolithiques)
                            eR = fi.thicknessformonolithiques;
                        if (eR < fi.thicknessformonolithiques2)
                            eR = fi.thicknessformonolithiques2;
                        if (eR < thickness)
                            eR = thickness;
                    }
                    //Double Monolithique + Feuilleté
                    else if (thicknesses != null && fi.GlazingTypeMonolithiqueIndex > -1 && fi.GlazingTypeMonolithiqueIndex2 > -1) 
                    {
                        epsilon2 = fi.equivalencefactor2[indexEpsilon2[0]];
                        eR += fi.thicknessformonolithiques + fi.thicknessformonolithiques2;

                        epsilon3 = fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex];
                        if (epsilon3 < fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex2])
                            epsilon3 = fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex2];
                        i = 0;
                        double second_part_of_eR = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            second_part_of_eR += thicknesses[i];
                            if (epsilon3 < fi.equivalencefactor3[indexEpsilons[i]])
                                epsilon3 = fi.equivalencefactor3[indexEpsilons[i]];
                            i++;
                        }
                        second_part_of_eR /= 0.9 * epsilon2;
                        eR += second_part_of_eR;
                        eR /= 0.9 * fi.equivalencefactor1[1] * epsilon3;

                        //TODO - check if eR is less than the thicknesses
                        if (eR < fi.thicknessformonolithiques)
                            eR = fi.thicknessformonolithiques;
                        if (eR < fi.thicknessformonolithiques2)
                            eR = fi.thicknessformonolithiques2;

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            if (eR < thicknesses[i])
                                eR = thicknesses[i];
                            i++;
                        }

                    }
                    //1 Monolithique + 2 Feuilletés
                    else if (thicknesses != null && fi.GlazingTypeMonolithiqueIndex > -1) 
                    {
                        epsilon2 = fi.equivalencefactor2[indexEpsilon2[0]];
                        double second_epsilon2 = fi.equivalencefactor2[indexEpsilon2[1]];

                        eR += fi.thicknessformonolithiques;
                        epsilon3 = fi.equivalencefactor3[fi.GlazingTypeMonolithiqueIndex];

                        double second_part_eR = 0;
                        double third_part_eR = 0;
                        i = 0;
                        //Calculation for second_part_eR
                        while (i < fi.thicknessfromeachLayerfeuillete2.Length && fi.thicknessfromeachLayerfeuillete2[i] > 0) 
                        {
                            second_part_eR += fi.thicknessfromeachLayerfeuillete2[i];
                            if (epsilon3 < fi.equivalencefactor3[fi.GlazingTypeFromLayersIndexes2[i]])
                                epsilon3 = fi.equivalencefactor3[fi.GlazingTypeFromLayersIndexes2[i]];
                            i++;
                        }
                        i = 0;
                        second_part_eR /= 0.9 * epsilon2;
                        //Calculation for third_part_eR
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            third_part_eR += thicknesses[i];
                            if (epsilon3 < fi.equivalencefactor3[indexEpsilons[i]])
                                epsilon3 = fi.equivalencefactor3[indexEpsilons[i]];
                            i++;
                        }
                        third_part_eR /= 0.9 * second_epsilon2;

                        eR += second_part_eR + third_part_eR;
                        eR /= 0.9 * fi.equivalencefactor1[1] * epsilon3;

                        //TODO - check if eR is less than the thicknesses
                        if (eR < fi.thicknessformonolithiques)
                            eR = fi.thicknessformonolithiques;

                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete2.Length && fi.thicknessfromeachLayerfeuillete2[i] > 0) 
                        {
                            if (eR < fi.thicknessfromeachLayerfeuillete2[i])
                                eR = fi.thicknessfromeachLayerfeuillete2[i];
                            i++;
                        }

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            if (eR < thicknesses[i])
                                eR = thicknesses[i];
                            i++;
                        }

                    }
                    //3 Feuilletés
                    else
                    {
                        epsilon2 = fi.equivalencefactor2[indexEpsilon2[0]];
                        double second_epsilon2 = fi.equivalencefactor2[indexEpsilon2[1]];
                        double third_epsilon2 = fi.equivalencefactor2[indexEpsilon2[2]];

                        epsilon3 = 0;
                        i = 0;
                        double first_part_of_eR = 0;
                        double second_part_of_eR = 0;
                        double third_part_of_eR = 0;

                        //Calculation for first eR
                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete.Length && fi.thicknessfromeachLayerfeuillete[i] > 0) 
                        {
                            first_part_of_eR += fi.thicknessfromeachLayerfeuillete[i];
                            if (epsilon3 < fi.equivalencefactor3[fi.GlazingTypeFromLayersIndexes[i]])
                                epsilon3 = fi.equivalencefactor3[fi.GlazingTypeFromLayersIndexes[i]];
                            i++;
                        }
                        first_part_of_eR /= 0.9 * epsilon2;

                        //Calculation for second eR
                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete2.Length && fi.thicknessfromeachLayerfeuillete2[i] > 0)
                        {
                            second_part_of_eR += fi.thicknessfromeachLayerfeuillete2[i];
                            if (epsilon3 < fi.equivalencefactor3[fi.GlazingTypeFromLayersIndexes2[i]])
                                epsilon3 = fi.equivalencefactor3[fi.GlazingTypeFromLayersIndexes2[i]];
                            i++;
                        }
                        second_part_of_eR /= 0.9 * second_epsilon2;

                        //Calculation for third eR
                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            third_part_of_eR += thicknesses[i];
                            if (epsilon3 < fi.equivalencefactor3[indexEpsilons[i]])
                                epsilon3 = fi.equivalencefactor3[indexEpsilons[i]];
                            i++;
                        }
                        third_part_of_eR /= 0.9 * third_epsilon2;

                        eR += first_part_of_eR + second_part_of_eR + third_part_of_eR;

                        eR /= 0.9 * fi.equivalencefactor1[1] * epsilon3;

                        //TODO - check if eR is less than the thicknesses
                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete.Length && fi.thicknessfromeachLayerfeuillete[i] > 0)
                        {
                            if (eR < fi.thicknessfromeachLayerfeuillete[i])
                                eR = fi.thicknessfromeachLayerfeuillete[i];
                            i++;
                        }

                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete2.Length && fi.thicknessfromeachLayerfeuillete2[i] > 0)
                        {
                            if (eR < fi.thicknessfromeachLayerfeuillete2[i])
                                eR = fi.thicknessfromeachLayerfeuillete2[i];
                            i++;
                        }

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            if (eR < thicknesses[i])
                                eR = thicknesses[i];
                            i++;
                        }


                    }
                    break;
                default:
                    throw new Exception("Wrong Index for the eR calculation");
            }
            eR = Math.Round(eR, 2);
        }

        public void eFCalculation(int GlazingTypeIndex, double thickness, double[] thicknesses, int[] indexEpsilon2) 
        {
            eF = 0;
            switch (GlazingTypeIndex) 
            {
                //Monolithique
                case 0:
                   eF = thickness; 
                   break;

                //Feuilleté
                case 1:
                    int i = 0;
                    while(i < thicknesses.Length && thicknesses[i] > 0) 
                    {
                        eF += thicknesses[i];
                        i++;
                    }
                    eF /= fi.equivalencefactor2[indexEpsilon2[0]];

                    //Check if eF is not less than the thickness
                    i = 0;
                    while (i < thicknesses.Length && thicknesses[i] > 0)
                    {
                        if (eF < thicknesses[i])
                            eF = thicknesses[i];
                        i++;
                    }
                    break;

                //Isolant 2 faces
                case 2:
                    //Double Monolithiques
                    if (thicknesses == null) 
                    {
                        eF = fi.thicknessformonolithiques + thickness;
                        eF /= fi.equivalencefactor1[0];

                        if (eF < fi.thicknessformonolithiques)
                            eF = fi.thicknessformonolithiques;
                        if (eF < thickness)
                            eF = thickness;
                    }

                    //M + F
                    else if (thicknesses != null && fi.GlazingTypeMonolithiqueIndex > -1) 
                    {
                        i = 0;
                        while(i < thicknesses.Length && thicknesses[i] > 0) 
                        {
                            eF += thicknesses[i];
                            i++;
                        }
                        eF /= fi.equivalencefactor2[indexEpsilon2[0]];
                        eF += fi.thicknessformonolithiques;
                        eF /= fi.equivalencefactor1[0];

                        //check if eF is less than the thickness
                        if (eF < fi.thicknessformonolithiques)
                            eF = fi.thicknessformonolithiques;

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0) 
                        {
                            if (eF < thicknesses[i])
                                eF = thicknesses[i];
                            i++;
                        }

                    }
                    //2F
                    else 
                    {
                        double first_part_ef = 0;
                        double second_part_ef = 0;

                        i = 0;
                        while(i < fi.thicknessfromeachLayerfeuillete.Length && fi.thicknessfromeachLayerfeuillete[i] > 0) 
                        {
                            first_part_ef += fi.thicknessfromeachLayerfeuillete[i];
                            i++;
                        }
                        first_part_ef /= fi.equivalencefactor2[indexEpsilon2[0]];

                        i = 0;
                        while(i < thicknesses.Length && thicknesses[i] > 0) 
                        {
                            second_part_ef += thicknesses[i];
                            i++;
                        }
                        second_part_ef /= fi.equivalencefactor2[indexEpsilon2[1]];
                        eF = (first_part_ef + second_part_ef) / fi.equivalencefactor1[0];

                        //check if ef is less than the thickness

                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete.Length && fi.thicknessfromeachLayerfeuillete[i] > 0) 
                        {
                            if (eF < fi.thicknessfromeachLayerfeuillete[i])
                                eF = fi.thicknessfromeachLayerfeuillete[i];
                            i++;
                        }

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            if (eF < thicknesses[i])
                                eF = thicknesses[i];
                            i++;
                        }

                    }
                    break;

                //Isolant 3 faces
                case 3:
                    //3M
                    if (thicknesses == null)
                    {
                        eF = (fi.thicknessformonolithiques + fi.thicknessformonolithiques2 + thickness) / fi.equivalencefactor1[1];

                        //check if ef is less than the thicknesses
                        if (eF < fi.thicknessformonolithiques)
                            eF = fi.thicknessformonolithiques;
                        if (eF < fi.thicknessformonolithiques2)
                            eF = fi.thicknessformonolithiques2;
                        if (eF < thickness)
                            eF = thickness;
                    }
                    //2M + F
                    else if (thicknesses != null && fi.GlazingTypeMonolithiqueIndex > -1 && fi.GlazingTypeMonolithiqueIndex2 > -1)
                    {
                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            eF += thicknesses[i];
                            i++;
                        }
                        eF /= fi.equivalencefactor2[indexEpsilon2[0]];
                        eF += fi.thicknessformonolithiques + fi.thicknessformonolithiques2;
                        eF /= fi.equivalencefactor1[1];

                        if (eF < fi.thicknessformonolithiques)
                            eF = fi.thicknessformonolithiques;
                        if (eF < fi.thicknessformonolithiques2)
                            eF = fi.thicknessformonolithiques2;

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            if (eF < thicknesses[i])
                                eF = thicknesses[i];
                            i++;
                        }
                    }

                    //M + 2F
                    else if (thicknesses != null && fi.GlazingTypeMonolithiqueIndex > -1) 
                    {
                        eF = fi.thicknessformonolithiques;
                        double second_part_ef = 0;
                        double third_part_ef = 0;

                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete2.Length && fi.thicknessfromeachLayerfeuillete2[i] > 0) 
                        {
                            second_part_ef += fi.thicknessfromeachLayerfeuillete2[i];
                            i++;
                        }
                        second_part_ef /= fi.equivalencefactor2[indexEpsilon2[0]];

                        i = 0;
                        while(i < thicknesses.Length && thicknesses[i] > 0) 
                        {
                            third_part_ef += thicknesses[i];
                            i++;
                        }
                        third_part_ef /= fi.equivalencefactor2[indexEpsilon2[1]];
                        eF += (second_part_ef + third_part_ef) / fi.equivalencefactor3[1];

                        //check if ef is less than the thicknesses
                        if (eF < fi.thicknessformonolithiques)
                            eF = fi.thicknessformonolithiques;

                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete2.Length && fi.thicknessfromeachLayerfeuillete2[i] > 0) 
                        {
                            if (eF < fi.thicknessfromeachLayerfeuillete2[i])
                                eF = fi.thicknessfromeachLayerfeuillete2[i];
                            i++;
                        }

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0) 
                        {
                            if (eF < thicknesses[i])
                                eF = thicknesses[i];
                            i++;
                        }
                    }
                    // 3F
                    else 
                    {
                        double first_part_ef = 0;
                        double second_part_ef = 0;
                        double third_part_ef = 0;

                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete.Length && fi.thicknessfromeachLayerfeuillete[i] > 0) 
                        {
                            first_part_ef += fi.thicknessfromeachLayerfeuillete[i];
                            i++;
                        }
                        first_part_ef /= fi.equivalencefactor2[indexEpsilon2[0]];

                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete2.Length && fi.thicknessfromeachLayerfeuillete2[i] > 0)
                        {
                            second_part_ef += fi.thicknessfromeachLayerfeuillete2[i];
                            i++;
                        }
                        second_part_ef /= fi.equivalencefactor2[indexEpsilon2[1]];

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0)
                        {
                            third_part_ef += thicknesses[i];
                            i++;
                        }
                        third_part_ef /= fi.equivalencefactor2[indexEpsilon2[2]];

                        eF = (first_part_ef + second_part_ef + third_part_ef) / fi.equivalencefactor1[1];

                        //check if ef is less than the thicknesses
                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete.Length && fi.thicknessfromeachLayerfeuillete[i] > 0) 
                        {
                            if (eF < fi.thicknessfromeachLayerfeuillete[i])
                                eF = fi.thicknessfromeachLayerfeuillete[i];
                            i++;
                        }

                        i = 0;
                        while (i < fi.thicknessfromeachLayerfeuillete2.Length && fi.thicknessfromeachLayerfeuillete2[i] > 0) 
                        {
                            if (eF < fi.thicknessfromeachLayerfeuillete2[i])
                                eF = fi.thicknessfromeachLayerfeuillete2[i];
                            i++;
                        }

                        i = 0;
                        while (i < thicknesses.Length && thicknesses[i] > 0) 
                        {
                            if (eF < thicknesses[i])
                                eF = thicknesses[i];
                            i++;
                        }


                    }

                    break;


            }
            eF = Math.Round(eF, 2);
        }
    }
}
