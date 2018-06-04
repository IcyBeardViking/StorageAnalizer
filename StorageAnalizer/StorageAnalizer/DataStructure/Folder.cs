using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StorageAnalizer.DataStructure
{
    class Folder : File
    {
        public List<Folder> Subfolders;
        public List<File> Files;
        public long totalSize = 0;

        public Folder(string FolderPath) : base(FolderPath)
        {
            Files = new List<File>();
            Subfolders = new List<Folder>();
        }

        public new long Size
        {
            get
            {
                if (totalSize == 0)
                {

                    foreach (File item in Files)
                    {
                        totalSize += item.Size;
                    }

                    foreach (Folder item in Subfolders)
                    {
                        totalSize += item.Size;
                    }

                }  
                
                return totalSize;
            }
        }

        public XElement getXml()
        {
            XElement xml = new XElement("Directory");

            xml.Add(
                new XElement("DirectoryName", this.Name),
                new XElement("DirectorySize", this.totalSize),
                new XElement("DateSaved", DateTime.Now.ToShortDateString()),
                new XElement("TimeSaved", DateTime.Now.ToShortTimeString())
                );

            foreach (File item in Files)
            {
                XElement file = new XElement("File");

                file.Add(
                    new XElement("FileName", item.Name),
                    new XElement("LastModified", item.LastModified.ToString()),
                    new XElement("Size", item.Size.ToString())
                    );

                xml.Add(file);
            }

            foreach (Folder item in Subfolders)
            {
                xml.Add(item.getXml());
            }

            return xml;
        }

        public void saveToFile(string FileName)
        {
            XDocument document = new XDocument(this.getXml());

            document.Save(FileName, SaveOptions.None);
        }
    }
}
