using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace FilmsDB
{
    public partial class MainWindow : Form
    {
        private DBFilm dbFilm = new DBFilm();

        public MainWindow()
        {
            InitializeComponent();
            dataGridView1.DataSource = dbFilm.GetBindingList(); // Привязываем BindingList
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dbFilm.LoadFromFile(openFileDialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (WindowAdd windowAdd = new WindowAdd(dbFilm))
            {
                if (windowAdd.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.Refresh(); // Обновляем отображение данных
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dbFilm.ClearAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                dbFilm.RemoveFilm(selectedIndex);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                dbFilm.SaveToFile(saveFileDialog.FileName);
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: [Ваше Имя]\nВерсия 1.0", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
