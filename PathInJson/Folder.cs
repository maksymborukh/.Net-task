using System.Collections.Generic;

namespace PathInJSON
{
    public class Folder
    {
        public Folder()
        {
            Files = new List<File>();
            Children = new List<Folder>();
        }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public List<File> Files { get; set; }
        public List<Folder> Children { get; set; }
    }
}
