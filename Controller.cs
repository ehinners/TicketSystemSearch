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
        private static ArrayList options = new ArrayList();

        public static void setLogger(NLog.Logger l)
        {
            logger = l;
        }

        public static void mainLoop()
        {
            string esc = "done";
            string input = "ready";



            while(input!=esc)
            {
                System.Console.WriteLine("Displaying Menu");
                System.Console.WriteLine("Tell User What To Enter");
                System.Console.WriteLine("Tell User What Exits (done)");
                input = Console.ReadLine();

                foreach(string s in options)
                {
                    if(s==input)
                    {
                        System.Console.WriteLine("Doing Action");
                    }
                }


            }
        }

    }


}