using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution.Graph
{
    public class ProjectGraph
    {
        public ProjectNode BuildGraph(ProjectFile file, bool traverseUp)
        {
            var relatedProjects = (traverseUp) ? file.ReferencedByProjects : file.ReferencesProjects;

            var thisNode = new ProjectNode(file)
            {
                TotalEdges = relatedProjects.Count
            };

            foreach (var project in relatedProjects.OrderBy(proj => proj.AssemblyName))
            {
                var relatedTreeNode = BuildGraph(project, traverseUp);
                if (relatedTreeNode != null)
                {
                    thisNode.Related.Add(relatedTreeNode);
                    thisNode.TotalEdges += relatedTreeNode.TotalEdges;
                }
            }

            return thisNode;
        }
    }
}
