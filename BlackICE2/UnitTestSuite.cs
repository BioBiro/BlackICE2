using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace BlackICE2
{
    [Serializable]
    public class UnitTestSuite
    {
        public ObservableCollection<UnitTest> unitTests;



        public string name;



        public UnitTestSuite(string name)
        {
            this.unitTests = new ObservableCollection<UnitTest>();            
            
            
            
            this.name = name;
        }
    }
}
