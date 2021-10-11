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
        private static ArrayList menuDisplay = new ArrayList()
        {
            "1) Add Movie" , 
            "2) Display All Movies", 
            "Enter to quit"
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

        public static void displayMovies()
        {
            List<Movie> movies = Model.getMovies();
            foreach(Movie m in movies)
            {
                System.Console.WriteLine(m.Display());           
            }
        }

    }
}