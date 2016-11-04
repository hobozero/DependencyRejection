using AltSource.Utilities.VSSolution;
using AltSource.Utilities.VSSolution.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AltSource.Utilities.VSSolution.Graph;

namespace DependercyRejectionUI
{
    public class RollupNode
    {
        Tuple<TreeNode, int> _tuple;
        public RollupNode(TreeNode node, int count)
        {
            _tuple = new Tuple<TreeNode, int>(node, count);
        }
        public TreeNode Node { get { return _tuple.Item1; } }
        public int ChildCount { get { return _tuple.Item2; } }
    }

    static class TreeNodeBuilder
    {
        public static RollupNode BuildTreeNodes(ProjectNode projNode, List<IGraphFilter> displayFilters)
        {
            foreach (var filter in displayFilters)
            {
                if (!filter.IsVisible(projNode.ProjectFile))
                    return null;
            }
            
            var thisNode = new TreeNode()
            {
                Text = projNode.ToString()
            };

            foreach (var project in projNode.Related.OrderBy(proj => proj.ProjectFile.AssemblyName))
            {
                var relatedTreeNode = BuildTreeNodes(project, displayFilters);
                if (relatedTreeNode != null)
                {
                    thisNode.Nodes.Add(relatedTreeNode.Node);
                }
            }

            return new RollupNode(thisNode, projNode.TotalEdges);
        }
    }
}
