using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalTimerTool
{
    public class NonExstingUpdateException : Exception
    {
        public NonExstingUpdateException()
        {
        }

        public NonExstingUpdateException(string message)
            : base(message)
        {
        }

        public NonExstingUpdateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class NonExstingProjectException : Exception
    {
        public NonExstingProjectException()
        {
        }

        public NonExstingProjectException(string message)
            : base(message)
        {
        }

        public NonExstingProjectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
