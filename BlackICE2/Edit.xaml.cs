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
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public int oldValue { get; set; }
        public int newValue { get; set; }



        public Edit()
        {
            InitializeComponent();

            this.lEdit.Content = this.oldValue;
            
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.newValue = Int32.Parse(this.tbEdit.Text);
            
            this.Close();
        }
    }
}
