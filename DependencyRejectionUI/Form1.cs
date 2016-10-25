using AltSource.Utilities.VSSolution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Xml.Linq;
using AltSource.Utilities.VSSolution.Filters;
using ApplicationCatalog;
using ApplicationCatalog.Repository;

namespace DependercyRejectionUI
{
    public partial class Form1 : Form
    {
        private enum LoadState
        {
            NotLoaded,
            Loading,
            Loaded,
        }

        private List<IGraphFilter> _displayFilters = new List<IGraphFilter>();

        private ProjectFile _lastFilterProjectFile = null;

        private LoadState _currentLoadState;

        private LoadState CurrentLoadState
        {
            get { return _currentLoadState; }
            set
            {
                _currentLoadState = value;
                switch (_currentLoadState)
                {
                    case LoadState.Loaded:
                        SetAssemblyControls(true);
                        break;
                    default:
                        SetAssemblyControls(false);
                        break;
                }
            }
        }

        private DependencyGraph DependencyGraph;

        private DependencyGraphFactory GraphFactory;

        public Form1()
        {
            InitializeComponent();

            GraphFactory = new DependencyGraphFactory();
            GraphFactory.OutputLog += GraphFactory_OutputLog;
            CurrentLoadState = LoadState.NotLoaded;

            LoadChkProjectTypes();
            LoadChkOutputTypes();

        }

        private void GraphFactory_OutputLog(object sender, string msg)
        {
            ToolStatusLabel_OutputStatus.Text = msg;
            Update();
        }

        private void Button_BuildFromDirectory_Click(object sender, EventArgs e)
        {
            DependencyGraph = GraphFactory.BuildFromDisk(TextBox_DirectoryInputText.Text, chkTrunkOnly.Checked);

            if (DependencyGraph != null)
            {
                PopulateComboBoxes();
                CurrentLoadState = LoadState.Loaded;
            }
        }

