using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace BlackICE2
{
    [Serializable]
    public class UnitTest : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }



        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                this.name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        public int status { get; set; }
        public int expectedResult { get; set; }
        public int actualResult { get; set; }
        public string message { get; set; }



        public UnitTest(string name, int status, int expectedResult, int actualResult, string message)
        {
            this.name = name;
            this.status = status;
            this.expectedResult = expectedResult;
            this.actualResult = actualResult;
            this.message = message;

            //this.NotifyPropertyChanged("propName");
        }
    }
}
