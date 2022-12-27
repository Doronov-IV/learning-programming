namespace NetworkingAuxiliaryLibrary.Packages
{
    public static class MessageParser
    {
        public static bool IsMessageIdenticalToAnotherOne(IMessage messageOne, IMessage messageTwo)
        {
            bool bMessageMatch = false;
            bool bDateMatch = false;
            bool bTimeMatch = false;
            bool bSenderMatch = false;

            bMessageMatch = messageOne.GetMessage().Equals(messageTwo.GetMessage());
            bDateMatch = messageOne.GetDate().Equals(messageTwo.GetDate());
            bTimeMatch = messageOne.GetTime().Equals(messageTwo.GetTime());
            bSenderMatch = messageOne.GetSender().Equals(messageTwo.GetSender());

            bool result = bMessageMatch && bDateMatch && bTimeMatch && bSenderMatch;

            return result;
        }
    }
}
