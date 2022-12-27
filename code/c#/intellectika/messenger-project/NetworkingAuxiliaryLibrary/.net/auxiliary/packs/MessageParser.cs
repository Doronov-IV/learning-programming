namespace NetworkingAuxiliaryLibrary.Packages
{
    public static class MessageParser
    {
        public static bool IsMessageIdenticalToAnotherOne(IMessage messageOne, IMessage messageTwo)
        {
            return
            (
                   messageOne.GetMessage().Equals(messageTwo.GetMessage())
                && messageOne.GetDate().Equals(messageTwo.GetDate())
                && messageOne.GetTime().Equals(messageTwo.GetTime())
                && messageOne.GetSender().Equals(messageTwo.GetSender())
            );
        }
    }
}
