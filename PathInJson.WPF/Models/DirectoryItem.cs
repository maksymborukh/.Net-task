using PathInJson.WPF.ViewModels;
using System.Collections.Generic;

namespace PathInJson.WPF.Models
{
    public class DirectoryItem : ViewModelBase
    {
        private string _title = string.Empty;
        private string _path = string.Empty;
        private List<DirectoryItem> _children = new List<DirectoryItem>();

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        public List<DirectoryItem> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                OnPropertyChanged("Children");
            }
        }

        public DirectoryItem(string title, string path)
        {
            Title = title;
            Path = path;
        }
    }
}
