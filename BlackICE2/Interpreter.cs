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



        public List<byte> Tokenize(List<string> source)
        {
            // This should return machine code from your assembly language!

            // Just convert mov eax, 7 and inc eax to machine code and return it, for now...

            //return new List<byte>() { 0 };
            return new List<byte>() { 184, 07, 40 };
        }



        public Program Interpret(List<string> source)
        {
            List<byte> codeSegment = this.Tokenize(source);



            //List<Tuple<int, byte[]>> data = this.BuildDataSegment();



            // Build the program.
            Program programToReturn = new Program();

            //programToReturn.data = data;
            programToReturn.codeSegment = codeSegment;
            programToReturn.entryPoint = 8; // todo --> replace with FindEntryPoint from old BlackICE.

            return programToReturn;
        }
    }
}
