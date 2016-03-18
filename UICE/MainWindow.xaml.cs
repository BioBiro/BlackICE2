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

namespace UICE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }




        /*
         * <Window x:Class="BlackICE2.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:BlackICE2"
        mc:Ignorable="d"
        Title="BlackICE2" Height="529" Width="544" ResizeMode="CanMinimize" Icon="crack_w51_icon.ico">
    <Grid Margin="0,0,-6,5">
        <Button x:Name="button" Content="Step" HorizontalAlignment="Left" Margin="10,430,0,0" VerticalAlignment="Top" Width="75" Click="button_Click"/>
        <Menu x:Name="menu" HorizontalAlignment="Left" Height="20" VerticalAlignment="Top" Width="538">
            <MenuItem Header="File" Margin="0">
                <MenuItem Header="Load ASM" HorizontalAlignment="Left" Click="MenuItem_Click"/>
                <MenuItem Header="Load machine code" HorizontalAlignment="Left" Click="MenuItem_Click_2"/>
            </MenuItem>
        </Menu>
        <GroupBox x:Name="groupBox" Header="ASM" HorizontalAlignment="Left" Height="202" Margin="10,25,0,0" VerticalAlignment="Top" Width="180">
            <Grid HorizontalAlignment="Left" Height="192" VerticalAlignment="Top" Width="170" Margin="0,0,-2,-8">
                <ListBox x:Name="listBox1" HorizontalAlignment="Left" Height="165" Margin="10,10,0,0" VerticalAlignment="Top" Width="150" IsHitTestVisible="False"/>
            </Grid>
        </GroupBox>
        <GroupBox x:Name="groupBox1" Header="Memory" HorizontalAlignment="Left" Margin="346,25,0,0" VerticalAlignment="Top" Width="179" Height="465">
            <Grid HorizontalAlignment="Left" Height="455" VerticalAlignment="Top" Width="169" Margin="0,0,-2,-8">
                <ListBox x:Name="listBox2" HorizontalAlignment="Left" Height="430" Margin="10,10,0,0" VerticalAlignment="Top" Width="148"/>
            </Grid>
        </GroupBox>
        <GroupBox x:Name="groupBox2" Header="Registers" HorizontalAlignment="Left" Margin="10,232,0,0" VerticalAlignment="Top" Height="166" Width="180">
            <Grid HorizontalAlignment="Left" Height="156" VerticalAlignment="Top" Width="170" Margin="0,0,-2,-8">
                <Label x:Name="label" Content="EAX" HorizontalAlignment="Left" Margin="10,10,0,0" VerticalAlignment="Top"/>
                <Label x:Name="label1" Content="Label" HorizontalAlignment="Left" Margin="44,10,0,0" VerticalAlignment="Top"/>
                <Label Content="EBX" HorizontalAlignment="Left" Margin="10,38,0,0" VerticalAlignment="Top"/>
                <Label x:Name="label2" Content="Label" HorizontalAlignment="Left" Margin="44,38,0,0" VerticalAlignment="Top"/>
                <Label Content="SP" HorizontalAlignment="Left" Margin="17,82,0,0" VerticalAlignment="Top"/>
                <Label x:Name="label3" Content="Label" HorizontalAlignment="Left" Margin="44,82,0,0" VerticalAlignment="Top"/>
                <Label Content="IP" HorizontalAlignment="Left" Margin="17,110,0,0" VerticalAlignment="Top"/>
                <Label x:Name="label4" Content="Label" HorizontalAlignment="Left" Margin="44,110,0,0" VerticalAlignment="Top"/>
            </Grid>
        </GroupBox>
        <TextBox x:Name="textBox" HorizontalAlignment="Left" Height="22" Margin="115,403,0,0" TextWrapping="Wrap" Text="8" VerticalAlignment="Top" Width="75"/>
        <GroupBox x:Name="groupBox3" Header="Memory Layout" HorizontalAlignment="Left" Margin="195,421,0,0" VerticalAlignment="Top" Height="68" Width="146">
            <Grid HorizontalAlignment="Left" Height="58" Margin="0,0,-2,-8" VerticalAlignment="Top" Width="136">
                <RadioButton x:Name="radioButton" Content="Bottom-up" HorizontalAlignment="Left" Margin="10,10,0,0" VerticalAlignment="Top" Checked="radioButton_Checked"/>
                <RadioButton x:Name="radioButton1" Content="Top-down" HorizontalAlignment="Left" Margin="10,28,0,0" VerticalAlignment="Top" Checked="radioButton1_Checked" IsChecked="True"/>
            </Grid>
        </GroupBox>
        <Label Content="Entry point (bytes)" HorizontalAlignment="Left" Margin="10,399,0,0" VerticalAlignment="Top"/>

    </Grid>
</Window>

         * */








        // really useful --> https://defuse.ca/online-x86-assembler.htm

        private void _MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbi = sender as ListBoxItem;

            //string input = Microsoft.VisualBasic.Interaction.InputBox("Enter a new line (was: " + lbi.Content as string + ")", "Change line", lbi.Content as string, -1, -1);

            //lbi.Content = input;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Singleton.GetSingleton().human.loader.Step(Singleton.GetSingleton().computer);
            Computer c = Singleton.GetSingleton().computer; // todo DELETE ME



            int ipx = Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];

            listBox1.SelectedIndex = Singleton.GetSingleton().computer.memory.virtualAddressSpace[ipx].asmLine;// ( - (int)(sliderEntryPointer.Value)) - 1;



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

                if (this.uglyGlobalDirection == 1)
                {
                    // Redraw virtual address space
                    listBox2.Items.Clear();

                    for (int i = 0; i < Singleton.GetSingleton().computer.memory.virtualAddressSpace.Count; i++)
                    {
                        ListBoxItem lbix = new ListBoxItem();
                        lbix.Content = "[" + i.ToString() + "] " + Singleton.GetSingleton().computer.memory.virtualAddressSpace[i].value.ToString();
                        //lbix.MouseDoubleClick += _MouseLeftButtonDown;
                        listBox2.Items.Add(lbix);
                    }



                    listBox2.SelectedIndex = Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];
                }
                else if (this.uglyGlobalDirection == 2)
                {
                    // Redraw virtual address space
                    listBox2.Items.Clear();

                    for (int i = Singleton.GetSingleton().computer.memory.virtualAddressSpace.Count - 1; i >= 0; i--)
                    {
                        ListBoxItem lbix = new ListBoxItem();
                        lbix.Content = "[" + i.ToString() + "] " + Singleton.GetSingleton().computer.memory.virtualAddressSpace[i].value.ToString();
                        //lbix.MouseDoubleClick += _MouseLeftButtonDown;
                        listBox2.Items.Add(lbix);
                    }



                    listBox2.SelectedIndex = 31 - Singleton.GetSingleton().computer.cPU.GetRegisters().GetRegister((int)(X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0];
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // new program typed in; open an existing program, etc. Instructions are put into GUI editor.
                // todo - make this work sooner rather than later --> Allow self-modyfying code ONLY on the code segment machine code (use an event that is triggered when you edit a line in the GUI or something).
                // ^ Don't worry about how you're going to index the correct machine code character(s) to change - just put the entire code segment into the GUI, then let the form component word-wrap it. You can send the whole thing forwards/back if you want into the Loader in memory, or come up with a fancier way of indexing it if you want.

                string[] source = File.ReadAllLines(openFileDialog.FileName);

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
    }
}
