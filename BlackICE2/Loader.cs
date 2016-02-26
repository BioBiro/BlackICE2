using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace BlackICE2
{
    public class Loader
    {
        public void Load(Computer computer, Program program)
        {


            // skip entry point stuff



            int nextLoadPoint = 8; // 8-byte stack reservation.

            // Load the data segment into memory.
            for (int i = 0; i < program.dataSegment.Count; i++)
            {
                computer.memory.virtualAddressSpace[i + nextLoadPoint] = program.dataSegment[i];
            }

            nextLoadPoint += program.dataSegment.Count;



            // Load the code segment into memory.
            for (int i = 0; i < program.codeSegment.Count; i++)
            {
                // stack (grows downwards to data/code).
                computer.memory.virtualAddressSpace[i + nextLoadPoint] = program.codeSegment[i];
            }



            // Set IP (instruction pointer) to entry point.
            this.i = program.entryPoint;
        }



        public int i { get; set; }

        public void Step(Computer computer)
        {
            // Run the program.
            //for (i = 0; i < computer.memory.virtualAddressSpace.Count; i++)
            //{
            ExecuteOpcode(computer, computer.memory.virtualAddressSpace[i]);
                        
            this.i += ToSkip(computer.memory.virtualAddressSpace[i]); // Get next opcode to instruction ready to read.
        }



        public int ToSkip(byte machineCode) // todo --> Available for dissasembler to use...
        {
            // Do your reflective invocation thing here...              
            Type reflectionType = typeof(X86InstructionSet);

            // Turn 32-bit instruction opcode into instruction opcode.

            // Prep for 'reflective invoke' :-).
            string methodName = "_" + machineCode;//Encoding.ASCII.GetString(this.operands[0].value).Trim(new char[] { '\0' }); // Remove empty character bytes from opcode.

            MethodInfo info = reflectionType.GetMethod(methodName);
            ParameterInfo[] parameterInfos = info.GetParameters();

            return parameterInfos.Length + 1; // Always return 1, as in - an instruction with no parameters.
        }



        public void ExecuteOpcode(Computer computer, byte opcode)
        {
            // Do your reflective invocation thing here...              
            Type reflectionType = typeof(X86InstructionSet);

            // Turn 32-bit instruction opcode into instruction opcode.

            // Prep for 'reflective invoke' :-).
            string methodName = "_" + computer.memory.virtualAddressSpace[i];//Encoding.ASCII.GetString(this.operands[0].value).Trim(new char[] { '\0' }); // Remove empty character bytes from opcode.

            MethodInfo info = reflectionType.GetMethod(methodName);
            ParameterInfo[] parameterInfos = info.GetParameters();

            if (parameterInfos.Length > 0)
            {
                byte[] parameters = new byte[0];

                Array.Resize(ref parameters, parameters.Count() + 1);
                parameters[parameters.Count() - 1] = computer.memory.virtualAddressSpace[i + 1]; // Next byte is opcode's needed parameter.                                  

                X86InstructionSet instructions = new X86InstructionSet(computer); // todo <-- X86 forced - needs to use IInstructionSet inteface! <-- horrible hack creating a new instruction set on the fly, here... //computer.cPU.instructionSet;                                                                                    

                info.Invoke(instructions, new object[] { parameters }); // Turn byte array into object array with single byte array element.
            }
            else
            {
                X86InstructionSet instructions = new X86InstructionSet(computer); // todo <-- X86 forced - needs to use IInstructionSet inteface! <-- horrible hack creating a new instruction set on the fly, here... //computer.cPU.instructionSet;                                                                                    
                
                info.Invoke(instructions, null); // Turn byte array into object array with single byte array element.
            }
        }
    }
}
