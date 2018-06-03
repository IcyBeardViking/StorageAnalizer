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
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using MahApps.Metro.Controls;
using System.ComponentModel;
using StorageAnalizer.DataStructure;

namespace StorageAnalizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            scanButton.IsEnabled = false;
            currentFolderLabel.Content = string.Empty;

        }

        private string path = string.Empty;

        public string pickedFolderPath {
            get
            {
                return path;
            }
            set
            {
                path = value;
                folderPathTextBox.Text = path;
            }
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog("Pick a folder...")
            {
                IsFolderPicker = true,
                EnsurePathExists = false,
                Multiselect = false,
                Title = "Pick a folder..."
            };

            CommonFileDialogResult result = CommonFileDialogResult.Ok;
            result = dialog.ShowDialog();

            if (result is CommonFileDialogResult.Ok)
            {
                pickedFolderPath = dialog.FileName;
                scanButton.IsEnabled = true;
            }
        }

        private void scan_Click(object sender, RoutedEventArgs e)
        {
            string size = string.Empty;

            Folder mainFolder = FileScanner.scanFolders(path, changeLabel);

            size = (mainFolder.Size / 1024 / 1024 / 1024).ToString() + " GB";

            System.Windows.Forms.MessageBox.Show(size);

            mainFolder.saveToFile("testing.xml");

        }

        public void changeLabel(string text)
        {
            currentFolderLabel.Content = text;
        }

        private void folderPathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            pickedFolderPath = folderPathTextBox.Text;
        }
    }
}
