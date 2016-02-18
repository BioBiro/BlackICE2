using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    class Interpreter
    {
        public Program Interpret(List<string> source)
        {
            List<byte> codeSegment = this.Tokenize(source);



            //List<Tuple<int, byte[]>> data = this.BuildDataSegment();



            // Build the program.
            Program programToReturn = new Program();

            //programToReturn.data = data;
            programToReturn.codeSegment = codeSegment;
            //programToReturn.entryPoint = 0;//this.entryPoint;

            return programToReturn;
        }



        public List<byte> Tokenize(List<string> source)
        {
            // This should return machine code from your assembly language!

            // Just convert mov eax, 7 and inc eax to machine code and return it, for now...

            //return new List<byte>() { 0 };
            return new List<byte>() { 88, 07, 69 }; // Not correct; just made up. (88 MOV EAX, 07 byte value as param, 69 INC EAX).
        }
    }
}
