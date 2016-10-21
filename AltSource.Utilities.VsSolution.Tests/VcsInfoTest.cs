using System;
using AltSource.Utilities.VSSolution.VCS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AltSource.Utilities.VsSolution.Tests
{
    [TestClass]
    public class VcsInfoTest
    {
        [TestMethod]
        public void VcsInfo_Should_ReturnTheRepo()
        {
            var vcsInfo = new VcsInfo(@"C:\\dev\\cci\\Trunk\\.svn");

            string repo = vcsInfo.Repo;

            Assert.IsNotNull(repo);
        }
    }
}
