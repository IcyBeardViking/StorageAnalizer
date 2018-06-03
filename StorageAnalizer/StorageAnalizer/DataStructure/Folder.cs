using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalizer.DataStructure
{
    class Folder : File
    {
        List<Folder> Subfolders;
        List<File> Files;

        public new int Size
        {
            get
            {
                int fullSize = 0;

                foreach (File item in Files)
                {
                    fullSize += item.Size;
                }

                foreach (Folder item in Subfolders)
                {
                    fullSize += item.Size;
                }

                return fullSize;
            }
        }

    }
}
