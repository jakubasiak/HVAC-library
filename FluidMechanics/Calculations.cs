using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVAC
{
    namespace FluidMechanics
    {
        public static class Calculations
        {
            public static double Density(
                double mass,
                double volume)
            {
                if (mass >= 0.0 && volume > 0.0)
                    return mass / volume;
                else
                    throw new ArgumentOutOfRangeException();
            }
            public static double KinematicViscosity(
                double density,
                double dynamicViscosity)
            {
                if (density > 0.0 && dynamicViscosity > 0.0)
                    return density / dynamicViscosity;
                else
                    throw new ArgumentOutOfRangeException();
            }

            public static double ToKelvin(double tempInCelcius)
            {
                if (tempInCelcius >= -273.15)
                    return tempInCelcius + 273.15;
                else
                    throw new ArgumentOutOfRangeException();
            }

            public static double ToCelcius(double tempInKelvin)
            {
                if (tempInKelvin >= 0.0)
                    return tempInKelvin - 273.15;
                else
                    throw new ArgumentOutOfRangeException();
            }

            public static double ReynoldsNumber(
                double density,
                double velocity,
                double linearDimension,
                double dynamicViscosity)
            {
                if (density > 0.0 && velocity > 0.0 && linearDimension > 0.0 && dynamicViscosity > 0.0)
                    return density * velocity * linearDimension / dynamicViscosity;
                else
                    throw new ArgumentOutOfRangeException();
            }
            public static double ReynoldsNumber(
                double velocity,
                double linearDimension,
                double kinematicViscosity)
            {
                return velocity * linearDimension / kinematicViscosity;
            }
            public static double RectangularHydraulicDiameter(double aSize, double bSize)
            {
                return (2 * aSize * bSize) / (aSize + bSize);
            }
            public static double OvalArea(double aSize, double bSize)
            {
                double a = aSize > bSize ? aSize : bSize; //większy bok
                double b = aSize < bSize ? aSize : bSize; //mniejszy bok
                return  Math.PI * b * b / 4.0 + (a - b) * b;
            }
            public static double OvalHydraulicDiameter(double aSize, double bSize)
            {
                double a = aSize > bSize ? aSize : bSize; //większy bok
                double b = aSize < bSize ? aSize : bSize; //mniejszy bok
                double area = Math.PI * b * b / 4.0 + (a - b) * b;
                double perimeter = Math.PI * b + 2.0 * (a - b);

                return 4.0 * area / perimeter;
            }
            public static double FlowVelocity(
                double flow,
                double crossSectionArea)
            {
                return flow / (3600 * crossSectionArea);
            }
            public static double VelocityPressure(
                double density,
                double flowVelocity)
            {
                return density * flowVelocity * flowVelocity / 2.0;
            }
            public static double FrictionLoss(
                double darcyFrictionFactor,
                double density,
                double hydraulicDiameter,
                double flowVelocity)
            {
                return (darcyFrictionFactor / hydraulicDiameter) * ((density * flowVelocity * flowVelocity) / 2);
            }
            public static double FrictionLoss(
                double darcyFrictionFactor,
                double hydraulicDiameter,
                double velocityPressure)
            {
                return (darcyFrictionFactor / hydraulicDiameter) * velocityPressure;
            }
            #region DarcyFrictionFactor
            public static double DarcyFrictionFactor(
                double reynoldsNumber,
                double relativeRoughness,
                double hydraulicDiameter,
                DarcyFrictionFactorApproximation approximation)
            {
                if (reynoldsNumber < 4000.0)
                {
                    return 64.0 / reynoldsNumber;
                }
                else
                {
                    switch (approximation)
                    {
                        case DarcyFrictionFactorApproximation.GoudarSonnad:
                            return GoudarSonnadApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Moody:
                            return MoodyApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Wood:
                            return WoodApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Eck:
                            return EckApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.SwameeJain:
                            return SwameeJainApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Churchill:
                            return ChurchillApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Jain:
                            return JainApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Chen:
                            return ChenApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Round:
                            return RoundApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Barr:
                            return BarrApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.ZigrangSylvester:
                            return ZigrangSylvesterApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Haaland:
                            return HaalandApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Serghides:
                            return SerghidesApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Alashkar:
                            return AlashkarApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Tsal:
                            return TsalApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.Manadilli:
                            return ManadilliApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.MonzonRomeoRoyo:
                            return MonzonRomeoRoyoApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                        case DarcyFrictionFactorApproximation.VatankhahKouchakzadeh:
                            return VatankhahKouchakzadehApproximation(reynoldsNumber, relativeRoughness, hydraulicDiameter);
                            break;
                    }
                    throw new UnspecifiedTypeOfFlowException();

                }
            }
            private static double GoudarSonnadApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
                
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                double s = 0.124 * reynoldsNumber * (relativeRoughness) + Math.Log(0.4587 * reynoldsNumber);
                return Math.Pow(1.0 / (0.8686 * Math.Log((0.4587 * reynoldsNumber) / Math.Pow((s - 0.31), (s / (s + 1))))), 2);
            }
            private static double MoodyApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                if (relativeRoughness > 0.0 && relativeRoughness < 0.01)
                {
                    if (reynoldsNumber >= 4000.0 && reynoldsNumber <= 500000000.0)
                    {
                        return 0.0055 * (1.0 + Math.Pow(2.0 * 10000.0 * relativeRoughness + (1000000.0 / reynoldsNumber), (1.0 / 3.0)));
                    }
                    else
                        throw new UnspecifiedTypeOfFlowException();
                }
                else
                    throw new UnspecifiedTypeOfFlowException();
            }

            private static double WoodApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                if (relativeRoughness > 0.00001 && relativeRoughness < 0.04)
                {
                    if (reynoldsNumber >= 4000.0 && reynoldsNumber <= 50000000.0)
                    {
                        double psi = 1.62 * Math.Pow(relativeRoughness, 0.134);
                        return 0.094 * Math.Pow(relativeRoughness, 0.225) + 0.53 * relativeRoughness + 88.0 * Math.Pow(relativeRoughness, 0.44) * Math.Pow(reynoldsNumber, (-psi));
                    }
                    else
                        throw new UnspecifiedTypeOfFlowException();
                }
                else
                    throw new UnspecifiedTypeOfFlowException();
            }


            private static double EckApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                return Math.Pow(1.0 / (-2.0 * Math.Log10((relativeRoughness / 3.715) + (15.0 / reynoldsNumber))), 2.0);
            }

            private static double SwameeJainApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                if (relativeRoughness > 0.000001 && relativeRoughness < 0.05)
                {
                    if (reynoldsNumber >= 4000.0 && reynoldsNumber <= 100000000.0)
                    {
                        return Math.Pow(1 / (-2.0 * Math.Log10((relativeRoughness / 3.7) + (5.74 / Math.Pow(reynoldsNumber, 0.9)))), 2.0);
                    }
                    else
                        throw new UnspecifiedTypeOfFlowException();
                }
                else
                    throw new UnspecifiedTypeOfFlowException();
            }

            private static double ChurchillApproximation(
               double reynoldsNumber,
               double effectiveRoughness,
               double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                return Math.Pow(1 / (-2.0 * Math.Log10((relativeRoughness / 3.71) + Math.Pow((7.0 / reynoldsNumber), 0.9))), 2.0);
            }
            private static double JainApproximation(
               double reynoldsNumber,
               double effectiveRoughness,
               double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                return Math.Pow(1.0 / (-2.0 * Math.Log10((relativeRoughness / 3.715) + Math.Pow((6.943 / reynoldsNumber), 0.9))), 2.0);
            }

            private static double ChenApproximation(
               double reynoldsNumber,
               double effectiveRoughness,
               double hydraulicDiameter)
            {
                if (reynoldsNumber >= 4000.0 && reynoldsNumber < 400000000.0)
                {
                    double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                    return Math.Pow(1.0 / (-2.0 * Math.Log10((relativeRoughness / 3.7065) - (5.0452 / reynoldsNumber) * Math.Log10((1.0 / 2.8257) * Math.Pow(relativeRoughness, 1.1098) + (5.8506 / Math.Pow(reynoldsNumber, 0.8981))))), 2.0);
                }
                else
                    throw new UnspecifiedTypeOfFlowException();
            }

            private static double RoundApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                return Math.Pow(1.0 / (1.8 * Math.Log10(reynoldsNumber / (0.135 * reynoldsNumber * relativeRoughness + 6.5))), 2.0);
            }

            private static double BarrApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                return Math.Pow(1.0 / (-2.0 * Math.Log10((relativeRoughness / 3.7) + (4.518 * Math.Log10(reynoldsNumber / 7.0) / (reynoldsNumber * (1 + (Math.Pow(reynoldsNumber, 0.52) / 29.0) * Math.Pow(relativeRoughness, 0.7)))))), 2.0);
            }
            private static double ZigrangSylvesterApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                return Math.Pow(1.0 / (-2.0 * Math.Log10((relativeRoughness / 3.7) - (5.02 / reynoldsNumber) * Math.Log10(relativeRoughness / 3.7 + 13.0 / reynoldsNumber))), 2);
            }
            private static double HaalandApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                return Math.Pow(1.0 / (-1.8 * Math.Log10(Math.Pow((relativeRoughness / 3.7), 1.11) + (6.9 / reynoldsNumber))), 2.0);
            }
            private static double SerghidesApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                double psi1 = -2.0 * Math.Log10((relativeRoughness / 3.7) + (12.0 / reynoldsNumber));
                double psi2 = -2.0 * Math.Log10((relativeRoughness / 3.7) + ((2.51 * psi1) / reynoldsNumber));
                double psi3 = -2.0 * Math.Log10((relativeRoughness / 3.7) + ((2.51 * psi2) / reynoldsNumber));

                return Math.Pow(1.0 / (psi1 - (((psi2 - psi1) * (psi2 - psi1)) / (psi3 - 2.0 * psi2 + psi1))), 2);
            }
            private static double AlashkarApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                double a = relativeRoughness / 3.7065;
                double b = 2.5226 / reynoldsNumber;

                return 1.325474505 * Math.Pow(Math.Log(a - 0.8686068432 * b * Math.Log(a - 0.8784893582 * b * Math.Log(a + Math.Pow(1.665368035 * b, 0.8373492157)))), .0 - 2);
            }
            private static double TsalApproximation(
               double reynoldsNumber,
               double effectiveRoughness,
               double hydraulicDiameter)
            {
                double a = 0.11 * Math.Pow((68.0 / reynoldsNumber) + effectiveRoughness, 0.25);
                if (a >= 0.018)
                    return a;
                else
                    return 0.0028 + 0.85 * a;
            }
            private static double ManadilliApproximation(
               double reynoldsNumber,
               double effectiveRoughness,
               double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                return Math.Pow(1.0 / (-2.0 * Math.Log10((relativeRoughness/3.7)+(95.0/Math.Pow(reynoldsNumber,0.983))-(96.82/reynoldsNumber))), 2.0);
            }
            private static double MonzonRomeoRoyoApproximation(
               double reynoldsNumber,
               double effectiveRoughness,
               double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                double a = relativeRoughness / 3.7065;
                double b = 5.0272 / reynoldsNumber;
                double c = relativeRoughness / 3.827;
                double d = 4.657 / reynoldsNumber;
                double e = Math.Pow((relativeRoughness / 7.7918), 0.9924);
                double f = Math.Pow((5.3326 / (208.815 + reynoldsNumber)), 0.9345);
                return Math.Pow(1.0 / (-2.0 * Math.Log10(a-b*Math.Log10(c-d*Math.Log10(e+f)))), 2.0);
            }
            private static double VatankhahKouchakzadehApproximation(
                double reynoldsNumber,
                double effectiveRoughness,
                double hydraulicDiameter)
            {
                double relativeRoughness = effectiveRoughness / hydraulicDiameter;
                double s = 0.124 * reynoldsNumber * relativeRoughness + Math.Log(0.4587 * reynoldsNumber);
                return Math.Pow(1.0 / (0.8686 * Math.Log((0.4587 * reynoldsNumber) / Math.Pow((s - 0.31), (s / (s + 0.9633))))), 2);
            }

            #endregion
        }
        public enum DarcyFrictionFactorApproximation
        {
            GoudarSonnad,
            Moody,
            Wood,
            Eck,
            SwameeJain,
            Churchill,
            Jain,
            Chen,
            Round,
            Barr,
            ZigrangSylvester,
            Haaland,
            Serghides,
            Alashkar,
            Tsal,
            Manadilli,
            MonzonRomeoRoyo,
            VatankhahKouchakzadeh
        }
    }
}
