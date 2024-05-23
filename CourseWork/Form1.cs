﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tielnov_Group_Course_project.CourseWork;

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
                
            } 
            else {
                tbInput.Enabled = false;
                tClock.Stop();
                bStart.Text = "Пуск";
                this.Mode = true;
                MajorObject.Write(tbInput.Text);// Запис даних у об'єкт
                MajorObject.Task();// Обробка даних
                label1.Text = MajorObject.Read();// Відображення результату
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
    }
}
