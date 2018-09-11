using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CompanyCMD.Models;
using CompanyCMD.Repository;
using CompanyCMD.View;

namespace CompanyCMD.Controller
{
    class EmployeeController : IController
    {
        private IRepository<Employee> Repository;
        private IViewer<Employee> Viewer;
        public EmployeeController(IRepository<Employee> Repository, IViewer<Employee> Viewer)
        {
            this.Repository = Repository;
            this.Viewer = Viewer;
        }

        public void POST()
        {
            Employee e = new Employee();
            Console.WriteLine("Enter Last Name:");
            e.LastName = Console.ReadLine();
            Console.WriteLine("Enter First Name:");
            e.FirstName = Console.ReadLine();
            //while(c.FoundedAt == null)
            //{
            Console.WriteLine("Enter Birthday:");
            try
            {
                e.Birthday = Convert.ToDateTime(Console.ReadLine());
            } catch(Exception)
            {
                Console.WriteLine("Invalid Date");
                return;
            }
            //}
            Console.WriteLine("Enter Phone:");
            e.Phone = Console.ReadLine();
            Console.WriteLine("Enter Gender:");
            e.Gender = Console.ReadLine();
            Console.WriteLine("Enter Employee Since:");
            try
            {
                e.EmployeeSince = Convert.ToDateTime(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Date");
                return;
            }
            Console.WriteLine(Repository.Create(e));
        }

        public void GET()
        {
            List<int> ids = new List<int>();
            for(string input; (input=Console.ReadLine()).Length > 0; )
            {
                if (!new Regex("[0-9]+").IsMatch(input))
                {
                    Console.WriteLine("Invalid Id");
                    return;
                } else
                {
                    ids.Add(Convert.ToInt32(input));
                }
            }
            if(ids.Count == 0)
            {
                Viewer.Print(Repository.RetrieveAll());
            } else
            {
                List<Employee> employees = new List<Employee>();
                foreach(int id in ids)
                {
                    Employee e = Repository.Retrieve(id);
                    if (e != null) employees.Add(e);
                }
                Viewer.Print(employees);
            }
        }

        public void PATCH()
        {
            Console.WriteLine("Enter CompanyId:");
            string input = Console.ReadLine();
            if(!new Regex("[0-9]+").IsMatch(input))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            Employee e = Repository.Retrieve(Convert.ToInt32(input));
            Console.WriteLine("Enter Last Name:");
            Console.WriteLine("Current: " + e.LastName);
            e.LastName = Console.ReadLine();
            Console.WriteLine("Enter First Name:");
            Console.WriteLine("Current: " + e.FirstName);
            e.FirstName = Console.ReadLine();
            //while(c.FoundedAt == null)
            //{
            Console.WriteLine("Enter Birthday:");
            Console.WriteLine("Current: " + e.Birthday);
            try
            {
                e.Birthday = Convert.ToDateTime(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Date");
                return;
            }
            //}
            Console.WriteLine("Enter Phone:");
            Console.WriteLine("Current: " + e.Phone);
            e.Phone = Console.ReadLine();
            Console.WriteLine("Enter Gender:");
            Console.WriteLine("Current: " + e.Gender);
            e.Gender = Console.ReadLine();
            Console.WriteLine("Enter Employee Since:");
            Console.WriteLine("Current: " + e.EmployeeSince);
            try
            {
                e.EmployeeSince = Convert.ToDateTime(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Date");
                return;
            }

            Repository.Update(e);
        }

        public void DELETE()
        {
            Console.WriteLine("Enter CompanyId:");
            string input = Console.ReadLine();
            if (!new Regex("[0-9]+").IsMatch(input))
            {
                Console.WriteLine("Invalid Id");
                return;
            }
            Repository.Delete(Convert.ToInt32(input));
        }

    }
}
