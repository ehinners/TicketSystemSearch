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
        // Holds all valid inputs for main menu. If anything other than
        // what is listed here is input from the user, the program will end
        private static ArrayList options = new ArrayList()
        {
            "1","2"
        };

        private static string genreEscape = "DONE";

        //Model.getLogger()

        // Displays menu
        // Takes User Input
        // Repeats 
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

        // Delegates to other methods based on user input
        private static void optionSelector(string input)
        {
            Model.getLogger().Info($"User Choice: \"{input}\"");
            if(input == "1")
            {
                //addMovie();
            }
            if(input == "2")
            {
                //View.displayMovies();
            }
        }

        //creates a csv string through user prompt
        //preppends a generated mediaID to the csv
        //adds csv to file
        //maps csv  to a movie object
        //adds movie object to list of movies found in model
        //logs the addition

        /*
        private static void addMovie()
        {
            string csv = promptNewMovie();
            Movie movie = MovieService.mapMovieFromCSVGenerateID(csv);
            Model.addMovie(movie);
            FileHandler.addLineToFile(Model.fileName, MovieService.movieToCSV(movie));
            UInt64 largestID = Model.getLargestID();
            Model.getLogger().Info($"Media id {largestID} added");
        }*/

        // gets the prompt from the view
        // builds a csv along with user input
        // loops the input for genres until user types "done"
        // loops input for runtime until it parses to a TimeSpan object without exception

        /*
        private static string promptNewMovie()
        {
            string selection;
            int movieAttribute = 1;
            string newCsv = "";
            bool verifiedRuntime = false;
            bool verifiedUniqueName = false;

            for(int i = 1; i < Model.getNumAttributes(); i++)
            {   
                selection = "ready";
                if(i ==1)
                {
                    while(!verifiedUniqueName)
                    {
                        View.creationPrompt(i);
                        selection = System.Console.ReadLine();                        
                        verifiedUniqueName = !MovieService.findNameRepeats(selection);
                        if(verifiedUniqueName)
                        {
                            newCsv += selection;
                        }
                        else
                        {
                            Model.getLogger().Error("Input Not Unique Movie Title");
                        }                                                
                    }
                }
                else if(i == 4)
                {
                    while(!verifiedRuntime)
                    {
                        View.creationPrompt(i);
                        selection = System.Console.ReadLine();
                        

                        try
                        {
                            TimeSpan.Parse(selection);
                            verifiedRuntime = true;
                        }
                        catch
                        {
                            Model.getLogger().Error("Input Not Valid TimeSpan");
                        }
                    }
                    newCsv += ",";
                    newCsv += selection;
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
                        while(selection.ToUpper() != genreEscape)
                        {
                            View.creationPrompt(i);
                            selection = System.Console.ReadLine();
                            if(selection.ToUpper() != genreEscape)
                            {
                                newCsv += "|";
                                newCsv += selection;
                            }
                        }
                    }
                }

                
            }

            return newCsv;
        }*/

    }


}