using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalizer.DataStructure
{
    class File
    {
        public DateTime LastModified;
        public long Size;
        public string Name;

        public File()
        {
            Name = string.Empty;
            Size = 0;
            LastModified = new DateTime();
        }

        public File(string fileName, long fileSize, DateTime modified)
        {
            Name = fileName;
            Size = fileSize;
            LastModified = modified;
        }

        public File(string fileName)
        {
            FileInfo file = new FileInfo(fileName);

            Name = file.Name;
        }
    }
}
