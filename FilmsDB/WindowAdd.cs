//@author Рычков Р.В.
using System; // Подключаем системную библиотеку
using System.Windows.Forms; // Для работы с формами и элементами интерфейса

namespace FilmsDB // Пространство имен FilmsDB
{
    // Класс окна добавления фильма
    public partial class WindowAdd : Form 
    {
        private DBFilm db; // Ссылка на базу данных фильмов

        // Конструктор окна добавления
        public WindowAdd(DBFilm database) 
        {
            InitializeComponent(); // Инициализация элементов интерфейса
            db = database; // Инициализация базы данных
        }

        // Обработчи кнопки "Добавить"
        private void button1_Click(object sender, EventArgs e) 
        {
            // Проверяем заполнение всех полей
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && 
                !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                try 
                {
                    string title = textBox1.Text; // Сохраняем название
                    string genre = textBox2.Text; // Сохраняем жанр
                    int year = int.Parse(textBox3.Text); // Преобразуем год в число
                    string director = textBox4.Text; // Сохраняем режиссёра
                    double rating = double.Parse(textBox5.Text); // Преобразуем рейтинг в число

                    Film newFilm = new Film(title, genre, year, director, rating); // Создаем новый фильм
                    db.AddFilm(newFilm); // Добавляем фильм в базу
                    this.Close(); // Закрываем окно после добавления
                }
                catch (Exception ex) // Ловим какой-либо крит
                {
                    MessageBox.Show("Ошибка при добавлении фильма: " + ex.Message); // Выводим сообщение об ошибке
                }
            }
            else // Если не все поля заполнены
            {
                MessageBox.Show("Заполните все поля!"); // Показываем предупреждение
            }
        }

        //Обработчик кнопки "Отмена"
        private void button2_Click(object sender, EventArgs e) 
        {
            this.Close(); // Закрываем окно без добавления
        }
    }
}
