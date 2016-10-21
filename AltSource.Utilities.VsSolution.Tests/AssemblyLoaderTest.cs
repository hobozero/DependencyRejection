using System;
using System.Linq;
using AltSource.Utilities.VSSolution;
using AltSource.Utilities.VSSolution.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using System.ServiceProcess;

namespace AltSource.Utilities.VsSolution.Tests
{
    [TestClass]
    public class AssemblyLoaderTest
    {
        [TestMethod]
        public void AssemblyLoader_Should_Load_The_Assembly()
        {
            string projFilePath =
                @"C:\dev\cci\Trunk\src\CCI.WebService.WarrantyService\CCI.WebService.WarrantyService.csproj";
            var projFile = ProjectFile.Build(projFilePath);
            var loader = new AssemblyLoader(projFile);

            var assembly = loader.Load();

            Assert.IsNotNull(assembly);
        }

        [TestMethod]
        public void AssemblyLoader_Should_Find_the_ServiceContracts()
        {
            string projFilePath =
                @"C:\dev\cci\Trunk\src\CCI.Services\CCI.Services.InventoryService\CCI.Services.InventoryService\CCI.Services.InventoryService.csproj";
            var projFile = ProjectFile.Build(projFilePath);
            var loader = new AssemblyLoader(projFile);

            var assembly = loader.Load();
            
            var types = loader.GetTypesWith<ServiceContractAttribute>(true);

            Assert.IsTrue(null != types && types.Count() > 0);
        }

        [TestMethod]
        public void AssemblyLoader_Should_Find_ServiceHosts()
        {
            string projFilePath =
                @"C:\dev\cci\Trunk\src\CCI.Services\ConsumerCellularSecureWorkflowService\ConsumerCellularSecureWorkflowService.csproj";
            var projFile = ProjectFile.Build(projFilePath);
            var loader = new AssemblyLoader(projFile);

            var assembly = loader.Load();

            var types = loader.GetTypesWith<ServiceContractAttribute>(true);

            Assert.IsTrue(null != types && types.Count() > 0);
        }

        [TestMethod]
        public void AssemblyLoader_Should_Find_ServiceBases()
        {
            string projFilePath =
                @"C:\dev\cci\Trunk\src\Billing\CCI.Billing.RatePlanService\CCI.Billing.RatePlanService.csproj";
            var projFile = ProjectFile.Build(projFilePath);
            var loader = new AssemblyLoader(projFile);

            var assembly = loader.Load();

            var types = loader.GetBaseTypes<ServiceBase>();

            Assert.IsTrue(null != types && types.Count() > 0);
        }
    }
}
