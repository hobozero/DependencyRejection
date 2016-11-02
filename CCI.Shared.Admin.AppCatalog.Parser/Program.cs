using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltSource.Utilities.VSSolution;
using CCI.Shared.Admin.AppCatalog.Core.Repository;

namespace CCI.Shared.Admin.AppCatalog.Parser
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage AppCatalogParser [FilePath] [Deployed Location/URI] [TeamCityBuildURI]");
                return (int) ExitCode.BadArguments;
            }
            else
            {
                string codeFolder = args[0];
                if (Directory.Exists(codeFolder))
                {
                    DependencyGraph grpah = new DependencyGraphFactory().BuildFromDisk(codeFolder, false);
                    var projects = grpah.ProjectFiles
                        .Where(item => item.AssemblyName != null);

                    using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["appCatalog"].ConnectionString))
                    {
                        db.Open();
                        var repo = new ProjectFileRepository(db);

                        foreach (var projectFile in projects)
                        {
                            repo.UpdateProjectFile(projectFile);
                        }

                        db.Close();
                    }
                }
                else
                {
                    Console.WriteLine("Bad path. No parse!");
                    return (int)ExitCode.BadArguments;
                }

                return (int) ExitCode.Success;
            }
        }
    }
}
