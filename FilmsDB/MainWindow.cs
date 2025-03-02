//@author Рычков Р.В.

//MVC - Model View Controller 
// Модель - компонент, отвечающий за данные, а также определяющий саму структуру приложения
// View - представление, отвечает за взаимодействие с пользователем
// Контроллер - компонент отвечающий за связь между моделью и представлением, определяет как приложение
// реагирует на действия пользователя внутри приложения

//Представление

using System; // Подключаем системную библиотеку
using System.IO; // Работа с файлами
using System.Windows.Forms; // Элементы интерфейса

namespace FilmsDB // Пространство имен FilmsDB
{
    // Главная форма приложения

    //Агрегация
    //- это когда один объект является частью другого объекта, но при этом оба объекта могут
    //существовать независимо друг от друга

    //Экземпляр DBFilm создается внутри MainWindow и хранится в приватном поле dbFilm.
    //Это агрегация, так как MainWindow владеет ссылкой на DBFilm,
    //но жизненный цикл DBFilm не обязательно жестко привязан к MainWindow (например, он может быть передан другому объекту).


    public partial class MainWindow : Form
    {
        private DBFilm dbFilm = new DBFilm(); // Создаем объект базы данных

        public MainWindow() // Конструктор главного окна
        {
            InitializeComponent(); // Инициализация интерфейса
            dataGridView1.DataSource = dbFilm.GetFilms(); // Подключаем BindingList к таблице
            SetColumnHeaders(); // Устанавливаем заголовки
        }


        
        // Обработчик кнопки "Открыть"
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Окно выбора файла
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Фильтр файлов
            if (openFileDialog.ShowDialog() == DialogResult.OK) // Если файл выбран
            {
                dbFilm.LoadFromFile(openFileDialog.FileName); // Загружаем базу из файла
            }
        }

        // Обработчик кнопки "Добавить"

        //Зависимость
        //класс MainWindow создает объект WindowAdd, передавая ему ссылку на dbFilm.
        //Это демонстрирует зависимость, так как WindowAdd не может быть создан без участия MainWindow,
        //который инициирует его появление.
        private void button2_Click(object sender, EventArgs e)
        {
            WindowAdd windowAdd = new WindowAdd(dbFilm); // Создаем окно добавления
            windowAdd.ShowDialog(); // Показываем окно
        }

        // Обработчик кнопки "Удалить всё"
        private void button3_Click(object sender, EventArgs e)
        {
            dbFilm.ClearAll(); // Очищаем всю базу
        }

        // Обработчик кнопки "Удалить выбранное"
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, выбрана ли строка
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index; // Получаем индекс выбранной строки
                dbFilm.RemoveFilm(selectedIndex); // Удаляем фильм по индексу
            }
        }

        // Обработчик "Сохранить"
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog(); // Диалог сохранения файла
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Фильтр
            if (saveFileDialog.ShowDialog() == DialogResult.OK) // Если выбран файл
            {
                dbFilm.SaveToFile(saveFileDialog.FileName); // Сохраняем базу в файл
            }
        }

        // Обработчик кнопки "О программе"
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Рычков Р.В.\nСтудент группы ВМК-21", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information); // Информация об авторе
        }

        // Обработчик "Открыть"
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e); // Вызываем метод открытия файла
        }

        //Колонки
        public void SetColumnHeaders()
        {
            dataGridView1.Columns["Title"].HeaderText = "Название"; 
            dataGridView1.Columns["Genre"].HeaderText = "Жанр";
            dataGridView1.Columns["Year"].HeaderText = "Год";
            dataGridView1.Columns["Director"].HeaderText = "Режиссер";
            dataGridView1.Columns["Rating"].HeaderText = "Рейтинг";
        }

    }
}
