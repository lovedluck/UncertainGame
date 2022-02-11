using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public enum EDebugState
    {
        OPEN = 0,
        CLOSE,
    }

    public enum EDebugGrade
    {
        NONE = 0,
        TRACE,
        DEBUG,
        INFO,
        WARN,
        ERROR
    }
}
