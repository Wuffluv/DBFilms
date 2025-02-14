using System;

namespace FilmsDB
{
    public class Film // изменено с internal на public
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public double Rating { get; set; }

        public Film(string title, string genre, int year, string director, double rating)
        {
            Title = title;
            Genre = genre;
            Year = year;
            Director = director;
            Rating = rating;
        }
    }
}
