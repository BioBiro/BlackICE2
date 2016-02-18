using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    class X86CPU : ICPU
    {
        X86InstructionSet x86InstructionSet;
        X86Registers x86Registers;



        // * Constructor *
        public X86CPU(Computer computer)
        {
            this.x86InstructionSet = new X86InstructionSet(computer);
            this.x86Registers = new X86Registers();
        }
    }
}
