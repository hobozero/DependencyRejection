using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltSource.Utilities.VSSolution;
using ApplicationCatalog.Extensions;
using Dapper;

namespace ApplicationCatalog.Repository
{
    public class ProjectFileRepository
    {
        private IDbConnection _db;


        public ProjectFileRepository(IDbConnection db)
        {
            _db = db;
        }

        public List<ProjectFile> GetProjectFiles()
        {
            throw new NotImplementedException();
        }

        public ProjectFile GetSingleProjectFile(int projectFileId)
        {
            throw new NotImplementedException();
        }

        public bool InsertProjectFile(ProjectFile projectFile)
        {
            string sql = @"INSERT INTO [app].[Projects]
           ([ProjectId]
           ,[FilePath]
           ,[AssemblyName]
           ,[OutputType]
           ,[OctoPack]
           ,[IsTopLevel]
           ,[SourceXML])
     VALUES
           (@projId
           ,@filePath
           ,@assemblyName
           ,@outputType
           ,@octoPack
           ,@isTopLevel
           ,@xml)";

            int rowsAffected = this._db.Execute(sql, 
                new
                {
                    ProjectId = projectFile.ProjectId,
                    FilePath = projectFile.FilePath,
                    AssemblyName = projectFile.AssemblyName,
                    OutputType = projectFile.OutputType.ToString(),
                    OctoPack = projectFile.OctoPackProjectName,
                    IsTopLevel = projectFile.IsTopLevel,
                    SourceXML = projectFile.Xml
                });

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }

        public bool DeleteProjectFile(int projectFileId)
        {
            throw new NotImplementedException();
        }

        public void UpdateProjectFile(ProjectFile projectFile)
        {
            string sql = @"app.usp_ProjectFile_Upsert";

            int rowsAffected = this._db.Execute(sql,
                new
                {
                    ProjectId = projectFile.ProjectId,
                    FilePath = projectFile.FilePath,
                    AssemblyName = projectFile.AssemblyName,
                    OutputType = projectFile.OutputType.ToString(),
                    ProjectType = projectFile.ProjectType.TypeName,
                    OctoPack = projectFile.OctoPackProjectName,
                    IsTopLevel = projectFile.IsTopLevel,
                    Repo = projectFile.VcsInfo.Repo,
                    Vcs = projectFile.VcsInfo.Vcs.ToString(),
                    SourceXML = projectFile.Xml
                },
                commandType: CommandType.StoredProcedure);

            RefreshDBs(projectFile);

            RefreshServices(projectFile);

            if (projectFile.HasHealthEndpoint())
            {
                UpsertHealthEndpoint(projectFile);
            }
        }

        protected void RefreshDBs(ProjectFile projectFile)
        {
            string sqlDbDelete = @"DELETE from [app].[ProjectDB] where [ProjectId] = @ProjectId";

            string sqlDbInsert = @"INSERT INTO [app].[ProjectDB]
           ([DbName]
           ,[ProjectId])
     VALUES
           (@dbName
           ,@projectId)";

            this._db.Execute(sqlDbDelete, new
            {
                ProjectId = projectFile.ProjectId
            });

            if (projectFile.ConfigFile != null)
            {
                foreach (var dbName in projectFile.ConfigFile.DbNames)
                {
                    this._db.Execute(sqlDbInsert, new
                    {
                        dbName = dbName,
                        ProjectId = projectFile.ProjectId
                    });
                }
            }
        }
        protected void RefreshServices(ProjectFile projectFile)
        {
            string sqlServicesDelete = @"DELETE from [app].[ProjectService] where [ProjectId] = @ProjectId";
            string sqlServiceClientsDelete = @"DELETE from [app].[ProjectClientService] where [ProjectId] = @ProjectId";


            string sqlServiceInsert = @"INSERT INTO [app].[ProjectService]
           ([ServiceName]
           ,[Address]
           ,[ProjectId]
            ,[Contract])
     VALUES
           (@serviceName
           ,@address
           ,@projectId
            ,@contract)";

            string sqlClientServiceInsert = @"INSERT INTO [app].[ProjectClientService]
           ([ServiceName]
            ,[Address]
           ,[ProjectId]
            ,[Contract])
     VALUES
           (@serviceName
           ,@address
           ,@projectId
            ,@contract)";


            this._db.Execute(sqlServiceClientsDelete, new
            {
                ProjectId = projectFile.ProjectId
            });
            this._db.Execute(sqlServicesDelete, new
            {
                ProjectId = projectFile.ProjectId
            });



            foreach (var service in projectFile.GetWcfServices())
            {
                this._db.Execute(sqlServiceInsert, new
                {
                    serviceName = service.Name,
                    address = (service.Address.IsAbsoluteUri ? service.Address.AbsolutePath : service.Address.OriginalString)
                                    .Trim('/'),
                    ProjectId = projectFile.ProjectId,
                    Contract = service.Contract
                });
            }



            if (projectFile.ConfigFile != null)
            {
                if (null != projectFile.ConfigFile.ServiceClients)
                {
                    foreach (var service in projectFile.ConfigFile.ServiceClients)
                    {
                        this._db.Execute(sqlClientServiceInsert, new
                        {
                            serviceName = service.Name,
                            address = (service.Address.IsAbsoluteUri ? service.Address.AbsolutePath : service.Address.OriginalString)
                                            .Trim('/'),
                            ProjectId = projectFile.ProjectId,
                            Contract = service.Contract
                        });
                    }
                }
                
            }
        }

        protected void UpsertHealthEndpoint(ProjectFile projectFile)
        {
            string sqlDbDelete = @"DELETE from [app].[HealthEndpoint] where [ProjectId] = @ProjectId";

            string sqlDbInsert = @"INSERT INTO [app].[HealthEndpoint]
           ([URI]
           ,[ProjectId])
     VALUES
           (@URI
           ,@projectId)";

            this._db.Execute(sqlDbDelete, new
            {
                ProjectId = projectFile.ProjectId
            });

            this._db.Execute(sqlDbInsert, new
            {
                ProjectId = projectFile.ProjectId,
                URI = "/health"
            });
        }
    }
}
