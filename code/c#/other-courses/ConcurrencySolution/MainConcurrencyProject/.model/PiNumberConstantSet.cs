using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConcurrencyProject.model
{
    public struct PiNumberConstantSet : INotifyCollectionChanged
    {



        private double _startingRadius;


        public double StartingRadius
        {
            get { return _startingRadius; }
            set
            {
                _startingRadius = value;
                OnPropertyChanged(nameof(StartingRadius));
            }
        }



        private string _resultPiNumber;
        private int _threadCount;

        public string resultPiNumber
        {
            get { return _resultPiNumber; }
            set
            {
                _resultPiNumber = value;
                OnPropertyChanged(nameof(resultPiNumber));
            }
        }


    }
}
