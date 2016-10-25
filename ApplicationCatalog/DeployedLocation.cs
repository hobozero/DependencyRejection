using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationCatalog
{
    public class DeployedLocation
    {
        private DeployedApplication _parentApp;
        public DeployedLocation(DeployedApplication parentApp)
        {
            _parentApp = parentApp;
        }

        static Regex regexIp = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
        public string MachineName { get; set; }
        public string Path { get; set; }
        public string Ip { get; set; }
        public int? Port { get; set; }
        public string UserAccount { get; set; }


        public void SetIpPort(string Ip, string port)
        {
            if (regexIp.IsMatch(Ip))
            {
                this.Ip = Ip;
            }

            if (_parentApp.DeployedType != AppType.ScheduledTask)
            {
                int portNum = 80;
                int.TryParse(port, out portNum);
                this.Port = portNum;
            }
        }
    }
}
