using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;

namespace eFacturesCat.Commons
{
    /// <summary>
    /// Utils class for eFacturesCat API
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Copy inputstrem to outputstream
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        /// <summary>
        /// Copy stream to byte array
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="initialLength"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream stream, int initialLength)
        {
            if (initialLength < 1)
            {
                initialLength = 51200;
            }

            byte[] buffer = new byte[initialLength];
            byte[] bufferAux = new byte[51200];
            int read = 0;
            int chunk = 0;
            chunk = stream.Read(bufferAux, 0, 51200);

            while (chunk > 0)
            {
                if (read + chunk >= buffer.Length)
                {
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    buffer = newBuffer;
                    Array.Copy(bufferAux, 0, buffer, read, chunk);
                    read += chunk;
                }
                else
                {
                    Array.Copy(bufferAux, 0, buffer, read, chunk);
                    read += chunk;
                }
                chunk = stream.Read(bufferAux, 0, 51200);
            }
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }
    }
}
