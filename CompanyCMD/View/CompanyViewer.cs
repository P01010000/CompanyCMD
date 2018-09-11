using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyCMD.Models;
using CompanyCMD.Repository;

namespace CompanyCMD.View
{
    class CompanyViewer
    {
        private IRepository<Company> Repository;
        public CompanyViewer(IRepository<Company> Repository)
        {
            this.Repository = Repository;
        }

        public void Print(int id)
        {
            PrintSeparatorTop();
            PrintHead();

            Company c = Repository.Retrieve(id);
            Console.Write("\u2551 ");
            Console.Write(Padding(c.Id.ToString(), 8));
            Console.Write(" \u2551 ");
            Console.Write(Padding(c.PersonId.ToString(), 16));
            Console.Write(" \u2551 ");
            Console.Write(Padding(c.Name, 16));
            Console.Write(" \u2551 ");
            Console.Write(Padding(c.Description, 32));
            Console.Write(" \u2551 ");
            Console.Write(Padding(c.FoundedAt.ToString("yyyy-MM-dd"), 12));
            Console.Write(" \u2551 ");
            Console.Write(Padding(c.Branch, 16));
            Console.Write(" \u2551\n");

            PrintSeparatorBot();
            Console.WriteLine();
        }

        public void PrintAll()
        {
            PrintSeparatorTop();
            PrintHead();
            foreach(Company c in Repository.RetrieveAll())
            {
                Console.Write("\u2551 ");
                Console.Write(Padding(c.Id.ToString(), 8));
                Console.Write(" \u2551 ");
                Console.Write(Padding(c.PersonId.ToString(), 16));
                Console.Write(" \u2551 ");
                Console.Write(Padding(c.Name, 16));
                Console.Write(" \u2551 ");
                Console.Write(Padding(c.Description, 32));
                Console.Write(" \u2551 ");
                Console.Write(Padding(c.FoundedAt.ToString("yyyy-MM-dd"), 12));
                Console.Write(" \u2551 ");
                Console.Write(Padding(c.Branch, 16));
                Console.Write(" \u2551\n");
            }
            PrintSeparatorBot();
            Console.WriteLine();
        }

        private void PrintHead()
        {
            Console.Write("\u2551 ");
            Console.Write("Id".PadRight(8, ' '));
            Console.Write(" \u2551 ");
            Console.Write("PersonId".PadRight(16, ' '));
            Console.Write(" \u2551 ");
            Console.Write("Name".PadRight(16, ' '));
            Console.Write(" \u2551 ");
            Console.Write("Description".PadRight(32, ' '));
            Console.Write(" \u2551 ");
            Console.Write("FoundedAt".PadRight(12, ' '));
            Console.Write(" \u2551 ");
            Console.Write("Branch".PadRight(16, ' '));
            Console.Write(" \u2551\n");
            PrintSeparator();
        }

        private void PrintSeparator()
        {
            Console.Write('\u2560');
            Console.Write("".PadRight(10, '\u2550'));
            Console.Write('\u256b');
            Console.Write("".PadRight(18, '\u2550'));
            Console.Write('\u256b');
            Console.Write("".PadRight(18, '\u2550'));
            Console.Write('\u256b');
            Console.Write("".PadRight(34, '\u2550'));
            Console.Write('\u256b');
            Console.Write("".PadRight(14, '\u2550'));
            Console.Write('\u256b');
            Console.Write("".PadRight(18, '\u2550'));
            Console.Write("\u2563\n");
        }

        private void PrintSeparatorTop()
        {
            Console.Write('\u2554');
            Console.Write("".PadRight(10, '\u2550'));
            Console.Write('\u2566');
            Console.Write("".PadRight(18, '\u2550'));
            Console.Write('\u2566');
            Console.Write("".PadRight(18, '\u2550'));
            Console.Write('\u2566');
            Console.Write("".PadRight(34, '\u2550'));
            Console.Write('\u2566');
            Console.Write("".PadRight(14, '\u2550'));
            Console.Write('\u2566');
            Console.Write("".PadRight(18, '\u2550'));
            Console.Write("\u2557\n");
        }

        private void PrintSeparatorBot()
        {
            Console.Write('\u255a');
            Console.Write("".PadRight(10, '\u2550'));
            Console.Write('\u2569');
            Console.Write("".PadRight(18, '\u2550'));
            Console.Write('\u2569');
            Console.Write("".PadRight(18, '\u2550'));
            Console.Write('\u2569');
            Console.Write("".PadRight(34, '\u2550'));
            Console.Write('\u2569');
            Console.Write("".PadRight(14, '\u2550'));
            Console.Write('\u2569');
            Console.Write("".PadRight(18, '\u2550'));
            Console.Write("\u255d\n");
        }

        private string Padding(string value, int length)
        {
            return value.Length > length ? value.Substring(0, length - 2) + ".." : value.PadRight(length, ' ');
        }
    }
}
