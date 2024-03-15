using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohlizec_obchodni_site
{
    internal class Control
    {
        public string Key { get; private set; }
        public string Description { get; private set; }

        public Control(string name, string description)
        {
            Key = name;
            Description = description;
        }

        public void Render()
        {
            Console.WriteLine($"<{Key}> {Description}");
        }
    }
}
