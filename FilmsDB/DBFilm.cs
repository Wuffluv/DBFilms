using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace FilmsDB
{
    public class DBFilm
    {
        private BindingList<Film> films = new BindingList<Film>(); // Используем BindingList

        public void AddFilm(Film film)
        {
            films.Add(film);
        }

        public BindingList<Film> GetBindingList()
        {
            return films;
        }

        public void RemoveFilm(int index)
        {
            if (index >= 0 && index < films.Count)
            {
                films.RemoveAt(index);
            }
        }

        public void ClearAll()
        {
            films.Clear();
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var film in films)
                {
                    writer.WriteLine($"{film.Title}|{film.Genre}|{film.Year}|{film.Director}|{film.Rating}");
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                films.Clear();
                foreach (var line in File.ReadLines(filePath))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        string title = parts[0];
                        string genre = parts[1];
                        int year = int.Parse(parts[2]);
                        string director = parts[3];
                        double rating = double.Parse(parts[4]);
                        films.Add(new Film(title, genre, year, director, rating));
                    }
                }
            }
        }
    }
}
