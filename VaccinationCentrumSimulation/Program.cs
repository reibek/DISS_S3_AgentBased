using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCentrumSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var simVC = new simulation.MySimulation();
            simVC.Simulate(5, 32400);

            Console.ReadKey();
        }
    }
}
