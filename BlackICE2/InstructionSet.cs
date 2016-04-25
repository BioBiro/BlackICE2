using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace BlackICE2
{
    public class InstructionSet
    {
        public int ToSkip(byte machineCode)
        {
            // Do your reflective invocation thing here...              
            Type reflectionType = typeof(X86InstructionSet); // todo - either make IInstructionSet a parameter to this method, or make IInstructionSet a public var in this base class, that is set by the constructor.

            // Turn 32-bit instruction opcode into instruction opcode.

            // Prep for 'reflective invoke' :-).
            string methodName = "_" + machineCode;//Encoding.ASCII.GetString(this.operands[0].value).Trim(new char[] { '\0' }); // Remove empty character bytes from opcode.

            MethodInfo info = reflectionType.GetMethod(methodName);
            ParameterInfo[] parameterInfos = info.GetParameters();

            return parameterInfos.Length + 1; // Always return at least 1, as in - an instruction with no parameters.
        }        
    }
}
