using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohlizec_obchodni_site
{
    internal class Tree
    {
        public static Salesman? GetSalesman(Salesman boss, int sub)
        {
            return FindSalesman(Program.boss, boss.Subordinates[sub].Name, boss.Subordinates[sub].Surname);
        }
        public static int GetSales(Salesman root)
        {
            int totalSales = root.Sales;

            foreach (Salesman sub in root.Subordinates)
            {
                totalSales += GetSales(sub);
            }

            return totalSales;
        }
        private static Salesman? FindSalesman(Salesman? root, string? name, string? surname)
        {
            //když jsem na hledané osobě, vrať
            if (root.Name == name && root.Surname == surname)
            {
                return root;
            }

            //zkus hledat mezi podřízenými
            foreach (Salesman s in root.Subordinates)
            {
                Salesman found = FindSalesman(s, name, surname);
                if (found != null)
                    return found;
            }

            //nenašel jsi? vrať null
            return null;
        }

        public static List<Salesman> FindSupervisors(Salesman root, string name, string surname)
        {
            List<Salesman> supervisors = new List<Salesman>();
            FindSupervisorsRekurze(root, name, surname, supervisors);
            return supervisors;
        }


        private static bool FindSupervisorsRekurze(Salesman current, string name, string surname, List<Salesman> supervisors)
        {
            if (current.Name == name && current.Surname == surname)
            {
                return true;
            }

            foreach (Salesman subordinate in current.Subordinates)
            {
                if (FindSupervisorsRekurze(subordinate, name, surname, supervisors))
                {
                    supervisors.Insert(0, current);
                    return true;
                }
            }

            return false;
        }
    }
}
