//@author Рычков Р.В.
using System; // Подключаем стандартную библиотеку
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic; // Подключаем коллекции (List)
using System.IO; // Для работы с файлами

namespace FilmsDB // Пространство имен FilmsDB
{
    // Класс базы данных фильмов
    public class DBFilm 
    {
        private List<Film> films = new List<Film>(); // Список фильмов

        // Метод добавления фильма
        public void AddFilm(Film film) 
        {
            films.Add(film); // Добавляем фильм в список
        }

        // Метод получения всех фильмов
        public List<Film> GetFilms() 
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
                    //какой тип переменной parts
                    var parts = line.Split('|'); // Разделяем строку, возвращает.....todo
                    if (parts.Length == 5) // Проверяем правильность формата. У нас 5 столбцов
                    {
                        string title = parts[0]; // Сохраняем название
                        string genre = parts[1]; // Сохраняем жанр
                        int year = int.Parse(parts[2]); // Сохраняем год
                        string director = parts[3]; // Сохраняем режиссёра
                        double rating = double.Parse(parts[4]); // Сохраняем рейтинг
                        films.Add(new Film(title, genre, year, director, rating)); // Добавляем фильм в список
                    }
                }
            }
        }
    }
}
