using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BlackICE2
{
    /// <summary>
    /// Interaction logic for TestCase.xaml
    /// </summary>
    public partial class TestCase : Window
    {           
        private UnitTest uT;
        public UnitTest UT
        {
            get { return this.uT; }
            set
            {
                this.uT = value;

                // Select current value.
                //cmTestCaseRegisters.SelectedIndex = uT.register; // pass in/selected test register, expected result, etc.
            }
        }


        
        public TestCase()
        {
            InitializeComponent();

            // Add registers.
            cmTestCaseRegisters.Items.Clear();            
            foreach (string register in Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegisters())
            {
                cmTestCaseRegisters.Items.Add(register);
            }            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.uT = new UnitTest(this.tbName.Text, cmTestCaseRegisters.SelectedIndex, 0, Int32.Parse(this.tbExpectedResult.Text), 0, "a");
            
            this.Close();
        }
    }
}
