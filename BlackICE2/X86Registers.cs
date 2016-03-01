using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    class X86Registers : IRegisters
    {
        public const int REGISTER_REGISTER = 0;
        public const int REGISTER_SEGMENT = 1;



        Dictionary<int, byte[]> registers;



        public X86Registers()
        {
            // * Create actual registers. (These need to be in the same order as the RegisterPointers enum). *
            this.registers = new Dictionary<int, byte[]>();



            // General purpose registers.
            this.registers.Add((int)(0), new byte[4]); // Can initialize to zero here if required...




        }



        public byte[] GetRegister(int register, int segment)
        {
            byte[] rBytes = new byte[1];

            rBytes[0] = this.registers[register][0];

            return rBytes;
        }


        public void SetRegister(int register, int segment, byte[] value)
        {
            this.registers[register][0] = value[0];
        }



        public void DecrementStackPointer()
        {
            this.SetRegister(stackPointerConst, 0, (this.GetRegister(stackPointerConst, 0) - 1));
        }
    }
}
