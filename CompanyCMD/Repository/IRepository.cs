using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCMD.Repository
{
    interface IRepository<T> where T : class
    {
        int Create(T obj);

        T Retrieve(params int[] id);
        IEnumerable<T> RetrieveAll();

        void Update(T obj);
        void UpdateAll(IEnumerable<T> list);

        void Delete(params int[] id);
    }
}
