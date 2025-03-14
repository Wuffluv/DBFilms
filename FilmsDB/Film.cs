//@author Рычков Р.В.

//Модель т.к у него есть конструктор для создания объекта фильма с заданными параметрами.  
//этот класс представляет собой структуру данных, то есть "кирпичик" модели.



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsDB // Пространство имен FilmsDB
{
    // Класс, описывающий фильм
    public class Film 
    {
        public string Title { get; set; } // Название фильма
        public string Genre { get; set; } //  Жанр фильма
        public int Year { get; set; } //Год выпуска
        public string Director { get; set; } //Режиссёр
        public double Rating { get; set; } // Рейтингя

        // Конструктор класса Film
        public Film(string title, string genre, int year, string director, double rating)
        {
            Title = title; // название
            Genre = genre; // жанр
            Year = year; //год
            Director = director; //режиссёр
            Rating = rating; //рейтинг
        }
    }
}
