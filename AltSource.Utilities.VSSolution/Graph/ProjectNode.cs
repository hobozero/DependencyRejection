using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution.Graph
{
    public class ProjectNode
    {
        private ProjectFile _file;
        public ProjectNode(ProjectFile file)
        {
            Related = new List<ProjectNode>();
            _file = file;
        }

        public ProjectFile ProjectFile
        {
            get { return _file; }
        }

        public int TotalEdges { get; set; }

        public List<ProjectNode> Related { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} - [{2}]",
                _file.AssemblyName,
                (0 == TotalEdges) ? string.Empty : TotalEdges.ToString(),
                _file.ProjectType.TypeName
                );
        }
    }
}
