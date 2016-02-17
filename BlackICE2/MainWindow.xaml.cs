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












            TextBox tb = new TextBox();

            tb.Text = "hahaha";



            listBox.Items.Add(tb);
            // ^ To allow editable code at runtime, always read code from the textBox, not the internal data structures, and... 
            // ... either use textBoxes at listBox items, or allow double-clicking or something on listBox rows to brign up modal dialog to edit the line's code?



            listBox.SelectedIndex = 1;

            //listBox.IsHitTestVisible = false;



            TextBox tb2 = new TextBox();

            tb2.Text = "wahaha~";

            listBox.Items.Add(tb2);












            ListBoxItem lbi = new ListBoxItem();
            lbi.Content = "hahaha";
            lbi.MouseDoubleClick += _MouseLeftButtonDown;

            listBox1.Items.Add(lbi);

            ListBoxItem lbi2 = new ListBoxItem();
            lbi2.Content = "wahaha~";
            lbi2.MouseDoubleClick += _MouseLeftButtonDown;
            listBox1.Items.Add(lbi2);

            listBox1.SelectedIndex = 0;
        }

        private void _MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbi = sender as ListBoxItem;

            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter a new line (was: " + lbi.Content as string + ")", "Change line", lbi.Content as string, -1, -1);

            lbi.Content = input;
        }
        // todo Remove reference to VisualBasic (used for test InputBox).
    }
}
