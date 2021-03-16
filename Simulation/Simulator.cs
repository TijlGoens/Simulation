using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class Simulator
    {
        public Grader Grader { get; set; }

        public Simulator()
        {
            Grader = new Grader(1);
        }

        async Task RunGraderSimulation()
        {
            await Task.Run(() =>
            {
                Trace.WriteLine("RunGraderSimulation called");
                Grader.FillInputConveyor(10000);
                Grader.Run();                
            });
        }

        async public Task Simulate()
        {
            await RunGraderSimulation();
        }
    }
}
