using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AltSource.Utilities.VSSolution.Filters;
using AltSource.Utilities.VSSolution.Reflection;
using AltSource.Utilities.VSSolution.VCS;

namespace AltSource.Utilities.VSSolution
{
    public enum PublicClassification
    {
        Console,
        WindowsService,
        Web,
        WinExe,
        WPF,
        WWCF,
    }

    [Serializable]
    public class ProjectFile
    {
        protected List<ProjectFile> _ancestors;

        protected bool _packagesLoaded;
        protected XDocument _packages;
        protected ConfigFile _configFile;
        protected bool _triedConfigLoad;
        protected VcsInfo _VcsInfo;
        #region Properties
        public XDocument Packages
        {
            get
            {
                if (!_packagesLoaded)
                {
                    _packagesLoaded = true;
                    _packages = XDocument.Load(Path.Combine(Path.GetDirectoryName(FilePath), "packages.config"));
                }
                return _packages;
            }
        }

        public bool Exists { get; protected set; }
        public string FilePath { get; protected set; }
        public string AssemblyName { get; protected set; }
        public ProjectOutputType OutputType { get; protected set; }
        public List<Guid> ReferencesProjectIds { get; protected set; } //intermediate step before graph building
        public List<ProjectFile> ReferencesProjects { get; protected set; }
        public List<ProjectFile> ReferencedByProjects { get; protected set; }
        public List<SolutionFile> ReferencedBySolutions { get; protected set; }
        public Guid ProjectId { get; protected set; } //guid identifier
        public string OctoPackProjectName { get; protected set; }

        public IEnumerable<string> OutputPaths { get; set; }

        public ConfigFile ConfigFile
        {
            get
            {
                if (IsTopLevel && !_triedConfigLoad && null == _configFile)
                {
                    _configFile = ConfigFile.Build(this);
                    _triedConfigLoad = true;
                }

                return _configFile;
            }
        }

        public VcsInfo VcsInfo
        {
            get
            {
                if (null == _VcsInfo)
                {
                    _VcsInfo = new VcsInfo(this.FilePath);
                }
                return _VcsInfo;
            }
        }

        public bool IsTopLevel
        {
            get
            {
                //{ project.OutputType.ToString()}-{project.ProjectType.TypeName

                return (OutputType == ProjectOutputType.Exe ||
                        OutputType == ProjectOutputType.WinExe ||
                        (ProjectType.TypeName != ProjectTypeDict.UNKNOWN &&
                         ProjectType.TypeName != ProjectTypeDict.CCI_TOOLS &&
                         ProjectType.TypeName != ProjectTypeDict.TEST_DRIVER &&
                         ProjectType.TypeName != ProjectTypeDict.TEST ));
            }
        }

        public XDocument Xml { get; protected set; }

        /// <summary>
        /// Microsoft project identifier
        /// </summary>
        public ProjectType ProjectType { get; protected set; }

        #endregion

        #region Factory
        private ProjectFile(string filePath)
        {
            FilePath = filePath;
            ReferencedByProjects = new List<ProjectFile>();
            ReferencedBySolutions = new List<SolutionFile>();
            ReferencesProjects = new List<ProjectFile>();
            ReferencesProjectIds = new List<Guid>(0);
        }

        public override string ToString()
        {
            return this.FilePath;
        }

        public static ProjectFile Build(Guid projectId, string projAss, ProjectType type)
        {
            var projFIle = new ProjectFile(string.Empty);
            projFIle.ProjectId = projectId;
            projFIle.AssemblyName = projAss;
            projFIle.ProjectType = type;
            return projFIle;
        }

        public static ProjectFile Build(string path) 
        {
            var projectFile = new ProjectFile(path);
            projectFile.Exists = true;

            projectFile.Xml = XDocument.Load(path);

            var projectIdString = (projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "ProjectGuid")
                .FirstOrDefault() ?? new XElement("bunk"))
                .Value;
            var projectId = Guid.Empty;

            if (! string.IsNullOrEmpty(projectIdString))
            {
                projectFile.ProjectId = Guid.Parse(projectIdString.Replace("{", "").Replace("}", ""));
            }

            projectFile.AssemblyName = (projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "AssemblyName")
                .FirstOrDefault() ?? new XElement("bunk"))
                .Value;

