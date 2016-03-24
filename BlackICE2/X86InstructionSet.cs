using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace BlackICE2
{
    /*public class FunkyDick : IEqualityComparer<Tuple<string, List<Operand.OperandType>>> // Only public so that we can unit test this.
    {
        public bool Equals(Tuple<string, List<Operand.OperandType>> x, Tuple<string, List<Operand.OperandType>> y)
        {
            //return x.Item2.SequenceEqual(y.Item2); // Old version. Checks just the operand list for equality.
            return ((x.Item1 == y.Item1) && (x.Item2.SequenceEqual(y.Item2))); // New version. Checks both keys for equality.
        }

        public int GetHashCode(Tuple<string, List<Operand.OperandType>> obj)
        {
            int hashcode = 0;
            foreach (Operand.OperandType t in obj.Item2)
            {
                hashcode ^= t.GetHashCode();
            }
            return hashcode;
        }
    }*/

    
    
    class X86InstructionSet : IInstructionSet
    {
        public X86InstructionSet(Computer parentComputer)
        {
            this.parentComputer = parentComputer;
            
            
            
            // * Opcodes. *
            this.opcodes = new Dictionary<string, string>();

            // MOV
            this.opcodes.Add("_184", "MOV EAX, ");

            this.opcodes.Add("_232", "CALL ");

            this.opcodes.Add("_40", "INC EAX");

            this.opcodes.Add("_50", "PUSH EAX");

            this.opcodes.Add("_58", "POP EAX");

            this.opcodes.Add("_195", "RET");

            this.opcodes.Add("_106", "PUSH ");

            //this.opcodes.Add("_233", "JMP ");

            this.opcodes.Add("_89", "MOV ");
        }



        public Dictionary<string, string> opcodes;



        public int ToSkip(byte machineCode) // todo --> Available for dissasembler to use... Should this method be moved to a base (inherited) InstructionSet class?
        {
            // Do your reflective invocation thing here...              
            Type reflectionType = typeof(X86InstructionSet);

            // Turn 32-bit instruction opcode into instruction opcode.

            // Prep for 'reflective invoke' :-).
            string methodName = "_" + machineCode;//Encoding.ASCII.GetString(this.operands[0].value).Trim(new char[] { '\0' }); // Remove empty character bytes from opcode.

            MethodInfo info = reflectionType.GetMethod(methodName);
            ParameterInfo[] parameterInfos = info.GetParameters();

            return parameterInfos.Length + 1; // Always return at least 1, as in - an instruction with no parameters.
        }



        public Computer parentComputer;


        
        public void _184(byte[] source) // MOV EAX,(parameter - literal value)
        {
            byte[] sValue;

            sValue = source;

            // * Get reference to destination register. *
            parentComputer.cPU.GetRegisters().SetRegister((int)(X86Registers.RegisterPointers.ACCUMULATOR), 0, sValue);
        }



        public void _89(byte[] destination) // register-to-register - parameter specifies destination register (no idea why x86 implements it this way...)
        {
            if (destination[0] == 195) // MOV EBX, EAX
            {
                byte[] a = parentComputer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.ACCUMULATOR), 0);

                parentComputer.cPU.GetRegisters().SetRegister((int)(X86Registers.RegisterPointers.BASE), 0, a);
            }
        }



        public void _XX(byte[] source) // todo rename method with correct opcode - MOV REG, ADDR
        {
            // *** NOTE - THIS IS DIRECT ADDRESSING, NOT INDIRECT (just shove the source[] value into the register for INDIRECT!) ***
            // Grab the value of the destination register, so you can find out how big the amount of data to transfer should be. TODO you should have a neater way of doing this than a 'dummy grab'.
            int iSizeOfDestinationRegister = parentComputer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.ACCUMULATOR), 0).Length;



            // Look address up and get the value out of memory.
            Int32 iStartOfAddress = BitConverter.ToInt32(source, 0);

            byte[] bytesToSaveToRegister = new byte[iSizeOfDestinationRegister]; // Set to size of register.

            for (int i = 0; i < bytesToSaveToRegister.Length; i++)
            {
                bytesToSaveToRegister[i] = this.parentComputer.memory.virtualAddressSpace[i + iStartOfAddress].value;
            }



            // Now you can save the bytes from memory into the destination register.
            parentComputer.cPU.GetRegisters().SetRegister((int)(X86Registers.RegisterPointers.ACCUMULATOR), 0, bytesToSaveToRegister);
        }



        public void _ZZ(byte[] destination, byte[] source) // todo todo rename method with correct opcode - MOV ADDR LIT
        {
            // Get the size of the literal, so you know how many bytes to write to memory.
            int iSizeOfLiteral = source.Length;



            // Get the start address of the memory location.
            int iStartOfAddress = BitConverter.ToInt32(destination, 0);



            // Copy the bytes from the literal value to the memory location.
            for (int i = 0; i < iSizeOfLiteral; i++)
            {
                this.parentComputer.memory.virtualAddressSpace[i + iStartOfAddress].value = source[i];
            }
        }



        public void _40() // INC EAX
        {
            byte[] sValue;


            // * Get reference to source register. *
            sValue = parentComputer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.ACCUMULATOR), 0);



            // Create increased byte value.

            byte[] modsvalue = Helper.GetHelper().PadWithBytes(sValue, 4);
            int inced = BitConverter.ToInt32(modsvalue, 0) + 1;//sValue, 0) + 1;
            byte[] incedb = BitConverter.GetBytes(inced);
            //byte[] incedValue = BitConverter.GetBytes((BitConverter.ToInt32(sValue, 0) + 1));



            // Set the increased value in place. (Byte array size will always be the same source-to-destinatino, since there's only one operand.)
            parentComputer.cPU.GetRegisters().SetRegister((int)(X86Registers.RegisterPointers.ACCUMULATOR), 0, incedb); // this shoves the 32-bit/4 byte value in, rather than the 1 byte/8bit value at obtained with GetRegister at the start of this method.



            // Flags?
        }



        public void _233(byte[] destination) // JMP
        {
            int iLocation = destination[0] - 1;

            parentComputer.cPU.GetRegisters().SetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0, BitConverter.GetBytes(iLocation));
        }



        public void _106(byte[] value) // PUSH literal
        {
            parentComputer.cPU.GetRegisters().DecrementStackPointer(); // Decrement stack pointer.

            int stackPointer = parentComputer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.STACK_POINTER), 0)[0];//stackPointerConst, 0); // Get stack pointer.
            
            // Push value onto stack.
            parentComputer.memory.virtualAddressSpace[stackPointer].value = value[0];
            // + 1
            // + 2
            // + 3
        }



        public void _50() // PUSH EAX
        {
            parentComputer.cPU.GetRegisters().DecrementStackPointer(); // Decrement stack pointer.

            int stackPointer = parentComputer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.STACK_POINTER), 0)[0];//stackPointerConst, 0); // Get stack pointer.

            byte[] bytes = parentComputer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.ACCUMULATOR), 0); // Get value in EAX.
            
            // Push value onto stack.
            parentComputer.memory.virtualAddressSpace[stackPointer].value = bytes[0];
            // + 1
            // + 2
            // + 3
        }



        public void _58() // POP EAX
        {
            int stackPointer = parentComputer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.STACK_POINTER), 0)[0];//stackPointerConst, 0); // Get stack pointer.

            byte[] bytes = new byte[4]; // todo - fill to size of CPU architechture.;

            bytes[0] = parentComputer.memory.virtualAddressSpace[stackPointer].value;
            // + 1
            // + 2
            // + 3

            parentComputer.cPU.GetRegisters().SetRegister((int)(X86Registers.RegisterPointers.ACCUMULATOR), 0, bytes);

            // Clear stack area where data was popped-off.
            parentComputer.memory.virtualAddressSpace[stackPointer].value = 0;
            // + 1
            // + 2
            // + 3

            parentComputer.cPU.GetRegisters().IncrementStackPointer(); // Decrement stack pointer.
        }



        public void _232(byte[] destination) // CALL. Note, this is implemented as a PUSH then a JMP.
        {
            byte[] bytes = parentComputer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0); // Save instruction pointer...

            this._106(bytes); // ... on the stack.

            this._233(destination); // Now jump to the location.
        }



        public void _195() // RET
        {
            int stackPointer = parentComputer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.STACK_POINTER), 0)[0];//stackPointerConst, 0); // Get stack pointer.

            byte[] bytes = new byte[4]; // todo - fill to size of CPU architechture.;

            bytes[0] = parentComputer.memory.virtualAddressSpace[stackPointer].value;
            // + 1
            // + 2
            // + 3

            parentComputer.cPU.GetRegisters().SetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0, bytes);

            // Clear stack area where data was popped-off.
            parentComputer.memory.virtualAddressSpace[stackPointer].value = 0;
            // + 1
            // + 2
            // + 3

            parentComputer.cPU.GetRegisters().IncrementStackPointer(); // Decrement stack pointer.
        }
    }
}
