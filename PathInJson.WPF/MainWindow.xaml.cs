using PathInJson.WPF.Helpers;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PathInJson.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FolderHelper folderHelper;
        public JsonHelper jsonHelper;

        public TreeViewItem selectedItem;
        public string jsonText;
        public string path;

        public MainWindow()
        {
            InitializeComponent();

            folderHelper = new FolderHelper();
            jsonHelper = new JsonHelper();
        }

        private void clear_btn_Click(object sender, RoutedEventArgs e)
        {
            showFile_txtbx.Text = string.Empty;
            jsonText = string.Empty;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string drive in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = drive;
                item.Tag = drive;
                item.Items.Add(null);
                item.Expanded += new RoutedEventHandler(foldersItem_Expanded);
                foldersItem.Items.Add(item);
            }
        }

        private void foldersItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item.Items.Count == 1 && item.Items[0] == null)
            {
                item.Items.Clear();
                try
                {
                    foreach (string folder in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem item1 = new TreeViewItem();
                        DirectoryInfo directoryInfo = new DirectoryInfo(folder);
                        item1.Header = directoryInfo.Name;
                        item1.Tag = folder;
                        item1.Items.Add(null);
                        item1.Expanded += new RoutedEventHandler(foldersItem_Expanded);
                        item.Items.Add(item1);
                    }
                }
                catch (UnauthorizedAccessException) { }
            }
        }

        private void select_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(path))
            {
                jsonText = jsonHelper.CreateJSONFromHierarchicalList(folderHelper.CreateHierarchy(path));
                showFile_txtbx.Text = jsonText;
            }
            else
            {
                MessageBox.Show("Folder doesn't exist!");
            }
        }

        private void foldersItem_Selected(object sender, RoutedEventArgs e)
        {
            selectedItem = e.OriginalSource as TreeViewItem;
            path = selectedItem.Tag.ToString();
            path_txtbx.Text = path;
        }

        private void save_bnt_Click(object sender, RoutedEventArgs e)
        {
            if (jsonText != string.Empty && path != string.Empty)
            {
                MessageBoxResult result = MessageBox.Show("File will be saved to " + path + " directory", "PathInJson", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        jsonHelper.WriteToFile(path, jsonText);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Nothing to save!");
            }
        }

        private void path_txtbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            path = path_txtbx.Text;
        }
    }
}
