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
        public UnitTest ut {get; set; } // Result grabbed by whoever created instance of class.


        
        public TestCase()
        {
            InitializeComponent();

            // Add registers.
            cmTestCaseRegisters.Items.Clear();            
            foreach (string register in Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegisters())
            {
                cmTestCaseRegisters.Items.Add(register);
            }

            // Select current value.
            //cmTestCaseRegisters.SelectedIndex = ut.register; // pass in/selected test register, expected result, etc.
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.ut = new UnitTest(this.tbExpectedResult.Text);
            
            this.Close();
        }
    }
}
