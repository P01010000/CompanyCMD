using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyCMD.Models;

namespace CompanyCMD.View
{
    class CompanyViewer2 : IViewer<Company>
    {
        public CompanyViewer2() : base(new int[] { 8, 16, 16, 32, 12, 16 })
        {
        }

        public override void Print(IEnumerable<Company> list)
        {
            Console.Clear();
            PrintLine(new string[] { "", "", "", "", "", "" }, '\u2554', '\u2566', '\u2557', '\u2550');
            PrintLine(new string[] { "Id", "PersonId", "Name", "Description", "FoundedAt", "Branch" }, '\u2551', '\u2551', '\u2551', ' ');
            PrintLine(new string[] { "", "", "", "", "", "" }, '\u2560', '\u256b', '\u2563', '\u2550');
            List<string> strings;
            foreach (Company c in list)
            {
                strings = new List<string>();
                strings.Add(c.Id.ToString());
                strings.Add(c.PersonId.ToString());
                strings.Add(c.Name.ToString());
                strings.Add(c.Description.ToString());
                strings.Add(c.FoundedAt.ToString("yyyy-MM-dd"));
                strings.Add(c.Branch.ToString());
                PrintLine(strings, '\u2551', '\u2551', '\u2551', ' ');
            }
            PrintLine(new string[] { "", "", "", "", "", "" }, '\u255a', '\u2569', '\u255d', '\u2550');
        }
    }
}
