using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TripleTicketType
{  
    public static class FileHandler
    {
        
        private static List<string> fileContents = new List<string>();

        // takes a file with given file name and returns its contents as a list of strings
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
                Model.getLogger().Error(ex.Message);
            }
            return fileContents;
        }

        // takes a file of the given name and appends the given string
        public static void addLineToFile(string writeFile, string newLine)
        {
            try
            {
                if (File.Exists(writeFile))
                {
                    StreamWriter sw;
                    sw = File.AppendText(writeFile);

                    sw.WriteLine(newLine);
                        
                    sw.Close(); // Saves the file 
                }                
            }
            catch (Exception ex)
            {
                Model.getLogger().Error(ex.Message);
            }
        }
    }
}