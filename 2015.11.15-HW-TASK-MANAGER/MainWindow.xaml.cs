using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Timers;

namespace _2015._11._15_HW_TASK_MANAGER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Process process = null;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            RefreshInformationToListBox();
            Info();
        }
        private void RefreshInformationToListBox()
        {
            listBoxProcess.ItemsSource = Process.GetProcesses(".").OrderBy(p => p.ProcessName).Select(p => p.ProcessName);
        }
        private void Info()
        {
            if (listBoxProcess.SelectedItem == null) return;

            Process myProc = Process.GetProcessesByName(listBoxProcess.SelectedItem.ToString()).ToList<Process>()[0];
            ProcessThreadCollection threads = myProc.Threads;
            textBoxInfo.Text = "";
            foreach (ProcessThread pt in threads)
                try
                {
                    textBoxInfo.Text += String.Format("ID: {0:d5}  Время: {1}\tПриоритет: {2}\n"
                        , pt.Id, pt.StartTime.ToShortTimeString(), pt.PriorityLevel);

                }
                catch (Exception ex)
                {
                    textBoxInfo.Text += ex.Message + "\n";
                }
        }
        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            if(btnRefresh != null) btnRefresh.IsEnabled = false;
            dispatcherTimer.Start();
        }
        private void rb2_Checked(object sender, RoutedEventArgs e)
        {
            if(btnRefresh != null) btnRefresh.IsEnabled = true;
            dispatcherTimer.Stop();
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshInformationToListBox();
         
            Info();
        }
        private void btnCloseProcess_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxProcess.SelectedItem != null)
            {
                List<Process> proc = Process.GetProcessesByName(listBoxProcess.SelectedItem.ToString()).ToList<Process>();
                foreach (var item in proc)
                {
                    if(item != null)
                        item.Kill();
                }
                Thread.Sleep(500); 
                RefreshInformationToListBox();
            }
        }
        private void listBoxProcess_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Info();
        }

        private void btnNewProcess_Click(object sender, RoutedEventArgs e)
        {
            string textBoxProcess = "";
            NewProcess np = new NewProcess();
            np.ReferenceToTextBox +=  delegate(string obj) {textBoxProcess = obj; };
            if (np.ShowDialog() == true)
            {
                try
                {
                    Process pr = new Process();
                    pr.StartInfo = new ProcessStartInfo(textBoxProcess);
                    pr.Start();
                }
                catch (Exception)
                {

                    MessageBox.Show("Извините такой программы нет");
                }
                
            }
           
        }
    }
}
