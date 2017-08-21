using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVAC
{
    namespace FluidMechanics
    {
        public class AirFlow : FluidFlow, ICloneable
        {
            public const double AIR_GAS_CONSTANT = 287.05;
            public const double VISCOSITY = 0.00000178;

            
            public double DynamicViscosity
            {
                get
                {
                    return VISCOSITY / Density;
                }
            }
            public double Density
            {
                get
                {
                    return Pressure / (AIR_GAS_CONSTANT * Temperature);
                }
            }
            public AirFlow()
            {

            }
            public AirFlow(double temperature, double pressure, double flow)
            {
                Temperature = temperature;
                Pressure = pressure;
                Flow = flow;
            }
            public object Clone()
            {
                return new AirFlow(this.Temperature, this.Pressure, this.Flow);
            }
            public static double CalculateDensity(double temperatureInKelvin, double pressureInPascal)
            {
                if (temperatureInKelvin >= -273.15 && pressureInPascal >= 0.0)
                    return pressureInPascal / (AIR_GAS_CONSTANT * temperatureInKelvin);
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}