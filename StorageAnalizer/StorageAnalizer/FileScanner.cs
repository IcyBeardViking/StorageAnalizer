using StorageAnalizer.DataStructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalizer
{
    static class FileScanner
    {
        static Action<string> sendCurrentFolder;

        static FileScanner()
        {
            sendCurrentFolder = null;
        }

        static public Folder scanFolders(string folderPath)
        {
            //If not null calls event
            sendCurrentFolder?.Invoke(folderPath);

            Folder folderTree = new Folder(folderPath);
            try
            {
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
                    Folder folder = new Folder(item);

                    folder = scanFolders(item);

                    folderTree.Subfolders.Add(folder);

                }
            }
            catch (UnauthorizedAccessException) { }

            return folderTree;
        }

        internal static Folder scanFolders(string path, Action<string> changeLabel, Action doneEvent)
        {
            sendCurrentFolder = changeLabel;

            Folder folderTreeDone = scanFolders(path);

            doneEvent();

            return folderTreeDone;
        }
    }
}
