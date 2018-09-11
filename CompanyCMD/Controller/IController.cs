using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCMD.Controller
{
    interface IController
    {
        void POST();
        void GET();
        void PATCH();
        void DELETE();
    }
}
