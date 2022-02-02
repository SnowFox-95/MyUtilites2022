using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace MyUtilites
{
    public partial class MainForm : Form
    {
        int count = 0;
        Random rnd;
        char[] spec_chars = new char[] { '%', '"', ')', '?', '#', '$', '^', '&', '~' };
        Dictionary<string, double> metrica;
        public MainForm()
        {
            InitializeComponent();
            rnd = new Random();
            metrica = new Dictionary<string, double>();
            metrica.Add("mm", 1);
            metrica.Add("cm", 10);
            metrica.Add("dm", 100);
            metrica.Add("m", 1000);
            metrica.Add("km", 1000000);
            metrica.Add("mile", 1609344);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа Мои утилиты, \nсодержит ряд небольших программ, \nкоторые могут пригодиться в жизни. \n \n Автор abespyatykh", "О программе");
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            count++;
            lblCount.Text = count.ToString();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            count--;
            lblCount.Text = count.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = Convert.ToString(count);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            //добавлена проверка на случай, если в поле "От" будет установлено число больше чем в "До" -> они просто поменяются местами, чтобы не выдало ошибку:
            if (numericUpDown1.Value > numericUpDown2.Value)
            {
                var temp = numericUpDown1.Value;
                numericUpDown1.Value = numericUpDown2.Value;
                numericUpDown2.Value = temp;
            }
            int n;
            n = rnd.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
            lblRandom.Text = Convert.ToString(n);
            if (cbRndwithRepeat.Checked)
            {
                int i = 0;
                while (tbRandom.Text.IndexOf(n.ToString()) != -1)
                {
                    n = rnd.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
                    i++;
                    if (i > 1000) break;
                }
                if (i <= 1000) tbRandom.AppendText(n + Environment.NewLine);
            }
            else
            {
                tbRandom.AppendText(n + Environment.NewLine);
            }
        }

        private void btnRandomClear_Click(object sender, EventArgs e)
        {
            tbRandom.Clear();
        }

        private void btnRandomCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbRandom.Text);
        }

        private void tsmiInsertDate_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortDateString() + "\n");
        }

        private void tsmiInsertTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortTimeString() + "\n");
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try { rtbNotepad.SaveFile("notepad.rtf"); }
            catch { MessageBox.Show("Ошибка сохранения"); }
        }
        void LoadNotePad()
        {
            try { rtbNotepad.LoadFile("notepad.rtf"); }
            catch { MessageBox.Show("Ошибка загрузки файла блокнота. Файл не существует или не создан"); }
        }
        private void tsmiLoad_Click(object sender, EventArgs e)
        {
            LoadNotePad();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadNotePad();
            clbPassword.SetItemChecked(0, true);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCreatePass_Click(object sender, EventArgs e)
        {
            if (clbPassword.CheckedItems.Count == 0) return;
            string Password = "";
            for (int i = 0; i < nudPassLength.Value; i++)
            {
                int n = rnd.Next(0, clbPassword.CheckedItems.Count);
                string s = clbPassword.CheckedItems[n].ToString();
                switch (s)
                {
                    case "Цифры":
                        Password += rnd.Next(10).ToString();
                        break;
                    case "Прописные буквы":
                        Password += Convert.ToChar(rnd.Next(65, 88));
                        break;
                    case "Строчные буквы":
                        Password += Convert.ToChar(rnd.Next(97, 122));
                        break;
                    default:
                        Password += spec_chars[rnd.Next(spec_chars.Length)];
                        break;
                }
                tbPassword.Text = Password;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            double m1 = metrica[cbFrom.Text];
            double m2 = metrica[cbTo.Text];
            double n = Convert.ToDouble(tbFrom.Text);
            tbTo.Text = (n * m1 / m2).ToString();
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            string t = cbFrom.Text;
            cbFrom.Text = cbTo.Text;
            cbTo.Text = t;
        }

        private void cbMetric_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbMetric.Text)
            {
                case "Длина":
                    metrica.Clear();
                    metrica.Add("mm", 1);
                    metrica.Add("cm", 10);
                    metrica.Add("dm", 100);
                    metrica.Add("m", 1000);
                    metrica.Add("km", 1000000);
                    metrica.Add("mile", 1609344);
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("mm");
                    cbFrom.Items.Add("cm");
                    cbFrom.Items.Add("dm");
                    cbFrom.Items.Add("m");
                    cbFrom.Items.Add("km");
                    cbFrom.Items.Add("mile");
                    cbTo.Items.Clear();
                    cbTo.Items.Add("mm");
                    cbTo.Items.Add("cm");
                    cbTo.Items.Add("dm");
                    cbTo.Items.Add("m");
                    cbTo.Items.Add("km");
                    cbTo.Items.Add("mile");
                    cbFrom.Text = "mm";
                    cbTo.Text = "mm";
                    break;
                case "Вес":
                    metrica.Clear();
                    metrica.Add("g", 1);
                    metrica.Add("kg", 1000);
                    metrica.Add("t", 1000000);
                    metrica.Add("lb", 453.6);
                    metrica.Add("oz", 283);
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("g");
                    cbFrom.Items.Add("kg");
                    cbFrom.Items.Add("t");
                    cbFrom.Items.Add("lb");
                    cbFrom.Items.Add("oz");
                    cbTo.Items.Clear();
                    cbTo.Items.Add("g");
                    cbTo.Items.Add("kg");
                    cbTo.Items.Add("t");
                    cbTo.Items.Add("lb");
                    cbTo.Items.Add("oz");
                    cbFrom.Text = "g";
                    cbTo.Text = "g";
                    break;

            }
        }

        private void cbTo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
