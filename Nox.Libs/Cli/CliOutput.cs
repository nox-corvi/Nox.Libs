using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nox.Libs.Cli
{
    public class Layout
    {
        public static readonly Layout Default;

        #region Properties
        public ConsoleColor Text;
        public ConsoleColor HighLight;

        public ConsoleColor Prompt;
        public ConsoleColor Layer;

        public ConsoleColor StateInfo;
        public ConsoleColor StateWarn;
        public ConsoleColor StateFail;
        public ConsoleColor StateDone;
        #endregion


        static Layout()
        {
            Default = new Layout()
            {
                Text = ConsoleColor.Gray,
                HighLight = ConsoleColor.White,

                Prompt = ConsoleColor.Cyan,
                Layer = ConsoleColor.Green,

                StateInfo = ConsoleColor.DarkMagenta,
                StateWarn = ConsoleColor.DarkYellow,
                StateFail = ConsoleColor.DarkRed,
                StateDone = ConsoleColor.DarkGreen,
            };
        }
    }

    public class ConPrint
    {
        public enum StateEnum
        {
            done,
            fail,
            warn,

            info,
            none
        }

        public Layout __layout = Layout.Default;

        public void Prompt(string Layer = "")
        {
            Console.ForegroundColor = __layout.Prompt;
            Console.Write($"\r# ");

            Console.ForegroundColor = __layout.Layer;
            Console.Write(Layer);

            Console.ForegroundColor = __layout.Prompt;
            Console.Write($"> ");

            Console.ResetColor();
        }

        public void Print(string Message, bool HighLight = false)
        {
            Prompt();

            if (HighLight)
                Console.ForegroundColor = __layout.HighLight;
            else
                Console.ForegroundColor = __layout.Text;

            Console.Write(Message);

            Console.ResetColor();
        }

        public void PrintLine(string Message) =>
            Print(Message + "\r\n");

        public void PrintError(string Message) =>
            Console.Error.WriteLine(Message);


        public void PrintText(string Text, bool HighLight = false)
        {
            if (HighLight)
                Console.ForegroundColor = __layout.HighLight;
            else
                Console.ForegroundColor = __layout.Text;

            Console.Write(Text);

            Console.ResetColor();
        }

        public void LF() => Console.WriteLine();

        public void PrintWithState(string Message, StateEnum StateValue)
        {
            Prompt();

            Console.ForegroundColor = __layout.Text;
            Console.Write(Message);

            this.PrintState(StateValue);
        }

        private string GetStateText(StateEnum StateValue)
        {
            switch (StateValue)
            {
                case StateEnum.done:
                    return " OK ";
                case StateEnum.fail:
                    return "FAIL";
                case StateEnum.warn:
                    return "WARN";
                case StateEnum.info:
                    return "INFO";
                default:
                    return " -- ";
            }
        }

        private void SetStateColor(StateEnum StateValue)
        {
            switch (StateValue)
            {
                case StateEnum.done:
                    Console.ForegroundColor = __layout.StateDone;
                    break;
                case StateEnum.fail:
                    Console.ForegroundColor = __layout.StateFail;
                    break;
                case StateEnum.warn:
                    Console.ForegroundColor = __layout.StateWarn;
                    break;
                case StateEnum.info:
                    Console.ForegroundColor = __layout.StateInfo;
                    break;
                default:
                    Console.ForegroundColor = __layout.Text;
                    break;
            }
        }

        public void PrintState(StateEnum StateValue, bool LineFeed = true)
        {
            string StateText = $"[ {GetStateText(StateValue)} ]";

            Console.CursorLeft = Console.WindowWidth - StateText.Length - 1;

            Console.ForegroundColor = __layout.Text;
            Console.Write("[");

            SetStateColor(StateValue);
            Console.Write(GetStateText(StateValue));

            Console.ForegroundColor = __layout.Text;
            Console.Write("]");

            Console.ResetColor();

            if (LineFeed)
                Console.WriteLine();
        }
    }
}
