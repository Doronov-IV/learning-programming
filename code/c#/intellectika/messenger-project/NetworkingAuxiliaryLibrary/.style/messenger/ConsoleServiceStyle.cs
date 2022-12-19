using NetworkingAuxiliaryLibrary.Packages;
using NetworkingAuxiliaryLibrary.Style.Common;

namespace NetworkingAuxiliaryLibrary.Style.Messenger
{
    public static class ConsoleServiceStyle
    {


        public static string GetClientMessageStyle(MessagePackage message)
        {
            return $"{ConsoleServiceStyleCommon.GetCurrentTime()} user [green]{message.Sender}[/] says to [green]{message.Reciever}[/]: \"[cyan1]{message.Message as string}[/]\".\n";
        }


        public static string GetLoginReceiptStyle(MessagePackage message)
        {
            return $"{ConsoleServiceStyleCommon.GetCurrentTime()} _login [mediumspringgreen]\"{message.Message as string}\"[/] has been recieved from [purple_1]Authorizer[/].\n";
        }

    }
}
