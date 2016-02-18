using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    public class FunkyDick : IEqualityComparer<Tuple<string, List<Operand.OperandType>>> // Only public so that we can unit test this.
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
    }


    
    class X86InstructionSet : IInstructionSet
    {
        public X86InstructionSet(Computer parentComputer)
        {
            // * Opcodes. *
            this.opcodes = new Dictionary<Tuple<string, List<Operand.OperandType>>, string>(new FunkyDick());

            // MOV
            this.opcodes.Add(new Tuple<string, List<Operand.OperandType>>("MOV", new List<Operand.OperandType> { Operand.OperandType.Register, Operand.OperandType.Literal }), "88");
        }



        public Dictionary<Tuple<string, List<Operand.OperandType>>, string> opcodes;



        public Computer parentComputer;


        
        public void _88(byte[] destination, byte[] source) // MOV literaltoaddress
        {
            // Get the size of the literal, so you know how many bytes to write to memory.
            int iSizeOfLiteral = source.Length;



            // Get the start address of the memory location.
            int iStartOfAddress = BitConverter.ToInt32(destination, 0);



            // Copy the bytes from the literal value to the memory location.
            for (int i = 0; i < iSizeOfLiteral; i++)
            {
                this.parentComputer.memory.virtualAddressSpace[i + iStartOfAddress] = source[i];
            }
        }



        public void _69(byte[] register) // INC reg
        {
            byte[] sValue;


            // * Get reference to source register. *
            sValue = parentComputer.cPU.registers.GetRegister(register[X86Registers.REGISTER_REGISTER], register[X86Registers.REGISTER_SEGMENT]);



            // Create increased byte value.
            byte[] incedValue = BitConverter.GetBytes((BitConverter.ToInt32(sValue, 0) + 1));



            // Set the increased value in place. (Byte array size will always be the same source-to-destinatino, since there's only one operand.)
            parentComputer.cPU.registers.SetRegister(register[X86Registers.REGISTER_REGISTER], register[X86Registers.REGISTER_SEGMENT], incedValue);



            // Flags?
        }
    }
}
