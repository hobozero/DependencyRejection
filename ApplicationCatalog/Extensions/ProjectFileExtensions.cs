using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AltSource.Utilities.VSSolution;
using AltSource.Utilities.VSSolution.Reflection;

namespace CCI.Shared.Admin.AppCatalog.Core.Extensions
{
    public static class ProjectFileExtensions
    {
        public static bool HasHealthEndpoint(this ProjectFile projFile)
        {
            AssemblyLoader loader = new AssemblyLoader(projFile);
            loader.Load();

            return loader.ContainsTypeName(new string[] { "CCI.Diagnostics.Mvc3.ConfigurationDiagnosticsController", "CCI.Diagnostics.Mvc4.ConfigurationDiagnosticsController" });
        }
    }
}
