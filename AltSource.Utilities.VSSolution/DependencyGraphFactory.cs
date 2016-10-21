using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using AltSource.Utilities.VSSolution.Filters;

namespace AltSource.Utilities.VSSolution
{
    public class DependencyGraphFactory
    {
        public event EventHandler<string> OutputLog;
        private void SafeLogRunner(string entry) { if (OutputLog != null) OutputLog(this, entry); }

        public DependencyGraph BuildFromDisk(string devFolder, bool trunkOnly)
        {
            SafeLogRunner(string.Format("Gathering Projects From: {0}", devFolder));
            string[] filePaths = new string[0];


            if (trunkOnly)
            {
                filePaths = Directory.GetDirectories(devFolder, "trunk", SearchOption.AllDirectories)
                    //.Where(dp => new DirectoryInfo(dp).Parent.Name == "trunk")
                    .SelectMany(dp => Directory.EnumerateFiles(dp, "*.*", SearchOption.AllDirectories) //<--- .NET 4.5
                                            .Where(file => file.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)
                                                || file.EndsWith(".dtproj", StringComparison.OrdinalIgnoreCase)
                                                || file.EndsWith(".sln", StringComparison.OrdinalIgnoreCase)))
                    .ToArray();
            }
            else
            {
                //Only want to traverse filesystem once
                filePaths = Directory.EnumerateFiles(devFolder, "*.*", SearchOption.AllDirectories) //<--- .NET 4.5
                                            .Where(file => file.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)
                                                || file.EndsWith(".dtproj", StringComparison.OrdinalIgnoreCase)
                                                || file.EndsWith(".sln", StringComparison.OrdinalIgnoreCase))
                                            .ToArray();
            }

            return BuildFromFileList(filePaths);
        }

        protected DependencyGraph BuildFromFileList(string[] filePaths)
        {
            try
            {
                var projects = filePaths
                                    .Where(f => f.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)
                                                || f.EndsWith(".dtprof", StringComparison.OrdinalIgnoreCase))
                                    .Select(projectPath => ProjectFile.Build(projectPath)).ToList();
                
            var solutions = filePaths
                                .Where(f => f.EndsWith(".sln", StringComparison.OrdinalIgnoreCase))
                                .Select(solutionPath => SolutionFile.BuildFromFile(solutionPath, projects)).ToArray();

            foreach (var outerProject in projects)
            {
                foreach (var innerProject in projects)
                {
                    if (outerProject.ReferencesProjectIds.Contains(innerProject.ProjectId))
                    {
                        outerProject.ReferencesProjects.Add(innerProject);
                        innerProject.ReferencedByProjects.Add(outerProject);
                    }
                }
            }

            projects.AddRange(solutions.SelectMany(s => s.Projects.Where(p => !p.Exists)).Distinct());

            SafeLogRunner("Complete!");
            return new DependencyGraph(projects, solutions);
        }
            catch(IOException)
            {
                SafeLogRunner("Unable to gather projects from that directory");
                return null;
            }
}

        public DependencyGraph LoadFromFile(string saveFileName)
        {
            var formatter = new BinaryFormatter();
            try
            {
                using (var inStream = File.OpenRead(saveFileName))
                {
                    return (DependencyGraph)formatter.Deserialize(inStream);
                }
            }
            catch
            {
                return null;
            }
        }

        public void SaveToFile(string saveFileName, DependencyGraph dependencies)
        {
            SafeLogRunner(string.Format("Complete, saving to: {0}\r\n --refresh to refresh from disk", saveFileName));
            try
            {
                try { File.Delete(saveFileName); }
                catch { }

                using (var stream = File.OpenWrite(saveFileName))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, dependencies);
                }

            }
            catch
            {
                SafeLogRunner(string.Format("Unable to save file: {0}", saveFileName));
            }
        }

        public DependencyGraph GetDependantsForProject(ProjectFile[] inputProjects)
        {
            return new DependencyGraph(inputProjects.SelectMany(proj => proj.GetAncestors()).ToArray(), inputProjects.SelectMany(p => p.WhosMyDaddys()).ToArray());
        }

        public string[] GetProjectDependencies(ProjectFile[] inputProjects, string treeBaseName, bool verbose)
        {
            var projects = new List<ProjectFile>(inputProjects.SelectMany(proj => proj.ReferencesProjects));
            var dependencies = new List<string>();

            foreach (var reference in inputProjects.SelectMany(proj => proj.ReferencesProjects))
            {
                var treeBase = reference.AssemblyName;
                if (verbose)
                {
                    treeBase = treeBaseName + "->" + reference.AssemblyName;
                }
                var references = GetProjectDependencies(new[] { reference }, treeBase, verbose);
                dependencies.Add(treeBase);
                dependencies.AddRange(references);
            }

            return dependencies.ToArray();
        }

    }
}
