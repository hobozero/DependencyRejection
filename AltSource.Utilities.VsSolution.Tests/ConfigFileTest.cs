using System;
using AltSource.Utilities.VSSolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AltSource.Utilities.VsSolution.Tests
{
    [TestClass]
    public class ConfigFileTest
    {
        [TestMethod]
        public void ConfigFile_Should_FindClients()
        {
            ProjectFile proj = ProjectFile.Build(@"C:\dev\cci\Trunk\src\CCI.WebUI.Inventory\CCI.WebUI.Inventory.csproj");

            ConfigFile configFile = ConfigFile.Build(proj);

            Assert.IsNotNull(configFile.ServiceClients);
        }

        [TestMethod]
        public void ConfigFile_Should_FindSvc()
        {
            ProjectFile proj = ProjectFile.Build(@"C:\dev\cci\Trunk\src\CCI.Services\ConsumerCellularInventoryService\ConsumerCellularInventoryService.csproj");

            ConfigFile configFile = ConfigFile.Build(proj);

            Assert.IsNotNull(configFile.Services);
        }
    }
}
