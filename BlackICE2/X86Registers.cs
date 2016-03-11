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



        public enum RegisterPointers // These are used for indexing.
        {
            ACCUMULATOR,
            BASE,
            STACK_POINTER,
            INSTRUCTION_POINTER
        }
        
        
        
        Dictionary<int, byte[]> registers;



        public X86Registers()
        {
            // * Create actual registers. (These need to be in the same order as the RegisterPointers enum). *
            this.registers = new Dictionary<int, byte[]>();



            // General purpose registers.
            this.registers.Add((int)((int)(RegisterPointers.ACCUMULATOR)), new byte[4]); // Can initialize to zero here if required...
            this.registers.Add((int)((int)(RegisterPointers.BASE)), new byte[4]); // Can initialize to zero here if required...

            // Stack pointer.
            this.registers.Add((int)((int)(RegisterPointers.STACK_POINTER)), new byte[4]); // Can initialize to zero here if required...

            // Instruction pointer.
            this.registers.Add((int)((int)(RegisterPointers.INSTRUCTION_POINTER)), new byte[4]); // Can initialize to zero here if required...
        }



        public byte[] GetRegister(int register, int segment) // todo Can't the register parameter be the RegisterPointer enum type instead?
        {
            byte[] rBytes = new byte[1];

            rBytes[0] = this.registers[register][0];

            return rBytes;
        }



        public void SetRegister(int register, int segment, byte[] value) // todo Can't the register parameter be the RegisterPointer enum type instead?
        {
            this.registers[register][0] = value[0];
        }



        public void IncrementStackPointer() // todo needs bytes (cpu architechture) as parameter?
        {
            int stackPointer = this.GetRegister((int)(RegisterPointers.STACK_POINTER), 0)[0] + 1;

            byte[] stackPointerBytes = BitConverter.GetBytes(stackPointer);

            this.SetRegister((int)(RegisterPointers.STACK_POINTER), 0, stackPointerBytes);
        }



        public void DecrementStackPointer() // todo needs bytes (cpu architechture) as parameter?
        {
            int stackPointer = this.GetRegister((int)(RegisterPointers.STACK_POINTER), 0)[0] - 1;

            byte[] stackPointerBytes = BitConverter.GetBytes(stackPointer);

            this.SetRegister((int)(RegisterPointers.STACK_POINTER), 0, stackPointerBytes);
        }



        public void IncrementInstructionPointer() // todo needs bytes (cpu architechture) as parameter?
        {
            int instructionPointer = this.GetRegister((int)(RegisterPointers.INSTRUCTION_POINTER), 0)[0] + 1;

            byte[] instructionPointerBytes = BitConverter.GetBytes(instructionPointer);

            this.SetRegister((int)(RegisterPointers.INSTRUCTION_POINTER), 0, instructionPointerBytes);
        }



        public void DecrementInstructionPointer() // todo needs bytes (cpu architechture) as parameter?
        {
            int instructionPointer = this.GetRegister((int)(RegisterPointers.INSTRUCTION_POINTER), 0)[0] - 1;

            byte[] instructionPointerBytes = BitConverter.GetBytes(instructionPointer);

            this.SetRegister((int)(RegisterPointers.INSTRUCTION_POINTER), 0, instructionPointerBytes);
        }
    }
}
