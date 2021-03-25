using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            List<int> intList2 = new List<int>() { 1, 2, 3, 4, 5 };

            intList.ForEach(x => Console.Write(x));
            Console.WriteLine("----");
            intList2.ForEach(x => Console.Write(x));
            var int3 = from p in intList
                       join x in intList2 on p equals x into px
                       from px1 in px.DefaultIfEmpty()
                       select p;
                       
            var int4 = int3.ToList();
            Console.WriteLine("----");
            int4.ForEach(x => Console.Write(x));
            Console.Read();
        }
    }
}
