using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Range
{
    public class CustomDouble : IDataType
    {
        private double _value;

        public double Value 
        { 
            get { return _value;} 
            set { _value = value; } 
        }

        public object GetValue()
        {
            return Value;
        }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public CustomDouble()
        {
            _value = 0.0d;
        }


        public CustomDouble(double value)
        {
            _value = value;
        }
    }
}
