using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace BlackICE2
{
    class Loader
    {
        public void Load(Computer computer, Program program)
        {


            // skip entry point stuff

            // skip loading data segment into memory



            // Load the code segment into memory.
            // todo We've not actually loaded the code segment into memory, we're just reading it like a herp-derp.
            // ^ You need to load it into the virtual address space like we did in the data segment.

            // (machine code is already generated)
            
            for (int i = 0; i < program.codeSegment.Count; i++)
            {
                if (program.codeSegment[i] == 88)
                {
                    // Execute machine code... somehow

                    // Do your reflective invocation thing here...
              
                    Type reflectionType = typeof(X86InstructionSet);



                    // Turn 32-bit instruction opcode into instruction opcode.
            


                    // Prep for 'reflective invoke' :-).
                    string methodName = "_" + program.codeSegment[i];//Encoding.ASCII.GetString(this.operands[0].value).Trim(new char[] { '\0' }); // Remove empty character bytes from opcode.



                    MethodInfo info = reflectionType.GetMethod(methodName);//this.instruction);
                    ParameterInfo[] parameterInfos = info.GetParameters();

                    byte[] parameters = new byte[0];

                    /*for (int i = 1; i <= parameterInfos.Count(); i++) // Skip first operand, as that will be the opcode.
                    {
                        Array.Resize(ref parameters, parameters.Count() + 1);

                        parameters[parameters.Count() - 1] = this.operands[i];
                    }*/
                    Array.Resize(ref parameters, parameters.Count() + 1);
                    parameters[parameters.Count() - 1] = program.codeSegment[i + 1]; // Next byte is opcode's needed parameter.                  

                    

                    //List<Operand> returnHolder = new List<Operand>();

                    X86InstructionSet instructions = new X86InstructionSet(computer); // todo <-- horrible hack creating a new instruction set on the fly, here... //computer.cPU.instructionSet;
                    //returnHolder = info.Invoke(methods, parameters) as List<Operand>;


                    object[] objects = parameters.Cast<object>().ToArray(); // Ugh...

                    info.Invoke(instructions, objects);//parameters);

                    //squishyOutput = returnHolder;
                }
                else if (program.codeSegment[i] == 69)
                {

                }
            }
        }
    }
}
