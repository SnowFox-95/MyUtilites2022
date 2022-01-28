using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUtilites
{
    public partial class MainForm : Form
    {
        int count = 0;
        Random rnd;

        public MainForm()
        {
            InitializeComponent();
            rnd = new Random();
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
    }
}
