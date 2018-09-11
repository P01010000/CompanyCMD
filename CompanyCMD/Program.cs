using CompanyCMD.Repository;
using CompanyCMD.Models;
using CompanyCMD.View;
using CompanyCMD.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start by using the company controller
            IController cont = new CompanyController(CompanyRepository.getInstance(), new CompanyViewer2());

            for (char k; (k = Console.ReadKey().KeyChar) != 'x';)
            {
                switch (k)
                {
                    case '1':
                        cont.POST();
                        break;
                    case '2':
                        cont.GET();
                        break;
                    case '3':
                        cont.PATCH();
                        break;
                    case '4':
                        cont.DELETE();
                        break;
                    case 'c':
                    case 'C':
                        cont = new CompanyController(CompanyRepository.getInstance(), new CompanyViewer2());
                        break;
                    case 'e':
                    case 'E':
                        cont = new EmployeeController(EmployeeRepository.getInstance(), new EmployeeViewer2());
                        break;
                    case 'a':
                    case 'A':
                        //cont = new AddressController(AddressRepository.getInstance(), new AddressViewer2());
                        break;
                    case 'd':
                    case 'D':
                        //cont = new DepartmentController(DepartmentRepository.getInstance(), new DepartmentViewer2());
                        break;
                }
            }

            Environment.Exit(0);

            IEnumerable<Address> d = AddressRepository.getInstance().RetrieveAll();
            /*IRepository<object> repository;
            switch(Console.ReadKey().KeyChar)
            {
                case '1':
                    repository = CompanyRepository.getInstance();
                    break;
            }*/
            printInstruction();

            for (char k; (k = Console.ReadKey().KeyChar) != 'x'; )
            {
                Console.WriteLine();
                int id;
                switch(k)
                {
                    case '1':
                        Company c = new Company();
                        Console.WriteLine("Enter Company Name:");
                        c.Name = Console.ReadLine();
                        Console.WriteLine("Enter Company Description:");
                        c.Description = Console.ReadLine();
                        Console.WriteLine("Enter Company Foundation Date:");
                        c.FoundedAt = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Enter Company Branch:");
                        c.Branch = Console.ReadLine();
                        Console.WriteLine(CompanyRepository.getInstance().Create(c));
                        break;
                    case '2':
                        Console.WriteLine("Enter CompanyId:");
                        id = Int32.Parse(Console.ReadLine());
                        new CompanyViewer(CompanyRepository.getInstance()).Print(id);
                        break;
                    case '3':
                        new CompanyViewer(CompanyRepository.getInstance()).PrintAll();
                        break;
                    case '4':
                        Console.WriteLine("Enter CompanyId:");
                        id = Int32.Parse(Console.ReadLine());
                        Company newCompany = CompanyRepository.getInstance().Retrieve(id);
                        Console.WriteLine("Enter Company Name:");
                        newCompany.Name = Console.ReadLine();
                        Console.WriteLine("Enter Company Description:");
                        newCompany.Description = Console.ReadLine();
                        Console.WriteLine("Enter Company Foundation Date:");
                        newCompany.FoundedAt = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Enter Company Branch:");
                        newCompany.Branch = Console.ReadLine();
                        CompanyRepository.getInstance().Update(newCompany);
                        break;
                    case '5':
                        Console.WriteLine("Enter CompanyId:");
                        id = Int32.Parse(Console.ReadLine());
                        CompanyRepository.getInstance().Delete(id);
                        break;
                    default:
                        Console.Clear();
                        printInstruction();
                        break;
                }
            }
        }


        static void printInstruction()
        {
            Console.WriteLine("1\tCreate\n2\tRead\n3\tRead All\n4\tUpdate\n5\tDelete\nx\tExit");
        }

    }
}
