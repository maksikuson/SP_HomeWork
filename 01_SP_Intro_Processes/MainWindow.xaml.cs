using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _01_SP_Intro_Processes
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grid.ItemsSource = Process.GetProcesses();
            MessageBox.Show((timeCB.SelectedItem).ToString());
            MessageBox.Show(check.Content.ToString());



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            dynamic selectedProcess = grid.SelectedItem;
            if (selectedProcess != null)
            {
                int processId = selectedProcess.Id;
                Process prToKill = Process.GetProcessById(processId);
                MessageBox.Show(prToKill.ProcessName);
                prToKill.Kill();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Process process = Process.GetProcessById((int)(grid.SelectedItem.GetType().GetProperty("Id").GetValue(grid.SelectedItem, null)));
            process.CloseMainWindow();
            MessageBox.Show($"Process {process.ProcessName} has been closed.");
        }



        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Process process = Process.GetProcessById((int)(grid.SelectedItem.GetType().GetProperty("Id").GetValue(grid.SelectedItem, null)));
            MessageBox.Show($"Process Info:\n" +
                            $"Name: {process.ProcessName}\n" +
                            $"PID: {process.Id}\n" +
                            $"Total Processor Time: {process.TotalProcessorTime}\n" +
                            $"Priority: {process.PriorityClass}\n");
            // $"User: {GetProcessOwner(process)}");

        }

        //private object GetProcessOwner(Process process)
        //{
        //    throw new NotImplementedException();
        //}

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string processName = nameProcess.Text;
            Process.Start(processName);
            MessageBox.Show($"Process {processName} started.");
            //MessageBox.Show(nameProcess.Text);

        }

    }
}