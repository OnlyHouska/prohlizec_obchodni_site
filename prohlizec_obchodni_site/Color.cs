using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohlizec_obchodni_site
{
    internal class Color
    {
        private static ConsoleColor _color;
        public static void Reset()
        {
            Console.ResetColor();
        }
        public static void Black(bool bg = false)
        {
            _color = ConsoleColor.Black;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void Blue(bool bg = false)
        {
            _color = ConsoleColor.Blue;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void Cyan(bool bg = false)
        {
            _color = ConsoleColor.Cyan;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void DarkBlue(bool bg = false)
        {
            _color = ConsoleColor.DarkBlue;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void DarkCyan(bool bg = false)
        {
            _color = ConsoleColor.DarkCyan;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void DarkGray(bool bg = false)
        {
            _color = ConsoleColor.DarkGray;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void DarkGreen(bool bg = false)
        {
            _color = ConsoleColor.DarkGreen;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void DarkMagenta(bool bg = false)
        {
            _color = ConsoleColor.DarkMagenta;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void DarkRed(bool bg = false)
        {
            _color = ConsoleColor.DarkRed;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void DarkYellow(bool bg = false)
        {
            _color = ConsoleColor.DarkYellow;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void Gray(bool bg = false)
        {
            _color = ConsoleColor.Gray;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void Green(bool bg = false)
        {
            _color = ConsoleColor.Green;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void Magenta(bool bg = false)
        {
            _color = ConsoleColor.Magenta;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void Red(bool bg = false)
        {
            _color = ConsoleColor.Red;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void White(bool bg = false)
        {
            _color = ConsoleColor.White;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }

        public static void Yellow(bool bg = false)
        {
            _color = ConsoleColor.Yellow;
            if (!bg)
                Console.ForegroundColor = _color;
            else
                Console.BackgroundColor = _color;
        }
    }
}
