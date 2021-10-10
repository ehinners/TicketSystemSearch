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
        private static NLog.Logger logger;
        private static List<string> fileContents;
        private static List<Movie> movies;
        private static string fileName;

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
                movies = MovieService.mapMoviesFromStringList(fileContents);
            }
            return movies;            
        }

        

        

    }


}