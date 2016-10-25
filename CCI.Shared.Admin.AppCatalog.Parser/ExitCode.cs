using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCI.Shared.Admin.AppCatalog.Parser
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms681382(v=vs.85).aspx
    /// </summary>
    enum ExitCode : int
    {
        Success = 0,
        UnknownError = 10,
        BadArguments = 160
    }
}
