using PathInJson.WPF.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using File = PathInJson.WPF.Entities.File;

namespace PathInJson.WPF.Helpers
{
    public class FolderHelper
    {
        public Folder CreateHierarchy(string path)
        {
            try
            {
                List<File> files = new List<File>();
                List<Folder> folders = new List<Folder>();

                string[] folders1 = Directory.GetDirectories(path);
                string[] files1 = Directory.GetFiles(path);

                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                foreach (string file in files1)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    File file1 = new File { Name = fileInfo.Name, Size = fileInfo.Length + " B", Path = fileInfo.FullName };
                    files.Add(file1);
                }

                foreach (string folder in folders1)
                {
                    folders.Add(CreateHierarchy(folder));
                }

                Folder folder1 = new Folder { Name = directoryInfo.Name, DateCreated = directoryInfo.CreationTime.ToString("d-MMM-y h:mm tt"), Files = files, Children = folders };
                return folder1;
            }
            catch (Exception)
            {
                return new Folder();
            }
        }
    }
}
