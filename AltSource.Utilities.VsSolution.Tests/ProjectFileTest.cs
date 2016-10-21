using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltSource.Utilities.VSSolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AltSource.Utilities.VsSolution.Tests
{
    [TestClass]
    public class ProjectFileTest
    {

        [TestMethod]
        public void ProjectFile_Should_FindSvc()
        {
            ProjectFile proj = ProjectFile.Build(@"C:\dev\cci\Trunk\src\CCI.Services\ConsumerCellularInventoryService\ConsumerCellularInventoryService.csproj");

            var svcs = proj.GetWcfServices();

            Assert.IsTrue(svcs.Count > 0);
        }
    }

}
