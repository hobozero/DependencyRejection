using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution
{
    public enum ServiceConfigType
    {
        Unknown,
        IisSvc,
        ServiceHost
    }
    
    public class WcfService
    {
        public string Name { get; set; }
        public Uri Address { get; set; }
        public ServiceConfigType ConfigType { get; set; }
        public string Contract { get;  set; }
    }
}
