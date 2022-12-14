using NetworkingAuxiliaryLibrary.Objects.Entities;
using NetworkingAuxiliaryLibrary.Net.Auxiliary.Processing;
using NetworkingAuxiliaryLibrary.Net.Auxiliary.Objects;

namespace ProjectTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UserParser_CorrectData_True()
        {
            User user = new User();
            user.Login = "admin_alpha";
            user.CurrentNickname = "User0";
            user.PublicId = "User0";
            Chat chat = new Chat();
            Message message = new Message();
            message.Author = user;
            message.Contents = "privet bitch kak dela";
            chat.MessageList.Add(message);
            user.ChatList.Add(chat);

            var popierdolinyUser = UserParser.ParseToDTO(user);
            var parsedUser = UserParser.ParseFromDTO(popierdolinyUser);

            Assert.That(user == parsedUser);
        }
    }
}