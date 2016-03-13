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
            this.virtualAddressSpace = new List<Address>(); // Can give it a capacity here if you wish.

            for (int i = 0; i < 32; i++) // todo, pick a constant? This has to include data and code segments, as well as stack on top growing downwards!
            {
                this.virtualAddressSpace.Add(new Address());//0, -1));
            }
        }



        public List<Address> virtualAddressSpace;
    }
}
