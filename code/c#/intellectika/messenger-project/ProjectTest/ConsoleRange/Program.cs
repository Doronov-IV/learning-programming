using Microsoft.EntityFrameworkCore;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            User user = null;
            using (MessengerDatabaseContext context = new())
            {
                foreach (var usr in context.Users.Include(u => u.ChatList).Include(u => u.MessageList))
                {
                    var aaaaaa = usr;
                    var bbbbbbb = usr.ChatList.FirstOrDefault();
                    if (bbbbbbb.MessageList.Count != 0)
                    {
                        user = usr;
                    }
                }
            }

            var popierdolinyUser = UserParser.ParseToDTO(user);
            var parsedUser = UserParser.ParseFromDTO(popierdolinyUser);
            var aaaaa = parsedUser.ChatList[0].MessageList[0];
            Console.WriteLine($"{aaaaa.Date} {aaaaa.Time} {aaaaa.Contents}");
        }
    }
}