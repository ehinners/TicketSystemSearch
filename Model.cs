using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MediaLibrary
{  
    public static class Model
    {
        // This class allows controller to communicate with services and views

        // static instance of logger held so multiple instances don't have to be created
        private static NLog.Logger logger;

        // static instance of file contents so multiple instances don't have to be created
        private static List<string> fileContents;

        // static instance of movie list so multiple instances don't have to be created
        private static List<Movie> movies;


        public static string fileName;

        public static void setLogger(NLog.Logger l)
        {
            logger = l;
        }

        public static NLog.Logger getLogger()
        {
            return logger;
        }

        public static void setFileName(string fn)
        {
            fileName = fn;
        }

        public static List<string> getFileContents()
        {
            if(fileContents == null)
            {
                fileContents = FileHandler.getFileContents(fileName);
            }
            return fileContents;            
        }

        public static List<Movie> getMovies()
        {
            if(movies == null)
            {
                movies = MovieService.mapMoviesFromStringList(getFileContents());
            }
            return movies;            
        }

        // elminates redundancies in code requiring this value
        public static UInt64 getLargestID()
        {
            
            UInt64 largestID = 0;
            foreach(Movie m in getMovies())
            {
                if(m.mediaId > largestID)
                {
                    largestID = m.mediaId;
                }
            }
            return largestID;
        }

        // useful in case number of attributes ever changes
        public static int getNumAttributes()
        {
            Movie movie = new Movie();
            string[] lines = movie.Display().Split("\n");
            return lines.Length - 1;
        }

        public static void addMovie(Movie movie)
        {
            getMovies().Add(movie);
        }

        

        

    }


}