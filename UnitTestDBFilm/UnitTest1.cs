using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FilmsDB; //Пространство имен FilmsDB

namespace UnitTestDBFilm
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Film_Creation()
        {
            // Тест создания объекта Film и проверки его свойств
            Film film = new Film("Дюна", "Фантастика", 2021, "Дени Вильнёв", 8.1);

            Assert.AreEqual("Дюна", film.Title, "Название фильма не совпадает с ожидаемым");
            Assert.AreEqual("Фантастика", film.Genre, "Жанр фильма не совпадает с ожидаемым");
            Assert.AreEqual(2021, film.Year, "Год выпуска не совпадает с ожидаемым");
            Assert.AreEqual("Дени Вильнёв", film.Director, "Режиссер не совпадает с ожидаемым");
            Assert.AreEqual(8.1, film.Rating, "Рейтинг не совпадает с ожидаемым");
        }

        [TestMethod]
        public void Test_DBFilm_AddFilm()
        {
            // Тест добавления фильма в DBFilm
            DBFilm db = new DBFilm();
            Film film = new Film("Бегущий по лезвию 2049", "Фантастика", 2017, "Дени Вильнёв", 8.0);

            db.AddFilm(film);
            var films = db.GetFilms();

            Assert.AreEqual(1, films.Count, "Количество фильмов в списке должно быть 1");
            Assert.AreEqual("Бегущий по лезвию 2049", films[0].Title, "Название добавленного фильма не совпадает");
        }

        [TestMethod]
        public void Test_DBFilm_RemoveFilm()
        {
            // Тест удаления фильма из DBFilm
            DBFilm db = new DBFilm();
            Film film1 = new Film("Трансформеры", "Экшен", 2019, "Майкл Бэй", 8.6);
            Film film2 = new Film("Мстители", "Фантастика", 2003, "Братья Руссо", 8.4);

            db.AddFilm(film1);
            db.AddFilm(film2);
            db.RemoveFilm(0); // Удаляем первый фильм

            var films = db.GetFilms();
            Assert.AreEqual(1, films.Count, "Количество фильмов после удаления должно быть 1");
            Assert.AreEqual("Трансформеры", films[0].Title, "Оставшийся фильм не тот, что ожидался");
        }

        [TestMethod]
        public void Test_DBFilm_ClearAll()
        {
            // Тест очистки всей базы
            DBFilm db = new DBFilm();
            db.AddFilm(new Film("Зеленая миля", "Драма", 1999, "Фрэнк Дарабонт", 8.6));
            db.AddFilm(new Film("Форрест Гамп", "Драма", 1994, "Роберт Земекис", 8.8));

            db.ClearAll();
            var films = db.GetFilms();

            Assert.AreEqual(0, films.Count, "Список фильмов должен быть пуст после очистки");
        }

        [TestMethod]
        public void Test_DBFilm_SaveAndLoad()
        {
            // Тест сохранения и загрузки данных из файла
            DBFilm db = new DBFilm();
            Film film = new Film("1+1", "Комедия", 2011, "Оливье Накаш", 8.5);
            db.AddFilm(film);

            string testFilePath = "test_films.txt";
            db.SaveToFile(testFilePath);

            // Создаем новый экземпляр DBFilm и загружаем данные
            DBFilm dbLoaded = new DBFilm();
            dbLoaded.LoadFromFile(testFilePath);

            var films = dbLoaded.GetFilms();
            Assert.AreEqual(1, films.Count, "Количество загруженных фильмов должно быть 1");
            Assert.AreEqual("1+1", films[0].Title, "Название загруженного фильма не совпадает");
            Assert.AreEqual(8.5, films[0].Rating, "Рейтинг загруженного фильма не совпадает");

            // Очистка: удаляем тестовый файл после выполнения теста
            if (System.IO.File.Exists(testFilePath))
            {
                System.IO.File.Delete(testFilePath);
            }
        }
    }
}