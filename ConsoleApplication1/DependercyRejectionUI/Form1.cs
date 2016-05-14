﻿using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public Form1()
        {
            InitializeComponent();

            CurrentLoadState = LoadState.NotLoaded;

            Button_BuildFromDirectory.Click += Button_BuildFromDirectory_Click;
            Button_LoadAssemblyInformation.Click += Button_LoadAssemblyInformation_Click;
        }

        private void Button_LoadAssemblyInformation_Click(object sender, EventArgs e)
        {
            if (ComboBox_AssemblySelector.SelectedItem is ComboBoxItem && ((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem) != null)
            {
                BuildTreeInfo(((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem).Value);
            }
        }

        private void BuildTreeInfo(ProjectFile projectFile)
        {
            TreeView_AssemblyInformationTree.Nodes.Clear();
            var baseNodes = BuildTreeNodes(projectFile);
            TreeView_AssemblyInformationTree.Nodes.Add(baseNodes.Item1);

            //traverse tree time...
        }

        private Tuple<TreeNode, int> BuildTreeNodes(ProjectFile file)
        {
            var thisNode = new TreeNode();

            int projectCount = file.ReferencesProjects.Count;
            foreach (var project in file.ReferencesProjects)
            {
                var info = BuildTreeNodes(project);
                thisNode.Nodes.Add(info.Item1);
                projectCount += info.Item2;
            }

            thisNode.Text = (string.Format("{0}: {1}", file.AssemblyName, projectCount));

            return new Tuple<TreeNode, int>(thisNode, projectCount);
        }

        private void Button_BuildFromDirectory_Click(object sender, EventArgs e)
        {
            DependencyGraph = ConsoleApplication1.DependencyGraphFactory.BuildFromDisk(TextBox_DirectoryInputText.Text);

            if (DependencyGraph != null)
            {
                PopulateComboBox();
                CurrentLoadState = LoadState.Loaded;
            }
        }

        private void PopulateComboBox()
        {
			var items = DependencyGraph.ProjectFiles.Select(project =>
			{
				return new ComboBoxItem()
				{
					Text = project.AssemblyName,
					Value = project
				};
			}).ToArray();

			foreach (var item in items.Where(item => item.Text != null))
			{
				ComboBox_AssemblySelector.Items.Add(item);
			}

		}

        private void SetAssemblyControls(bool enabled)
        {
            Button_SaveToCache.Enabled = enabled;
            ComboBox_AssemblySelector.Enabled = enabled;
            Button_LoadAssemblyInformation.Enabled = enabled;
            TreeView_AssemblyInformationTree.Enabled = enabled;
        }

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public ProjectFile Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}