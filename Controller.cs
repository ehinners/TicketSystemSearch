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
                        optionSelector(input);
                    }                    
                }
            }
        }

        private static void optionSelector(string input)
        {
            
            if(input == "1")
            {
                Movie movie = MovieService.mapMovieFromCSVGenerateID(promptNewMovie());
                Model.addMovie(movie);
            }
            if(input == "2")
            {
                View.displayMovies();
            }
        }

        private static string promptNewMovie()
        {
            string selection;
            int movieAttribute = 1;
            string newCsv = "";
            bool verifiedRuntime = false;


            for(int i = 1; i < Model.getNumAttributes(); i++)
            {   
                selection = "ready";
                if(i == 4)
                {
                    while(!verifiedRuntime)
                    {
                        View.creationPrompt(i);
                        selection = System.Console.ReadLine();
                        TimeSpan runningTime;

                        try
                        {
                            runningTime = TimeSpan.Parse(selection);
                            verifiedRuntime = true;
                        }
                        catch
                        {
                            Model.getLogger().Error("Input Not Valid TimeSpan");
                            runningTime = new TimeSpan();
                        }
                    }
                }
                else
                {
                    View.creationPrompt(i);
                    if(i>1)
                    {
                        newCsv += ",";
                    }
                    newCsv += System.Console.ReadLine();
                    if(i==2)
                    {
                        while(selection.ToUpper() != "DONE")
                        {
                            View.creationPrompt(i);
                            selection = System.Console.ReadLine();
                            if(selection.ToUpper() != "DONE")
                            {
                                newCsv += "|";
                                newCsv += selection;
                            }
                        }
                    }
                }

                
            }

            return newCsv;
        }

    }


}