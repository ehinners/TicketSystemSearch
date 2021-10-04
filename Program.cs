using System;
using System.IO;
using NLog.Web;
using System.Collections.Generic;

namespace MediaLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("");

            //////////////////////////////
            //      NLOG Instantiation  //
            //////////////////////////////

            string path = Directory.GetCurrentDirectory() + "\\nlog.config";

            // create instance of Logger
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            logger.Info("NLOG Loaded");
            Console.WriteLine("Hello World!");
        }
    }
}
