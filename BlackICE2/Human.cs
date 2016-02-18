﻿using System;
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

            Program program = interpreter.Interpret(listing);



            // 2. Execute loader on source.
            Loader loader = new Loader();

            loader.Load(computer, program);
        }
    }
}
