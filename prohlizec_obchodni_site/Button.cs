namespace prohlizec_obchodni_site
{
    internal class Button
    {
        public string Label { get; set; }
        public bool Selected { get; set; } = false;
        public ConsoleColor ButtonColor { get; set; }
        public string? Shortcut { get; private set; }

        public Button(string label, bool selected = false, ConsoleColor color = ConsoleColor.DarkCyan, string? shortcut = null)
        {
            Label = label;
            Selected = selected;
            ButtonColor = color;
            Shortcut = shortcut;
        }

        public void Render(bool white = false)
        {
            if (white)
                Color.White();
            else
                Console.ForegroundColor = ButtonColor;

            string shortcutText = "\0";

            if (Shortcut is not null)
                shortcutText = $" <{Shortcut}>".PadLeft(6);

            if (Selected)
            {
                Color.Black();
                Color.DarkYellow(true);
            }
            Console.Write(Label + shortcutText);
            Color.Reset();
        }

        internal static Button[] CreateButtons(Salesman boss, int focused)
        {
            List<Button> buttons = new List<Button>();

            for (int i = 0; i < boss.Subordinates.Count; i++)
            {
                Salesman sub = boss.Subordinates[i];

                string label = $"{sub.Name} {sub.Surname}";

                Button button = new Button(label);
                buttons.Add(button);
            }

            if (focused >= 0)
                buttons[focused].Selected = true;

            return buttons.ToArray();
        }

        internal static Button[] CreateButtons(SalesmenList? list, int focused)
        {
            List<Button> buttons = new List<Button>();

            for (int i = 0; i < list?.Salesmen.Count; i++)
            {
                Salesman slsm = list.Salesmen[i];

                string label = $"{slsm.Surname}, {slsm.Name} ";

                Button button = new Button(label);
                buttons.Add(button);
            }

            if (list?.Salesmen.Count > 0)
                if (focused >= 0)
                    buttons[focused].Selected = true;

            return buttons.ToArray();
        }
    }
}
