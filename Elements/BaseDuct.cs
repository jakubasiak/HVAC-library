using HVAC.FluidMechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVAC.Elements
{
    abstract public class BaseDuct : ICloneable
    {
        public List<LocalLoss> LocalLosses { get; set; }
        public DarcyFrictionFactorApproximation Approximation { get; set; }
        public double RelativeRoughness { get; set; }
        public double Length { get; set; }
        public AirFlow AirFlow { get; set; }
        public abstract double HydraulicDiameter { get; }
        public abstract string Size { get; }
        public abstract double Velocity { get; }
        public double FrictionFactor
        {
            get
            {
                return Calculations.DarcyFrictionFactor(ReynoldsNumber, RelativeRoughness, HydraulicDiameter, Approximation);
            }
        }
        public double VelocityPressure
        {
            get
            {
                return Calculations.VelocityPressure(AirFlow.Density, Velocity);
            }
        }
        public double FrictionLoss
        {
            get
            {
                return Calculations.FrictionLoss(FrictionFactor, HydraulicDiameter, VelocityPressure);
            }
        }
        public double LocalPressureDrop
        {
            get
            {
                double localPressureDrop = 0;
                if (LocalLosses != null)
                    foreach (var localLoss in LocalLosses)
                    {
                        localPressureDrop += localPressureDrop + localLoss.LocalLossCoefficient * VelocityPressure;
                    }
                return localPressureDrop;
            }
        }
        public double LinearPressureDrop
        {
            get
            {
                return FrictionLoss * Length;
            }
        }
        public double PressureDrop
        {
            get
            {
                return LinearPressureDrop + LocalPressureDrop;
            }
        }
        public double ReynoldsNumber
        {
            get
            {
                return Calculations.ReynoldsNumber(Velocity, HydraulicDiameter, AirFlow.DynamicViscosity);
            }
        }

        public abstract object Clone();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Size: " + Size);
            sb.Append("\nLenght: " + Length);
            sb.Append("\nHydraulic Diameter: " + HydraulicDiameter);
            sb.Append("\nRelative Roughness : " + RelativeRoughness);
            sb.Append("\nAir Flow: " + AirFlow.Flow);
            sb.Append("\nVelocity: " + Velocity);
            sb.Append("\nReynolds Number: " + ReynoldsNumber);
            sb.Append("\nFriction Factory: " + FrictionFactor);
            sb.Append("\nVelocity Pressure: " + VelocityPressure);
            sb.Append("\nFriction Loss: " + FrictionLoss);
            sb.Append("\nPressure Drop: " + PressureDrop);

            return sb.ToString();
        }

    }
}
