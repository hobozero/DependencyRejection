using System;
using System.CodeDom;
using System.Text.RegularExpressions;
using AltSource.Utilities.VSSolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AltSource.Utilities.VsSolution.Tests
{
    [TestClass]
    public class SolutionFileTest
    {
        [TestMethod]
        public void RemoveProjectFileFromSolution_Should_Remove_ProjectFile()
        {
            //Arrange
            var solnFile  = new SolutionFile();
            var origText = @"MinimumVisualStudioVersion = 10.0.40219.1
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""Solution Items"", ""Solution Items"", ""{BB52B956-AEF5-4EF3-8DE7-B15A4B2E7616}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""CCI.WindowsServices.CarrierFileWatcherService"", ""WindowsServices\CarrierFileWatcherService\CCI.WindowsServices.CarrierFileWatcherService.csproj"", ""{2F4B7C6F-935B-4657-9547-36FD0A1ABD8B}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""CCI.Interfaces.Logging"", ""CCI.Interfaces.Logging\CCI.Interfaces.Logging.csproj"",\ ""{45e444ac-a513-40c5-9fd1-abbb9f265a05}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""CCI.Interfaces.Logging"", ""CCI.Interfaces.Logging\CCI.Interfaces.Logging.csproj"",\ ""{45e444ac-a513-40c5-9fd1-abbb9f265a05}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""CCI.Utility"", ""Utility\CCI.Utility.csproj"", ""{F1DFADEC-DAC8-46D0-B01E-A882BE12F0A2}""
EndProject
";

            solnFile.InputText = origText;
            var projFile = ProjectFile.Build(Guid.Parse(@"45e444ac-a513-40c5-9fd1-abbb9f265a05"), "CCI.Interfaces.Logging", ProjectTypeDict.Get(Guid.Parse("45e444ac-a513-40c5-9fd1-abbb9f265a05")));

            var removed = @"MinimumVisualStudioVersion = 10.0.40219.1
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""Solution Items"", ""Solution Items"", ""{BB52B956-AEF5-4EF3-8DE7-B15A4B2E7616}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""CCI.WindowsServices.CarrierFileWatcherService"", ""WindowsServices\CarrierFileWatcherService\CCI.WindowsServices.CarrierFileWatcherService.csproj"", ""{2F4B7C6F-935B-4657-9547-36FD0A1ABD8B}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""CCI.Utility"", ""Utility\CCI.Utility.csproj"", ""{F1DFADEC-DAC8-46D0-B01E-A882BE12F0A2}""
EndProject
";

            //Act
            solnFile.RemoveProjectFileFromSolution(projFile);

            //Assert
            Assert.AreEqual(solnFile.InputText, removed);
        }
    }
    
}