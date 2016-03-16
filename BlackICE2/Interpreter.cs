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
            // This should return machine code from your assembly language!

            // Just convert mov eax, 7 and inc eax to machine code and return it, for now...

            Tuple<List<byte>, List<int>> t = new Tuple<List<byte>, List<int>>(
                   new List<byte>() { 184, 10 // mov eax, 10
                                    , 232, 8 // call @@myfunction - opcode 14 (6 with 8 byte stack push downwards) <-- NOTICE HOW WE KNOCK THE LABEL'S ADDRESS BACK BY -1 AGAIN (AFTER REMOVING THE DEAD LINE WHERE THE LABEL WAS DEFINED, SO IT'S ACTUALLY A -2).
                                    , 40 // inc eax
                                    , 50 // push eax
                                    , 40 // inc eax
                                    , 58 // pop eax
                                    //, label - doesnt get assembled into machine code, as replaced label CALLs with memory addresses // @@myfunction
                                    , 89, 195 // mov ebx, eax
                                    , 106, 77 // push 77
                                    , 40 // inc eax
                                    , 58 // pop eax
                                    , 195 } // ret

                 , new List<int>() { 0
                                   , 0
                                   , 1
                                   , 1
                                   , 2
                                   , 3
                                   , 4
                                   , 5
                                   , 7 // Notice how we skip line 6, where the label is defined (which is not present as a line within the opcodes).
                                   , 7
                                   , 8
                                   , 8
                                   , 9
                                   , 10
                                   , 11
                                   }
                 );

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
