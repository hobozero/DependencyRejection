using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCi.Shared.Admin.AppCatalog.Models
{

    public enum SolarWinds
    {
        Yes,
        No,
        Available,
        NotAvailable
    }

    public enum DeployedType
    {
        Unknown,
        ScheduledTask,
        ManualConsole,
        Web,
        WindowsService,
        WPF,
        WebService,
    }


    public class ApplicationDto
    {
        public string UniqueId { get; set; }
        public Guid ProjectId { get; set; }
        public string Description { get; set; }
        public string VCS { get; set; }
        public string Path { get; set; }
        public string Assembly { get; set; }
        public OutputType OutputType { get; set; }
        public string ProjectType { get; set; }

        public bool AutomatedDeploy { get; set; }
        public string BusinessArea { get; set; }
        public DeployedType DeployedType { get; set; }
        public SolarWinds SolarWinds { get; set; }
    }

}