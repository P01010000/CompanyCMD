using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCMD.Models
{
    class Company
    {
        private string _name;
        private string _description;
        private DateTime _foundedAt;
        private string _branch;


        public int? Id { get; set; }
        public int? PersonId { get; set; }
        public string Name { get { return _name; } set { if (value != "") _name = value; } }
        public string Description { get { return _description; } set { if (value != "") _description = value; } }
        public DateTime FoundedAt { get { return _foundedAt; } set { if (value != null) _foundedAt = value; } }
        public string Branch { get { return _branch; } set { if (value != "") _branch = value; } }
    }
}
