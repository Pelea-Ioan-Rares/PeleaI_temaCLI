using Immediate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelea
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (InteractiveWindow window = new InteractiveWindow())
            {
                window.Run(30.0);
            }
        }
    }
}
