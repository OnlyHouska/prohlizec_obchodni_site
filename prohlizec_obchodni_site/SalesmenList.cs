    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    namespace prohlizec_obchodni_site
    {
        internal class SalesmenList
        {
            public bool Saved { get; private set; } = false;
            //public bool Saved { get; private set; } = true;
            public bool DefaultName { get; private set; } = true;
            public List<Salesman> Salesmen { get; private set; }
            public string Name { get; private set; }

            public SalesmenList(List<Salesman> salesmans, string name)
            {
                Salesmen = salesmans;
                Name = name + Program.fileFormat;
            }

            public void ChangeName(string name)
            {
                Name = name + Program.fileFormat;
                DefaultName = false;
            }

            public bool AddSalesman(Salesman salesman)
            {
                Saved = false;

                ArgumentNullException.ThrowIfNull(salesman);

                Salesman slsm = salesman;

                if (!Salesmen.Contains(salesman))
                    Salesmen.Add(slsm);
                else return false;

                return true;
            }

            public bool RemoveSalesman(Salesman salesman)
            {
                Saved = false;

                var tempList = new List<Salesman>(Salesmen);

                if (tempList.Contains(salesman))
                {
                    tempList.Remove(salesman);
                    Salesmen = tempList;
                }
                else return false;
                return true;
            }

            public void SaveList()
            {
                Saved = true;

                if (DefaultName)
                    SaveUnderName();

                if (!File.Exists(Name))
                {
                    using (File.Create(Name)) { }
                }
                string jsonString = JsonSerializer.Serialize(Salesmen);

                File.WriteAllText(Name, jsonString);
            }


            public static SalesmenList LoadList(string name)
            {
                SalesmenList list = new SalesmenList(null, name);
                name += Program.fileFormat;

                if (File.Exists(name))
                {
                    string jsonString = File.ReadAllText(name);

                    list.Salesmen = JsonSerializer.Deserialize<List<Salesman>>(jsonString);
                    list.Saved = true;
                }
                else
                {
                    Console.WriteLine("Nenašel jsem soubor: " + name);
                }

                return list;
            }

            public void SaveUnderName()
            {
                Console.Clear();
                Color.Yellow();
                Browser.display.WriteWithUnderline("Pod jakým názvem ho chceš uložit? (Nech prázdné pro " + Name + ")");
                Console.WriteLine();
                Color.White();
                string? name = Console.ReadLine();

                name = Regex.Replace(name, "[\\\\/:*?\"<>|]", "");
                name = name?.Trim();

                if (!string.IsNullOrEmpty(name))
                    ChangeName(name);
            }
        }
    }
