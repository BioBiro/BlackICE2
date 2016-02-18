using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    public class Memory
    {
        public Memory()
        {
            // * Memory. *
            this.virtualAddressSpace = new List<byte>(); // Can give it a capacity here if you wish.

            for (int i = 0; i < 32; i++) // todo, pick a constant? A 32-byte stack will overflow fairly quickly...
            {
                this.virtualAddressSpace.Add(new byte { });
            }
        }



        public List<byte> virtualAddressSpace;
    }
}
