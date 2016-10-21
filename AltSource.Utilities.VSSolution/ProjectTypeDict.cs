using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution
{

    public class ProjectType
    {
        public ProjectType(Guid id, string typeName, Color color)
        {
            ID = id;
            TypeName = typeName;
            Color = color;
        }
        public Guid ID
        {
            get; set;
        }
        public string TypeName
        {
            get; set;
        }

        public Color Color { get; set; }
    }

    public static class ProjectTypeDict
    {

        public static string TEST_DRIVER = "TestDriver";
        public static string CCI_TOOLS = "CciTools";
        public static string UNKNOWN = "Unknown";
        public static string TEST = "Test";

        private static Dictionary<Guid, ProjectType> typeDict;
        static ProjectTypeDict()
        {
            typeDict = new Dictionary<Guid, ProjectType>()
            {
                {Guid.Empty, new ProjectType(Guid.Empty, ProjectTypeDict.UNKNOWN, Color.Black)},
                {Guid.NewGuid(), new ProjectType(Guid.Empty, ProjectTypeDict.TEST_DRIVER, Color.Black)},
                {Guid.NewGuid(), new ProjectType(Guid.Empty, ProjectTypeDict.CCI_TOOLS, Color.Black)},

                {new Guid("F184B08F-C81C-45F6-A57F-5ABD9991F28F"), new ProjectType(new Guid("F184B08F-C81C-45F6-A57F-5ABD9991F28F"), "Windows (VB.NET)", Color.Black)},
                {new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942"), new ProjectType(new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942"), "Windows (Visual C++)", Color.Black)},
                //{new Guid("349C5851-65DF-11DA-9384-00065B846F21"), new ProjectType(new Guid("349C5851-65DF-11DA-9384-00065B846F21"), "Web Application", Color)},
                {new Guid("E24C65DC-7377-472B-9ABA-BC803B73C61A"), new ProjectType(new Guid("E24C65DC-7377-472B-9ABA-BC803B73C61A"), "Web Site", Color.BlueViolet)},
                {new Guid("F135691A-BF7E-435D-8960-F99683D2D49C"), new ProjectType(new Guid("F135691A-BF7E-435D-8960-F99683D2D49C"), "Distributed System", Color.Black)},
                {new Guid("3D9AD99F-2412-4246-B90B-4EAA41C64699"), new ProjectType(new Guid("3D9AD99F-2412-4246-B90B-4EAA41C64699"), "WWCF", Color.DarkGoldenrod)},
                {new Guid("60DC8134-EBA5-43B8-BCC9-BB4BC16C2548"), new ProjectType(new Guid("60DC8134-EBA5-43B8-BCC9-BB4BC16C2548"), "WPF", Color.Plum)},
                {new Guid("C252FEB5-A946-4202-B1D4-9916A0590387"), new ProjectType(new Guid("C252FEB5-A946-4202-B1D4-9916A0590387"), "Visual Database Tools", Color.DarkTurquoise)},
                {new Guid("A9ACE9BB-CECE-4E62-9AA4-C7E7C5BD2124"), new ProjectType(new Guid("A9ACE9BB-CECE-4E62-9AA4-C7E7C5BD2124"), "Database", Color.DarkTurquoise)},
                {new Guid("4F174C21-8C12-11D0-8340-0000F80270F8"), new ProjectType(new Guid("4F174C21-8C12-11D0-8340-0000F80270F8"), "Database (other project types)", Color.DarkTurquoise)},
                {new Guid("3AC096D0-A1C2-E12C-1390-A8335801FDAB"), new ProjectType(new Guid("3AC096D0-A1C2-E12C-1390-A8335801FDAB"), TEST, Color.DarkMagenta)},
                {new Guid("66A26720-8FB5-11D2-AA7E-00C04F688DDE"), new ProjectType(new Guid("66A26720-8FB5-11D2-AA7E-00C04F688DDE"), "Solution Folder", Color.Brown)},
                //{new Guid("14822709-B5A1-4724-98CA-57A101D1B079"), new ProjectType(new Guid("14822709-B5A1-4724-98CA-57A101D1B079"), "Workflow (C#)", Color.DarkSlateGray)},
                //{new Guid("D59BE175-2ED0-4C54-BE3D-CDAA9F3214C8"), new ProjectType(new Guid("D59BE175-2ED0-4C54-BE3D-CDAA9F3214C8"), "Workflow (VB.NET)", Color)},
                {new Guid("06A35CCD-C46D-44D5-987B-CF40FF872267"), new ProjectType(new Guid("06A35CCD-C46D-44D5-987B-CF40FF872267"), "Deployment Merge Module", Color.DeepPink)},
                {new Guid("3EA9E505-35AC-4774-B492-AD1749C4943A"), new ProjectType(new Guid("3EA9E505-35AC-4774-B492-AD1749C4943A"), "Deployment Cab", Color.DeepPink)},
                {new Guid("978C614F-708E-4E1A-B201-565925725DBA"), new ProjectType(new Guid("978C614F-708E-4E1A-B201-565925725DBA"), "Deployment Setup", Color.DeepPink)},
                {new Guid("AB322303-2255-48EF-A496-5904EB18DA55"), new ProjectType(new Guid("AB322303-2255-48EF-A496-5904EB18DA55"), "Deployment Smart Device Cab", Color.DeepPink)},
                //{new Guid("E6FDF86B-F3D1-11D4-8576-0002A516ECE8"), new ProjectType(new Guid("E6FDF86B-F3D1-11D4-8576-0002A516ECE8"), "Visual J#", Color)},
                //{new Guid("F8810EC1-6754-47FC-A15F-DFABD2E3FA90"), new ProjectType(new Guid("F8810EC1-6754-47FC-A15F-DFABD2E3FA90"), "SharePoint Workflow", Color)},
                //{new Guid("593B0543-81F6-4436-BA1E-4747859CAAE2"), new ProjectType(new Guid("593B0543-81F6-4436-BA1E-4747859CAAE2"),  "SharePoint (C#)", Color)},

                {new Guid("603C0E0B-DB56-11DC-BE95-000D561079B0"), new ProjectType(new Guid("603C0E0B-DB56-11DC-BE95-000D561079B0"),  "ASP.NET MVC 1", Color.Blue)},
                {new Guid("F85E285D-A4E0-4152-9332-AB1D724D3325"), new ProjectType(new Guid("F85E285D-A4E0-4152-9332-AB1D724D3325"),  "ASP.NET MVC 2", Color.Blue)},
                {new Guid("E53F8FEA-EAE0-44A6-8774-FFD645390401"), new ProjectType(new Guid("E53F8FEA-EAE0-44A6-8774-FFD645390401"),  "ASP.NET MVC 3", Color.Blue)},
                {new Guid("E3E379DF-F4C6-4180-9B81-6769533ABE47"), new ProjectType(new Guid("E3E379DF-F4C6-4180-9B81-6769533ABE47"),  "ASP.NET MVC 4", Color.Blue)},
                {new Guid("349C5851-65DF-11DA-9384-00065B846F21"), new ProjectType(new Guid("349C5851-65DF-11DA-9384-00065B846F21"),  "ASP.NET MVC 5", Color.Blue)},


                {new Guid("D183A3D8-5FD8-494B-B014-37F57B35E655"), new ProjectType(new Guid("D183A3D8-5FD8-494B-B014-37F57B35E655"),  "SSIS Data Transfer", Color.Tan)},

                {new Guid("2150E333-8FDC-42A3-9474-1A3956D46DE8"), new ProjectType(new Guid("2150E333-8FDC-42A3-9474-1A3956D46DE8"),  "Solution Folder", Color.MediumPurple)},
                {new Guid("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC"), new ProjectType(new Guid("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC"),  "C#", Color.DarkGray)},
            };
        }

        public static ProjectType Get(Guid projectTypeId)
        {
            return typeDict[projectTypeId];
        }

        public static ProjectType GetByName(string projectTypeName)
        {
            return typeDict.Values.Where(p => p.TypeName.ToLower() == projectTypeName.ToLower()).FirstOrDefault();
        }

        public static bool Contains(Guid projectTypeId)
        {
            return typeDict.ContainsKey(projectTypeId);
        }

        public static Dictionary<Guid, ProjectType> All
        {
            get
            {
                return typeDict;
            }
        }

}
}
