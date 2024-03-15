using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace prohlizec_obchodni_site
{
    internal class Display
    {
        public Salesman CurrentSalesman {  get; set; }

        public Display(Salesman current)
        {
            CurrentSalesman = current;
        }

        public void Init()
        {
            Console.CursorVisible = false;
        }
        public static void setPos(int X, int Y, bool center = false)
        {
            if (center)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                return;
            }
            Console.SetCursorPosition(X, Y);
        }

        public void Buttons(Button[] buttons, bool vertical = false, bool white = false, bool isSub = false)
        {
            if (vertical)
            {
                for (int i = 1; i < buttons.Length + 1; i++)
                {
                    if (isSub)
                    {
                        if (i > 1)
                            setPos("Podřízení: ".Length, Console.CursorTop + 1);
                        else if (i == 1)
                            setPos("Podřízení: ".Length, Console.CursorTop);
                        buttons[i - 1].Render(white);
                    }
                    else
                    {
                        buttons[i - 1].Render(white);
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].Render(white);
                    
                    if (!(i >= buttons.Length - 1))
                    {
                        Color.White();
                        Console.Write(" | ");
                    }
                }
            }
        }
        public void WriteWithUnderline(string? text = null, Button[]? buttons = null)
        {
            //Render text
            if (buttons is null && text is not null)
            {
                Console.WriteLine(text);
                for (int i = 0; i < text.Length; i++)
                    Console.Write("-");
            } 
            //Render buttons
            else if (text is null && buttons is not null)
            {
                Buttons(buttons);
                Console.WriteLine();
                for (int i = 0; i < buttons.Length; i++)
                {
                    for (int j = 0; j < buttons[i].Label.Length; j++)
                        Console.Write("-");
                    if (i < buttons.Length - 1)
                       Console.Write("---");
                }
            }
            //Both are null
            else if (buttons is null && text is null)
            {
                throw new NullReferenceException();
            }
            //None is null
            else
            {
                throw new Exception($"Only one value is allowed. Current values:  {text}  &&  {buttons}");
            }
        }

        public static string Money(int money)
        {
            return money + " $";
        }

        private static Control _changeFocus = new Control("Tab", "Změnit výběr");
        private static Control _choose = new Control("Enter", "Zvolit");
        private static Control _switchSection = new Control("Shift + Tab", "Přepnout sekci");
        private static Control _switchDisplay = new Control("Backpace", "Přepnout zobrazení");
        private static Control _goUp = new Control("Up Arrow", "Přejít nahoru");
        public void DisplayControls(bool sectionSwitch = false, bool displaySwitch = true, Control custom = null, bool browserRender = true)
        {
            Color.DarkGray();
            Console.WriteLine("\n");
            
            _choose.Render();
            _changeFocus.Render();
            if (sectionSwitch)
            {
                _switchSection.Render();
            }

            if (displaySwitch)
            {
                _switchDisplay.Render();
            }
            if (browserRender)
            {
                _goUp.Render();
            }

            if (custom is not null)
                custom.Render();

            Color.White();
        }
    }
}
