using System;
using System.Collections.Generic;
using AltSource.Utilities.VSSolution;

namespace DependercyRejectionUI.Repository
{
    public interface IProjectFileRepository
    {
        List<ProjectFile> GetProjectFiles();

        ProjectFile GetSingleProjectFile(int projectFileId);

        bool InsertProjectFile(ProjectFile ourProjectFile);

        bool DeleteProjectFile(int projectFileId);

        bool UpdateProjectFile(ProjectFile ourProjectFile);
    }
}