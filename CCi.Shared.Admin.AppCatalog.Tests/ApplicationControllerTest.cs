using System;
using System.Collections.Generic;
using System.Linq;
using AltSource.Utilities.VSSolution;
using ApplicationCatalog;
using CCi.Shared.Admin.AppCatalog.Controllers;
using CCI.Shared.Admin.AppCatalog.Core.Repository;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CCi.Shared.Admin.AppCatalog.Tests
{
    [TestClass]
    public class ApplicationControllerTest
    {
        [TestMethod]
        public void Get_Should_Have_a_projectFile()
        {
            MapConfig.Config();
            var repo = A.Fake<IDeployedApplicationRepository>();
            A.CallTo(() => repo.GetApps()).Returns(
                new List<DeployedApplication>()
                {
                    new DeployedApplication()
                    {
                        BusinessArea = "BusArea",
                        DeployedLocations = new List<DeployedLocation>(),
                        DeployedType = AppType.Web,
                        Description = "Description",
                        Logs = "Logs",
                        OctoName = "OctoName",
                        ProjectFile =
                            ProjectFile.Build(
                                Guid.NewGuid(),
                                "AssemblyName",
                                ProjectTypeDict.Get(new Guid("349C5851-65DF-11DA-9384-00065B846F21"))
                                ),
                        Schedules = new List<string>(),
                        SolarWinds = SolarWinds.Available
                    }
                });

            var ctrl = new ApplicationController(repo);

            var apps = ctrl.Get();

            Assert.IsNotNull(apps.First().ProjectId);

        }
    }
}
