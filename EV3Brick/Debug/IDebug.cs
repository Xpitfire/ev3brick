using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1.Debug
{
    public interface IDebug
    {
        /// <summary>
        /// Default debug interface method to pass or return objects,
        /// which enables the possibility to detect, log or handle different 
        /// occurrences within an application.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        object[] Debug(object[] args);
    }
}
