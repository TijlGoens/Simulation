using System;

namespace Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulator simulator = new Simulator();
            simulator.Simulate().Wait();
        }
    }
}
