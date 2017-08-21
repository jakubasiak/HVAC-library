using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVAC.FluidMechanics
{
    public class FluidFlow
    {
        private double _flow;
        public double Flow
        {
            get
            {
                return _flow;
            }
            set
            {
                if (value >= 0)
                    _flow = value;
            }
        } //m3/h
        private double _temperature;
        public double Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                if (value >= -273.15)
                    _temperature = value;
            }
        }
        private double _pressure;
        public double Pressure
        {
            get
            {
                return _pressure;
            }
            set
            {
                if (value >= 0)
                    _pressure = value;
            }
        }
    }
}
