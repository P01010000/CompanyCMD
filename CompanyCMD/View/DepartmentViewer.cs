﻿using CompanyCMD.Repository;
using CompanyCMD.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCMD.View
{
    class DepartmentViewer
    {
        private IRepository<Department> Repository;
        public DepartmentViewer(IRepository<Department> Repository)
        {
            this.Repository = Repository;
        }

        public void Print(int id)
        {
            PrintSeparatorTop();
            PrintHead();

            Department d = Repository.Retrieve(id);
            Console.Write("\u2551 ");
            Console.Write(Padding(d.Id.ToString(), 8));
            Console.Write(" \u2551 ");
            Console.Write(Padding(d.Name, 16));
            Console.Write(" \u2551 ");
            Console.Write(Padding(d.Description, 32));
            Console.Write(" \u2551 ");
            Console.Write(Padding(d.Supervisor.ToString(), 12));
            Console.Write(" \u2551 ");
            Console.Write(Padding(d.SuperDepartment.ToString(), 16));
            Console.Write(" \u2551 ");
            Console.Write(Padding(d.CompanyId.ToString(), 8));
            Console.Write(" \u2551\n");

            PrintSeparatorBot();
            Console.WriteLine();
        }

        public void PrintAll()
        {
            PrintSeparatorTop();
            PrintHead();
            foreach (Department d in Repository.RetrieveAll())
            {
                Console.Write("\u2551 ");
                Console.Write(Padding(d.Id.ToString(), 8));
                Console.Write(" \u2551 ");
                Console.Write(Padding(d.Name, 16));
                Console.Write(" \u2551 ");
                Console.Write(Padding(d.Description, 32));
                Console.Write(" \u2551 ");
                Console.Write(Padding(d.Supervisor.ToString(), 12));
                Console.Write(" \u2551 ");
                Console.Write(Padding(d.SuperDepartment.ToString(), 16));
                Console.Write(" \u2551 ");
                Console.Write(Padding(d.CompanyId.ToString(), 16));
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
            Console.Write("Name".PadRight(16, ' '));
            Console.Write(" \u2551 ");
            Console.Write("Description".PadRight(32, ' '));
            Console.Write(" \u2551 ");
            Console.Write("Supervisor".PadRight(12, ' '));
            Console.Write(" \u2551 ");
            Console.Write("SuperDepartment".PadRight(16, ' '));
            Console.Write(" \u2551 ");
            Console.Write("CompanyId".PadRight(16, ' '));
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
            Console.Write("".PadRight(34, '\u2550'));
            Console.Write('\u256b');
            Console.Write("".PadRight(14, '\u2550'));
            Console.Write('\u256b');
            Console.Write("".PadRight(18, '\u2550'));
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
            Console.Write("".PadRight(34, '\u2550'));
            Console.Write('\u2566');
            Console.Write("".PadRight(14, '\u2550'));
            Console.Write('\u2566');
            Console.Write("".PadRight(18, '\u2550'));
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
            Console.Write("".PadRight(34, '\u2550'));
            Console.Write('\u2569');
            Console.Write("".PadRight(14, '\u2550'));
            Console.Write('\u2569');
            Console.Write("".PadRight(18, '\u2550'));
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
