using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Range
{
    public class CustomInteger : IDataType
    {

        private Int32 _value;

        public Int32 Value
        {
            get { return _value; }
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
        public CustomInteger()
        {
            _value = 0;
        }


        public CustomInteger(Int32 value)
        {
            _value = value;
        }
    }
}
