using HVAC.FluidMechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVAC.Elements
{
    public class RectangularDuct : BaseDuct, ICloneable
    {
        public double ASize { get; set; } //in meters
        public double BSize { get; set; } //in meters
        public override double HydraulicDiameter
        {
            get
            {
                return Calculations.RectangularHydraulicDiameter(ASize, BSize);
            }
        }
        public override double Velocity
        {
            get
            {
                return Calculations.FlowVelocity(AirFlow.Flow, ASize * BSize);
            }
        }
        public override string Size
        {
            get
            {
                return ASize * 1000 + "x" + BSize * 1000;
            }
        }
        public RectangularDuct()
        {

        }
        public RectangularDuct(
            DarcyFrictionFactorApproximation approximation,
            double relativeRoughness,
            double aSize,
            double bSize,
            double length,
            AirFlow airFlow)
        {
            Approximation = approximation;
            Length = length;
            ASize = aSize;
            BSize = bSize;
            RelativeRoughness = relativeRoughness;
            AirFlow = airFlow;

        }

        public override object Clone()
        {
            return new RectangularDuct()
            {
                Approximation = this.Approximation,
                Length = this.Length,
                ASize = this.ASize,
                BSize = this.BSize,
                RelativeRoughness = this.RelativeRoughness,
                AirFlow = this.AirFlow.Clone() as AirFlow,
                LocalLosses = this.LocalLosses.Clone<LocalLoss>() as List<LocalLoss>

            };
        }
    }
}
