using System;

namespace Framework
{
    public abstract class ExceptionBase: ApplicationException
    {
        public ExceptionBase(string message) : base(message)
        {
            LDebug.Instance.DebugState = EDebugState.OPEN;
            LDebug.Instance.PrintLog(EDebugGrade.TRACE, message);
        }
    }
}
