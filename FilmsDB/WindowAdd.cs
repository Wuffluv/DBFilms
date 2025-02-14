using System;
using System.Windows.Forms;

namespace FilmsDB
{
    public partial class WindowAdd : Form
    {
        private DBFilm db; // Ссылка на основную базу данных

        public WindowAdd(DBFilm database) // Передаем существующую базу данных
        {
            InitializeComponent();
            db = database;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                try
                {
                    string title = textBox1.Text;
                    string genre = textBox2.Text;
                    int year = int.Parse(textBox3.Text);
                    string director = textBox4.Text;
                    double rating = double.Parse(textBox5.Text);

                    Film newFilm = new Film(title, genre, year, director, rating);
                    db.AddFilm(newFilm); // Добавляем в существующую базу

                    this.Close(); // Закрываем форму после добавления
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении фильма: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // Просто закрывает форму без добавления
        }
    }
}
