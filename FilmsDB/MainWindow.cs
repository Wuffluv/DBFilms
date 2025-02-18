//@author Рычков Р.В.
using System; // Подключаем системную библиотеку
using System.Collections.Generic; // Коллекции
using System.IO; // Работа с файлами
using System.Windows.Forms; // Элементы интерфейса

//использовать BindingList

namespace FilmsDB // Пространство имен FilmsDB
{
    // Главная форма приложения
    public partial class MainWindow : Form 
    {
        private DBFilm dbFilm = new DBFilm(); // Создаем объект базы данных

        public MainWindow() // Конструктор главного окна
        {
            InitializeComponent(); // Инициализация интерфейса
        }

        // Обработчик кнопки "Открыть"
        private void button1_Click(object sender, EventArgs e) 
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // окно выбора файла
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Фильтр файлов
            if (openFileDialog.ShowDialog() == DialogResult.OK) // Если файл выбран
            {
                dbFilm.LoadFromFile(openFileDialog.FileName); // Загружаем базу из файла
                UpdateDataGridView(); // Обновляем таблицу
            }
        }

        // Обрабочтик кнопки "Добавить"
        private void button2_Click(object sender, EventArgs e) 
        {
            WindowAdd windowAdd = new WindowAdd(dbFilm); // Создаем окно добавления
            windowAdd.ShowDialog(); // Показываем окно
            UpdateDataGridView(); // Обновляем таблицу после добавления
        }

        // Обработчик нкнопик"Удалить всё"
        private void button3_Click(object sender, EventArgs e) 
        {
            dbFilm.ClearAll(); // Очищаем всю базу
            UpdateDataGridView(); // Обновляем таблицу
        }

        // Обработчик кнопки "Удалить выбранное"
        private void button4_Click(object sender, EventArgs e) 
        {
            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, выбрана ли строка
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index; // Получаем индекс выбранной строки
                dbFilm.RemoveFilm(selectedIndex); // Удаляем фильм по индексу
                UpdateDataGridView(); // Обновляем таблицу
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
            MessageBox.Show("Автор: Рычков Р.В.\\nСтудент группы ВМК-21", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information); // Информация об авторе
        }

        // обработчик"Открыть"
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            button1_Click(sender, e); // Вызываем метод открытия файла
        }

        // Метод обновления DataGridView
        private void UpdateDataGridView() 
        {
            //необходимо, чтобы избежать ситуации, когда старые данные остаются в таблице, а новые не отображаются
            //корректно и поэтому мы используем это чтобы очистить инфу в DataGridView
            dataGridView1.DataSource = null; // Сбрасываем источник данных
            dataGridView1.DataSource = dbFilm.GetFilms(); // Подключаем обновлённый список

            // Устанавливаем заголовки колонок
            dataGridView1.Columns[0].HeaderText = "Название";
            dataGridView1.Columns[1].HeaderText = "Жанр";
            dataGridView1.Columns[2].HeaderText = "Год";
            dataGridView1.Columns[3].HeaderText = "Режиссёр";
            dataGridView1.Columns[4].HeaderText = "Рейтинг";
        }
    }
}
