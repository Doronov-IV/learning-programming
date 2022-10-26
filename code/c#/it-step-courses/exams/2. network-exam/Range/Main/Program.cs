using System;
using NetworkingAuxiliaryLibrary.Packages;

namespace Range.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OuterMethod();
        }



        #region CLOSURE


        protected static void OuterMethod()
        {
            int variable = 5;

            var inner = () =>
            {
                variable++;
                Console.WriteLine(variable);
            };

            inner();

            Console.WriteLine(variable);
        }

        #endregion CLOSURE




        #region PACKAGES

        public static void TryFilePackage()
        {
            FileMessagePackage AssembledFileMessage = new("Mario", "Luigi", new("C:\\Users\\i.doronov\\Desktop\\VirtualBox-7.0.2-154219-Win.exe"));

            FileMessagePackage UnassembledFileMessage = new();

            UnassembledFileMessage.Data = AssembledFileMessage.Data;

            UnassembledFileMessage.Disassemble();

            Console.WriteLine($"Sender: {UnassembledFileMessage.Sender}");
            Console.WriteLine($"Reciever: {UnassembledFileMessage.Reciever}");
            Console.WriteLine($"Message: {UnassembledFileMessage.FileName}");

            UnassembledFileMessage = null;
            AssembledFileMessage = null;
        }


        public static void TryTextPackage()
        {
            TextMessagePackage AssembledTextMessage = new("Mario", "Luigi", "It's me, Mario!");

            TextMessagePackage UnassembledTextMessage = new();

            UnassembledTextMessage.Data = AssembledTextMessage.Data;

            UnassembledTextMessage.Disassemble();

            Console.WriteLine($"Sender: {UnassembledTextMessage.Sender}");
            Console.WriteLine($"Reciever: {UnassembledTextMessage.Reciever}");
            Console.WriteLine($"Message: {UnassembledTextMessage.Message}");
        }


        #endregion PACKAGES
    
    
    }
}