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
        private static ArrayList options = new ArrayList()
        {
            "1","2"
        };

        //Model.getLogger()

        public static void mainLoop()
        {
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