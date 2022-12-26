using System;
using Newtonsoft.Json;
using NetworkingAuxiliaryLibrary.Processing;
using NetworkingAuxiliaryLibrary.Packages;
using System.Text;

namespace Range // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var jsonString = JsonMessageFactory.GetJsonMessageSimplified("Mario", "Luigi", "Some text idk");

            PackageBuilder builder = new();
            builder.WriteJsonMessage(jsonString);

            var aaaaaa = builder.GetPacketBytes();

            var stringForDebug = Encoding.UTF8.GetString(aaaaaa);

            Console.WriteLine(stringForDebug);

        }
    }
}