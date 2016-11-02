using System.Collections.Generic;
using ApplicationCatalog;

namespace CCI.Shared.Admin.AppCatalog.Core.Repository
{
    public interface IDeployedApplicationRepository
    {
        IEnumerable<DeployedApplication> GetApps();
        IEnumerable<DeployedApplication> GetSheetApps();
        void UpsertApplication(DeployedApplication application);
    }
}