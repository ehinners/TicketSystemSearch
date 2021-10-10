using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MediaLibrary
{  
    public static class FileHandler
    {
        private static NLog.Logger logger;
        
        private static List<string> fileContents = new List<string>();

        public static void setLogger(NLog.Logger l)
        {
            logger = l;
        }

        public static List<string> getFileContents(string readFile)
        {
            List<string> fileContents = new List<string>();
            try
            {                
                if (File.Exists(readFile))
                {
                    // open read file
                    StreamReader sr = new StreamReader(readFile);                    
                    
                    while (!sr.EndOfStream)
                    {
                        fileContents.Add(sr.ReadLine());                                          
                    }
                    sr.Close();
                }                
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return fileContents;
        }
    }
}