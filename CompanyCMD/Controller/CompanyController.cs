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
    class CompanyController : IController
    {
        private IRepository<Company> Repository;
        private IViewer<Company> Viewer;
        public CompanyController(IRepository<Company> Repository, IViewer<Company> Viewer)
        {
            this.Repository = Repository;
            this.Viewer = Viewer;
        }

        public void POST()
        {
            Company c = new Company();
            Console.WriteLine("Enter Company Name:");
            c.Name = Console.ReadLine();
            Console.WriteLine("Enter Company Description:");
            c.Description = Console.ReadLine();
            //while(c.FoundedAt == null)
            //{
            Console.WriteLine("Enter Company Foundation Date:");
            try
            {
                c.FoundedAt = Convert.ToDateTime(Console.ReadLine());
            } catch(Exception)
            {
                Console.WriteLine("Invalid Date");
                return;
            }
            //}
            Console.WriteLine("Enter Company Branch:");
            c.Branch = Console.ReadLine();
            Console.WriteLine(Repository.Create(c));
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
                List<Company> companies = new List<Company>();
                foreach(int id in ids)
                {
                    Company c = Repository.Retrieve(id);
                    if (c != null) companies.Add(c);
                }
                Viewer.Print(companies);
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

            Company newCompany = Repository.Retrieve(Convert.ToInt32(input));
            Console.WriteLine("Enter Company Name:");
            Console.WriteLine("Current: " + newCompany.Name);
            newCompany.Name = Console.ReadLine();
            Console.WriteLine("Enter Company Description:");
            Console.WriteLine("Current: " + newCompany.Description);
            newCompany.Description = Console.ReadLine();
            Console.WriteLine("Enter Company Foundation Date:");
            Console.WriteLine("Current: " + newCompany.FoundedAt.ToString("yyyy-MM-dd"));
            try
            {
                newCompany.FoundedAt = Convert.ToDateTime(Console.ReadLine());
            }
            catch (Exception)
            {
            }
            Console.WriteLine("Enter Company Branch:");
            Console.WriteLine("Current: " + newCompany.Branch);
            newCompany.Branch = Console.ReadLine();
            Repository.Update(newCompany);
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
