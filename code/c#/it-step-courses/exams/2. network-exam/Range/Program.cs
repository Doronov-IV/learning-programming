using System;

namespace Range
{
    public class Program
    {
        static void Main(string[] args)
        {
            TextMessagePackage AssembledMessage = new ("Mario", "Luigi", "It's me, Mario!");

            TextMessagePackage UnassembledMessage = new();

            UnassembledMessage.Data = AssembledMessage.Data;

            UnassembledMessage.Disassemble();

            Console.WriteLine(UnassembledMessage.Message);
        }
    }
}