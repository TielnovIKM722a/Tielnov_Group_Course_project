using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tielnov_Group_Course_project.CourseWork;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Tielnov_Group_Course_project
{
    public partial class Form1 : Form
    {
        private bool Mode;
        private MajorWork MajorObject; // Створення об'єкта класу MajorWork

        public Form1()
        {
            InitializeComponent();
            MajorObject = new MajorWork();
        }

        private void tClockTick(object sender, EventArgs e)
        {
            tClock.Stop();
            MessageBox.Show("Минуло 25 секунд", "Увага");
            tClock.Start();
        }

        private void Form1Load(object sender, EventArgs e)
        {
            this.Mode = true;
            MajorObject = new MajorWork();
            About A = new About(); // створення форми About
            A.tAbout.Start();
            A.ShowDialog(); // відображення діалогового вікна About
            MajorObject.SetTime();
        }

        private void bStartClick(object sender, EventArgs e)
        {
            if (Mode) {
                tbInput.Enabled = true;
                tbInput.Focus();
                tClock.Start();
                bStart.Text = "Стоп";
                this.Mode = false;
                пускToolStripMenuItem.Text = "Стоп";

            } 
            else {
                tbInput.Enabled = false;
                tClock.Stop();
                bStart.Text = "Пуск";
                this.Mode = true;
                MajorObject.Write(tbInput.Text);// Запис даних у об'єкт
                MajorObject.Task();// Обробка даних
                label1.Text = MajorObject.Read();// Відображення результату
                пускToolStripMenuItem.Text = "Старт";
            }
        }

        private void tbInputKeyPress(object sender, KeyPressEventArgs e)
        {
            tClock.Stop();
            tClock.Start();
            if ((e.KeyChar >= '0') & (e.KeyChar <= '9') | (e.KeyChar == (char)8)) {
                return;
            } 
            else {
                tClock.Stop();
                MessageBox.Show("Неправильний символ", "Помилка");
                tClock.Start();
                e.KeyChar = (char)0;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string s;
            s = (System.DateTime.Now - MajorObject.GetTime()).ToString();
            MessageBox.Show(s, "Час роботи програми"); // Виведення часу роботи програми і
                                                       // повідомлення "Час роботи програми"
                                                       // на екран
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void проПрограToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About A = new About();
            A.ShowDialog();
        }

        private void зберегтиЯкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdSave.ShowDialog() == DialogResult.OK)// Виклик діалогового вікна збереження
                                                        // файлу
{
                MessageBox.Show(sfdSave.FileName);
            }
        }

        private void відкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdOpen.ShowDialog() == DialogResult.OK) // Виклик діалогового вікна відкриття           файлу

{
                MessageBox.Show(ofdOpen.FileName);
            }
        }

        private void проНакопичувачіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] disks = System.IO.Directory.GetLogicalDrives(); // Строковий масив з  логічніх дисків
            string disk = "";
            for (int i = 0; i < disks.Length; i++)
            {
                try
                {
                    System.IO.DriveInfo D = new System.IO.DriveInfo(disks[i]);
                    disk += D.Name + "-" + (D.TotalSize/1073741824).ToString() + "-" + (D.TotalFreeSpace/1073741824).ToString()
                    + (char)13;// змінній присвоюється ім’я диска, загальна кількість місця и вільне
                               // місце на диску
                }
                catch
                {
                    disk += disks[i] + "- не готовий" + (char)13; // якщо пристрій не готовий,
                                                                   // то виведення на екран ім’я пристрою і повідомлення «не готовий»
}
            }

            MessageBox.Show(disk, "Накопичувачі");
        }
    }
}
