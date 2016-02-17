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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackICE2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            // new program typed in; open an existing program, etc. Instructions are put into GUI editor.
            // todo - make this work sooner rather than later --> Allow self-modyfying code ONLY on the code segment machine code (use an event that is triggered when you edit a line in the GUI or something).
            ListBoxItem lbi = new ListBoxItem();
            lbi.Content = "mov eax, 7";
            lbi.MouseDoubleClick += _MouseLeftButtonDown;

            listBox1.Items.Add(lbi);

            ListBoxItem lbi2 = new ListBoxItem();
            lbi2.Content = "inc eax";
            lbi2.MouseDoubleClick += _MouseLeftButtonDown;
            listBox1.Items.Add(lbi2);

            listBox1.SelectedIndex = 0;

            

            // run the text in the GUI editor.
            Computer computer = new Computer();
            // todo computer.cPU = new X86CPU(computer);
            computer.memory = new Memory();

            Human Matthew = new Human();

            // todo Matthew.Run(computer, MatthewsProgram);
        }

        private void _MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbi = sender as ListBoxItem;

            //string input = Microsoft.VisualBasic.Interaction.InputBox("Enter a new line (was: " + lbi.Content as string + ")", "Change line", lbi.Content as string, -1, -1);

            //lbi.Content = input;
        }
        // todo Remove reference to VisualBasic (used for test InputBox).
    }
}
