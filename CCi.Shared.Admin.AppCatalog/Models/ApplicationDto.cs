using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCi.Shared.Admin.AppCatalog.Models
{
    public class ApplicationDto
    {
        public string UniqueId { get; set; }
        public Guid ProjectId { get; set; }
    }
}