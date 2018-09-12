using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyCMD.Models;

namespace CompanyCMD.View
{
    class EmployeeViewer2 : IViewer<Employee>
    {
        public EmployeeViewer2() : base(new int[] { 8, 8, 16, 16, 12, 12, 10, 12 })
        {
        }

        public override void Print(IEnumerable<Employee> list)
        {
            Console.Clear();
            PrintLine(new string[] { "", "", "", "", "", "", "", "" }, '\u2554', '\u2566', '\u2557', '\u2550');
            PrintLine(new string[] { "Id", "PersonId", "LastName", "FirstName", "Birthday", "Phone", "Gender", "EmployeeSince" }, '\u2551', '\u2551', '\u2551', ' ');
            PrintLine(new string[] { "", "", "", "", "", "", "", "" }, '\u2560', '\u256b', '\u2563', '\u2550');
            List<string> strings;
            foreach (Employee e in list)
            {
                strings = new List<string>();
                strings.Add(e.Id.ToString());
                strings.Add(e.PersonId.ToString());
                strings.Add(e.LastName.ToString());
                strings.Add(e.FirstName.ToString());
                strings.Add(e.Birthday != null ? ((DateTime)e.Birthday).ToString("yyyy-MM-dd") : "");
                strings.Add(e.Phone.ToString());
                strings.Add(e.Gender.ToString());
                strings.Add(e.EmployeeSince != null ? ((DateTime)e.EmployeeSince).ToString("yyyy-MM-dd") : "");
                PrintLine(strings, '\u2551', '\u2551', '\u2551', ' ');
            }
            PrintLine(new string[] { "", "", "", "", "", "", "", "" }, '\u255a', '\u2569', '\u255d', '\u2550');
        }
    }
}
