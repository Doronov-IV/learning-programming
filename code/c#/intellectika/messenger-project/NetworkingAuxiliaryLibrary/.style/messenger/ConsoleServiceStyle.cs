using NetworkingAuxiliaryLibrary.Packages;
using NetworkingAuxiliaryLibrary.Style.Common;

namespace NetworkingAuxiliaryLibrary.Style.Messenger
{
    public static class ConsoleServiceStyle
    {


        public static string GetClientMessageStyle(IMessage message)
        {
            return $"{ConsoleServiceStyleCommon.GetCurrentTime()} user [green]{message.GetSender()}[/] says to [green]{message.GetReciever()}[/]: \"[cyan1]{message.GetMessage() as string}[/]\".\n";
        }


        public static string GetLoginReceiptStyle(IMessage message)
        {
            return $"{ConsoleServiceStyleCommon.GetCurrentTime()} login [mediumspringgreen]\"{message.GetMessage() as string}\"[/] has been recieved from [purple_1]Authorizer[/].\n";
        }

    }
}
