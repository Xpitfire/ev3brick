using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1.Debug
{
    interface IEv3Debug
    {
        void Log();

        object[] Debug(object[] args);
    }
}
