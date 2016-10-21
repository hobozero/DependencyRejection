using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.IO;

namespace AltSource.Utilities.VSSolution
{
    public class PackagesFile
    {

        public XDocument Xml { get; protected set; }

        public string Path { get; set; }

        public static PackagesFile Build(string path)
        {
            return new PackagesFile()
            {
                Xml = XDocument.Load(path),
                Path = path
            };
        }

        public string GetVersionOfLibrary(string libraryName)
        {
            var packageElm = this.Xml.Descendants()
                .Where(a => a.Name.LocalName == "package" && a.Attribute("id").Value == libraryName)
                .FirstOrDefault();

            string version = string.Empty;
            if (null != packageElm)
            {
                version = packageElm.Attribute("version").Value;
            }
            return version;
        }

        public ProjectFile GetProject()
        {
            return Directory.GetFiles(System.IO.Path.GetDirectoryName(this.Path), "*.csproj", SearchOption.TopDirectoryOnly)
                    .Select(p => ProjectFile.Build(p))
                    .FirstOrDefault();
        }
    }
}
