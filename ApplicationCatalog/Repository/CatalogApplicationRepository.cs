using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCatalog;
using Dapper;
using Google.Apis.Auth.OAuth2;
using Microsoft.VisualBasic.FileIO;

namespace DependercyRejectionUI.Repository
{
    public class CatalogApplicationRepository
    {
        private IDbConnection _dbCn;
        private string _csv;
        public CatalogApplicationRepository(string csv, string dbConnectionString)
        {
            _csv = csv;
            _dbCn = new SqlConnection(dbConnectionString);
        }

        public IEnumerable<DeployedApplication> GetApps()
        {
            List <DeployedApplication> apps = new List<DeployedApplication>();

            using (TextFieldParser parser = new TextFieldParser(@_csv))
            {
                parser.TextFieldType = FieldType.Delimited;

                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (fields[0].ToLower().Trim() == "cci-archive")
                    {
                        break;
                    }

                    if (fields.Length > 5 && 
                        !string.IsNullOrEmpty(fields[1]) && fields[1] != "-" &&
                        fields[0].ToLower().Trim() != "server")
                    {
                        DeployedApplication app = new DeployedApplication()
                        {
                            BusinessArea = fields[17],
                            Description = fields[6],
                            OctoName = fields[1].Trim(),
                            UserAccount = fields[11].Trim()
                        };

                        var alreadyApp = apps
                            .FirstOrDefault(a => a.OctoName == app.OctoName);
                        if (null == alreadyApp)
                        {
                            app.SetAppType(fields[7]);
                            app.SetIpPort(fields[4].Trim(), fields[5].Trim());
                            app.SetSolarWinds(fields[2].Trim());
                            app.Logs = fields[10].Trim();
                        }
                        else
                        {
                            app = alreadyApp;
                        }
                        
                        app.DeployedLocations.Add(fields[9].Trim());
                        app.AddSchedule(fields[8].Trim());


                        apps.Add(app);

                    }
                }
            }
            return apps;

        }

        public void UpsertApplication(DeployedApplication application)
        {
            _dbCn.Open();
            try
            {
                string sql = "[app].[usp_DeployedApplication_Upsert]";

                int rowsAffected = this._dbCn.Execute(sql,
                    new
                    {
                        Octopack = application.OctoName,
                        BusinessArea = application.BusinessArea,
                        DeployedType = application.DeployedType.ToString(),
                        Description  = application.Description,
                        SolarWinds = application.SolarWinds.ToString(),
                        Logs = application.Logs,
                        IP = application.Ip,
                        Port = application.Port
                    },
                    commandType: CommandType.StoredProcedure);
                //TODO: Get schedules and Locations
            }
            finally
            {
                _dbCn.Close();
            }
        }
    }
}
