using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Shapes;

namespace _05_SP_Async_Await
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string source = "";
        static string destination = "";
        static Random random = new Random();
        static Task CopyFileAsync()
        {
            return Task.Run(() => File.Copy(source, destination + "\\copy" + System.IO.Path.GetFileName(source)));

        }
        public MainWindow()
        {
            InitializeComponent();
        }
        //async - allow method to use await keyword
        //await - wait task without freezing

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users";
            //dialog.IsFolderPicker = true;
            //dialog.Multiselect = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                source = dialog.FileName;
                from.Text = source;
                MessageBox.Show("You selected: " + dialog.FileName);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            //dialog.Multiselect = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                destination = dialog.FileName;
                to.Text = destination;
                MessageBox.Show("You selected: " + dialog.FileName);
            }

        }
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            await CopyFileAsync();
            MessageBox.Show($"Complited");
        }
        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            source = from.Text;
            destination = to.Text;
            await CopyFileAsync();
            MessageBox.Show($"Complited");
        }
    }
}