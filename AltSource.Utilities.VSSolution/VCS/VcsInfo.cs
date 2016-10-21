using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using ConfigurationParser;
using SharpSvn;

namespace AltSource.Utilities.VSSolution.VCS
{
    public enum VcsType
    {
        None,
        Git,
        SVN
    }

    public class VcsInfo
    {
        private readonly string _filePath = string.Empty;

        StringBuilder commandResult = new StringBuilder();
        public VcsInfo(string filePath)
        {
            _filePath = filePath;
            Vcs = VcsType.None;
            Repo = string.Empty;

            if (!string.IsNullOrEmpty(filePath))
            {
                var fi = new FileInfo(_filePath);

                var dir = fi.Directory;

                while (null != dir && Vcs == VcsType.None)
                {
                    var gitDir = dir.GetDirectories(".git").FirstOrDefault();
                    if (null != gitDir)
                    {
                        Vcs = VcsType.Git;
                        var configFile = gitDir.GetFiles("config").FirstOrDefault();
                        if (null != configFile)
                        {
                            var parser = new Parser(configFile.FullName);
                            Repo = parser.GetString("remote \"origin\"", "url");
                        }
                    }

                    var svnDir = dir.GetDirectories(".svn").FirstOrDefault();
                    if (null != svnDir)
                    {
                        Vcs = VcsType.SVN;

                        svnDir = svnDir.Parent;
                        SvnClient svnClient = new SvnClient();
                        Repo = svnClient.GetUriFromWorkingCopy(svnDir.FullName).AbsolutePath;
                        
                    }

                    dir = dir.Parent;
                }
            }
        }

        public string Repo { get; protected set; }

        public VcsType Vcs { get; protected set; }

    }
}
