namespace DependercyRejectionUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TextBox_DirectoryInputText = new System.Windows.Forms.TextBox();
            this.Button_BuildFromDirectory = new System.Windows.Forms.Button();
            this.Button_LoadFromCache = new System.Windows.Forms.Button();
            this.Button_SaveToCache = new System.Windows.Forms.Button();
            this.ComboBox_AssemblySelector = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TreeView_AssemblyInformationTree = new System.Windows.Forms.TreeView();
            this.ComboBox_FilterAssembly = new System.Windows.Forms.ComboBox();
            this.Button_LoadAssemblyInformation = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howTheShitDoIUseThisThingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpProjectTypes = new System.Windows.Forms.GroupBox();
            this.btnLoadAppCatalog = new System.Windows.Forms.Button();
            this.chkExportSelectedOnly = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkAppOnly = new System.Windows.Forms.CheckBox();
            this.Out = new System.Windows.Forms.Button();
            this.chkOutputTypes = new System.Windows.Forms.CheckedListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolStatusLabel_OutputStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAddOctoPack = new System.Windows.Forms.Button();
            this.btnListPackages = new System.Windows.Forms.Button();
            this.cboSolutionToClean = new System.Windows.Forms.ComboBox();
            this.btnRemoveProjectFromOneSoln = new System.Windows.Forms.Button();
            this.btnCorruptedProjects = new System.Windows.Forms.Button();
            this.txtMissing = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFrameworkVersion = new System.Windows.Forms.TextBox();
            this.Version = new System.Windows.Forms.Label();
            this.txtPackageVersion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPackageId = new System.Windows.Forms.TextBox();
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.txtDestProjects = new System.Windows.Forms.RichTextBox();
            this.btnFillWithAncestors = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProjGuid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProjName = new System.Windows.Forms.TextBox();
            this.btnRemoveFromAllSolns = new System.Windows.Forms.Button();
            this.btnRemoveProjectFromSolns = new System.Windows.Forms.Button();
            this.btnFixDeadbeatSolutions = new System.Windows.Forms.Button();
            this.btnDeadBeatSolns = new System.Windows.Forms.Button();
            this.btnRemoveReferences = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddReference = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tooltipDeadbeats = new System.Windows.Forms.ToolTip(this.components);
            this.chkTrunkOnly = new System.Windows.Forms.CheckBox();
            this.chkProjectTypes = new DependercyRejectionUI.ColoredCheckListBox();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.grpProjectTypes.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox_DirectoryInputText
            // 
            this.TextBox_DirectoryInputText.Location = new System.Drawing.Point(12, 39);
            this.TextBox_DirectoryInputText.Name = "TextBox_DirectoryInputText";
            this.TextBox_DirectoryInputText.Size = new System.Drawing.Size(180, 20);
            this.TextBox_DirectoryInputText.TabIndex = 0;
            this.TextBox_DirectoryInputText.Text = "C:\\dev\\cci\\Trunk\\src";
            // 
            // Button_BuildFromDirectory
            // 
            this.Button_BuildFromDirectory.Location = new System.Drawing.Point(309, 37);
            this.Button_BuildFromDirectory.Name = "Button_BuildFromDirectory";
            this.Button_BuildFromDirectory.Size = new System.Drawing.Size(119, 23);
            this.Button_BuildFromDirectory.TabIndex = 1;
            this.Button_BuildFromDirectory.Text = "Load from FileSystem";
            this.Button_BuildFromDirectory.UseVisualStyleBackColor = true;
            this.Button_BuildFromDirectory.Click += new System.EventHandler(this.Button_BuildFromDirectory_Click);
            // 
            // Button_LoadFromCache
            // 
            this.Button_LoadFromCache.Location = new System.Drawing.Point(434, 37);
            this.Button_LoadFromCache.Name = "Button_LoadFromCache";
            this.Button_LoadFromCache.Size = new System.Drawing.Size(121, 23);
            this.Button_LoadFromCache.TabIndex = 2;
            this.Button_LoadFromCache.Text = "Load From Cache";
            this.Button_LoadFromCache.UseVisualStyleBackColor = true;
            this.Button_LoadFromCache.Click += new System.EventHandler(this.Button_LoadFromCache_Click);
            // 
            // Button_SaveToCache
            // 
            this.Button_SaveToCache.Location = new System.Drawing.Point(561, 37);
            this.Button_SaveToCache.Name = "Button_SaveToCache";
            this.Button_SaveToCache.Size = new System.Drawing.Size(106, 23);
            this.Button_SaveToCache.TabIndex = 3;
            this.Button_SaveToCache.Text = "Save To Cache";
            this.Button_SaveToCache.UseVisualStyleBackColor = true;
            this.Button_SaveToCache.Click += new System.EventHandler(this.Button_SaveToCache_Click);
            // 
            // ComboBox_AssemblySelector
            // 
            this.ComboBox_AssemblySelector.FormattingEnabled = true;
            this.ComboBox_AssemblySelector.Location = new System.Drawing.Point(12, 66);
            this.ComboBox_AssemblySelector.Name = "ComboBox_AssemblySelector";
            this.ComboBox_AssemblySelector.Size = new System.Drawing.Size(368, 21);
            this.ComboBox_AssemblySelector.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TreeView_AssemblyInformationTree);
            this.groupBox1.Location = new System.Drawing.Point(17, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 711);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lookup Assemblies";
            // 
            // TreeView_AssemblyInformationTree
            // 
            this.TreeView_AssemblyInformationTree.Location = new System.Drawing.Point(6, 19);
            this.TreeView_AssemblyInformationTree.Name = "TreeView_AssemblyInformationTree";
            this.TreeView_AssemblyInformationTree.Size = new System.Drawing.Size(651, 686);
            this.TreeView_AssemblyInformationTree.TabIndex = 6;
            // 
            // ComboBox_FilterAssembly
            // 
            this.ComboBox_FilterAssembly.FormattingEnabled = true;
            this.ComboBox_FilterAssembly.Location = new System.Drawing.Point(12, 93);
            this.ComboBox_FilterAssembly.Name = "ComboBox_FilterAssembly";
            this.ComboBox_FilterAssembly.Size = new System.Drawing.Size(370, 21);
            this.ComboBox_FilterAssembly.TabIndex = 7;
            this.ComboBox_FilterAssembly.Text = "Select a filter assembly";
            this.ComboBox_FilterAssembly.SelectedIndexChanged += new System.EventHandler(this.ComboBox_FilterAssembly_SelectedIndexChanged);
            // 
            // Button_LoadAssemblyInformation
            // 
            this.Button_LoadAssemblyInformation.Location = new System.Drawing.Point(388, 62);
            this.Button_LoadAssemblyInformation.Name = "Button_LoadAssemblyInformation";
            this.Button_LoadAssemblyInformation.Size = new System.Drawing.Size(75, 23);
            this.Button_LoadAssemblyInformation.TabIndex = 5;
            this.Button_LoadAssemblyInformation.Text = "Load";
            this.Button_LoadAssemblyInformation.UseVisualStyleBackColor = true;
            this.Button_LoadAssemblyInformation.Click += new System.EventHandler(this.Button_LoadAssemblyInformation_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1026, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howTheShitDoIUseThisThingToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howTheShitDoIUseThisThingToolStripMenuItem
            // 
            this.howTheShitDoIUseThisThingToolStripMenuItem.Name = "howTheShitDoIUseThisThingToolStripMenuItem";
            this.howTheShitDoIUseThisThingToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.howTheShitDoIUseThisThingToolStripMenuItem.Text = "How the shit do I use this thing?";
            this.howTheShitDoIUseThisThingToolStripMenuItem.Click += new System.EventHandler(this.howTheShitDoIUseThisThingToolStripMenuItem_Click);
            // 
            // grpProjectTypes
            // 
            this.grpProjectTypes.Controls.Add(this.btnLoadAppCatalog);
            this.grpProjectTypes.Controls.Add(this.chkExportSelectedOnly);
            this.grpProjectTypes.Controls.Add(this.btnSave);
            this.grpProjectTypes.Controls.Add(this.chkAppOnly);
            this.grpProjectTypes.Controls.Add(this.Out);
            this.grpProjectTypes.Controls.Add(this.chkOutputTypes);
            this.grpProjectTypes.Controls.Add(this.chkProjectTypes);
            this.grpProjectTypes.Location = new System.Drawing.Point(686, 37);
            this.grpProjectTypes.Name = "grpProjectTypes";
            this.grpProjectTypes.Size = new System.Drawing.Size(281, 711);
            this.grpProjectTypes.TabIndex = 7;
            this.grpProjectTypes.TabStop = false;
            this.grpProjectTypes.Text = "Project Types";
            // 
            // btnLoadAppCatalog
            // 
            this.btnLoadAppCatalog.Location = new System.Drawing.Point(7, 682);
            this.btnLoadAppCatalog.Name = "btnLoadAppCatalog";
            this.btnLoadAppCatalog.Size = new System.Drawing.Size(172, 23);
            this.btnLoadAppCatalog.TabIndex = 7;
            this.btnLoadAppCatalog.Text = "Save App Catalog to DB";
            this.btnLoadAppCatalog.UseVisualStyleBackColor = true;
            this.btnLoadAppCatalog.Click += new System.EventHandler(this.btnLoadAppCatalog_Click);
            // 
            // chkExportSelectedOnly
            // 
            this.chkExportSelectedOnly.AutoSize = true;
            this.chkExportSelectedOnly.Location = new System.Drawing.Point(111, 613);
            this.chkExportSelectedOnly.Name = "chkExportSelectedOnly";
            this.chkExportSelectedOnly.Size = new System.Drawing.Size(92, 17);
            this.chkExportSelectedOnly.TabIndex = 6;
            this.chkExportSelectedOnly.Text = "Selected Only";
            this.tooltipDeadbeats.SetToolTip(this.chkExportSelectedOnly, "Ignore shared libraries.");
            this.chkExportSelectedOnly.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(7, 641);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(172, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save Projects to DB";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkAppOnly
            // 
            this.chkAppOnly.AutoSize = true;
            this.chkAppOnly.Location = new System.Drawing.Point(209, 613);
            this.chkAppOnly.Name = "chkAppOnly";
            this.chkAppOnly.Size = new System.Drawing.Size(74, 17);
            this.chkAppOnly.TabIndex = 4;
            this.chkAppOnly.Text = "Apps Only";
            this.tooltipDeadbeats.SetToolTip(this.chkAppOnly, "Ignore shared libraries.");
            this.chkAppOnly.UseVisualStyleBackColor = true;
            // 
            // Out
            // 
            this.Out.Location = new System.Drawing.Point(7, 612);
            this.Out.Name = "Out";
            this.Out.Size = new System.Drawing.Size(98, 23);
            this.Out.TabIndex = 2;
            this.Out.Text = "Export all CSV";
            this.Out.UseVisualStyleBackColor = true;
            this.Out.Click += new System.EventHandler(this.Out_Click);
            // 
            // chkOutputTypes
            // 
            this.chkOutputTypes.FormattingEnabled = true;
            this.chkOutputTypes.Location = new System.Drawing.Point(6, 389);
            this.chkOutputTypes.Name = "chkOutputTypes";
            this.chkOutputTypes.Size = new System.Drawing.Size(269, 94);
            this.chkOutputTypes.TabIndex = 1;
            this.chkOutputTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkOutputTypes_ItemCheck);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStatusLabel_OutputStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 742);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(569, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ToolStatusLabel_OutputStatus
            // 
            this.ToolStatusLabel_OutputStatus.Name = "ToolStatusLabel_OutputStatus";
            this.ToolStatusLabel_OutputStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(12, 129);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1001, 789);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grpProjectTypes);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(993, 763);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Analysis";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAddOctoPack);
            this.tabPage1.Controls.Add(this.btnListPackages);
            this.tabPage1.Controls.Add(this.cboSolutionToClean);
            this.tabPage1.Controls.Add(this.btnRemoveProjectFromOneSoln);
            this.tabPage1.Controls.Add(this.btnCorruptedProjects);
            this.tabPage1.Controls.Add(this.txtMissing);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtFrameworkVersion);
            this.tabPage1.Controls.Add(this.Version);
            this.tabPage1.Controls.Add(this.txtPackageVersion);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtPackageId);
            this.tabPage1.Controls.Add(this.txtResults);
            this.tabPage1.Controls.Add(this.txtDestProjects);
            this.tabPage1.Controls.Add(this.btnFillWithAncestors);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtProjGuid);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtProjName);
            this.tabPage1.Controls.Add(this.btnRemoveFromAllSolns);
            this.tabPage1.Controls.Add(this.btnRemoveProjectFromSolns);
            this.tabPage1.Controls.Add(this.btnFixDeadbeatSolutions);
            this.tabPage1.Controls.Add(this.btnDeadBeatSolns);
            this.tabPage1.Controls.Add(this.btnRemoveReferences);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnAddReference);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(993, 763);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Modify Projects";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnAddOctoPack
            // 
            this.btnAddOctoPack.Location = new System.Drawing.Point(4, 548);
            this.btnAddOctoPack.Name = "btnAddOctoPack";
            this.btnAddOctoPack.Size = new System.Drawing.Size(186, 23);
            this.btnAddOctoPack.TabIndex = 30;
            this.btnAddOctoPack.Text = "add missing OctoPacks";
            this.btnAddOctoPack.UseVisualStyleBackColor = true;
            this.btnAddOctoPack.Click += new System.EventHandler(this.btnAddOctoPack_Click);
            // 
            // btnListPackages
            // 
            this.btnListPackages.Location = new System.Drawing.Point(0, 607);
            this.btnListPackages.Name = "btnListPackages";
            this.btnListPackages.Size = new System.Drawing.Size(190, 23);
            this.btnListPackages.TabIndex = 28;
            this.btnListPackages.Text = "List Projects With Package";
            this.btnListPackages.UseVisualStyleBackColor = true;
            this.btnListPackages.Click += new System.EventHandler(this.btnListPackages_Click);
            // 
            // cboSolutionToClean
            // 
            this.cboSolutionToClean.FormattingEnabled = true;
            this.cboSolutionToClean.Location = new System.Drawing.Point(6, 382);
            this.cboSolutionToClean.Name = "cboSolutionToClean";
            this.cboSolutionToClean.Size = new System.Drawing.Size(193, 21);
            this.cboSolutionToClean.TabIndex = 27;
            // 
            // btnRemoveProjectFromOneSoln
            // 
            this.btnRemoveProjectFromOneSoln.Location = new System.Drawing.Point(7, 352);
            this.btnRemoveProjectFromOneSoln.Name = "btnRemoveProjectFromOneSoln";
            this.btnRemoveProjectFromOneSoln.Size = new System.Drawing.Size(192, 23);
            this.btnRemoveProjectFromOneSoln.TabIndex = 26;
            this.btnRemoveProjectFromOneSoln.Text = "Remove Project from this Solution";
            this.btnRemoveProjectFromOneSoln.UseVisualStyleBackColor = true;
            this.btnRemoveProjectFromOneSoln.Click += new System.EventHandler(this.btnRemoveProjectFromOneSoln_Click);
            // 
            // btnCorruptedProjects
            // 
            this.btnCorruptedProjects.Location = new System.Drawing.Point(10, 225);
            this.btnCorruptedProjects.Name = "btnCorruptedProjects";
            this.btnCorruptedProjects.Size = new System.Drawing.Size(189, 23);
            this.btnCorruptedProjects.TabIndex = 25;
            this.btnCorruptedProjects.Text = "List Corrupted Projects in Solns";
            this.btnCorruptedProjects.UseVisualStyleBackColor = true;
            this.btnCorruptedProjects.Click += new System.EventHandler(this.btnCorruptedProjects_Click);
            // 
            // txtMissing
            // 
            this.txtMissing.Location = new System.Drawing.Point(10, 198);
            this.txtMissing.Name = "txtMissing";
            this.txtMissing.Size = new System.Drawing.Size(193, 21);
            this.txtMissing.TabIndex = 24;
            this.txtMissing.Text = "List Missing Projects";
            this.txtMissing.UseVisualStyleBackColor = true;
            this.txtMissing.Click += new System.EventHandler(this.txtMissing_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 718);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "FrameworkVersion";
            // 
            // txtFrameworkVersion
            // 
            this.txtFrameworkVersion.Location = new System.Drawing.Point(7, 737);
            this.txtFrameworkVersion.Name = "txtFrameworkVersion";
            this.txtFrameworkVersion.Size = new System.Drawing.Size(190, 20);
            this.txtFrameworkVersion.TabIndex = 22;
            this.txtFrameworkVersion.Text = "net45";
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.Location = new System.Drawing.Point(4, 676);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(88, 13);
            this.Version.TabIndex = 21;
            this.Version.Text = "Package Version";
            // 
            // txtPackageVersion
            // 
            this.txtPackageVersion.Location = new System.Drawing.Point(4, 695);
            this.txtPackageVersion.Name = "txtPackageVersion";
            this.txtPackageVersion.Size = new System.Drawing.Size(190, 20);
            this.txtPackageVersion.TabIndex = 20;
            this.txtPackageVersion.Text = "1.0.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 633);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Package Id";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPackageId
            // 
            this.txtPackageId.Location = new System.Drawing.Point(1, 649);
            this.txtPackageId.Name = "txtPackageId";
            this.txtPackageId.Size = new System.Drawing.Size(193, 20);
            this.txtPackageId.TabIndex = 18;
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(548, 51);
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(429, 567);
            this.txtResults.TabIndex = 16;
            this.txtResults.Text = "";
            // 
            // txtDestProjects
            // 
            this.txtDestProjects.Location = new System.Drawing.Point(206, 163);
            this.txtDestProjects.Name = "txtDestProjects";
            this.txtDestProjects.Size = new System.Drawing.Size(334, 452);
            this.txtDestProjects.TabIndex = 15;
            this.txtDestProjects.Text = "";
            // 
            // btnFillWithAncestors
            // 
            this.btnFillWithAncestors.Location = new System.Drawing.Point(284, 115);
            this.btnFillWithAncestors.Name = "btnFillWithAncestors";
            this.btnFillWithAncestors.Size = new System.Drawing.Size(240, 23);
            this.btnFillWithAncestors.TabIndex = 14;
            this.btnFillWithAncestors.Text = "Fill with ancestor projects";
            this.btnFillWithAncestors.UseVisualStyleBackColor = true;
            this.btnFillWithAncestors.Click += new System.EventHandler(this.btnFillWithAncestors_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Override Project GUID";
            // 
            // txtProjGuid
            // 
            this.txtProjGuid.Location = new System.Drawing.Point(206, 87);
            this.txtProjGuid.Name = "txtProjGuid";
            this.txtProjGuid.Size = new System.Drawing.Size(318, 20);
            this.txtProjGuid.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Override Project Name";
            // 
            // txtProjName
            // 
            this.txtProjName.Location = new System.Drawing.Point(206, 48);
            this.txtProjName.Name = "txtProjName";
            this.txtProjName.Size = new System.Drawing.Size(318, 20);
            this.txtProjName.TabIndex = 10;
            // 
            // btnRemoveFromAllSolns
            // 
            this.btnRemoveFromAllSolns.Location = new System.Drawing.Point(6, 317);
            this.btnRemoveFromAllSolns.Name = "btnRemoveFromAllSolns";
            this.btnRemoveFromAllSolns.Size = new System.Drawing.Size(193, 28);
            this.btnRemoveFromAllSolns.TabIndex = 9;
            this.btnRemoveFromAllSolns.Text = "Remove project from ALL Solutions";
            this.btnRemoveFromAllSolns.UseVisualStyleBackColor = true;
            this.btnRemoveFromAllSolns.Click += new System.EventHandler(this.btnRemoveFromAllSolns_Click);
            // 
            // btnRemoveProjectFromSolns
            // 
            this.btnRemoveProjectFromSolns.Location = new System.Drawing.Point(6, 284);
            this.btnRemoveProjectFromSolns.Name = "btnRemoveProjectFromSolns";
            this.btnRemoveProjectFromSolns.Size = new System.Drawing.Size(195, 27);
            this.btnRemoveProjectFromSolns.TabIndex = 8;
            this.btnRemoveProjectFromSolns.Text = "Remove Project From My Solns";
            this.btnRemoveProjectFromSolns.UseVisualStyleBackColor = true;
            this.btnRemoveProjectFromSolns.Click += new System.EventHandler(this.btnRemoveProjectFromSolns_Click);
            // 
            // btnFixDeadbeatSolutions
            // 
            this.btnFixDeadbeatSolutions.Location = new System.Drawing.Point(9, 164);
            this.btnFixDeadbeatSolutions.Name = "btnFixDeadbeatSolutions";
            this.btnFixDeadbeatSolutions.Size = new System.Drawing.Size(194, 23);
            this.btnFixDeadbeatSolutions.TabIndex = 7;
            this.btnFixDeadbeatSolutions.Text = "Fix Deadbeat Solutions";
            this.btnFixDeadbeatSolutions.UseVisualStyleBackColor = true;
            this.btnFixDeadbeatSolutions.Click += new System.EventHandler(this.btnFixDeadbeatSolutions_Click);
            // 
            // btnDeadBeatSolns
            // 
            this.btnDeadBeatSolns.Location = new System.Drawing.Point(7, 135);
            this.btnDeadBeatSolns.Name = "btnDeadBeatSolns";
            this.btnDeadBeatSolns.Size = new System.Drawing.Size(194, 23);
            this.btnDeadBeatSolns.TabIndex = 6;
            this.btnDeadBeatSolns.Text = "List DeadBeat Solutions";
            this.btnDeadBeatSolns.UseVisualStyleBackColor = true;
            this.btnDeadBeatSolns.Click += new System.EventHandler(this.btnDeadBeatSolns_Click);
            // 
            // btnRemoveReferences
            // 
            this.btnRemoveReferences.Location = new System.Drawing.Point(7, 94);
            this.btnRemoveReferences.Name = "btnRemoveReferences";
            this.btnRemoveReferences.Size = new System.Drawing.Size(194, 25);
            this.btnRemoveReferences.TabIndex = 5;
            this.btnRemoveReferences.Text = "Remove References from Projects";
            this.btnRemoveReferences.UseVisualStyleBackColor = true;
            this.btnRemoveReferences.Click += new System.EventHandler(this.btnRemoveReferences_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(545, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Results";
            // 
            // btnAddReference
            // 
            this.btnAddReference.Location = new System.Drawing.Point(6, 36);
            this.btnAddReference.Name = "btnAddReference";
            this.btnAddReference.Size = new System.Drawing.Size(195, 48);
            this.btnAddReference.TabIndex = 2;
            this.btnAddReference.Text = "(Re)Add  project reference to ancestors or those listed";
            this.tooltipDeadbeats.SetToolTip(this.btnAddReference, "A deadbeat solution should reference a project, but doesn\'t");
            this.btnAddReference.UseVisualStyleBackColor = true;
            this.btnAddReference.Click += new System.EventHandler(this.btnAddReference_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Destination(s)";
            // 
            // tooltipDeadbeats
            // 
            this.tooltipDeadbeats.ToolTipTitle = "Show this";
            // 
            // chkTrunkOnly
            // 
            this.chkTrunkOnly.AutoSize = true;
            this.chkTrunkOnly.Location = new System.Drawing.Point(199, 39);
            this.chkTrunkOnly.Name = "chkTrunkOnly";
            this.chkTrunkOnly.Size = new System.Drawing.Size(71, 17);
            this.chkTrunkOnly.TabIndex = 9;
            this.chkTrunkOnly.Text = "trunkOnly";
            this.chkTrunkOnly.UseVisualStyleBackColor = true;
            // 
            // chkProjectTypes
            // 
            this.chkProjectTypes.FormattingEnabled = true;
            this.chkProjectTypes.Location = new System.Drawing.Point(6, 19);
            this.chkProjectTypes.Name = "chkProjectTypes";
            this.chkProjectTypes.Size = new System.Drawing.Size(269, 364);
            this.chkProjectTypes.TabIndex = 0;
            this.chkProjectTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkProjectTypes_ItemCheck);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 921);
            this.Controls.Add(this.chkTrunkOnly);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.ComboBox_FilterAssembly);
            this.Controls.Add(this.Button_SaveToCache);
            this.Controls.Add(this.Button_LoadFromCache);
            this.Controls.Add(this.Button_LoadAssemblyInformation);
            this.Controls.Add(this.Button_BuildFromDirectory);
            this.Controls.Add(this.ComboBox_AssemblySelector);
            this.Controls.Add(this.TextBox_DirectoryInputText);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpProjectTypes.ResumeLayout(false);
            this.grpProjectTypes.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_DirectoryInputText;
        private System.Windows.Forms.Button Button_BuildFromDirectory;
        private System.Windows.Forms.Button Button_LoadFromCache;
        private System.Windows.Forms.Button Button_SaveToCache;
        private System.Windows.Forms.ComboBox ComboBox_AssemblySelector;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Button_LoadAssemblyInformation;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howTheShitDoIUseThisThingToolStripMenuItem;
        private System.Windows.Forms.ComboBox ComboBox_FilterAssembly;
		private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ToolStatusLabel_OutputStatus;
        private System.Windows.Forms.GroupBox grpProjectTypes;
        private System.Windows.Forms.CheckedListBox chkOutputTypes;
        private ColoredCheckListBox chkProjectTypes;
        private System.Windows.Forms.Button Out;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView TreeView_AssemblyInformationTree;
        private System.Windows.Forms.Button btnAddReference;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRemoveReferences;
        private System.Windows.Forms.Button btnDeadBeatSolns;
        private System.Windows.Forms.Button btnFixDeadbeatSolutions;
        private System.Windows.Forms.Button btnRemoveProjectFromSolns;
        private System.Windows.Forms.Button btnRemoveFromAllSolns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProjName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProjGuid;
        private System.Windows.Forms.Button btnFillWithAncestors;
        private System.Windows.Forms.RichTextBox txtDestProjects;
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFrameworkVersion;
        private System.Windows.Forms.Label Version;
        private System.Windows.Forms.TextBox txtPackageVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPackageId;
        private System.Windows.Forms.ToolTip tooltipDeadbeats;
        private System.Windows.Forms.Button txtMissing;
        private System.Windows.Forms.Button btnCorruptedProjects;
        private System.Windows.Forms.ComboBox cboSolutionToClean;
        private System.Windows.Forms.Button btnRemoveProjectFromOneSoln;
        private System.Windows.Forms.Button btnListPackages;
        private System.Windows.Forms.CheckBox chkAppOnly;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkExportSelectedOnly;
        private System.Windows.Forms.CheckBox chkTrunkOnly;
        private System.Windows.Forms.Button btnLoadAppCatalog;
        private System.Windows.Forms.Button btnAddOctoPack;
    }
}

