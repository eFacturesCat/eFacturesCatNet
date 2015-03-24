using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eFacturesCat.Deliver.AOCeFACT
{
    /// <summary>
    /// Class to send einvoices to AOC e.FACT service
    /// </summary>
    public class EndPointAOCEfact : EndPointEmail
    {
        public static String env_STAGE = "hubefactpru@aoc.cat";
        public static String env_PROD = "hubefact@aoc.cat";

        /// <summary>
        /// Constructor using an auth SSL smtp server
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public EndPointAOCEfact(String hostname, int port, String username, String password)
            : base(hostname, port, username, password)
        {
        }
        /// <summary>
        /// Prepare message to send invoice to eFACT
        /// </summary>
        /// <param name="from">from address</param>
        /// <param name="env">Environment (env_STAGE or env_PROD)</param>
        public void createMessage(String from, String env)
        {
            createMessage(from, env, "Factura pel eFACT", "");
        }
    }
}
