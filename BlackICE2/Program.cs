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
        public Tuple<List<byte>, List<int>> codeSegment;        



        public Program()
        {
            this.dataSegment = new List<byte>();
            //this.codeSegment = new Tuple<List<byte>, List<int>>();
        }
    }
}
