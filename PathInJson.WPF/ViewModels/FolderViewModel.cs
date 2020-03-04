using PathInJson.WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace PathInJson.WPF.ViewModels
{
    public class FolderViewModel : ViewModelBase
    {
        private DirectoryItem _item;
        private ObservableCollection<FolderViewModel> _children;
        private FolderViewModel _parent;

        private bool _isExpanded;
        private bool _isSelected;

        public FolderViewModel(DirectoryItem item)
            : this(item, null)
        {
        }

        public FolderViewModel(DirectoryItem item, FolderViewModel parent)
        {
            _item = item;
            _parent = parent;

            _children = new ObservableCollection<FolderViewModel>();
            _children.Add(null);
        }

        public DirectoryItem Item
        {
            get { return _item; }
        }

        public ObservableCollection<FolderViewModel> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                OnPropertyChanged("Children");
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }

                LoadChildren();
            }
        }

        private void LoadChildren()
        {
            if (Children.Count == 1 && Children[0] == null)
            {
                Children.Clear();
                try
                {
                    foreach (string folder in Directory.GetDirectories(Item.Path))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(folder);
                        DirectoryItem item = new DirectoryItem(directoryInfo.Name, folder);
                        item.Children.Add(null);
                        FolderViewModel viewModel = new FolderViewModel(item, this);
                        Children.Add(viewModel);
                    }
                }
                catch (UnauthorizedAccessException) { }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        public FolderViewModel Parent
        {
            get { return _parent; }
        }
    }
}
