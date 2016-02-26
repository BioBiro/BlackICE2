using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // needs uncommenting!--> this.opcodes = new Dictionary<Tuple<string, List<Operand.OperandType>>, string>(new FunkyDick());

            // MOV
            // needs uncommenting!--> this.opcodes.Add(new Tuple<string, List<Operand.OperandType>>("MOV", new List<Operand.OperandType> { Operand.OperandType.Register, Operand.OperandType.Literal }), "88");
        }



        // needs uncommenting!--> public Dictionary<Tuple<string, List<Operand.OperandType>>, string> opcodes;



        public Computer parentComputer;


        
        public void _184(byte[] source) // MOV EAX,(parameter - literal value)
        {
            byte[] sValue;

            sValue = source;

            // * Get reference to destination register. *
            parentComputer.cPU.GetRegisters().SetRegister(0, 0, sValue);
        }



        public void _40() // INC EAX
        {
            byte[] sValue;


            // * Get reference to source register. *
            sValue = parentComputer.cPU.GetRegisters().GetRegister(0, 0);



            // Create increased byte value.

            byte[] modsvalue = Helper.GetHelper().PadWithBytes(sValue, 4);
            int inced = BitConverter.ToInt32(modsvalue, 0) + 1;//sValue, 0) + 1;
            byte[] incedb = BitConverter.GetBytes(inced);
            //byte[] incedValue = BitConverter.GetBytes((BitConverter.ToInt32(sValue, 0) + 1));



            // Set the increased value in place. (Byte array size will always be the same source-to-destinatino, since there's only one operand.)
            parentComputer.cPU.GetRegisters().SetRegister(0, 0, incedb); // this shoves the 32-bit/4 byte value in, rather than the 1 byte/8bit value at obtained with GetRegister at the start of this method.



            // Flags?
        }



        // todo PUSH
        // todo POP
        // todo MOV REG ADDR
        // todo MOV ADDR LIT
        // todo CALL
        // todo RET
        // todo JE
    }
}
