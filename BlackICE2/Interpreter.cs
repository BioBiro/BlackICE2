using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    class Interpreter
    {
        public Interpreter()
        {

        }



        public Tuple<List<byte>, List<int>> Tokenize(List<string> source)
        {
            // Split by space or whatever, first of all.
            // Then, maybe skip parameters/whatever by opcode ToSkip? I.e., mov eax is one opcode, so you can tell the interpreter just to read the next byte only, not the next two bytes, as the destination is implied.
            
            
            
            
            Tuple<List<byte>, List<int>> t = new Tuple<List<byte>, List<int>>(new List<byte>(), new List<int>());
            
            
            
            X86InstructionSet instructions = new X86InstructionSet(Singleton.GetSingleton().computer);            


            
            int i;

            for (i = 0; i < source.Count; i++)
            {
                if (source[i] == "mov eax, 10")
                {
                    t.Item1.Add(184); // Byte.
                    t.Item2.Add(1); // ASM line.
                    
                    int toSkip = instructions.ToSkip(184); // Number of parameters.
                    
                    for (int j = 0; j < toSkip; j++)
                    {
                        t.Item1.Add(Byte.Parse(source[i + j + 1])); // Byte.
                        t.Item2.Add(j + 1); // ASM line.
                    }

                    i += toSkip; // Push loop past any parameters.
                }
                else if (source[i] == "call @@myfunction")
                {

                }
                else if (source[i] == "inc eax")
                {

                }
                else if (source[i] == "push eax")
                {

                }
            }
                        
            

            return t;
        }



        public Program Interpret(List<string> source)
        {
            Tuple<List<byte>, List<int>> codeSegment = this.Tokenize(source);



            //List<Tuple<int, byte[]>> data = this.BuildDataSegment();



            // Build the program.
            Program programToReturn = new Program();

            //programToReturn.data = data;
            programToReturn.codeSegment = codeSegment;
            programToReturn.entryPoint = 0;//31 - codeSegment.Item1.Count - 2; // todo --> replace with FindEntryPoint from old BlackICE.

            return programToReturn;
        }
    }
}
