using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    public class Helper
    {
        private static Helper _helper;



        // * Constructor *
        private Helper()
        {
            //
        }



        // * Safe getter for singleton instance. *
        public static Helper GetHelper()
        {
            if (_helper == null) // Prevent spawning multiple objects in class instance.
            {
                _helper = new Helper();
            }

            return _helper;
        }



        public byte[] PadWithBytes(byte input, int bytesToPad)
        {
            byte[] output = new byte[bytesToPad];

            output[0] = input;

            return output;
        }



        public byte[] PadWithBytes(byte[] inputArray, int bytesToPad)
        {
            if (inputArray.Length < bytesToPad)
            {
                Array.Resize(ref inputArray, bytesToPad);
            }

            return inputArray;
        }
    }
}
