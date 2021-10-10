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

        public static void displayMovies(List<Movie> movies)
        {
            foreach(Movie m in movies)
            {
                m.Display();
            }
        }

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

        public static Movie mapMovieFromCSV(string csv)
        {
            Movie movie = new Movie();

            string[] lines = movie.Display().Split("\n");
            int numAttributes = lines.Length - 1;

            string[] movieAttributes;
            movieAttributes = csv.Split(",");

            int movieAttribute = 1;
            bool excededAttributes;
            string titleStitcher = "";
            TimeSpan tempRunningTime;

            if(movieAttributes.Length > numAttributes)
            {
                excededAttributes = true;
            }
            else
            {
                excededAttributes = false;
            }

            
            foreach(string s in movieAttributes)
            {
                if(movieAttribute==1)
                {
                    try
                    {
                        movie.mediaId = (UInt16)int.Parse(s);
                    }
                    catch
                    {
                        Model.getLogger().Error("ID not Integer Value");
                    }
                    
                }
                if(movieAttribute==2)
                {
                    movie.title = s;
                }
                if(movieAttribute==3)
                {
                    if(excededAttributes)
                    {
                        titleStitcher = movie.title;
                        titleStitcher += s;
                        movie.title = titleStitcher;
                    }
                    else
                    {
                        movie.genres = genreSplitter(s);
                    }
                }
                if(movieAttribute==4)
                {
                    if(excededAttributes)
                    {
                        movie.genres = genreSplitter(s);
                    }
                    else
                    {
                        movie.director = s;
                    }
                }
                if(movieAttribute==5)
                {
                    if(excededAttributes)
                    {
                        movie.director = s;
                    }
                    else
                    {
                        movie.runningTime = spanParser(s);
                    }
                }
                if(movieAttribute==6)
                {
                    movie.runningTime = spanParser(s);
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