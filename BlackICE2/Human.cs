using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    class Human
    {
        public void Run(Computer computer, List<string> listing)
        {
            // Fingers on the Spectrum cassette player...

            // 1. Execute interpreter on listing.
            Interpreter interpreter = new Interpreter();

            // todo Program program = interpreter.Interpret(listing, computer.cPU.CPU_BYTES);



            // 2. Execute loader on source.
            Loader loader = new Loader();

            // todo loader.Load(computer, program);
        }
    }
}
