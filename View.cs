using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MediaLibrary
{  
    public static class View
    {

        // this class handles all output to the user

        private static ArrayList menuDisplay = new ArrayList()
        {
            "1: To Create A New Ticket", 
            "2: To View All Current Tickets", 
            "Enter to quit"
        };

        private static ArrayList typeDisplay = new ArrayList()
        {
            "1: Bug/Defect Ticket", 
            "2: Enhancement Ticket", 
            "3: Task Ticket"
        };

        private static ArrayList promptDisplay = new ArrayList()
        {
            "Enter " , 
            "Enter (or done to quit)", 
            "Enter ",
            "Enter "
        };


        public static void setMenuDisplay(ArrayList md)
        {
            menuDisplay = md;
        }

        public static void displayMenu()
        {
            foreach(string line in menuDisplay)
            {
                System.Console.WriteLine(line);
            }
        }

        public static void displayTypes()
        {
            foreach(string line in typeDisplay)
            {
                System.Console.WriteLine(line);
            }
        }

        /*
        public static void displayMovies()
        {
            List<Movie> movies = Model.getMovies();
            foreach(Movie m in movies)
            {
                System.Console.WriteLine(m.Display());           
            }
        } */

        public static void creationPrompt(int movieAttribute)
        {
            System.Console.WriteLine(promptDisplay[movieAttribute-1]);
        }

    }
}