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
            using (Immediate window = new Immediate())
            {
                window.Run(60.0);  // Rulează aplicația la 60 FPS
            }
        }
    }
}
