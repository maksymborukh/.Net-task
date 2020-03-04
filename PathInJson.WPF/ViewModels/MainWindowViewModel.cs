using PathInJson.WPF.Command;
using PathInJson.WPF.Helpers;
using PathInJson.WPF.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace PathInJson.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private FolderHelper _folderHelper;
        private JsonHelper _jsonHelper;
        public ObservableCollection<FolderViewModel> Items { get; set; }

        private string _path;
        private string _jsonText;

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        public string JsonText
        {
            get { return _jsonText; }
            set
            {
                _jsonText = value;
                OnPropertyChanged("JsonText");
            }
        }

        public ICommand ClearCommand { get; set; }  
        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand SelectedItemChangedCommand { get; set; }


        private void selectedChanged(object obj)
        {
            foreach (var item in Items)
            {
                var t = FindSelected(item);
                if (t != null)
                {
                    Path = t.Item.Path;
                    break;
                }
            }
        }

        private FolderViewModel FindSelected(FolderViewModel folder)
        {
            FolderViewModel result = null;

            if (folder.IsSelected)
                return folder;
            else
            {
                var t = folder.Children;
                if (t != null)
                {
                    foreach (FolderViewModel child in t)
                    {
                        if (child != null && child.IsSelected)
                        {
                            Path = child.Item.Path;
                            break;
                        }
                        if (child != null && result == null)
                        {
                            FindSelected(child);
                        }
                    }
                }
            }
            return result;
        }

        private void loadBtn(object obj)
        {
            if (Directory.Exists(Path))
            {
                JsonText = _jsonHelper.CreateJSONFromHierarchicalList(_folderHelper.CreateHierarchy(Path));
            }
            else
            {
                MessageBox.Show("Folder doesn't exist!");
            }
        }

        private void clearBtn(object obj)
        {
            JsonText = string.Empty;
        }

        private void saveBtn(object obj)
        {
            if (JsonText != string.Empty && Path != string.Empty)
            {
                MessageBoxResult result = MessageBox.Show("File will be saved to " + Path + " directory", "PathInJson", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        _jsonHelper.WriteToFile(Path, JsonText);
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

        public MainWindowViewModel()
        {
            Path = string.Empty;
            JsonText = string.Empty;
            _jsonHelper = new JsonHelper();
            _folderHelper = new FolderHelper();

            SaveCommand = new RelayCommand(saveBtn);
            ClearCommand = new RelayCommand(clearBtn);
            LoadCommand = new RelayCommand(loadBtn);
            SelectedItemChangedCommand = new RelayCommand(selectedChanged);

            LoadLogicDrives();
        }

        private void LoadLogicDrives()
        {
            Items = new ObservableCollection<FolderViewModel>();
            foreach (string drive in Directory.GetLogicalDrives())
            {
                DirectoryItem item = new DirectoryItem(drive, drive);
                FolderViewModel folder = new FolderViewModel(item);
                Items.Add(folder);
            }         
        }
    }
}
