using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVAC
{
    namespace Elements
    {
        public class LocalLoss : ICloneable
        {
            public double LocalLossCoefficient { get; set; }
            public string Description { get; set; }

            public LocalLoss()
            {

            }

            public object Clone()
            {
                return new LocalLoss()
                {
                    LocalLossCoefficient = this.LocalLossCoefficient,
                    Description = this.Description
                };
            }
        }
    }
}
