using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    public class Human
    {
        public Loader loader;



        public Human()
        {
            loader = new Loader();
        }



        public Program CreateProgram(Computer computer, List<string> listing)
        {
            // Fingers on the Spectrum cassette player...

            // 1. Execute interpreter on listing.
            Interpreter interpreter = new Interpreter();

            return interpreter.Interpret(listing);
        }



        public void PrepareProgram(Computer computer, Program program)
        {
            // 2. Execute loader on source.
            loader.Load(computer, program);
        }
    }
}
