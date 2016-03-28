using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    public interface IRegisters
    {
        string[] GetRegisters();

        byte[] GetRegister(int register, int segment);
        void SetRegister(int register, int segment, byte[] value);

        void IncrementStackPointer();
        void DecrementStackPointer();

        void IncrementProgramCounter();
        void DecrementProgramCounter();
    }
}
