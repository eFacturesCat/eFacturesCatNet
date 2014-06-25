using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using eFacturesCat.Commons;

namespace eFacturesCat.Deliver
{
    /// <summary>
    /// Class for Response to Deliver Invoice Method
    /// </summary>
    /// <remarks>eFactures.cat project</remarks>
    /// <author>@santicasas</author>
    public class DeliverResponse:Response
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public DeliverResponse()
        {
            setCorrect("correct", "invoice sent successfully");
        }

    }
}