        private void Button_SaveToCache_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = TextBox_DirectoryInputText.Text;
            dialog.AddExtension = true;
            dialog.DefaultExt = "derp";
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (File.Exists(dialog.FileName))
                {
                    File.Delete(dialog.FileName);
                }
                GraphFactory.SaveToFile(dialog.FileName, this.DependencyGraph);
            }
        }

        private void Button_LoadFromCache_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = TextBox_DirectoryInputText.Text;
            //Text files (*.txt)|*.txt|All files (*.*)|*.*
            dialog.Filter = "Derp files (*.derp)|*.derp";
            dialog.Multiselect = false;
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.DependencyGraph = GraphFactory.LoadFromFile(dialog.FileName);
                this.CurrentLoadState = LoadState.Loaded;
                PopulateComboBoxes();
            }
        }

        private void Button_LoadAssemblyInformation_Click(object sender, EventArgs e)
        {
            var projectFile = GetACtiveProjectFile(false);
            if (null != projectFile)
            {
                BuildFilteredProjectTree(projectFile, this._displayFilters);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void howTheShitDoIUseThisThingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Load projects from branch:
1) enter path in textbox
2) click load
3) wait a really long time for monster to populate
4) Select assembly in question
5) Watch and be amazed!", "How to use this shit");
        }

        private void SetAssemblyControls(bool enabled)
        {
            Button_SaveToCache.Enabled = enabled;
            ComboBox_AssemblySelector.Enabled = enabled;
            ComboBox_FilterAssembly.Enabled = enabled;
            Button_LoadAssemblyInformation.Enabled = enabled;
            TreeView_AssemblyInformationTree.Enabled = enabled;
        }

        private void PopulateComboBoxes()
        {
            ComboBox_AssemblySelector.Items.Clear();
            var projItems = DependencyGraph.ProjectFiles
                .OrderBy(proj => proj.AssemblyName)
                .Select(project =>
                {
                    return new ComboBoxProject()
                    {
                        Text =
                            ((project.Exists) ? string.Empty : "   ### (in sln but missing) - ") + project.AssemblyName,
                        Value = project
                    };
                }).ToArray();

            foreach (var item in projItems.Where(item => item.Text != null))
            {
                ComboBox_AssemblySelector.Items.Add(item);
                ComboBox_FilterAssembly.Items.Add(item);
            }


            cboSolutionToClean.Items.Clear();
            var solnItems = DependencyGraph.SolutionFiles
                .Select(soln =>
                {
                    return new ComboBoxSolution()
                    {
                        Text = soln.FileName,
                        Value = soln
                    };
                }).ToArray();

            foreach (var item in solnItems.Where(item => item.Text != null))
            {
                cboSolutionToClean.Items.Add(item);
            }
        }

        private void BuildFilteredProjectTree(ProjectFile projectFile, List<IGraphFilter> displayFilters)
        {
            TreeView_AssemblyInformationTree.Nodes.Clear();

            var down = TreeNodeBuilder.BuildTreeNodes(projectFile, displayFilters, false);
            if (down != null)
            {
                var node = new TreeNode() {Text = "Dependencies Down " + down.ChildCount};
                node.Nodes.Add(down.Node);
                TreeView_AssemblyInformationTree.Nodes.Add(node);
            }

            var up = TreeNodeBuilder.BuildTreeNodes(projectFile, displayFilters, true);
            if (up != null)
            {
                var node = new TreeNode() {Text = "Dependencies Up " + up.ChildCount};
                node.Nodes.Add(up.Node);
                TreeView_AssemblyInformationTree.Nodes.Add(node);
            }

            var dependents = GraphFactory.GetDependantsForProject(new[] {projectFile});
            var solutionNames = dependents
                .SolutionFiles.Distinct()
                .Select(sol => sol.FilePath)
                .OrderBy(s => s)
                .ToArray();
            var solutionsNode = new TreeNode() {Text = "Solutions " + solutionNames.Length};
            solutionsNode.Nodes.AddRange(solutionNames.Select(sol => new TreeNode() {Text = sol}).ToArray());
            TreeView_AssemblyInformationTree.Nodes.Add(solutionsNode);

        }

        private void LoadChkProjectTypes()
        {
            chkProjectTypes.Items.AddRange(
                ProjectTypeDict.All.Select(i => new ProjectType(i.Key, i.Value.TypeName, i.Value.Color))
                    .ToArray());

            chkProjectTypes.DisplayMember = "TypeName";

            for (int i = 0; i < chkProjectTypes.Items.Count; i++)
            {
                chkProjectTypes.SetItemChecked(i, true);
            }
        }

        private void LoadChkOutputTypes()
        {
            var i = 0;
            foreach (var outputType in Enum.GetValues(typeof (ProjectOutputType)))
            {
                chkOutputTypes.Items.Add(outputType);
                chkOutputTypes.SetItemChecked(i++, true);
            }
        }

        private void chkProjectTypes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var selectedGuid = ((ProjectType) ((CheckedListBox) sender).Items[e.Index]).ID;

            ProjectTypeFilter filter = new ProjectTypeFilter()
            {
                ProjectType = ProjectTypeDict.Get(selectedGuid)
            };

            if (e.NewValue == CheckState.Checked)
            {
                this._displayFilters.Remove(filter);
            }
            else
            {
                this._displayFilters.Add(filter);
            }

            var assemblyToAnalyze = GetACtiveProjectFile(false);
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(assemblyToAnalyze, _displayFilters);
            }
        }

        private void chkOutputTypes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var outputType = ((ProjectOutputType) ((CheckedListBox) sender).Items[e.Index]);

            var filter = new ProjectOutputTypeFilter()
            {
                OutputType = outputType
            };

            if (e.NewValue == CheckState.Checked)
            {
                this._displayFilters.Remove(filter);
            }
            else
            {
                this._displayFilters.Add(filter);
            }

            var assemblyToAnalyze = GetACtiveProjectFile(false);
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(assemblyToAnalyze, _displayFilters);
            }
        }

        private void ComboBox_FilterAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newFilterProject = (ComboBox_FilterAssembly.SelectedItem as ComboBoxProject);

            if (null != newFilterProject)
            {
                var filter = new ProjectIdFilter()
                {
                    FilterProjectGuid = newFilterProject.Value.ProjectId
                };
                this._displayFilters.Remove(filter);
            }
            else if (null != _lastFilterProjectFile)
            {
                var filter = new ProjectIdFilter()
                {
                    FilterProjectGuid = _lastFilterProjectFile.ProjectId
                };
                this._displayFilters.Remove(filter);
            }

            _lastFilterProjectFile = newFilterProject.Value;

            var assemblyToAnalyze = GetACtiveProjectFile(false);
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(assemblyToAnalyze, _displayFilters);
            }
        }

        private void Out_Click(object sender, EventArgs e)
        {
            IEnumerable<ProjectFile> projects;

            if (chkExportSelectedOnly.Checked)
            {
                var currentlySelectedPRoject = GetACtiveProjectFile(false);
                projects = currentlySelectedPRoject.ReferencesProjects;

            }
            else
            {
                var assemblies = DependencyGraph.ProjectFiles
                    .OrderBy(proj => proj.AssemblyName);

                projects = assemblies.Where(item => item.AssemblyName != null);
            }

            Output(projects, chkAppOnly.Checked);
        }

        protected void Output(IEnumerable<ProjectFile> projects, bool appOnly)
        {
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.ShowDialog();

            string fileName = sfDialog.FileName;

            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.WriteLine(@"AssemblyName,OctoPack,Path,OutputType-ProjectType,ReferencedBy,References,DBs");

                foreach (var project in projects)
                {
                    if (!appOnly ||
                        (project.IsTopLevel &&
                         project.ProjectType.TypeName != ProjectTypeDict.CCI_TOOLS &&
                         project.ProjectType.TypeName != ProjectTypeDict.TEST_DRIVER))
                    {
                        string dbNAmes = (project.ConfigFile != null)
                            ? string.Join("\t", project.ConfigFile.DbNames.ToArray())
                            : string.Empty;

                        sw.WriteLine(
                            $@"""{project.AssemblyName}"",""{project.OctoPackProjectName}"",""{project.FilePath}"",""{
                                project.OutputType.ToString()}-{project.ProjectType.TypeName}"",{
                                project.ReferencedByProjects.Count},{project.ReferencesProjects.Count},""{dbNAmes}""");
                    }
                }
            }
        }

        private void btnAddReference_Click(object sender, EventArgs e)
        {
            if (TextBox_DirectoryInputText.Text.ToLower().EndsWith("trunk"))
            {
                var confirmResult = MessageBox.Show("You are about to update trunk! you sure?",
                    "Confirm update!!",
                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.No)
                    return;
            }

            var currentlySelectedProject = GetACtiveProjectFile(false);

            if (null == currentlySelectedProject)
            {
                txtResults.Text = "No source project selected";
                return;
            }


            IEnumerable<ProjectFile> projList = txtDestProjects.Text.Split(new string[] {"\r", "\n", "\t"},
                StringSplitOptions.RemoveEmptyEntries)
                .Select(p => DependencyGraph.FindFileByName(p));
            WriteLog("Updating listed projects");


            foreach (var projFile in projList)
            {
                if (null == projFile)
                {
                    AppendLog("[Dependency Graph does not contain file] ");
                }
                else
                {
                    AppendLog("[Found] " + projFile.AssemblyName);

                    int removed = projFile.AddReference(currentlySelectedProject);
                    if (removed < 0)
                    {
                        AppendLog("Project skipped.");
                    }

                    AppendLog("[Removed] " + removed.ToString() + " existing references.");
                    AppendLog("[Reference added] ");
                }
            }
        }

        private void btnDeadBeatSolns_Click(object sender, EventArgs e)
        {
            txtResults.Text = string.Empty;
            var currentlySelectedProject = GetACtiveProjectFile(true);

            foreach (var deadBeatSolutionFile in currentlySelectedProject.GetDeadBeatSolutionFiles())
            {
                AppendLog(deadBeatSolutionFile.FileName);
            }
        }

        private void btnFixDeadbeatSolutions_Click(object sender, EventArgs e)
        {
            txtResults.Text = string.Empty;
            var currentlySelectedProject = GetACtiveProjectFile(true);

            ProjectType defaultAddType = (currentlySelectedProject.ProjectType.ID == Guid.Empty)
                ? ProjectTypeDict.GetByName("c#")
                : currentlySelectedProject.ProjectType;

            foreach (var deadBeatSolutionFile in currentlySelectedProject.GetDeadBeatSolutionFiles())
            {
                if (deadBeatSolutionFile.AddProjectFileToSolution(currentlySelectedProject, defaultAddType))
                {
                    AppendLog("[Fixed] " + deadBeatSolutionFile.FileName);
                }
                else
                {
                    AppendLog("[Ignored] " + deadBeatSolutionFile.FileName);
                }
            }
        }

        private void btnRemoveProjectFromSolns_Click(object sender, EventArgs e)
        {
            var currentlySelectedProject = GetACtiveProjectFile(true);

            if (null != currentlySelectedProject)
            {
                var removed = currentlySelectedProject.Emancipate();
                foreach (var solutionFile in removed)
                {
                    AppendLog("Updated " + solutionFile.FilePath + " solution files.");
                }
            }
            else
            {
                WriteLog("No ProjectFile selected or stud data provided");
            }

        }

        private void btnRemoveFromAllSolns_Click(object sender, EventArgs e)
        {
            var currentlySelectedProject = GetACtiveProjectFile(true);

            var removed = currentlySelectedProject.Emancipate(DependencyGraph.SolutionFiles);

            foreach (var solutionFile in removed)
            {
                AppendLog("Updated " + solutionFile.FilePath + " soution files.");
            }
        }

        private void btnRemoveReferences_Click(object sender, EventArgs e)
        {
            ProjectFile delProjFile = GetACtiveProjectFile(true);

            if (null != DependencyGraph)
            {
                foreach (
                    var projFile in
                        txtDestProjects.Text.Split(new string[] {"\r", "\n", "\t"},
                            StringSplitOptions.RemoveEmptyEntries).Select(pn => DependencyGraph.FindFileByName(pn)))
                {
                    if (null == projFile)
                    {
                        txtResults.Text += "[Dependency Graph does not contain project] " + Environment.NewLine;
                    }
                    else
                    {
                        int removed = projFile.RemoveExistingProjectReferences(delProjFile);

                        projFile.Xml.Save(projFile.FilePath, SaveOptions.OmitDuplicateNamespaces);

                        txtResults.Text += "[Removed from] " + removed.ToString() + " existing references." +
                                           Environment.NewLine;
                    }
                }
            }
            else
            {
                WriteLog("Gotta load first");
            }
        }

        private void btnFillWithAncestors_Click(object sender, EventArgs e)
        {
            var currentlySelectedProject = GetACtiveProjectFile(false);

            if (null == currentlySelectedProject)
            {
                txtResults.Text = "No source project selected";
                return;
            }

            txtDestProjects.Text = string.Join(Environment.NewLine,
                currentlySelectedProject.GetAncestors().Select(p => p.AssemblyName));

        }

        #region Helpers

        private class ComboBoxProject
        {
            public string Text { get; set; }
            public ProjectFile Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private class ComboBoxSolution
        {
            public string Text { get; set; }
            public SolutionFile Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private ProjectFile GetACtiveProjectFile(bool overrideWithManualProject)
        {
            ProjectFile rtnProj = null;
            if (overrideWithManualProject && (txtProjGuid.Text.Length > 0 || txtProjName.Text.Length > 0))
            {
                Guid guid;
                Guid.TryParse(txtProjGuid.Text, out guid);
                rtnProj = ProjectFile.Build(guid, txtProjName.Text, ProjectTypeDict.GetByName("C#"));


                //See if it is in Graph as a missing file in a solution 
                rtnProj = DependencyGraph.ProjectFiles.Where(p => p == rtnProj).DefaultIfEmpty(rtnProj).First();
            }
            else
            {
                var selectedItem = ComboBox_AssemblySelector.SelectedItem as ComboBoxProject;
                if (null != selectedItem)
                {
                    rtnProj = selectedItem.Value;
                }
            }

            return rtnProj;
        }


        private void AppendLog(string text)
        {
            txtResults.Text += text + Environment.NewLine;
        }

        private void WriteLog(string text)
        {
            txtResults.Text = text + Environment.NewLine;
        }

        #endregion

        private void txtMissing_Click(object sender, EventArgs e)
        {
            List<Tuple<SolutionFile, ProjectFile>> missTuples = new List<Tuple<SolutionFile, ProjectFile>>();
            foreach (var solutionFile in DependencyGraph.SolutionFiles)
            {
                foreach (var projectFile in solutionFile.Projects)
                {
                    if (!projectFile.Exists)
                    {
                        missTuples.Add(new Tuple<SolutionFile, ProjectFile>(solutionFile, projectFile));
                    }
                }
            }

            WriteLog("Unique Projects\r\n---------------\r\n");
            AppendLog(
                string.Join("\r\n",
                    missTuples.Select(t => string.Format("{0} {1}", t.Item2.AssemblyName, t.Item2.ProjectId))
                        .Distinct()
                        .ToArray())
                );
            AppendLog("");
            AppendLog(
                string.Join(
                    "\r\n===========================\r\n",
                    missTuples.Select(
                        t =>
                            string.Format("{0}\r\n{1} {2}", t.Item1.FileName, t.Item2.AssemblyName,
                                t.Item2.ProjectId.ToString())).ToArray())
                );

        }

        private void btnCorruptedProjects_Click(object sender, EventArgs e)
        {
            if (null == DependencyGraph.SolutionFiles)
            {
                WriteLog("Gotta load the Graph first");
                return;
            }
            foreach (var solutionFile in DependencyGraph.SolutionFiles)
            {
                AppendLog("\r\n");
                AppendLog(solutionFile.FilePath);
                AppendLog("====================================================");

                AppendLog(string.Join("\r\n",
                    solutionFile.AbsentProjects(solutionFile.InputText).Select(p => p.ProjectId).ToArray()));

            }
        }

        private void btnRemoveProjectFromOneSoln_Click(object sender, EventArgs e)
        {
            var soln = cboSolutionToClean.SelectedItem as ComboBoxSolution;
            if (null != soln)
            {
                var proj = GetACtiveProjectFile(true);
                if (proj != null)
                {
                    proj.Emancipate(new SolutionFile[] {soln.Value});
                }

            }
            else
            {
                WriteLog("Solution file not selected");
            }
        }

        private void btnListPackages_Click(object sender, EventArgs e)
        {
            var devFolder = TextBox_DirectoryInputText.Text;
            var packages = Directory.EnumerateFiles(devFolder, "packages.config", SearchOption.AllDirectories)
                //<--- .NET 4.5
                .ToArray()
                .Select(p => PackagesFile.Build(p))
                .Where(p => p.GetVersionOfLibrary(txtPackageId.Text) == txtPackageVersion.Text);

            foreach (var package in packages)
            {
                var proj = package.GetProject();
                if (null != proj)
                {
                    AppendLog(string.Format("Update-Package {0} {1}", txtPackageId.Text, proj.AssemblyName));
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IEnumerable<ProjectFile> projects;

            if (chkExportSelectedOnly.Checked)
            {
                var currentlySelectedPRoject = GetACtiveProjectFile(false);
                projects = currentlySelectedPRoject.ReferencesProjects;
            }
            else
            {
                var assemblies = DependencyGraph.ProjectFiles
                    .OrderBy(proj => proj.AssemblyName);

                projects = assemblies.Where(item => item.AssemblyName != null);
            }

            using (
                IDbConnection db =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Open();
                var repo = new ProjectFileRepository(db);

                foreach (var projectFile in projects)
                {
                    repo.UpdateProjectFile(projectFile);
                }

                db.Close();
            }
        }

        private void btnLoadAppCatalog_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = TextBox_DirectoryInputText.Text;
            //Text files (*.txt)|*.txt|All files (*.*)|*.*
            dialog.Filter = "CSV files (*.csv)|*.csv";
            dialog.Multiselect = false;
            var result = dialog.ShowDialog();

            IEnumerable<DeployedApplication> catalogApps;
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string db = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                var repo = new DeployedApplicationRepository(dialog.FileName, db);

                catalogApps = repo.GetApps();

                foreach (var catalogApp in catalogApps)
                {
                    repo.UpsertApplication(catalogApp);
                }
            }
        }

        private void btnAddOctoPack_Click(object sender, EventArgs e)
        {
            var projs = DependencyGraph.ProjectFiles
                .Where(p => p.IsTopLevel && string.IsNullOrEmpty(p.OctoPackProjectName));

            foreach (var projectFile in projs)
            {
                var newOctoName = string.Empty;
                string[] segments = projectFile.AssemblyName.Split(new char[] {'.'},
                    StringSplitOptions.RemoveEmptyEntries);
                if (segments.Length == 1)
                {
                    newOctoName = projectFile.AssemblyName;
                }

                else if (segments.Length > 2 && segments[0] == "CCI" && segments[1] == "Api")
                {
                    newOctoName = string.Join("", segments.Skip(2).ToArray()) + "Api";
                }
                else if (segments.Length > 2 && segments[0] == "CCI" && segments[1] == "App")
                {
                    newOctoName = string.Join("", segments.Skip(2).ToArray());
                }
                else if (segments.Length > 2 && segments[0] == "CCI" && segments[1] == "Billing")
                {
                    newOctoName = string.Join("", segments.Skip(2).ToArray());
                }
                else if (segments.Length > 2 && segments[0] == "CCI" && segments[1] == "Accounting")
                {
                    newOctoName = string.Join("", segments.Skip(1).ToArray());
                }
                else if (segments[0] == "CCI")
                {
                    newOctoName = string.Join("", segments.Skip(1).ToArray());
                }
                else
                {
                    newOctoName = string.Join("", segments.ToArray());
                }

                if (projectFile.AddOctoPackName(newOctoName))
                {
                    projectFile.Xml.Save(projectFile.FilePath, SaveOptions.OmitDuplicateNamespaces);

                    AppendLog(projectFile.FilePath);

                }
            }
        }
        
    }
}
