using Tools.Formatting;

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
            bTimeMatch = IsTimeAproximatelyEqual(messageOne.GetTime(), messageTwo.GetTime());
            bSenderMatch = messageOne.GetSender().Equals(messageTwo.GetSender());

            bool result = bMessageMatch && bDateMatch && bTimeMatch && bSenderMatch;

            return result;
        }

        public static bool IsTimeAproximatelyEqual(string timeOne, string timeTwo)
        {
            bool bRes = false;

            int nTimeOne = Int32.Parse(StringDateTime.RemoveSeparation(timeOne));
            int nTimeTwo = Int32.Parse(StringDateTime.RemoveSeparation(timeTwo));

            return Math.Abs(nTimeTwo - nTimeOne) < 3;
        }
    }
}
