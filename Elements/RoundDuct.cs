using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVAC.FluidMechanics;


namespace HVAC
{
    namespace Elements
    {

        public class RoundDuct : BaseDuct, ICloneable
        {
            public double Diameter { get; set; }
            public override double HydraulicDiameter
            {
                get
                {
                    return Diameter;
                }
            }
            public override double Velocity
            {
                get
                {
                    return Calculations.FlowVelocity(AirFlow.Flow, Math.PI * Diameter * Diameter / 4.0);
                }
            }
            public override string Size
            {
                get
                {
                    return ((char)8709).ToString() + Diameter * 1000 + "";
                }
            }
            public RoundDuct()
            {

            }
            public RoundDuct(
                DarcyFrictionFactorApproximation approximation,
                double relativeRoughness,
                double diameter,
                double length,
                AirFlow airFlow)
            {
                Approximation = approximation;
                Length = length;
                Diameter = diameter;
                RelativeRoughness = relativeRoughness;
                AirFlow = airFlow;

            }

            public override object Clone()
            {
                return new RoundDuct()
                {
                    Approximation = this.Approximation,
                    Length = this.Length,
                    Diameter = this.Diameter,
                    RelativeRoughness = this.RelativeRoughness,
                    AirFlow = this.AirFlow.Clone() as AirFlow,
                    LocalLosses = this.LocalLosses.Clone<LocalLoss>() as List<LocalLoss>
                };
            }
        }

        //public class OvalDuct : BaseDuct
        //{
        //    public double ASize { get; set; } //in meters
        //    public double BSize { get; set; } //in meters
        //    public override double HydraulicDiameter
        //    {
        //        get
        //        {
        //            return Calculations.OvalHydraulicDiameter(ASize, BSize);
        //        }
        //    }
        //    public override double Velocity
        //    {
        //        get
        //        {
        //            return Calculations.FlowVelocity(AirFlow.Flow, Math.PI * Diameter * Diameter / 4.0);
        //        }
        //    }
        //    public override string Size
        //    {
        //        get
        //        {
        //            return ((char)632).ToString() + Diameter + "";
        //        }
        //    }

        //    public OvalDuct(
        //        double relativeRoughness,
        //        double aSize,
        //        double bSize,
        //        double length,
        //        AirFlow airFlow)
        //    {
        //        Length = length;
        //        ASize = aSize;
        //        BSize = bSize;
        //        RelativeRoughness = relativeRoughness;
        //        AirFlow = airFlow;

        //    }
        //}
    }
}
