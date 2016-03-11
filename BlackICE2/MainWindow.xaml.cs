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



            // It would be neat if you could dissasembler the binary/machine code into assembly instructions, for external files in this program (a dissasembler using the instruction-fetch for next instructino that you'll end up writing for this ICE).



            // new program typed in; open an existing program, etc. Instructions are put into GUI editor.
            // todo - make this work sooner rather than later --> Allow self-modyfying code ONLY on the code segment machine code (use an event that is triggered when you edit a line in the GUI or something).
            // ^ Don't worry about how you're going to index the correct machine code character(s) to change - just put the entire code segment into the GUI, then let the form component word-wrap it. You can send the whole thing forwards/back if you want into the Loader in memory, or come up with a fancier way of indexing it if you want.
            ListBoxItem lbi = new ListBoxItem();
            lbi.Content = "mov eax, 10";
            lbi.MouseDoubleClick += _MouseLeftButtonDown;
            listBox1.Items.Add(lbi);

            ListBoxItem lbi2 = new ListBoxItem();
            lbi2.Content = "call @@myfunction";
            lbi2.MouseDoubleClick += _MouseLeftButtonDown;
            listBox1.Items.Add(lbi2);

            ListBoxItem lbi3 = new ListBoxItem();
            lbi3.Content = "inc eax";
            lbi3.MouseDoubleClick += _MouseLeftButtonDown;
            listBox1.Items.Add(lbi3);

            ListBoxItem lbi4 = new ListBoxItem();
            lbi4.Content = "inc eax";
            lbi4.MouseDoubleClick += _MouseLeftButtonDown;
            listBox1.Items.Add(lbi4);

            ListBoxItem lbi5 = new ListBoxItem();
            lbi5.Content = "@@myfunction";
            lbi5.MouseDoubleClick += _MouseLeftButtonDown;
            listBox1.Items.Add(lbi5);

            ListBoxItem lbi6 = new ListBoxItem();
            lbi6.Content = "mov ebx, eax";
            lbi6.MouseDoubleClick += _MouseLeftButtonDown;
            listBox1.Items.Add(lbi6);

            ListBoxItem lbi7 = new ListBoxItem();
            lbi7.Content = "ret";
            lbi7.MouseDoubleClick += _MouseLeftButtonDown;
            listBox1.Items.Add(lbi7);

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

            // skipped as we are testing direct machine code below --> 
            Program mahProgram = Singleton.GetSingleton().human.CreateProgram(Singleton.GetSingleton().computer, MatthewsProgram);



            // Now load the program into the computer/CPU.
            Singleton.GetSingleton().human.PrepareProgram(Singleton.GetSingleton().computer, mahProgram);

            // shove virtual address space into GUI.
            /*ListBoxItem lbix1 = new ListBoxItem();
            lbix1.Content = "184";
            listBox.Items.Add(lbix1);

            ListBoxItem lbix2 = new ListBoxItem();
            lbix2.Content = "07";
            listBox.Items.Add(lbix2);

            ListBoxItem lbix3 = new ListBoxItem();
            lbix3.Content = "40";
            listBox.Items.Add(lbix3);

            listBox.SelectedIndex = 0;

            // entryPoint assumed to be 0.


            // Create Program object with code segment on fly...
            Program p = new Program();
            foreach (ListBoxItem listBoxItem in listBox.Items)
            {
                Int32 int32 = Int32.Parse(listBoxItem.Content as string);
                p.codeSegment.Add( BitConverter.GetBytes(int32)[0]); // First byte only (8-bits).
            }

            p.entryPoint = (int)(sliderEntryPointer.Value);

            Singleton.GetSingleton().human.loader.Load(Singleton.GetSingleton().computer, p);*/


            // really useful --> https://defuse.ca/online-x86-assembler.htm
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



            listBox.SelectedIndex += (Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0] - (int)(sliderEntryPointer.Value));



            byte[] modsvalue = Helper.GetHelper().PadWithBytes(Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.ACCUMULATOR), 0), 4);
            int inced = BitConverter.ToInt32(modsvalue, 0);
            label1.Content = inced.ToString();



            modsvalue = Helper.GetHelper().PadWithBytes(Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.BASE), 0), 4);
            inced = BitConverter.ToInt32(modsvalue, 0);
            label2.Content = inced.ToString();



            modsvalue = Helper.GetHelper().PadWithBytes(Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.STACK_POINTER), 0), 4);
            inced = BitConverter.ToInt32(modsvalue, 0);
            label3.Content = inced.ToString();



            modsvalue = Helper.GetHelper().PadWithBytes(Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0), 4);
            inced = BitConverter.ToInt32(modsvalue, 0);
            label4.Content = inced.ToString();
        }
    }
}
