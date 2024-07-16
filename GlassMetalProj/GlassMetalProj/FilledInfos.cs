using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace GlassMetalProj
{
    public class FilledInfos
    {
        public double Pressure {  get; set; }

        //For finding the windpressure
        public int IndexHeight { get; set; }
        public int IndexRegion {  get; set; }

        public int IndexFieldType { get; set; }

        private double[,] windPressure = new double[40, 5];

        //The three first boxes 
        public bool vitrageoutside = true;
        public bool inFrance = true;
        public bool inclined = false;

        public double[] pressureinclined = new double[7];

        //Angle for the inclined glazing
        public double BetaAngle { get; set; }

        public double Altitude { get; set; }


        //Snow if inclined
        public int[] SnowChargeBelow200;
        public int[] SnowChargeAD;

        public bool Avalanche;

        public double S1 {  get; set; }
        public double S2 { get; set; }

        //Dimensions of the glazing 
        public double L { get; set; }
        public double l { get; set; }

        public double[] equivalencefactor1 { get; set; }
        public double[] equivalencefactor2 { get; set; }
        public double[] equivalencefactor3 { get; set; }

        public double epsilon1;
        public double epsilon2;
        public double epsilon3;

        //equals to 1 every time expect one time
        public double c = 1.0;

        //e or ep.
        public string thickness;

        public FilledInfos(string pathPressure,string pathFactors) 
        {
            Pressure = 0;
            IndexHeight = -1; 
            IndexRegion = -1;
            IndexFieldType = -1;
            InitalizePressures(pathPressure);
            InitializeEquivalenceFactors(pathFactors);
        }

        public void CalculateDimensions(int i, double a, double b, double c, double d) 
        {
            // triangle isocèle
            if (i == 1 || i == 2)
            {
                if (b >= (2.0 / 3.0) * a) 
                {
                    L = b;
                    l = (2.0/3.0) * a;
                }
                else 
                {
                    L = (2.0/3.0) * a;
                    l = b;
                }
            }
            if (i == 3) 
            {
                if (b > c + (2.0 / 3.0) * (a - c)) 
                {
                    L = b;
                    l = c + (2.0 / 3.0) * (a - c);
                }
                else 
                {
                    L = l = c + (2.0 / 3.0) * (a - c);
                    l = b;
                }
            }
            if (i == 4) 
            {
                if (b > (d + c + a) / 3.0) 
                {
                    L = b;
                    l = (d + c + a) / 3.0;
                }
                else 
                {
                    L = (d + c + a) / 3.0;
                    l = b;
                }
            }
            if (i == 5) 
            {
                L = 0.85 * a;
                l = 0.85 * a;
            }
            if (i == 6) 
            {
                if (b > 0.425 *b + c) 
                {
                    L = b;
                    l = 0.425 * b + c;
                }
                else 
                {
                    L = (float)0.425 * b + c;
                    l = b;
                }
            }
            L = Math.Round(L, 2);
            l = Math.Round(l, 2);
        }
        public void CalculatePressure() 
        {
            if (vitrageoutside)
            {
                if (!inFrance)
                    IndexRegion += 4;
                // To get the region index, we need to multiply by 5 because there are 5 type of field by region, and we add the index fieldtype.
                int i = IndexRegion * 5 + IndexFieldType;
                int j = IndexHeight;
                Pressure = windPressure[i, j];
                if (inclined) 
                {
                    //Pvent 
                    pressureinclined[0] = Pressure;

                    //Poids Propre


                    //P2
                }
            }
            else Pressure = 600;
        }

        public void FindS1andS2(int indexS,double mu, double Ce, double Ct) 
        {
            if (Altitude <= 200) 
            {
                double Sk = SnowChargeBelow200[indexS];
                S1 = Sk * mu + Ce * Ct;
            }
            else
            {
                double deltaS;
                if (Altitude > 200 && Altitude <= 500)
                    deltaS = Altitude - 200;
                else if (Altitude > 500 && Altitude <= 1000) ;
            }
            double Sad = SnowChargeAD[indexS];
            S2 = Sad * mu + Ce * Ct;
        }

        private void InitializeEquivalenceFactors(string path) 
        {
            if (path == string.Empty) 
            {
                //insulating glazing
                equivalencefactor1 = new double[2];
                equivalencefactor1[0] = 1.6;
                equivalencefactor1[1] = 2.0;

                //Laminated glass
                equivalencefactor2 = new double[5];
                equivalencefactor2[0] = 1.3;
                equivalencefactor2[1] = 1.5;
                equivalencefactor2[2] = 1.6;
                equivalencefactor2[3] = 1.6;
                equivalencefactor2[4] = 2.0;

                //Monolithic single glazing
                equivalencefactor3 = new double[20];
                equivalencefactor3[0] = 1.0;
                equivalencefactor3[1] = 1.2;
                equivalencefactor3[2] = 1.1;
                equivalencefactor3[3] = 1.1;
                equivalencefactor3[4] = 1.3;
                equivalencefactor3[5] = 0.61;
                equivalencefactor3[6] = 0.77;
                equivalencefactor3[7] = 0.71;
                equivalencefactor3[8] = 0.8;
                equivalencefactor3[9] = 1.0;
                equivalencefactor3[10] = 0.61;
                equivalencefactor3[11] = 1.0;
                equivalencefactor3[12] = 1.0;
                equivalencefactor3[13] = 0.6;
                equivalencefactor3[14] = 1.0;
                equivalencefactor3[15] = 0.55;
                equivalencefactor3[16] = 1.0;
                equivalencefactor3[17] = 1.1;
                equivalencefactor3[18] = 1.4;
                equivalencefactor3[19] = 1.2;

            }
        }


        private void InitalizePressures(string filepath)
        {
            if (filepath == string.Empty)
            {
                //Région 1
                windPressure[0, 0] = 850;
                windPressure[0, 1] = 950;
                windPressure[0, 2] = 1150;
                windPressure[0, 3] = 1400;
                windPressure[0, 4] = 1800;

                windPressure[1, 0] = 900;
                windPressure[1, 1] = 1200;
                windPressure[1, 2] = 1400;
                windPressure[1, 3] = 1700;
                windPressure[1, 4] = 2050;

                windPressure[2, 0] = 1200;
                windPressure[2, 1] = 1500;
                windPressure[2, 2] = 1700;
                windPressure[2, 3] = 2000;
                windPressure[2, 4] = 2350;

                windPressure[3, 0] = 1500;
                windPressure[3, 1] = 1800;
                windPressure[3, 2] = 2050;
                windPressure[3, 3] = 2300;
                windPressure[3, 4] = 2650;

                windPressure[4, 0] = 1900;
                windPressure[4, 1] = 2150;
                windPressure[4, 2] = 2350;
                windPressure[4, 3] = 2600;
                windPressure[4, 4] = 2900;

                //Région 2
                windPressure[5, 0] = 1050;
                windPressure[5, 1] = 1100;
                windPressure[5, 2] = 1350;
                windPressure[5, 3] = 1700;
                windPressure[5, 4] = 2100;

                windPressure[6, 0] = 1050;
                windPressure[6, 1] = 1400;
                windPressure[6, 2] = 1650;
                windPressure[6, 3] = 2000;
                windPressure[6, 4] = 2450;

                windPressure[7, 0] = 1400;
                windPressure[7, 1] = 1750;
                windPressure[7, 2] = 2000;
                windPressure[7, 3] = 2350;
                windPressure[7, 4] = 2800;

                windPressure[8, 0] = 1800;
                windPressure[8, 1] = 2150;
                windPressure[8, 2] = 2400;
                windPressure[8, 3] = 2750;
                windPressure[8, 4] = 3150;

                windPressure[9, 0] = 2250;
                windPressure[9, 1] = 2600;
                windPressure[9, 2] = 2800;
                windPressure[9, 3] = 3100;
                windPressure[9, 4] = 3500;

                // Région 3
                windPressure[10, 0] = 1200;
                windPressure[10, 1] = 1300;
                windPressure[10, 2] = 1600;
                windPressure[10, 3] = 2000;
                windPressure[10, 4] = 2500;

                windPressure[11, 0] = 1250;
                windPressure[11, 1] = 1650;
                windPressure[11, 2] = 1950;
                windPressure[11, 3] = 2350;
                windPressure[11, 4] = 2900;

                windPressure[12, 0] = 1650;
                windPressure[12, 1] = 2050;
                windPressure[12, 2] = 2350;
                windPressure[12, 3] = 2800;
                windPressure[12, 4] = 3300;

                windPressure[13, 0] = 2100;
                windPressure[13, 1] = 2550;
                windPressure[13, 2] = 2850;
                windPressure[13, 3] = 3200;
                windPressure[13, 4] = 3700;

                windPressure[14, 0] = 2650;
                windPressure[14, 1] = 3050;
                windPressure[14, 2] = 3300;
                windPressure[14, 3] = 3650;
                windPressure[14, 4] = 4100;

                // Région 4
                windPressure[15, 0] = 1400;
                windPressure[15, 1] = 1500;
                windPressure[15, 2] = 1850;
                windPressure[15, 3] = 2300;
                windPressure[15, 4] = 2900;

                windPressure[16, 0] = 1450;
                windPressure[16, 1] = 1950;
                windPressure[16, 2] = 2250;
                windPressure[16, 3] = 2750;
                windPressure[16, 4] = 3350;

                windPressure[17, 0] = 1900;
                windPressure[17, 1] = 2400;
                windPressure[17, 2] = 2750;
                windPressure[17, 3] = 3200;
                windPressure[17, 4] = 3850;

                windPressure[18, 0] = 2450;
                windPressure[18, 1] = 2950;
                windPressure[18, 2] = 3300;
                windPressure[18, 3] = 3750;
                windPressure[18, 4] = 4300;

                windPressure[19, 0] = 3050;
                windPressure[19, 1] = 3500;
                windPressure[19, 2] = 3800;
                windPressure[19, 3] = 4200;
                windPressure[19, 4] = 4750;

                // Guadeloupe
                windPressure[20, 0] = 2300;
                windPressure[20, 1] = 2500;
                windPressure[20, 2] = 3050;
                windPressure[20, 3] = 3800;
                windPressure[20, 4] = 4800;

                windPressure[21, 0] = 2400;
                windPressure[21, 1] = 3200;
                windPressure[21, 2] = 3750;
                windPressure[21, 3] = 4550;
                windPressure[21, 4] = 5550;

                windPressure[22, 0] = 3150;
                windPressure[22, 1] = 4000;
                windPressure[22, 2] = 4550;
                windPressure[22, 3] = 5300;
                windPressure[22, 4] = 6350;

                windPressure[23, 0] = 4050;
                windPressure[23, 1] = 4900;
                windPressure[23, 2] = 5400;
                windPressure[23, 3] = 6200;
                windPressure[23, 4] = 7150;

                windPressure[24, 0] = 5050;
                windPressure[24, 1] = 5800;
                windPressure[24, 2] = 6300;
                windPressure[24, 3] = 6950;
                windPressure[24, 4] = 7800;

                // Guyane
                windPressure[25, 0] = 500;
                windPressure[25, 1] = 550;
                windPressure[25, 2] = 700;
                windPressure[25, 3] = 850;
                windPressure[25, 4] = 1050;

                windPressure[26, 0] = 550;
                windPressure[26, 1] = 700;
                windPressure[26, 2] = 850;
                windPressure[26, 3] = 1000;
                windPressure[26, 4] = 1250;

                windPressure[27, 0] = 700;
                windPressure[27, 1] = 900;
                windPressure[27, 2] = 1000;
                windPressure[27, 3] = 1200;
                windPressure[27, 4] = 1400;

                windPressure[28, 0] = 900;
                windPressure[28, 1] = 1100;
                windPressure[28, 2] = 1200;
                windPressure[28, 3] = 1400;
                windPressure[28, 4] = 1600;

                windPressure[29, 0] = 1150;
                windPressure[29, 1] = 1300;
                windPressure[29, 2] = 1400;
                windPressure[29, 3] = 1550;
                windPressure[29, 4] = 1750;

                // Martinique
                windPressure[30, 0] = 1800;
                windPressure[30, 1] = 2000;
                windPressure[30, 2] = 2400;
                windPressure[30, 3] = 3000;
                windPressure[30, 4] = 3800;

                windPressure[31, 0] = 1900;
                windPressure[31, 1] = 2550;
                windPressure[31, 2] = 2950;
                windPressure[31, 3] = 3600;
                windPressure[31, 4] = 4400;

                windPressure[32, 0] = 2500;
                windPressure[32, 1] = 3150;
                windPressure[32, 2] = 3600;
                windPressure[32, 3] = 4200;
                windPressure[32, 4] = 5000;

                windPressure[33, 0] = 3200;
                windPressure[33, 1] = 3850;
                windPressure[33, 2] = 4300;
                windPressure[33, 3] = 4900;
                windPressure[33, 4] = 5650;

                windPressure[34, 0] = 4000;
                windPressure[34, 1] = 4600;
                windPressure[34, 2] = 5000;
                windPressure[34, 3] = 5500;
                windPressure[34, 4] = 6200;

                // Réunion
                windPressure[35, 0] = 2050;
                windPressure[35, 1] = 2250;
                windPressure[35, 2] = 2700;
                windPressure[35, 3] = 3400;
                windPressure[35, 4] = 4250;

                windPressure[36, 0] = 2150;
                windPressure[36, 1] = 2850;
                windPressure[36, 2] = 3350;
                windPressure[36, 3] = 4050;
                windPressure[36, 4] = 4950;

                windPressure[37, 0] = 2800;
                windPressure[37, 1] = 3550;
                windPressure[37, 2] = 4050;
                windPressure[37, 3] = 4750;
                windPressure[37, 4] = 5650;

                windPressure[38, 0] = 3650;
                windPressure[38, 1] = 4350;
                windPressure[38, 2] = 4850;
                windPressure[38, 3] = 5500;
                windPressure[38, 4] = 6350;

                windPressure[39, 0] = 4550;
                windPressure[39, 1] = 5200;
                windPressure[39, 2] = 5600;
                windPressure[39, 3] = 6200;
                windPressure[39, 4] = 6950;

            }
        }

        private void FindEpsilon(int type, int index, int[] indexesforcase3) 
        {
            switch (type)
            {
                case 0:
                    epsilon1 = equivalencefactor1[index];
                    break;
                case 1:
                    epsilon2 = equivalencefactor2[index];
                    break;
                case 2:
                    epsilon3 = equivalencefactor3[indexesforcase3[0]];
                    for (int i = 1; i < indexesforcase3.Length; i++)
                    {
                        if (epsilon3 < equivalencefactor3[indexesforcase3[i]])
                            epsilon3 = equivalencefactor3[indexesforcase3[i]];
                    }
                    break;
                default:
                    throw new Exception("Error in type value");
            }
        }
    }
}
