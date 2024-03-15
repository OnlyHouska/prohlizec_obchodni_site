
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace prohlizec_obchodni_site
{
    internal class Browser
    {
        public static Display display = new Display(Program.boss);

        internal static void MainMenu(Salesman salesman, SalesmenList? list = null)
        {
            display.CurrentSalesman = salesman;
            RenderMenu(list);
        }

        private static void RenderMenu(SalesmenList? list = null)
        {
            Salesman salesman = display.CurrentSalesman;

            List<Salesman> supervisors = Tree.FindSupervisors(Program.boss, salesman.Name, salesman.Surname);
            Salesman supervisor;
            if (supervisors.Count == 0)
                supervisor = salesman;
            else
                supervisor = supervisors[supervisors.Count - 1];

            Button goUpBtn = new Button("Přejít nahoru");
            Button listBtn = new Button("Přejít na seznam");
            Button bossBtn = new Button("Přejít na šéfa stromu");

            Button actionBtn = new Button("action");
            if (list is not null)
            {
                actionBtn.Label = list.Salesmen.Contains(salesman) ? "Odebrat" : "Přidat";
                actionBtn.ButtonColor = list.Salesmen.Contains(salesman) ? ConsoleColor.Red : ConsoleColor.Green;
            }

            Button exitBtn = new Button("[Exit]", false, ConsoleColor.Red, "Esc");


            List<Button> buttons = [goUpBtn, listBtn, bossBtn];
            if (list is not null)
            {
                buttons.Add(actionBtn);
            }
            buttons.Add(exitBtn);

            bool focusingSubordinates = false;
            int focused = 0;
            int focusedSub = -1;

            do
            {
                Color.Gray();
                if (focused < 0)
                    foreach (var btn in buttons)
                        btn.Selected = false;
                else
                    buttons[focused].Selected = true;

                Console.Clear();
                display.WriteWithUnderline(null, [goUpBtn, listBtn, bossBtn]);
                Console.WriteLine("\n");
                display.WriteWithUnderline($"Obchodník: {salesman.Name} {salesman.Surname}");
                Display.setPos(Console.CursorLeft, Console.CursorTop - 1);
                Console.Write("".PadLeft(5));

                if (list is not null)
                {
                    actionBtn.Render();
                }

                Console.WriteLine($"\n\nPřímé prodeje: {Display.Money(salesman.Sales)}");
                Console.WriteLine($"Celkové prodeje sítě: {Display.Money(Tree.GetSales(salesman))}");

                if (supervisors.Count > 0)
                {
                    Console.Write($"\nNadřízený: ");
                    Console.WriteLine(supervisor.Name + " " + supervisor.Surname);
                }
                Console.Write($"\nPodřízení: ");
                //Render subordinates
                if (salesman.Subordinates.Count > 0)
                    display.Buttons(Button.CreateButtons(salesman, focusedSub), true, true, true);
                else
                    Console.Write("Nenašel jsem žádné podřízené");

                if (salesman.Subordinates.Count > 0)
                    display.DisplayControls(sectionSwitch: true);
                else
                    display.DisplayControls(sectionSwitch: false);


                Console.WriteLine();
                exitBtn.Render();

                int input = -1;
                bool combo = false;
                if (salesman.Subordinates.Count > 0)
                    (input, combo) = Input.CheckForInputs([ConsoleKey.Tab, ConsoleKey.Enter, ConsoleKey.Escape, ConsoleKey.Backspace, ConsoleKey.UpArrow], ConsoleKey.Tab, ConsoleModifiers.Shift);
                else
                    input = Input.CheckForInputs([ConsoleKey.Tab, ConsoleKey.Enter, ConsoleKey.Escape, ConsoleKey.Backspace, ConsoleKey.UpArrow]);

                if (combo)
                {
                    if (focusingSubordinates)
                    {
                        focused = 0;
                        focusingSubordinates = false;
                        focusedSub = -1;
                    }
                    else
                    {
                        focused = -1;
                        focusingSubordinates = true;
                        focusedSub = 0;
                    }
                }

                //Tab
                if (input == 0)
                {
                    if (focusingSubordinates)
                    {
                        if (focusedSub + 1 < salesman.Subordinates.Count)
                            focusedSub++;
                        else
                            focusedSub = 0;
                    }
                    else
                    {

                        if (focused + 1 < buttons.Count)
                        {
                            if (focused < 0) focused = 0;
                            buttons[focused].Selected = false;
                            focused++;
                            buttons[focused].Selected = true;
                        }
                        else
                        {
                            buttons[focused].Selected = false;
                            focused = 0;
                            buttons[focused].Selected = true;
                        }
                    }

                }
                //Enter key
                else if (input == 1)
                {
                    if (focusingSubordinates)
                    {
                        if (salesman.Subordinates.Count > 0)
                            display.CurrentSalesman = Tree.GetSalesman(salesman, focusedSub);
                    }

                    focusingSubordinates = false;
                    switch (focused)
                    {
                        case 0:
                            display.CurrentSalesman = supervisor;
                            break;
                        case 1:
                            display.CurrentSalesman = Program.boss;
                            RenderFileMenu(list);
                            break;
                        case 2:
                            display.CurrentSalesman = Program.boss;
                            break;
                        case 3:
                            if (list is not null)
                            {
                                if (list.Salesmen.Contains(salesman))
                                    list.RemoveSalesman(salesman);
                                else
                                    list.AddSalesman(salesman);
                            }
                            else Exit(list);
                            break;
                        case 4:
                            if (supervisors.Count > 0)
                                display.CurrentSalesman = supervisor;
                            break;
                        case 5:
                            Exit(list);
                            break;
                        default:
                            break;
                    }
                    break;
                }
                //Backspace
                else if (input == 3)
                {
                    display.CurrentSalesman = Program.boss;
                    RenderFileMenu(list);
                }
                //UpArrow
                else if (input == 4)
                {
                    display.CurrentSalesman = supervisor;
                    break;
                }
                else
                {
                    if (!combo)
                        Exit(list);
                }

            } while (true);
            RenderMenu(list);
        }

        public static void RenderFileMenu(SalesmenList? list = null)
        {
            Salesman salesman = display.CurrentSalesman;

            Button createBtn = new Button("Založit");
            Button loadBtn = new Button("Načíst");
            Button saveBtn = new Button("Uložit");
            Button explorerBtn = new Button("Přejít na prohlížeč");
            Button exitBtn = new Button("[Exit]", false, ConsoleColor.Red, "Esc");

            List<Button> buttons;

            bool focusingPeople = false;
            int focused = 0;
            int focusedPerson = -1;
            do
            {
                if (list is not null)
                    buttons = [createBtn, loadBtn, saveBtn, explorerBtn, exitBtn];
                else
                    buttons = [createBtn, loadBtn, explorerBtn, exitBtn];

                bool loaded = false;
                if (list is not null) loaded = true;

                Color.Gray();
                if (focused < 0)
                    foreach (var btn in buttons)
                        btn.Selected = false;
                else
                    buttons[focused].Selected = true;

                Console.Clear();
                if (loaded)
                    display.WriteWithUnderline(null, [createBtn, loadBtn, saveBtn, explorerBtn]);
                else
                    display.WriteWithUnderline(null, [createBtn, loadBtn, explorerBtn]);

                if (loaded)
                    display.WriteWithUnderline($"\nSeznam: {list?.Name}");


                if (loaded)
                {
                    //Render subordinates
                    Console.WriteLine("\n");
                    display.Buttons(Button.CreateButtons(list, focusedPerson), true, true);
                }

                if (loaded && list?.Salesmen.Count > 0)
                    display.DisplayControls(sectionSwitch: true, browserRender: false);
                else
                    display.DisplayControls(sectionSwitch: false, browserRender: false);

                Console.WriteLine();
                exitBtn.Render();

                int input = -1;
                bool combo = false;
                List<ConsoleKey> keys = [ConsoleKey.Tab, ConsoleKey.Enter, ConsoleKey.Escape, ConsoleKey.Backspace, ConsoleKey.UpArrow];

                if (list is not null && list.Salesmen.Count > 0)
                    (input, combo) = Input.CheckForInputs(keys.ToArray(), ConsoleKey.Tab, ConsoleModifiers.Shift);
                else
                    input = Input.CheckForInputs(keys.ToArray());

                if (combo)
                {
                    if (focusingPeople)
                    {
                        focused = 0;
                        focusingPeople = false;
                        focusedPerson = -1;
                    }
                    else
                    {
                        focused = -1;
                        focusingPeople = true;
                        focusedPerson = 0;
                    }
                }

                //Tab
                if (input == 0)
                {
                    if (focusingPeople)
                    {
                        if (focusedPerson + 1 < list?.Salesmen.Count)
                            focusedPerson++;
                        else
                            focusedPerson = 0;
                    }
                    else
                    {

                        if (focused + 1 < buttons.Count)
                        {
                            buttons[focused].Selected = false;
                            focused++;
                            buttons[focused].Selected = true;
                        }
                        else
                        {
                            buttons[focused].Selected = false;
                            focused = 0;
                            buttons[focused].Selected = true;
                        }
                    }

                }
                //Enter key
                else if (input == 1)
                {
                    if (focusingPeople)
                    {
                        if (list?.Salesmen.Count > 0)
                        {
                            display.CurrentSalesman = list.Salesmen[focusedPerson];
                            RenderMenu(list);
                        }
                    }

                    switch (focused)
                    {
                        case 0:
                            list = CreateList(list);
                            break;
                        case 1:
                            list = LoadList(list);
                            break;
                        case 2:
                            if (loaded)
                                list?.SaveList();
                            else
                            {
                                display.CurrentSalesman = salesman;
                                RenderMenu(list);
                            }
                            break;
                        case 3:
                            if (loaded)
                            {
                                display.CurrentSalesman = salesman;
                                RenderMenu(list);
                            }
                            else
                                Exit(list);
                            break;
                        case 4:
                            Exit(list);
                            break;
                        default:
                            break;
                    }
                }
                else if (input == 3)
                {
                    display.CurrentSalesman = Program.boss;
                    RenderMenu(list);
                }
                else
                {
                    if (!combo)
                        Exit(list);
                }
            } while (true);
        }

        private static SalesmenList? LoadList(SalesmenList list)
        {
            Console.Clear();
            Color.Yellow();
            display.WriteWithUnderline("Napiš název souboru (Nech prázdné pro návrat)");
            Console.WriteLine();
            Color.White();
            string name = Console.ReadLine();

            name = Regex.Replace(name, "[\\\\/:*?\"<>|]", "");
            name = name?.Trim();

            if (string.IsNullOrEmpty(name))
                return list;

            return SalesmenList.LoadList(name);
        }

        private static SalesmenList CreateList(SalesmenList list)
        {
            if (list is not null && !list.Saved)
            {
                int focused = 0;

                Button yesBtn = new Button("Ano", true, ConsoleColor.Green);
                Button noBtn = new Button("Ne", false, ConsoleColor.Red);

                List<Button> buttons = [yesBtn, noBtn];

                do
                {
                    Console.Clear();
                    Color.Yellow();
                    display.WriteWithUnderline("Jeden neuložený seznam je již aktivní. Chceš ho uložit před vytvořením nového?");
                    Console.WriteLine("\n");

                    if (focused < 0)
                        foreach (var btn in buttons)
                            btn.Selected = false;
                    else
                        buttons[focused].Selected = true;

                    display.Buttons(buttons.ToArray(), true);

                    display.DisplayControls(sectionSwitch: false, displaySwitch: false, new Control("Backspace", "Zpět"));

                    List<ConsoleKey> keys = [ConsoleKey.Tab, ConsoleKey.Enter, ConsoleKey.Backspace];

                    int input = Input.CheckForInputs(keys.ToArray());

                    //TAB
                    if (input == 0)
                    {
                        if (focused + 1 < buttons.Count)
                        {
                            buttons[focused].Selected = false;
                            focused++;
                            buttons[focused].Selected = true;
                        }
                        else
                        {
                            buttons[focused].Selected = false;
                            focused = 0;
                            buttons[focused].Selected = true;
                        }
                    }
                    //ENTER
                    else if (input == 1)
                    {
                        if (focused == 0)
                            list.SaveList();
                        break;
                    }
                    //BACKSPACE
                    else if (input == 2)
                    {
                        RenderFileMenu(list);
                    }

                } while (true);

            }

            int name;
            do
            {
                name = Program.rnd.Next(1, 9999999);
            } while (File.Exists(name + Program.fileFormat));
            list = new SalesmenList([], name.ToString());
            return list;
        }

        private static void Exit(SalesmenList list, bool force = false)
        {
            if (force || list is null || list.Saved)
                Environment.Exit(0);

            int focused = 0;

            Button yesBtn = new Button("Ano", true, ConsoleColor.Green);
            Button noBtn = new Button("Ne", false, ConsoleColor.Red);

            List<Button> buttons = [yesBtn, noBtn];

            do
            {
                Console.Clear();
                Color.Yellow();
                display.WriteWithUnderline("Nemáš uložený seznam. Chceš ho uložit?");
                Console.WriteLine("\n");

                if (focused < 0)
                    foreach (var btn in buttons)
                        btn.Selected = false;
                else
                    buttons[focused].Selected = true;

                display.Buttons(buttons.ToArray(), true);

                display.DisplayControls(sectionSwitch: false, displaySwitch: false, new Control("Backspace", "Zpět"));

                List<ConsoleKey> keys = [ConsoleKey.Tab, ConsoleKey.Enter, ConsoleKey.Backspace];

                int input = Input.CheckForInputs(keys.ToArray());

                //TAB
                if (input == 0)
                {
                    if (focused + 1 < buttons.Count)
                    {
                        buttons[focused].Selected = false;
                        focused++;
                        buttons[focused].Selected = true;
                    }
                    else
                    {
                        buttons[focused].Selected = false;
                        focused = 0;
                        buttons[focused].Selected = true;
                    }
                }
                //ENTER
                else if (input == 1)
                {
                    if (focused == 0)
                    {
                        list.SaveList();
                    }

                    Exit(list, force: true);
                }
                //BACKSPACE
                else if (input == 2)
                {
                    RenderMenu(list);
                }

            } while (true);
        }
    }
}