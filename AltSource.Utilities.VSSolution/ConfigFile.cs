using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution
{
    public enum ConfigFileType
    {
        App,
        Web
    }

    public class ConfigFile
    {
        private ConfigFile()
        {
            
        }

        public IEnumerable<WcfService> Services { get; private set; }
        public IEnumerable<WcfService> ServiceClients { get; private set; }

        public bool IsWcf
        {
            get { return (null != Services && Services.Any()); }
        }

        public ConfigFileType ConfigFileType
        {
            get; private set;
        }

        public IEnumerable<string> DbNames { get; private set; }

        public static ConfigFile Build(ProjectFile projectFile)
        {
            var rtnConfig = new ConfigFile();

            string configPath = GetConfigPath(projectFile.FilePath);

            if (string.IsNullOrEmpty(configPath))
                return null;

            ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            rtnConfig.ConfigFileType = configPath.EndsWith("web.config") ? ConfigFileType.Web : ConfigFileType.App;

            var dbconnections = config.ConnectionStrings.ConnectionStrings;
            ConnectionStringSettings[] dbConfigElements = new ConnectionStringSettings[dbconnections.Count];
            dbconnections.CopyTo(dbConfigElements, 0);

            rtnConfig.DbNames = dbConfigElements.SelectMany(cs =>
                cs.ConnectionString.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                    .Where(d => !d.ToUpper().Contains("SQLEXPRESS") &&
                                !d.ToLower().Contains("aspnetdb.mdf") &&
                                (d.Trim().StartsWith("Initial Catalog", StringComparison.CurrentCultureIgnoreCase) || 
                                d.Trim().StartsWith("AttachDBFilename", StringComparison.CurrentCultureIgnoreCase)) )
                    .Select(d => d.Split('=')[1].Trim())
                );
            
            
            rtnConfig.ServiceClients= GetServiceClientsFrom(config);
            
            rtnConfig.Services = GetServicesFrom(config);

            return rtnConfig;
        }

        protected static List<WcfService> GetServicesFrom(Configuration config)
        {
            var servicesCollection =
                     (config.GetSection("system.serviceModel/services") as ServicesSection).Services;

            var services = new List<WcfService>();
            for (int i = 0; i < servicesCollection.Count; i++)
            {
                foreach (var endPoint in servicesCollection[i].Endpoints)
                {
                    var endPointElm = (endPoint as ServiceEndpointElement) ??
                                      new ServiceEndpointElement()
                                      {
                                          Address = new Uri(string.Empty, UriKind.Relative)
                                      };

                    var service = new WcfService()
                    {
                        Name = servicesCollection[i].Name,
                        Address = endPointElm.Address,
                        ConfigType = ServiceConfigType.ServiceHost,
                        Contract = endPointElm.Contract
                    };
                    services.Add(service);
                }

                //foreach (var baseAddress in servicesCollection[i].Host.BaseAddresses)
                //{
                //    var baseAddressElm = (baseAddress as BaseAddressElement) ??
                //                         new BaseAddressElement() { BaseAddress = "http://notTheSErviceYoureLookingFor" };

                //    var service = new WcfService()
                //    {
                //        Name = servicesCollection[i].Name,
                //        Address = new Uri(baseAddressElm.BaseAddress),
                //        ConfigType = ServiceConfigType.ServiceHost,
                //    };
                //    services.Add(service);
                //}
            }

            return services;
        }

        protected static List<WcfService> GetServiceClientsFrom(Configuration config)
        {
            var clientServices = new List<WcfService>();
            try
            {
                var clientCollection =
                    (config.GetSection("system.serviceModel/client") as ClientSection).Endpoints;

                for (int i = 0; i < clientCollection.Count; i++)
                {
                    var clientAddress = clientCollection[i].Address ?? new Uri(string.Empty, UriKind.Relative);
                    var client = new WcfService()
                    {
                        Name = clientCollection[i].Name,
                        Address = clientAddress,
                        ConfigType = ServiceConfigType.ServiceHost,
                        Contract = clientCollection[i].Contract
                    };
                    clientServices.Add(client);
                }
            }
            catch
            {
            }

            return clientServices;
        }
        

        protected static string GetConfigPath(string projectPath)
        {
            string configFolder = Path.GetDirectoryName(projectPath);
            string configPath = Path.Combine(configFolder, "app.config");
            if (!File.Exists(configPath))
            {
                configPath = Path.Combine(configFolder, "web.config");
            }

            if (!File.Exists(configPath))
            {
                configPath = string.Empty;
            }
            return configPath;
        }
    }
}
