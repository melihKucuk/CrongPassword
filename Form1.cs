using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrongPassword
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static void AddComboBox(ref ComboBox cbox, int min, int max)
        {
            for (int i = min; i <= max; i++)
                cbox.Items.Add(i);
        }
        private static void GroupBoxControl(ref GroupBox gbox)
        {
            RadioButton rbtn = (RadioButton)gbox.Controls[0];
            rbtn.Checked = true;

            foreach (RadioButton item in gbox.Controls)
                item.Appearance = Appearance.Button;
        }
        private static string RadioButtonControl(ref GroupBox gbox)
        {
            foreach (RadioButton item in gbox.Controls)
            {
                if (item.Checked)
                    return item.Name;
            }
            return string.Empty;
        }
        // özel karakter, buyuk kucuk harfler, rakam
        private static List<char> Allascii = new List<char>(93);
        // özel karakter yok 
        private static List<char> notSpecialch = new List<char>(62);
        private static void AddASCII()
        {
            for (int i = 33; i <= 125; i++)
                Allascii.Add(Convert.ToChar(i));

            for (int j = 48; j <= 122; j++)
            {
                if (char.IsLetterOrDigit(Convert.ToChar(j)))
                    notSpecialch.Add(Convert.ToChar(j));
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Form
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            // AddASCII Method
            AddASCII();
            // btnMake
            btnMake.Enabled = false;
            // groupBox1Controls
            GroupBoxControl(ref groupBox1);
            // CboxLenght
            if (RadioButtonControl(ref groupBox1) == rbtnEasy.Name)
                AddComboBox(ref cboxLenght, 4, 8);
            else if (RadioButtonControl(ref groupBox1) == rbtnMiddle.Name)
                AddComboBox(ref cboxLenght, 4, 16);
            else if (RadioButtonControl(ref groupBox1) == rbtnHard.Name)
                AddComboBox(ref cboxLenght, 8, 32);

            cboxLenght.DropDownStyle = ComboBoxStyle.DropDownList;

            // ProgessBar1
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Enabled = false;

            // toolTip
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(this.lblPercent, "No password is 100% secure!");

        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            if (!(cboxLenght.SelectedIndex > -1))
            {
                MessageBox.Show("uzunluk secilmedi");
            }
            int capacity = Convert.ToInt32(cboxLenght.SelectedItem);
            List<char> password = new List<char>(capacity);
            Random rnd = new Random();
            string result = string.Empty;

            if (RadioButtonControl(ref groupBox1) == rbtnEasy.Name)
            {
                for (int i = 0; i < capacity; i++)
                {
                    int x = rnd.Next(0, notSpecialch.Count); // 0 62 
                    password.Add(notSpecialch[x]);
                }
                result = new string(password.ToArray());
                lblPassword.Text = result.ToLower();


                progressBar1.Value = 33;
                lblPercent.Text = "% " + progressBar1.Value.ToString();
            }
            else if (RadioButtonControl(ref groupBox1) == rbtnMiddle.Name)
            {
                for (int i = 0; i < capacity; i++)
                {
                    int x = rnd.Next(0, Allascii.Count);
                    password.Add(Allascii[x]);
                }
                result = new string(password.ToArray());
                lblPassword.Text = result;
                progressBar1.Value = 66;
                lblPercent.Text = "% " + progressBar1.Value.ToString();

            }
            else if (RadioButtonControl(ref groupBox1) == rbtnHard.Name)
            {
                for (int i = 0; i < capacity; i++)
                {
                    int x = rnd.Next(0, Allascii.Count);
                    // control gelicek
                    password.Add(Allascii[x]);
                }
                result = new string(password.ToArray());
                lblPassword.Text = result;
                progressBar1.Value = 99;
                lblPercent.Text = "% " + progressBar1.Value.ToString();
            }
            if (lblPassword.Text != string.Empty)
                Clipboard.SetText(lblPassword.Text);
        }

        private void CammonRadioButton_Click(object sender, EventArgs e)
        {
            if (RadioButtonControl(ref groupBox1) == rbtnEasy.Name)
            {
                cboxLenght.Items.Clear();
                AddComboBox(ref cboxLenght, 4, 8);
            }
            else if (RadioButtonControl(ref groupBox1) == rbtnMiddle.Name)
            {
                cboxLenght.Items.Clear();
                AddComboBox(ref cboxLenght, 4, 16);
            }

            else if (RadioButtonControl(ref groupBox1) == rbtnHard.Name)
            {
                cboxLenght.Items.Clear();
                AddComboBox(ref cboxLenght, 8, 32);
            }
            if (cboxLenght.SelectedIndex > -1)
                btnMake.Enabled = true;
            else
                btnMake.Enabled = false;
        }

        private void cboxLenght_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxLenght.SelectedIndex > -1)
                btnMake.Enabled = true;
            else
                btnMake.Enabled = false;
        }
    }
}
