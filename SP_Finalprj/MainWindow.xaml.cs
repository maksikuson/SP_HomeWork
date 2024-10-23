using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace WordSearchApp
{
    public partial class MainWindow : Window
    {
        private List<string> searchResults = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                Multiselect = false,
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DirectoryPath.Text = dialog.FileName;
            }
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            ResultList.Items.Clear();
            searchResults.Clear();

            string directory = DirectoryPath.Text;
            string searchWord = SearchWord.Text;

            if (string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(searchWord))
            {
                MessageBox.Show("Please specify both the directory and the word.");
                return;
            }

            if (!Directory.Exists(directory))
            {
                MessageBox.Show("Directory does not exist.");
                return;
            }

            var files = Directory.EnumerateFiles(directory, "*.txt", SearchOption.AllDirectories);
            int totalFiles = files.Count();
            int processedFiles = 0;

            ProgressBar.Maximum = totalFiles;

            foreach (var file in files)
            {
                int wordCount = await SearchWordInFileAsync(file, searchWord);
                if (wordCount > 0)
                {
                    string result = $"File: {Path.GetFileName(file)}, Path: {file}, Word Count: {wordCount}";
                    searchResults.Add(result);
                    ResultList.Items.Add(result);
                }
                processedFiles++;
                ProgressBar.Value = processedFiles;
            }

            if (processedFiles == totalFiles)
            {
                MessageBox.Show("Search completed!");
            }
        }

        private async Task<int> SearchWordInFileAsync(string filePath, string searchWord)
        {
            return await Task.Run(() => CountWordOccurrencesInFile(filePath, searchWord));
        }

        private int CountWordOccurrencesInFile(string filePath, string searchWord)
        {
            int count = 0;
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    count += line.Split(new string[] { searchWord }, StringSplitOptions.None).Length - 1;
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => ResultList.Items.Add($"Error reading file: {filePath} - {ex.Message}"));
            }
            return count;
        }

        private async void SaveResults_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text file (*.txt)|*.txt",
                Title = "Save search results"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var item in searchResults)
                    {
                        await writer.WriteLineAsync(item);
                    }
                }
                MessageBox.Show("Results saved successfully.");
            }
        }
    }
}