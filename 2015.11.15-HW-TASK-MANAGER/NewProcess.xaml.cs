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

namespace _2015._11._15_HW_TASK_MANAGER
{
    /// <summary>
    /// Interaction logic for NewProcess.xaml
    /// </summary>
    public partial class NewProcess : Window
    {
        public Action<string> ReferenceToTextBox { set; get; }
        public NewProcess()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxNameProcess.Text == "") return;
            ReferenceToTextBox(textBoxNameProcess.Text);
            DialogResult = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
