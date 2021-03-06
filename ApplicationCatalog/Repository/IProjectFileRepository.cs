﻿using System;
using System.Collections.Generic;
using AltSource.Utilities.VSSolution;

namespace CCI.Shared.Admin.AppCatalog.Core.Repository
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