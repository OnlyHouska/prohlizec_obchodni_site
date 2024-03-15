using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohlizec_obchodni_site
{
    internal class Input
    {
        public static void CheckForInput(ConsoleKey key)
        {
            while (Console.ReadKey(true).Key != key) ;
        }

        public static (int, bool) CheckForInputs(ConsoleKey[] keys, ConsoleKey comboKey = ConsoleKey.Tab, ConsoleModifiers modifier = ConsoleModifiers.Shift)
        {
            while (!Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (CheckForKeyCombo(key, comboKey, modifier))
                    return (-1, true);
                else if (keys.Contains(key.Key))
                    return (Array.IndexOf(keys, key.Key), false);
            }
            return (-1, false);
        }

        public static int CheckForInputs(ConsoleKey[] keys)
        {
            while (!Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (keys.Contains(key))
                    return Array.IndexOf(keys, key);
            }
            return -1;
        }

        private static bool CheckForKeyCombo(ConsoleKeyInfo keyInfo, ConsoleKey inputKey, ConsoleModifiers modifier)
        {
            if (keyInfo.Key == inputKey && (keyInfo.Modifiers & modifier) != 0)
                return true;
            return false;
        }
    }
}
