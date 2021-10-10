using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MediaLibrary
{  
    public static class Controller
    {
        private static NLog.Logger logger;
        private static ArrayList options = new ArrayList()
        {
            "1","2"
        };

        public static void setLogger(NLog.Logger l)
        {
            logger = l;
        }

        public static void mainLoop()
        {
            string esc = "done";
            string input = "ready";

            bool keepLoop = true;

            while(keepLoop)
            {
                View.displayMenu();
                input = Console.ReadLine();

                keepLoop = false;
                foreach(string s in options)
                {
                    if(s==input)
                    {
                        keepLoop = true;
                        System.Console.WriteLine("Doing Action");
                    }                    
                }
            }
        }

    }


}