using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace eFacturesCat.Commons
{
    /// <summary>
    /// Exception class for eFacturesCat API
    /// </summary>
    class EFacturesException:Exception
    {
        public EFacturesException(String str) : base(str) { }
    }
}
