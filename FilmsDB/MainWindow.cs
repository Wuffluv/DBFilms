//@author Рычков Р.В.

//MVC - Model View Controller 
// Модель - компонент, отвечающий за данные, а также определяющий саму структуру приложения
// View - представление, отвечает за взаимодействие с пользователем
// Контроллер - компонент, отвечающий за связь между моделью и представлением, определяет как приложение
// реагирует на действия пользователя внутри приложения

// Представление отвечает за отображение данных и взаимодействие с пользователем.
// Контроллер - MainWindow

// создать экземпляр mainwindow не создавая экземпляр DBFilm ?

//Агрегация -  это отношение между объектами, при котором один объект содержит другой как часть,
//но внешний объект не является владельцем внутреннего


using System; // Подключаем системную библиотеку
using System.IO; // Работа с файлами
using System.Windows.Forms; // Элементы интерфейса

namespace FilmsDB // Пространство имен FilmsDB
{
    // Главная форма приложения
    public partial class MainWindow : Form
    {
        private DBFilm dbFilm; // Объявляем поле без автоматической инициализации

        // Конструктор с необязательным параметром DBFilm
        public MainWindow(DBFilm dbFilm = null)
        {
            this.dbFilm = dbFilm; // Присваиваем переданный экземпляр полю
            InitializeComponent(); // Инициализация интерфейса
            if (dbFilm != null) // Проверяем, передан ли dbFilm
            {
                dataGridView1.DataSource = dbFilm.GetFilms(); // Подключаем данные к таблице
            }
            SetColumnHeaders(); // Устанавливаем заголовки
        }

        // Обработчик кнопки "Открыть"
        private void button1_Click(object sender, EventArgs e)
        {
            if (dbFilm == null) return; // Если dbFilm не инициализирован, выходим
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Окно выбора файла
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Фильтр файлов
            if (openFileDialog.ShowDialog() == DialogResult.OK) // Если файл выбран
            {
                dbFilm.LoadFromFile(openFileDialog.FileName); // Загружаем базу из файла
            }
        }

        // Обработчик кнопки "Добавить"
        //Это зависимость ?
        //Агрегация - один воалаеет другим но они могут сущ независимо. Взяв в пример mainwindow хранил dbfilm
        //как поле но DBFilm может сущ отдельно если передать его в др объект
        // Зависиомть - 1 объект использует другой для вып. задачи но не владеет им постоянно.
        //MainWindow создает windowadd и передает ему данные и плказывает его но не хранит как часть себя

        private void button2_Click(object sender, EventArgs e)
        {
            if (dbFilm == null) return; // Если dbFilm не инициализирован, выходим
            WindowAdd windowAdd = new WindowAdd(dbFilm); // Создаем окно добавления
            windowAdd.ShowDialog(); // Показываем окно
        }

        // Обработчик кнопки "Удалить всё"
        private void button3_Click(object sender, EventArgs e)
        {
            if (dbFilm == null) return; // Если dbFilm не инициализирован, выходим
            dbFilm.ClearAll(); // Очищаем всю базу
        }

        // Обработчик кнопки "Удалить выбранное"
        private void button4_Click(object sender, EventArgs e)
        {
            if (dbFilm == null || dataGridView1.SelectedRows.Count == 0) return; // Проверяем dbFilm и выбранную строку
            int selectedIndex = dataGridView1.SelectedRows[0].Index; // Получаем индекс выбранной строки
            dbFilm.RemoveFilm(selectedIndex); // Удаляем фильм по индексу
        }

        // Обработчик "Сохранить"
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dbFilm == null) return; // Если dbFilm не инициализирован, выходим
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

        // Установка заголовков колонок
        public void SetColumnHeaders()
        {
            if (dataGridView1.Columns.Count > 0) // Проверяем, есть ли колонки
            {
                dataGridView1.Columns["Title"].HeaderText = "Название";
                dataGridView1.Columns["Genre"].HeaderText = "Жанр";
                dataGridView1.Columns["Year"].HeaderText = "Год";
                dataGridView1.Columns["Director"].HeaderText = "Режиссер";
                dataGridView1.Columns["Rating"].HeaderText = "Рейтинг";
            }
        }
    }
}