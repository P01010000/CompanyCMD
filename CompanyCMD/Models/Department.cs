﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCMD.Models
{
    class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Supervisor { get; set; }
        public int? SuperDepartment { get; set; }
        public int CompanyId { get; set; }
    }
}
