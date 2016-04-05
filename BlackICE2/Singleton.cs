using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackICE2
{
    public class Singleton
    {
        private static Singleton _singleton;



        // * Constructor *
        private Singleton()
        {
            this.human = new Human();
        }



        // * Safe getter for singleton instance. *
        public static Singleton GetSingleton()
        {
            if (_singleton == null) // Prevent spawning multiple objects in class instance.
            {
                _singleton = new Singleton();
            }

            return _singleton;
        }



        public string[] asmSource;

        public Human human;

        public Computer computer;

        public UnitTestSuite unitTestSuite;
    }
}
