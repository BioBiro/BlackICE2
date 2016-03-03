using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    public class Program
    {
        public int entryPoint;
        
        public List<byte> dataSegment;
        public List<byte> codeSegment;        



        public Program()
        {
            this.dataSegment = new List<byte>();
            this.codeSegment = new List<byte>();
        }
    }
}
