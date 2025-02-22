//@author Рычков Р.В.

//Модель 

using System; // Подключаем стандартную библиотеку
using System.IO; // Для работы с файлами
using System.ComponentModel; // Для BindingList

namespace FilmsDB // Пространство имен FilmsDB
{
    // Класс базы данных фильмов
    public class DBFilm
    {
        private BindingList<Film> films = new BindingList<Film>(); // BindingList вместо List для автообновления UI

        // Метод добавления фильма
        public void AddFilm(Film film)
        {
            films.Add(film); // Добавляем фильм в список
        }

        // Метод получения всех фильмов
        public BindingList<Film> GetFilms()
        {
            return films; // Возвращаем список фильмов
        }

        // Метод удаления фильма по индексу
        public void RemoveFilm(int index)
        {
            if (index >= 0 && index < films.Count) // Проверяем, что индекс в пределах
            {
                films.RemoveAt(index); // Удаляем фильм по индексу
            }
        }

        // Метод очистки всей базы
        public void ClearAll()
        {
            films.Clear(); // Очищаем список
        }

        // Метод сохранения в файл
        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath)) // Открываем поток записи
            {
                foreach (var film in films) // Перебираем все фильмы
                {
                    writer.WriteLine($"{film.Title}|{film.Genre}|{film.Year}|{film.Director}|{film.Rating}"); // Записываем в файл
                }
            }
        }

        // Метод загрузки из файла
        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath)) // Проверяем наличие файла
            {
                films.Clear(); // Очищаем список перед загрузкой
                foreach (var line in File.ReadLines(filePath)) // Читаем построчно
                {
                    var parts = line.Split('|'); // Разделяем строку
                    if (parts.Length == 5) // Проверяем правильность формата
                    {
                        string title = parts[0];
                        string genre = parts[1];
                        int year = int.Parse(parts[2]);
                        string director = parts[3];
                        double rating = double.Parse(parts[4]);
                        films.Add(new Film(title, genre, year, director, rating)); // Добавляем фильм в список
                    }
                }
            }
        }
    }
}
