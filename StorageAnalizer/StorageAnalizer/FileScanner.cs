using StorageAnalizer.DataStructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalizer
{
    class FileScanner
    {

        public Folder scanFolders(string folderPath)
        {
            Folder folderTree = new Folder();

            foreach (string item in Directory.GetFiles(folderPath))
            {
                FileInfo info = new FileInfo(item);

                DataStructure.File file = new DataStructure.File(
                    info.Name,
                    info.Length,
                    info.LastWriteTime
                    );

                folderTree.Files.Add(file);
            }

            foreach (string item in Directory.GetDirectories(folderPath))
            {
                Folder folder = new Folder();

            }

            

            return folderTree;
        }
    }
}
