using System;
using NetworkingAuxiliaryLibrary.Packages;
using Range.Encryption;

namespace Range.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TryPasswordEncryption();
        }




        #region PACKAGES

        public static void TryFilePackage()
        {
            FileMessagePackage AssembledFileMessage = new("Mario", "Luigi", new("C:\\Users\\i.doronov\\Desktop\\specific files\\VirtualBox-7.0.2-154219-Win.exe"));

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
            TextMessagePackage AssembledTextMessage = new("abc", "@All", "It's me, Mario!");

            TextMessagePackage UnassembledTextMessage = new();

            UnassembledTextMessage.Data = AssembledTextMessage.Data;

            UnassembledTextMessage.Disassemble();

            Console.WriteLine($"Sender: {UnassembledTextMessage.Sender}");
            Console.WriteLine($"Reciever: {UnassembledTextMessage.Reciever}");
            Console.WriteLine($"Message: {UnassembledTextMessage.Message}");
        }


        #endregion PACKAGES





        #region ENCRYPTION


        public static void TryPasswordEncryption()
        {
            var hash = SecurePasswordHasher.Hash("admin");

            var result = SecurePasswordHasher.Verify("admin", hash);

            Console.WriteLine(result);
        }


        #endregion ENCRYPTION

    }
}