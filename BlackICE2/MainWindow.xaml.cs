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

using System.IO;
using Microsoft.Win32;

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



            // really useful --> https://defuse.ca/online-x86-assembler.htm



            TextRange rangeOfText1 = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd); // end to end
            rangeOfText1.Text = "Text1 ";
            rangeOfText1.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
            rangeOfText1.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);

            TextRange rangeOfWord = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd); // end to end
            rangeOfWord.Text = "word ";
            rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
            rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);

            TextRange rangeOfText2 = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd); // start to end
            rangeOfText2.Text = "Text2 ";
            rangeOfText2.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
            rangeOfText2.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            rangeOfText2.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
        }

        private void _MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbi = sender as ListBoxItem;

            Edit edit = new Edit();
            edit.oldValue = Int32.Parse(lbi.Content as string);
            lbi.Content = edit.newValue;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Singleton.GetSingleton().human.loader.Step(Singleton.GetSingleton().computer);
            Computer c = Singleton.GetSingleton().computer; // todo DELETE ME


            int ipx = Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];



            // Redraw the assembly instructions.
            listBox1.Items.Clear();

            for (int i = 0; i < this.source.Length; i++)
            {
                ListBoxItem lbix = new ListBoxItem();
                lbix.Content = this.source[i];
                //lbix.MouseDoubleClick += _MouseLeftButtonDown;

                if (i == Singleton.GetSingleton().computer.memory.virtualAddressSpace[ipx].asmLine)// ( - (int)(sliderEntryPointer.Value)) - 1)
                {
                    lbix.Foreground = System.Windows.Media.Brushes.Yellow;
                    lbix.Background = System.Windows.Media.Brushes.Aqua;
                }
                else
                {
                    lbix.Foreground = System.Windows.Media.Brushes.Red;
                    lbix.Background = System.Windows.Media.Brushes.Lime;
                }

                listBox1.Items.Add(lbix);
            }

            //listBox1.SelectedIndex = Singleton.GetSingleton().computer.memory.virtualAddressSpace[ipx].asmLine;// ( - (int)(sliderEntryPointer.Value)) - 1;









            // Redraw reverse assembly instructions.
            listBox3.Items.Clear();

            X86InstructionSet instructions = new X86InstructionSet(Singleton.GetSingleton().computer); // todo Remove, as only instantiated for the ToSkip() call below.

            int loopipx = 0;
            int reverseAsmLineCounter = 0;

            for (loopipx = 0; loopipx < 15; loopipx += 0) // Replace loop iteration maximum constant with length of 'codeSegment' in 'Program'.
            {
                int toSkip = instructions.ToSkip(Singleton.GetSingleton().computer.memory.virtualAddressSpace[loopipx].value); // Number of parameters.
                //int toSkip2 = toSkip + Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];

                string methodName = "_" + Singleton.GetSingleton().computer.memory.virtualAddressSpace[loopipx].value;

                string parameterGarbage = "";



                // Loop through opcode's parameters.
                for (int x = 1; x < toSkip; x++)
                {
                    parameterGarbage = "";

                    parameterGarbage += Singleton.GetSingleton().computer.memory.virtualAddressSpace[loopipx + x].value;
                }               
                


                Singleton.GetSingleton().computer.memory.virtualAddressSpace[loopipx].reverseAsmLine = reverseAsmLineCounter;
                loopipx += toSkip;
                if (loopipx == 14)
                {
                    List<Address> la = Singleton.GetSingleton().computer.memory.virtualAddressSpace; // todo DELETE ME
                }
                reverseAsmLineCounter += 1; // toSkip may be more than 1, but asmLine bump will always be 1.

                ListBoxItem lbix2 = new ListBoxItem();

                lbix2.Content = instructions.opcodes[methodName] + parameterGarbage;



                if (loopipx == Singleton.GetSingleton().computer.memory.virtualAddressSpace[ipx].reverseAsmLine)// ( - (int)(sliderEntryPointer.Value)) - 1)
                {
                    lbix2.Foreground = System.Windows.Media.Brushes.Yellow;
                    lbix2.Background = System.Windows.Media.Brushes.Aqua;
                }
                else
                {
                    lbix2.Foreground = System.Windows.Media.Brushes.Red;
                    lbix2.Background = System.Windows.Media.Brushes.Lime;
                }



                listBox3.Items.Add(lbix2);
            }
            
            









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


            RedrawMemory();
        }

        private void RedrawMemory()
        {
            if (Singleton.GetSingleton().computer != null)
            {
                int instructionPointer = Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];
                int stackPointer = Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.STACK_POINTER), 0)[0];

                if (this.uglyGlobalDirection == 1)
                {
                    // Redraw virtual address space
                    listBox2.Items.Clear();

                    for (int i = 0; i < Singleton.GetSingleton().computer.memory.virtualAddressSpace.Count; i++)
                    {
                        ListBoxItem lbix = new ListBoxItem();
                        lbix.Content = "[" + i.ToString() + "] " + Singleton.GetSingleton().computer.memory.virtualAddressSpace[i].value.ToString();
                        lbix.MouseDoubleClick += _MouseLeftButtonDown;

                        if (i == instructionPointer)
                        {
                            lbix.Foreground = System.Windows.Media.Brushes.Yellow;
                            lbix.Background = System.Windows.Media.Brushes.Aqua;
                        }
                        else if (i == stackPointer)
                        {
                            lbix.Foreground = System.Windows.Media.Brushes.Aqua;
                            lbix.Background = System.Windows.Media.Brushes.Yellow;
                        }
                        else
                        {
                            lbix.Foreground = System.Windows.Media.Brushes.Red;
                            lbix.Background = System.Windows.Media.Brushes.Lime;
                        }

                        listBox2.Items.Add(lbix);
                    }
                }
                else if (this.uglyGlobalDirection == 2)
                {
                    // Redraw virtual address space
                    listBox2.Items.Clear();

                    for (int i = Singleton.GetSingleton().computer.memory.virtualAddressSpace.Count - 1; i >= 0; i--)
                    {
                        ListBoxItem lbix = new ListBoxItem();
                        lbix.Content = "[" + i.ToString() + "] " + Singleton.GetSingleton().computer.memory.virtualAddressSpace[i].value.ToString();
                        lbix.MouseDoubleClick += _MouseLeftButtonDown;
                        listBox2.Items.Add(lbix);
                    }


                    listBox2.SelectedIndex = 31 - Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }



        string[] source; // todo Remove - global hack
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Assembly language|*.asm";
            if (openFileDialog.ShowDialog() == true)
            {
                // new program typed in; open an existing program, etc. Instructions are put into GUI editor.
                // todo - make this work sooner rather than later --> Allow self-modyfying code ONLY on the code segment machine code (use an event that is triggered when you edit a line in the GUI or something).
                // ^ Don't worry about how you're going to index the correct machine code character(s) to change - just put the entire code segment into the GUI, then let the form component word-wrap it. You can send the whole thing forwards/back if you want into the Loader in memory, or come up with a fancier way of indexing it if you want.

                source = File.ReadAllLines(openFileDialog.FileName);

                foreach (string s in source)
                {
                    ListBoxItem lbi = new ListBoxItem();
                    lbi.Content = s;
                    lbi.MouseDoubleClick += _MouseLeftButtonDown;
                    listBox1.Items.Add(lbi);
                }

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

                p.entryPoint = (int)(sliderEntryPointer.Value); <-- textBox replaces this now!

                Singleton.GetSingleton().human.loader.Load(Singleton.GetSingleton().computer, p);*/
            }

            RedrawMemory();
        }



        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Machine code|*.bin";
            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    // Read bytes from stream and interpret them as ints. Use BinaryReader if you need bytes instead of integers when reading.
                    int value = 0;
                    while ((value = fs.ReadByte()) != -1)
                    {
                        Console.WriteLine(value);
                    }
                }                
            }
        }



        int uglyGlobalDirection = 1; // todo Remove.

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.uglyGlobalDirection = 1;

            RedrawMemory();
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            this.uglyGlobalDirection = 2;

            RedrawMemory();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BlackICE2 - a CPU emulator and low-level test harness."
                + Environment.NewLine
                + Environment.NewLine
                + "Written by Matthew Hirst, 2016."
                , "About BlackICE2");
        }
    }
}