            projectFile.ReferencesProjectIds = projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "ProjectReference")
                .SelectMany(pr => pr.Descendants().Where(p => p.Name.LocalName == "Project"))
                .Where(p => !string.IsNullOrWhiteSpace(p.Value))
                .Select(p => Guid.Parse(p.Value))
                .ToList();

            projectFile.OctoPackProjectName = (projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "OctoPackProjectName")
                .FirstOrDefault() ?? new XElement("bunk"))
                .Value;


            var outputTypeString = (projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "OutputType")
                .FirstOrDefault() ?? new XElement("bunk"))
                .Value;

            projectFile.OutputPaths = projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "OutputPath")
                .Select(x => x.Value);

            projectFile.OutputType = string.IsNullOrEmpty(outputTypeString) ? 
                            ProjectOutputType.Library : 
                            (ProjectOutputType)Enum.Parse(typeof(ProjectOutputType), outputTypeString);


            var projectTypeElement = projectFile.Xml.Descendants().Where(d => "ProjectTypeGuids" == d.Name.LocalName).FirstOrDefault();


            Guid projGuid = Guid.Empty;
            if (projectTypeElement != null)
            {
                projGuid = projectTypeElement.Value.Split(new[] { ';' })
                   .Select(p => new Guid(p))
                   .ToArray()
                   [0];
            }

            projectFile.SetProjectType(projGuid);


            return projectFile;
        }
        #endregion

        #region Behaviors
        protected void SetProjectType(Guid guid)
        {
            if (Guid.Empty != guid)
            {
                this.ProjectType = ProjectTypeDict.Get(guid);
            }
            else
            {
                if (AssemblyName.StartsWith("CCI.Tools", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.ProjectType = new ProjectType(Guid.Empty, ProjectTypeDict.CCI_TOOLS, Color.Black);
                }
                else if (AssemblyName.ToLower().Contains("testdrive") || AssemblyName.ToLower().Contains("proxytest") || AssemblyName.ToLower().Contains("testharness") || AssemblyName.ToLower().EndsWith(".tests"))
                {
                    this.ProjectType = new ProjectType(Guid.Empty, ProjectTypeDict.TEST_DRIVER, Color.Black);
                }
                else
                {
                    this.ProjectType = ProjectTypeDict.Get(Guid.Empty);
                }
                
            }
        }

        public int AddReference(ProjectFile referencedProject)
        {
            //can't reference self
            if (referencedProject.Equals(this))
            {
                return -1;
            }
            try
            {
                //update local XML file
                var relativePath = this.FilePath.GetRelativePathTo(referencedProject.FilePath);

                XElement ItemGroup = null;

                var ns = this.Xml.Root.GetDefaultNamespace();

                var newProjectElement = new XElement(ns+ "ProjectReference",
                    new XAttribute("Include", relativePath),
                    new XElement(ns + "Project", referencedProject.ProjectId.ToString("B")),
                    new XElement(ns + "Name", referencedProject.AssemblyName));
                
                int removed = RemoveExistingProjectReferences(referencedProject);

                var itemGroup = GetProjectItemGroupElemment();
                itemGroup.Add(newProjectElement);
                

                this.ReferencesProjectIds.Add(referencedProject.ProjectId);
                this.ReferencesProjects.Add(referencedProject);

                this.Xml.Save(this.FilePath, SaveOptions.OmitDuplicateNamespaces );

                return removed;

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// REturn all Solution files that should include me but don't
        /// </summary>
        public IEnumerable<SolutionFile> GetDeadBeatSolutionFiles()
        {
            var allSolutionsThatShouldReferenceMe = WhosMyDaddys();

            return allSolutionsThatShouldReferenceMe.Where(s => !s.Projects.Contains(this, new ProjectComparer()));
        }
        /// <summary>
        /// Return all Solution Files that should include me.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SolutionFile> WhosMyDaddys()
        {
            return this.GetAncestors().Union(new ProjectFile[]{this}).SelectMany(p => p.ReferencedBySolutions).Distinct(new SolutionComparer());
        }

        public IEnumerable<ProjectFile> GetAncestors()
        {
            _ancestors = new List<ProjectFile>();
            return AddUniqueAncestors(new ProjectFile[] {this});
        }

        public List<WcfService> GetWcfServices()
        {
            var services = (null == ConfigFile || null == ConfigFile.Services) ? new List<WcfService>() : ConfigFile.Services.ToList() ;

            var assLoader = new AssemblyLoader(this);
            var ass = assLoader.Load();

            //if (this.ConfigFile.ConfigFileType == ConfigFileType.Web)
            //{
            //    //Pulls *.svc from projectFile. Replaced by Reflection implementation
            //    ////< Content Include = "WarrantyService.svc" />
            //    //services.AddRange(
            //    //    this.Xml.Descendants()
            //    //        .Where(a => a.Name.LocalName == "Content" &&
            //    //                    a.Attribute("Include") != null &&
            //    //                    a.Attribute("Include").Value.EndsWith(".svc"))
            //    //        .Select(x =>
            //    //            new WcfService()
            //    //            {
            //    //                Address = new Uri(x.Attribute("Include").Value, UriKind.Relative),
            //    //                Name = x.Attribute("Include").Value,
            //    //                ConfigType = ServiceConfigType.IisSvc
            //    //            })
            //    //);
            //}


            if (null != ass)
            {
                var contracts = assLoader.GetTypesWith<ServiceContractAttribute>(true)
                    .Select(t => new WcfService()
                    {
                        Address = new Uri("/", UriKind.Relative),
                        ConfigType = ServiceConfigType.Unknown,
                        Contract = t.FullName,
                        Name = t.Name
                    });
                services.AddRange(contracts);

                //Get types inheriting from ServiceBase
                //Ty: Ideally we could traverse back to the contract name, but this is proving to be impossible 
                //due to runtime loading of ServiceHost
                var serviceBaseTypes = assLoader.GetBaseTypes<ServiceBase>()
                    .Select(t => new WcfService()
                    {
                        Address = new Uri("/", UriKind.Relative),
                        ConfigType = ServiceConfigType.Unknown,
                        Contract = t.Name,
                        Name = t.Name
                    });

                services.AddRange(serviceBaseTypes);

            }

            return services;
        } 

        protected IEnumerable<ProjectFile> AddUniqueAncestors(IEnumerable<ProjectFile> inputProjects)
        {
            //Add unique files
            inputProjects = inputProjects.Distinct(new ProjectComparer());
            _ancestors.AddRange(inputProjects.Where(i => i != this && !_ancestors.Any(a => a.ProjectId == i.ProjectId)));

            var referencingProjects = new List<ProjectFile>(
                inputProjects
                    .SelectMany(proj => proj.ReferencedByProjects)
                    .Distinct(new ProjectComparer())
                    .Where(i => !_ancestors.Any(a => a.ProjectId == i.ProjectId))
                    );

            if (referencingProjects.Count > 0)
            {   
                AddUniqueAncestors(referencingProjects);
            }

            return _ancestors;
        }

        /// <summary>
        /// Remove me from all solution files where I appear
        /// </summary>
        public IEnumerable<SolutionFile> Emancipate()
        {
            return Emancipate(WhosMyDaddys());
        }

        public IEnumerable<SolutionFile> Emancipate(IEnumerable<SolutionFile> solutionFiles)
        {
            var cleaned = new List<SolutionFile>();
            foreach (var solutionFile in solutionFiles)
            {
                if (solutionFile.RemoveProjectFileFromSolution(this))
                {
                    cleaned.Add(solutionFile);
                }
            }

            return cleaned;
        }

        public int RemoveExistingProjectReferences(ProjectFile projectToRemove)
        {
            var proj = Xml.Descendants()
                .Where(a => a.Name.LocalName == "ProjectReference" && 
                        (a.Descendants().Any(d => d.Name.LocalName == "Project" && d.Value.ToLower() == projectToRemove.ProjectId.ToString("B").ToLower() ) ||
                        a.Descendants().Any(d => d.Name.LocalName == "Name" && d.Value.ToLower() == projectToRemove.AssemblyName.ToLower()))
                        );
            int ct = proj.Count();

            proj.Remove();

            return ct;
        }

        public bool AddOctoPackName(string octoName)
        {
            var hasOcto = Xml.Descendants()
                .Any(a => a.Name.LocalName == "OctoPackProjectName");

            XNamespace defaultNs = Xml.Root.GetDefaultNamespace();

            if (!hasOcto)
            {
                var xmlOcto = new XElement(defaultNs + "OctoPackProjectName",
                        new XAttribute("Condition", "'$(OctoPackProjectName)' == ''"));
                xmlOcto.Value = octoName;

                var propertyGroup = Xml.Descendants()
                    .FirstOrDefault(a => a.Name.LocalName == "PropertyGroup");
                propertyGroup.AddFirst(xmlOcto);
            }

            return !hasOcto;
        }

        protected XElement GetProjectItemGroupElemment()
        {
            var ns = Xml.Root.GetDefaultNamespace();

            var someOtherProjectReference = this.Xml.Descendants()
                     .Where(a => a.Name.LocalName == "ProjectReference")
                    .FirstOrDefault();

            if (null != someOtherProjectReference)
            {
                return someOtherProjectReference.Parent;
            }

            var newItemGroup = new XElement(ns + "ItemGroup");

            Xml.Root.Add(newItemGroup);

            return newItemGroup;
        }

        public override bool Equals(object obj)
        {
            return this.ProjectId == ((ProjectFile)obj).ProjectId;
        }

        public override int GetHashCode()
        {
            return this.ProjectId.GetHashCode();
        }
        #endregion
    }
}
