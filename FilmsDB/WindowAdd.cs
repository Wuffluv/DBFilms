//@Author Рычков Р.В.
using System; // Подключаем системную библиотеку
using System.Windows.Forms; // Для работы с формами и элементами интерфейса
using System.Drawing; //Подключаем для работы с цветом при выделении ошибочного элемента в textbox

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

           //разобартсья с лямбдой функцией
            // Убираем красную рамку, если текст изменился
            textBox3.TextChanged += (s, e) => textBox3.BackColor = SystemColors.Window;//Поле года
            textBox5.TextChanged += (s, e) => textBox5.BackColor = SystemColors.Window;//поле рейтинга
        }

        // Обработчик кнопки "Добавить"
        private void button1_Click(object sender, EventArgs e)
        {
            bool isValid = true; //флаг пдля проверки правильности вводимых данных

            // Проверяем поле год – должно быть числом
            if (!int.TryParse(textBox3.Text, out _))
            {
                textBox3.BackColor = System.Drawing.Color.LightCoral; // Красная подсветка
                isValid = false;
            }

            // Проверяем поле "Рейтинг", указывать надо с плавающей точкой
            if (!double.TryParse(textBox5.Text, out _))
            {
                textBox5.BackColor = System.Drawing.Color.LightCoral; // Красная подсветка
                isValid = false;
            }

            // Если проврка не прошла, выходим
            if (!isValid) return;

            // Проверяем заполнение всех остальных полей
            if (!string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                try


                {
                    string title = textBox1.Text; // Сохраняем название
                    string genre = textBox2.Text; // Сохраняем жанря
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
