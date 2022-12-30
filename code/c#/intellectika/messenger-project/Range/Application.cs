global using System;
global using Newtonsoft.Json;
global using NetworkingAuxiliaryLibrary.Processing;
global using NetworkingAuxiliaryLibrary.Packages;
global using System.Text;
global using System.Reflection;

namespace Range
{
    public class Application
    {
        public void Run()
        {
            DynamicTypedList List = new DynamicTypedList();

            List.Add(new CustomInteger(5));
            List.Add(new CustomDouble(14.2));

            List.ForEach(x => Console.WriteLine(x));

            List<dynamic> list = new();

            list.Add(5);
            list.Add(14.2);

            list.ForEach(x => Console.WriteLine(x));
        }
    }
}
