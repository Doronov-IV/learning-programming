using System;

namespace Range
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TryFilePackage();
        }


        public static void TryFilePackage()
        {
            FileMessagePackage AssembledFileMessage = new("Mario", "Luigi", new("C:\\Users\\i.doronov\\Desktop\\khleb-salo-vodka.jpg"));

            FileMessagePackage UnassembledFileMessage = new();

            UnassembledFileMessage.Data = AssembledFileMessage.Data;

            UnassembledFileMessage.Disassemble();

            Console.WriteLine($"Sender: {UnassembledFileMessage.Sender}");
            Console.WriteLine($"Reciever: {UnassembledFileMessage.Reciever}");
            Console.WriteLine($"Message: {UnassembledFileMessage.FileName}");
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
    }
}