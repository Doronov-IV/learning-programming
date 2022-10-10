using System;
namespace MainNetworkingProject.Misc
{
    /// <summary>
    /// A simple set of tools to provide objects with ability to be set up in different I/O conditions;
    /// <br />
    /// Простой набор инструментов, чтобы предоставить объектам возмонжость быть использоваными в разных системах ввода/вывода;
    /// </summary>
    public interface IPendingOutput
    {

        public delegate void OutputDelegate(string sOutputMessage);


        public event OutputDelegate SendOutput;


        /// <summary>
        /// Delay text message output till you'll be able to pass it out somewhere;
        /// <br />
        /// How to: In a constructor of a wrapper-class (or ui-class) you should '+=' it to the handler method;
        /// <br />
        /// <br />
        /// Отложить вывод сообщения до тех пор, пока вы не сможете его где-нибудь вывести;
        /// <br />
        /// Как работает: В конструкторе класса-обёртки (или класса-визуального-интерфейса) передать его методу-обработчику через "+="; 
        /// </summary>
        public void PendOutput(string Message);

    }
}
