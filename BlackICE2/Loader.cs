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



            // Load the code segment into memory.
            for (int i = 0; i < program.codeSegment.Item1.Count; i++)
            {
                // stack (grows downwards to data/code).
                computer.memory.virtualAddressSpace[i].value = program.codeSegment.Item1[i];
                computer.memory.virtualAddressSpace[i].asmLine = program.codeSegment.Item2[i];
            }

            int nextLoadPoint = program.codeSegment.Item1.Count; // 8-byte stack reservation.



            // Load the data segment into memory.
            //todo - doesn't work at mo, because it needs to minus the nextLoadPoint, not add it, perhaps, in memory?
            /*for (int i = 0; i < program.dataSegment.Count; i++)
            {
                computer.memory.virtualAddressSpace[i + nextLoadPoint].value = program.dataSegment[i];
            }*/

            nextLoadPoint += program.dataSegment.Count;



            // Set IP (instruction pointer) to entry point.
            computer.cPU.GetRegisters().SetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0, Helper.GetHelper().PadWithBytes((byte)(program.entryPoint), 4));
        }



        //public int line { get; set; }

        public void Step(Computer computer)
        {
            // Run the program.
            //for (i = 0; i < computer.memory.virtualAddressSpace.Count; i++)
            //{
                        
            
            
            //ExecuteOpcode(computer, computer.memory.virtualAddressSpace[line]);
            int castedIP = computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];

            ExecuteOpcode(computer, computer.memory.virtualAddressSpace[castedIP].value);

            //this.line += ToSkip(computer.memory.virtualAddressSpace[castedIP]); // Get next opcode to instruction ready to read.



            // todo - Horrible hack to re-get the IP if it may have been changed by an instruction such as RET.
            castedIP = computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];



            int toSkip = ToSkip(computer.memory.virtualAddressSpace[castedIP].value);
            int toSkip2 = toSkip + computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];
            computer.cPU.GetRegisters().SetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0, Helper.GetHelper().PadWithBytes((byte)(toSkip2), 4)); // Get next opcode to instruction ready to read.
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

            return parameterInfos.Length + 1; // Always return at least 1, as in - an instruction with no parameters.
        }



        public void ExecuteOpcode(Computer computer, byte opcode)
        {
            // Do your reflective invocation thing here...              
            Type reflectionType = typeof(X86InstructionSet);

            // Turn 32-bit instruction opcode into instruction opcode.

            // Prep for 'reflective invoke' :-).
            int line = computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];
            string methodName = "_" + computer.memory.virtualAddressSpace[line].value;//Encoding.ASCII.GetString(this.operands[0].value).Trim(new char[] { '\0' }); // Remove empty character bytes from opcode.

            MethodInfo info = reflectionType.GetMethod(methodName);
            ParameterInfo[] parameterInfos = info.GetParameters();

            if (parameterInfos.Length > 0)
            {
                byte[] parameters = new byte[0];

                Array.Resize(ref parameters, parameters.Count() + 1);
                parameters[parameters.Count() - 1] = computer.memory.virtualAddressSpace[line + 1].value; // Next byte is opcode's needed parameter.                                  

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
