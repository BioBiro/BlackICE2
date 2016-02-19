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

            // skip loading data segment into memory



            // Load the code segment into memory.
            for (int i = 0; i < program.codeSegment.Count; i++)
            {
                computer.memory.virtualAddressSpace[i] = program.codeSegment[i];
            }
        }



        int i;

        public void Step(Computer computer)
        {
            // Run the program.
            //for (i = 0; i < computer.memory.virtualAddressSpace.Count; i++)
            //{
            if (computer.memory.virtualAddressSpace[i] == 184)
            {
                // Execute machine code... somehow

                // Do your reflective invocation thing here...
              
                Type reflectionType = typeof(X86InstructionSet);



                // Turn 32-bit instruction opcode into instruction opcode.
            


                // Prep for 'reflective invoke' :-).
                string methodName = "_" + computer.memory.virtualAddressSpace[i];//Encoding.ASCII.GetString(this.operands[0].value).Trim(new char[] { '\0' }); // Remove empty character bytes from opcode.



                MethodInfo info = reflectionType.GetMethod(methodName);//this.instruction);
                ParameterInfo[] parameterInfos = info.GetParameters();

                byte[] parameters = new byte[0];

                /*for (int i = 1; i <= parameterInfos.Count(); i++) // Skip first operand, as that will be the opcode.
                {
                    Array.Resize(ref parameters, parameters.Count() + 1);

                    parameters[parameters.Count() - 1] = this.operands[i];
                }*/
                Array.Resize(ref parameters, parameters.Count() + 1);
                parameters[parameters.Count() - 1] = computer.memory.virtualAddressSpace[i + 1]; // Next byte is opcode's needed parameter.                  

                    

                //List<Operand> returnHolder = new List<Operand>();

                X86InstructionSet instructions = new X86InstructionSet(computer); // todo <-- horrible hack creating a new instruction set on the fly, here... //computer.cPU.instructionSet;
                                                                                    //returnHolder = info.Invoke(methods, parameters) as List<Operand>;
                                                                                                          
                info.Invoke(instructions, new object[] { parameters });//parameters); // Turn byte array into object array with single byte array element.

                //squishyOutput = returnHolder;



                this.i += 1; // push next opcode to front (skip over current opcode's parameter(s)).
            }
            else if (computer.memory.virtualAddressSpace[i] == 40)
            {
                // Execute machine code... somehow

                // Do your reflective invocation thing here...

                Type reflectionType = typeof(X86InstructionSet);



                // Turn 32-bit instruction opcode into instruction opcode.



                // Prep for 'reflective invoke' :-).
                string methodName = "_" + computer.memory.virtualAddressSpace[i];//Encoding.ASCII.GetString(this.operands[0].value).Trim(new char[] { '\0' }); // Remove empty character bytes from opcode.



                MethodInfo info = reflectionType.GetMethod(methodName);//this.instruction);
                ParameterInfo[] parameterInfos = info.GetParameters();

                //byte[] parameters = new byte[]();

                /*for (int i = 1; i <= parameterInfos.Count(); i++) // Skip first operand, as that will be the opcode.
                {
                    Array.Resize(ref parameters, parameters.Count() + 1);

                    parameters[parameters.Count() - 1] = this.operands[i];
                }*/
                //Array.Resize(ref parameters, parameters.Count() + 1);               



                //List<Operand> returnHolder = new List<Operand>();

                X86InstructionSet instructions = new X86InstructionSet(computer); // todo <-- horrible hack creating a new instruction set on the fly, here... //computer.cPU.instructionSet;
                                                                                    //returnHolder = info.Invoke(methods, parameters) as List<Operand>;

                info.Invoke(instructions, null);//parameters); // Turn byte array into object array with single byte array element.

                //squishyOutput = returnHolder;
            }



            this.i += 1;
        }
    }
}
