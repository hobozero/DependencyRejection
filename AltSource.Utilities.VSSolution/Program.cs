using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution
{
	class Program
	{
		static void Main()
		{
			var devFolder = Environment.CurrentDirectory;
            var saveFileName = "DependercyRejection.derp";
            string inputAssembly;
			bool verbose = false;

            DependencyGraph dependencies = null;
            var factory = new DependencyGraphFactory();
            //load saved if exists
            if (File.Exists(saveFileName))
            {
                Console.WriteLine("Loading From Cache");
                dependencies = factory.LoadFromFile(saveFileName);
            }

            //if save didn't populate
            if (dependencies == null)
            {
                dependencies = factory.BuildFromDisk(devFolder, false);
                factory.SaveToFile(saveFileName, dependencies);
            }



			//would be a good idea to save this....
			string consoleInput = string.Empty;

			do
			{
				Console.WriteLine("Enter Assembly name");
				consoleInput = Console.ReadLine();
				Console.Clear();
				inputAssembly = consoleInput.Split(' ')[0];
				verbose = consoleInput.Contains("-v");

                if (consoleInput.Contains("--refresh"))
                {
                    //refresh here
                    factory.BuildFromDisk(devFolder, false);
                    continue;
                }

				Console.WriteLine("Finding Assembly {0}", inputAssembly);

                var selectedProjects = dependencies.ProjectFiles.Where(proj => proj.AssemblyName != null && proj.AssemblyName.ToLower() == inputAssembly.ToLower());
				if (!selectedProjects.Any())
				{
					Console.WriteLine("Assembly not found {0}", inputAssembly);
				}
				else
				{

					//based on input file, 
                    var upTheGraph = factory.GetDependantsForProject(selectedProjects.ToArray());
                    var downTheGraph = factory.GetProjectDependencies(selectedProjects.ToArray(), string.Empty, verbose);

					Console.WriteLine(
		@"
===================
Dependent Solutions
===================");
					int index = 1;
					foreach (var solution in upTheGraph.SolutionFiles.Distinct().OrderBy(sol => sol.FilePath))
					{
						Console.WriteLine("{0} - {1}", index++, solution.FilePath);
					}

					Console.WriteLine(@"
==================
Dependent Projects
==================");
					index = 1;
					foreach (var project in upTheGraph.ProjectFiles.Distinct().OrderBy(proj => proj.AssemblyName))
					{
						Console.WriteLine("{0} - {1}", index++, project.AssemblyName);
					}

					Console.WriteLine(@"
================
Project Requires
================");
					index = 1;
                    foreach (var project in downTheGraph.Distinct().OrderBy(proj => proj))
					{
						Console.WriteLine("{0} - {1}", index++, project);
					}

					Console.WriteLine("\r\nComplete...");
				}
			}
			while (consoleInput != "exit");
		}
	}
}
