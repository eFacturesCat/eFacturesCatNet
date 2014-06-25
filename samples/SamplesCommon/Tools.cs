using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SamplesCommon
{
    public static class Tools
    {
		private static String BaseDir;
        public static String baseDir { get { return BaseDir; } set { BaseDir = value; } }

        /// <summary>
        /// Move processed invoices to processed folder
        /// </summary>
        /// <param name="fileName"></param>
        public static void InvoiceProcessed(string fileName)
        {
            Console.WriteLine(fileName + " processed!");
            Console.WriteLine("");
            String tmpDir = baseDir + "processed";
            if (!Directory.Exists(tmpDir))
                Directory.CreateDirectory(tmpDir);
            if (File.Exists(tmpDir + "/" + Path.GetFileName(fileName)))
                File.Delete(tmpDir + "/" + Path.GetFileName(fileName));
            File.Move(fileName, tmpDir + "/" + Path.GetFileName(fileName));
        }

        /// <summary>
        /// Move non-processed invoice to not_processed folder
        /// </summary>
        /// <param name="fileName"></param>
        public static void InvoiceNotProcessed(string fileName)
        {
            Console.WriteLine("ERROR:" + fileName + " NOT processed!");
            Console.WriteLine("");

            String tmpDir = baseDir + "not_processed";
            if (!Directory.Exists(tmpDir))
                Directory.CreateDirectory(tmpDir);
            if (File.Exists(tmpDir + "/" + Path.GetFileName(fileName)))
                File.Delete(tmpDir + "/" + Path.GetFileName(fileName));
            File.Move(fileName, tmpDir + "/" + Path.GetFileName(fileName));
        }

        /// <summary>
        /// Get signed path (baseDir + signed) for signed invoice file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string getSignedFileName(string fileName)
        {
            String tmpDir = baseDir + "signed";
            if (!Directory.Exists(tmpDir))
                Directory.CreateDirectory(tmpDir);
            String signedFileName = tmpDir + "/" + Path.GetFileName(fileName);
            Console.WriteLine("Saving signed " + signedFileName);
            return signedFileName;
        }

    }
}
