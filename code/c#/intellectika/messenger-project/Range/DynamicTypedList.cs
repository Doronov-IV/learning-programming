using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Range
{
    public class DynamicTypedList : IEnumerable<IDataType>
    {

        #region STATE


        private IDataType[] array;  


        public int Count
        {
            get { return array.Length; }
        }

        public IDataType? Last
        {
            get
            {
                if (Count >= 0) return array[Count - 1];
                else return null;
            }
            set
            {
                if (Count >= 0) array[Count - 1] = value;
                else throw new NullReferenceException("[Custom] The list was empty.");
            }
        }

        public IDataType? First
        {
            get 
            {
                if (Count >= 0) return array[0];
                else return null;
            }
            set
            {
                if (Count >= 0) array[0] = value;
                else throw new NullReferenceException("[Custom] The list was empty."); 
            }
        }


        #endregion STATE


        public IEnumerator<IDataType> GetEnumerator()
        {
            foreach (IDataType item in array)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public DynamicTypedList()
        {
            array = new IDataType[0];
        }


        public void Add(IDataType item)
        {
            int nNewSize = Count + 1;
            int nOldSize = Count;

            var newArray = new IDataType[nNewSize];

            newArray[nOldSize] = item;

            for (int i = 0, iSize = Count; i < iSize; i++)
            {
                newArray[i] = array[i];
            }

            array = newArray;
        }

        public void AddRange(IEnumerable<IDataType> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void ForEach(Action<IDataType> action)
        {
            foreach(var item in array)
            {
                action(item);
            }
        }
    }
}
