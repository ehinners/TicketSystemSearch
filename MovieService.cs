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
        // static utilities related to movie objects
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

        // user input should not generate movie ids
        public static Movie mapMovieFromCSVGenerateID(string csv)
        {
            UInt64 newID = Model.getLargestID() + 1;
            string newCsv =  newID.ToString();
            newCsv += ",";
            newCsv += csv;
            System.Console.WriteLine(newCsv);
            return mapMovieFromCSV(newCsv);
        }

        public static string movieToCSV(Movie movie)
        {
            bool notFirstGenre = false;
            string filter;
            //164979,"Women of '69, Unboxed",Documentary,unassigned,00:00:00
            string csv = "";
            filter = movie.mediaId.ToString();
            csv += filter;
            csv += ",";
            filter = movie.title;
            csv += filter;
            csv += ",";

            foreach(string genre in movie.genres)
            {
                if(notFirstGenre)
                {
                    csv+="|";
                }
                filter = genre;
                csv += filter;
                notFirstGenre = true;
            }

            csv += ",";
            filter = movie.director;
            csv += filter;
            csv += ",";
            filter = movie.runningTime.ToString();
            csv += filter;
            System.Console.WriteLine(csv);
            return csv;
        }


        // turns csvs to movie objects
        // includes some intricacies to potentially combat future changes to 
        // additional movie file attributes
        // handles commas found in movie titles by counting number of
        // items the string Splits to and comparing it to a 
        // dynamic measure of how many attributes are found in the media
        // display method
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

        // returns true if entered name is NOT unique
        public static bool findNameRepeats(string movieName)
        {
            bool nameFound = false;
            foreach(Movie m in Model.getMovies())
            {
                if(m.title == movieName)
                {
                    nameFound = true;
                }
            }

            return nameFound;
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