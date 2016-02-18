using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    public interface ICPU
    {
        IRegisters GetRegisters();

        // todo CPUs need to support big/little endian. C#'s BitConverter has a method for checking this IsLittleEndian() <--.
    }
}
