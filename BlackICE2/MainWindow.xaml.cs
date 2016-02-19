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
            // ^ Don't worry about how you're going to index the correct machine code character(s) to change - just put the entire code segment into the GUI, then let the form component word-wrap it. You can send the whole thing forwards/back if you want into the Loader in memory, or come up with a fancier way of indexing it if you want.
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
            Singleton.GetSingleton().computer = new Computer();
            Singleton.GetSingleton().computer.cPU = new X86CPU(Singleton.GetSingleton().computer);
            Singleton.GetSingleton().computer.memory = new Memory();

            Singleton.GetSingleton().human = new Human();

            List<string> MatthewsProgram = new List<string>();

            foreach (ListBoxItem listBoxItem in listBox1.Items)
            {
                MatthewsProgram.Add(listBoxItem.Content as string);
            }

            Singleton.GetSingleton().human.Run(Singleton.GetSingleton().computer, MatthewsProgram);

            // https://defuse.ca/online-x86-assembler.htm
        }

        private void _MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbi = sender as ListBoxItem;

            //string input = Microsoft.VisualBasic.Interaction.InputBox("Enter a new line (was: " + lbi.Content as string + ")", "Change line", lbi.Content as string, -1, -1);

            //lbi.Content = input;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Singleton.GetSingleton().human.loader.Step(Singleton.GetSingleton().computer);
            listBox1.SelectedIndex += 1;



            byte[] modsvalue = Helper.GetHelper().PadWithBytes(Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister(0, 0), 4);
            int inced = BitConverter.ToInt32(modsvalue, 0);

            label1.Content = inced.ToString();
        }
    }
}
