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

        Folder lastScannedFolder;

        Folder LastScannedFolder
        {
            set
            {
                lastScannedFolder = value;
                scanningDone();
            }

            get
            {
                return lastScannedFolder;
            }
        }

        private string path = string.Empty;
        private bool scanningInProgress = false;

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

        private async void scan_ClickAsync(object sender, RoutedEventArgs e)
        {
            scanningInProgress = true;
            scanButton.Content = "Scanning...";
            LastScannedFolder = await Task.Run(() => FileScanner.scanFolders(path, changeLabel, scanningDone));
        }

        public void changeLabel(string text)
        {
            //Invoking the main thread of the UI
            currentFolderLabel.Dispatcher.Invoke(new Action(() => { currentFolderLabel.Content = text; }));
        }

        public void scanningDone()
        {
            scanningInProgress = false;
            scanButton.Dispatcher.Invoke(new Action(() => { scanButton.Content = "Scan"; }));
            currentFolderLabel.Dispatcher.Invoke(new Action(() => { currentFolderLabel.Content = string.Empty; }));

            lastScannedFolder?.saveToFile(getFilename());
        }

        private string getFilename()
        {
            return ((DateTime.Now.ToShortTimeString() + "_" + DateTime.Now.ToShortDateString()).Replace('/','_').Replace(':','_')) + ".XML";
        }

        private void folderPathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            pickedFolderPath = folderPathTextBox.Text;
        }
    }
}
