using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public XElement getXml()
        {
            XElement xml = new XElement("Directory");

            XElement DirectoryName = new XElement("DirectoryName")
            {
                Value = this.Name
            };

            xml.Add(DirectoryName);

            foreach (File item in Files)
            {
                XElement file = new XElement("File");

                XElement Name     = new XElement("FileName"       , item.Name);
                XElement Modified = new XElement("LastModified"   , item.LastModified.ToString());
                XElement fileSize = new XElement("Size"           , item.Size.ToString());

                file.Add(Name, Modified, fileSize);

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
