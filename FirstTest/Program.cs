using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new Game(1280, 720);
            //var window = new HelloCube();
            window.Run(60d);
        }
    }
}
