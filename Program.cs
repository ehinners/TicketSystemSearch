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

            //////////////////////////////
            //      NLOG Instantiation  //
            //////////////////////////////

            NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

            logger.Info("NLOG Loaded");

            Model.setLogger(logger);
            Model.setFileName("movies.scrubbed.csv");
            Controller.mainLoop();
            
            
        }
    }
}
