using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AltSource.Utilities.VSSolution.Filters;

namespace AltSource.Utilities.VSSolution
{
    [Serializable]
    public class SolutionFile
    {
        public string FilePath;
        public List<ProjectFile> Projects; //guid identifier
        public string InputText { get; set; }
        private static Regex _regexProjectMention = new Regex(@"^\s*{([0-9A-F]{8}-(?:[0-9A-F]{4}-){3}[0-9A-F]{12})}", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static Regex _regexProjectSection = new Regex(@"ProjectSection\(ProjectDependencies\)\s=\spostProject((?:.|[\r\n])+?)\sEndProjectSection", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static Regex _regexProject = new Regex(@"Project(\((?:.|[\r\n])+?)EndProject[\r\n]", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public string FileName
        {
            get { return Path.GetFileName(this.FilePath); }
        }

        public static SolutionFile BuildFromFile(string filePath, List<ProjectFile> projectList)
        {
            var inputText = File.ReadAllText(filePath);

            var solution = new SolutionFile()
            {
                FilePath = filePath,
                Projects = new List<ProjectFile>(),
                InputText = inputText

            };


            foreach (Match projectMatch in _regexProject.Matches(inputText))
            {
                var projectString = projectMatch.Groups[1].Value;
                if (projectString != null)
                {
                    var projectGuid = ParseProjectGuid(projectString);
                    var dependentProjects = projectList.Where(proj => proj.ProjectId == projectGuid).ToList();

                    if (null == dependentProjects)
                    {
                        ProjectType projectType = ParseProjectType(projectString);
                        //Is SolutionFolder
                        if (projectType.ID != Guid.Parse("2150E333-8FDC-42A3-9474-1A3956D46DE8"))
                        {
                            var assName = ParseProjectAssemblyName(projectString);
                            dependentProjects.Add(ProjectFile.Build(projectGuid, assName, projectType));
                            projectList.AddRange(dependentProjects);
                        }
                    }

                    dependentProjects.AddRange(ParsePostProjects(projectString, projectList));

                    var newDependencies = dependentProjects.Where(p => !solution.Projects.Any(sp => sp == p));
                    solution.Projects.AddRange(newDependencies);
                    foreach (var dependentProject in newDependencies)
                    {
                        dependentProject.ReferencedBySolutions.Add(solution);
                    }
                }
            }
            
            return solution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectFile"></param>
        /// <param name="addAsType">In case project doesn't know it's own type</param>
        /// <returns></returns>
        public bool AddProjectFileToSolution(ProjectFile projectFile, ProjectType addAsType)
        {
            //Adding this : ("typeGuid") = "NAME", "path", "IDGuid"
            //Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "CCI.Interfaces.Dependencies", "CCI.Interfaces.Dependencies\CCI.Interfaces.Dependencies.csproj", "{24C4E082-2D60-4434-998C-200FF15675DF}"
            //EndProject

            if (this.Projects.Contains(projectFile, new ProjectComparer()))
            {
                return false;
            }

            bool wroteProject = false;
            if (!this.Projects.Contains(projectFile, new ProjectComparer()))
            {
                var insertionPoint = 0;
                int index = 0;
                var done = false;
                while (index < InputText.Length && !done)
                {
                    var projectString = InputText.GetBetween("Project", "EndProject", ref index);
                    if (projectString != null)
                    {
                        var projectType = ParseProjectType(projectString);
                        if (projectType == null || 
                            (projectType.TypeName != "Solution Folder" &&
                            new Guid("2150E333-8FDC-42A3-9474-1A3956D46DE8") != projectType.ID))
                        {
                            done = true;
                        }
                    }
                }

                InputText = InputText.Insert(index, string.Format(@"
Project(""{{{0}}}"") = ""{1}"", ""{2}"",\ ""{{{3}}}""
EndProject",
                   addAsType.ID.ToString().ToUpper(), projectFile.AssemblyName, this.FilePath.GetRelativePathTo(projectFile.FilePath), projectFile.ProjectId.ToString().ToLower()));
                File.WriteAllText(this.FilePath, this.InputText);
            }

            return true;
        }
        
        public bool RemoveProjectFileFromSolution(ProjectFile projectFile)
        {
            var regexConfigs = new Regex(@"\s*\{"+ projectFile.ProjectId.ToString() + @"\}(?:\.|\s=).+?[\r\n]", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var regexProject = new Regex(@"Project\(\""[^\n\r]+?\{" + projectFile.ProjectId + @"\}\"".+?EndProject[\n\r]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            int originalLEngth = InputText.Length;
            InputText = regexProject.Replace(InputText, string.Empty);

            InputText = regexConfigs.Replace(InputText, String.Empty);

            if (originalLEngth != InputText.Length) { 
                File.WriteAllText(this.FilePath, this.InputText);
                this.Projects.RemoveAll( p => p.ProjectId == projectFile.ProjectId);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Lists any projects that may be referenced in teh solution, but that aren't specifically included.  This might prevent the solution from loading.
        /// You can clean the solution by running the Emanciapte()
        /// </summary>
        /// <returns></returns>
        public List<ProjectFile> AbsentProjects(string projectText)
        {
            var mentionables = new List<ProjectFile>();
            foreach (Match match in _regexProjectMention.Matches(this.InputText))
            {
                Guid guid = Guid.Parse(match.Captures[0].Value);

                ProjectFile mentionable = ProjectFile.Build(guid, string.Empty, ProjectTypeDict.Get(Guid.Empty));

                if (!Projects.Contains(mentionable) && !mentionables.Contains(mentionable))
                {
                    mentionables.Add(mentionable);
                }
            }
            return mentionables;
        }

        #region Internals


        private static List<ProjectFile> ParsePostProjects(string projectString, List<ProjectFile> projectList)
        {
            var postProjects = new List<ProjectFile>(5);
            var postProjectMatch = _regexProjectSection.Match(projectString);

            if (postProjectMatch.Length > 0)
            {
                var contents = postProjectMatch.Groups[1].Value;
                foreach (Match match in _regexProjectMention.Matches(contents))
                {
                    Guid postProjectGuid = Guid.Parse(match.Groups[1].Value);
                    var postProject = projectList.Where(proj => proj.ProjectId == postProjectGuid).FirstOrDefault();

                    if (null == postProject)
                    {
                        postProject = ProjectFile.Build(postProjectGuid, string.Empty, ProjectTypeDict.Get(Guid.Empty));
                        projectList.Add(postProject);
                        postProjects.Add(postProject);
                    }
                    postProjects.Add(postProject);
                }
            }
            return postProjects;
        }
        
        private static ProjectType ParseProjectType(string projectString)
        {
            var idGuidStr = projectString
                                .Split(',')[0]
                                .Split('=')[0]
                                .GetBetween("\"{", "}\"")
                                .Trim();
            var guid = Guid.Parse(idGuidStr);

            ProjectType projectType = null;
            if (ProjectTypeDict.Contains(guid))
            {
                projectType = ProjectTypeDict.Get(guid);
            }

            return projectType;
        }

        private static Guid ParseProjectGuid(string projectString)
        {
            //Parsing this: ("typeGuid") = "NAME", "path", "IDGuid"
            var idGuidStr = projectString
                                .Split(',')[2]
                                .Split('\"')[1]
                                .Replace("\"", "")
                                .Trim();

            return Guid.Parse(idGuidStr);
        }

        private static string ParseProjectAssemblyName(string projectString)
        {
            //Parsing this: ("typeGuid") = "NAME", "path", "IDGuid"
            var assName = projectString
                                .Split(',')[0]
                                .Split('=')[1]
                                .Replace("\"", "")
                                .Trim();

            return assName;
        }
        #endregion

        public override bool Equals(object obj)
        {
            return this.FilePath == ((SolutionFile)obj).FilePath;
        }

        public override int GetHashCode()
        {
            return this.FilePath.GetHashCode();
        }
    }
}
