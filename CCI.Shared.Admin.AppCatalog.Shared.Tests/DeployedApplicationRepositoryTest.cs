using System;
using CCI.Shared.Admin.AppCatalog.Core.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CCI.Shared.Admin.AppCatalog.Shared.Tests
{
    [TestClass]
    public class DeployedApplicationRepositoryTest
    {
        [TestMethod]
        public void DeployedApplicationRepository_Should_return_allitems()
        {
            var repo = new DeployedApplicationRepository(null, "Server=sql-dev;Database=ApplicationCatalog;User Id=AppCatalogUser;Password=ACDev123!");

            var results = repo.GetApps();
        }
    }
}
