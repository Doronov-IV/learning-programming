using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Packages
{
    public interface IMessage
    {

        public string GetSender();

        public string GetReceiver();

        public string GetDate();

        public string GetTime();

        public object GetMessage();


    }
}
