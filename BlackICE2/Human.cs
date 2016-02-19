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



        public void Run(Computer computer, List<string> listing)
        {
            // Fingers on the Spectrum cassette player...

            // 1. Execute interpreter on listing.
            Interpreter interpreter = new Interpreter();

            Program program = interpreter.Interpret(listing);



            // 2. Execute loader on source.
            loader = new Loader();

            loader.Load(computer, program);
            
            
            
            // 3. Run the loaded program.
        }
    }
}
