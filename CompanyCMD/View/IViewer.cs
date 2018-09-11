using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCMD.View
{
    abstract class IViewer<T> where T : class
    {
        private int[] ColumnLengths;
        protected IViewer(int[] ColumnLengths)
        {
            this.ColumnLengths = ColumnLengths;
        }

        public IViewer() : this(new int[] { 16 })
        {
        }

        public abstract void Print(IEnumerable<T> list);

        protected void PrintLine(IEnumerable<string> strings, char start, char middle, char end, char filler)
        {
            Console.Write(start);
            Console.Write(filler);
            Console.Write(Format(strings, filler).Aggregate((a, b) => a + filler + middle + filler + b));
            Console.Write(filler);
            Console.WriteLine(end);
        }

        private IEnumerable<string> Format(IEnumerable<string> strings, char filler)
        {
            int col = 0;

            List<string> formatted = new List<string>();
            foreach (string str in strings)
            {
                formatted.Add(Padding(str, ColumnLengths[col], filler));
                col = (col + 1) % ColumnLengths.Length;
            }
            return formatted;
        }

        private string Padding(string value, int length, char filler)
        {
            return value.Length > length ? value.Substring(0, length - 2) + ".." : value.PadRight(length, filler);
        }

    }
}
