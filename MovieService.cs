using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;


namespace MediaLibrary
{  
    public static class MovieService
    {
        
        //Model.getLogger()

        public static List<Movie> mapMoviesFromStringList(List<string> contents)
        {
            Movie tempMovie = new Movie();
            List<Movie> movies = new List<Movie>();
            foreach(string s in contents)
            {
                tempMovie = mapMovieFromCSV(s);
                movies.Add(tempMovie);
            }
            return movies;
        }

        public static Movie mapMovieFromCSVGenerateID(string csv)
        {
            UInt64 newID = Model.getLargestID() + 1;
            string newCsv =  newID.ToString();
            newCsv += ",";
            newCsv += csv;
            System.Console.WriteLine(newCsv);
            return mapMovieFromCSV(newCsv);
        }

        

        public static Movie mapMovieFromCSV(string csv)
        {
            Movie movie = new Movie();
            int numAttributes = Model.getNumAttributes();

            string[] movieAttributes;
            movieAttributes = csv.Split(",");

            int futureModifier = numAttributes -5;

            int genrePos = (movieAttributes.Length) -3 + futureModifier;
            int directorPos = (movieAttributes.Length) -2 + futureModifier;
            int runningTimePos = (movieAttributes.Length) -1 + futureModifier;

            

            int movieAttribute = 1;
            bool excededAttributes = false;
            string titleStitcher = "";
            TimeSpan tempRunningTime;

            


            try
            {
                movie.mediaId = (UInt16)int.Parse(movieAttributes[0]);
            }
            catch
            {
                Model.getLogger().Error("ID not Integer Value");
            }


            movie.runningTime = spanParser(movieAttributes[runningTimePos]);
            movie.director = movieAttributes[directorPos];
            movie.genres = genreSplitter(movieAttributes[genrePos]);

            if(movieAttributes.Length > numAttributes)
            {
                excededAttributes = true;
            }
            else
            {
                excededAttributes = false;
            }

            movieAttribute = 1;

            
            foreach(string s in movieAttributes)
            {                
                if(movieAttribute==2)
                {
                    movie.title = s;            
                    titleStitcher = movie.title;       
                }
                else if(movieAttribute-1 < (genrePos))
                {
                    if(excededAttributes)
                    {   
                        titleStitcher += ", ";
                        titleStitcher += s;
                        movie.title = titleStitcher;     
                    }                    
                }          
                movieAttribute++;
            }
            return movie;
        }

        private static List<string> genreSplitter(string genreCSV)
        {
            List<string> genres;
            genres = genreCSV.Split("|").ToList();
            return genres;
        }


        private static TimeSpan spanParser(string ts)
        {
            TimeSpan runningTime;

            try
            {
                runningTime = TimeSpan.Parse(ts);
                
            }
            catch
            {
                Model.getLogger().Error("Running Time Not Valid TimeSpan");
                runningTime = new TimeSpan();
            }

            return runningTime;

        }

    }
}