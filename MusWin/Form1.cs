using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusWin
{
    public partial class Form1 : Form
    {
        
        private bool isDragging = false;

        private Point lastCursor;
        private Point lastForm;

        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                for(int i = 0; i < openFileDialog1.SafeFileNames.Length; i++)
                {
                    listBox1.Items.Add(openFileDialog1.FileNames[i]);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MusWin.Sound.GetSound().IsPaused())
                {
                    MusWin.Sound.GetSound().Play(false);
                }
                else if (listBox1.SelectedItem.ToString().Length > 0)
                {
                    MusWin.Sound.GetSound().Play(listBox1.SelectedItem.ToString());
                }
            }
            catch 
            {
                MessageBox.Show("Не выбрана композиция");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MusWin.Sound.GetSound().IsPlaying() || MusWin.Sound.GetSound().IsPaused())
            {
                MusWin.Sound.GetSound().Close();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;

            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location =
                    Point.Add(lastForm, new Size(Point.Subtract(Cursor.Position, new Size(lastCursor))));
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MusWin.Sound.GetSound().IsPlaying())
            {
                MusWin.Sound.GetSound().Pause();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (MusWin.Sound.GetSound().IsPlaying())
            {
                MusWin.Sound.GetSound().MasterVolume = trackBar1.Value;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (MusWin.Sound.GetSound().IsPlaying())
            {
                Int32 valueBalans = trackBar2.Value;
                if (valueBalans < 0)
                {
                    MusWin.Sound.GetSound().RightVolume = trackBar2.Maximum + valueBalans;
                }
                else
                {
                    MusWin.Sound.GetSound().LeftVolume = trackBar2.Maximum - valueBalans;
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MusWin.Sound.GetSound().IsPaused())
            {
                MusWin.Sound.GetSound().Play(false);
            }
            else if (listBox1.SelectedItem.ToString().Length > 0)
            {
                MusWin.Sound.GetSound().Play(listBox1.SelectedItem.ToString());
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listBox1.SelectedIndex == this.listBox1.Items.Count - 1)
                {
                    this.listBox1.SelectedIndex = this.listBox1.Items.Count - 1;
                }
                else
                {
                    this.listBox1.SelectedIndex++;
                }
                if (MusWin.Sound.GetSound().IsPaused())
                {
                    MusWin.Sound.GetSound().Play(false);
                }
                else if (listBox1.SelectedItem.ToString().Length > 0)
                {
                    MusWin.Sound.GetSound().Play(listBox1.SelectedItem.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Список воспроизведения пуст");
            }         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listBox1.SelectedIndex <= 0)
                {
                    this.listBox1.SelectedIndex = 0;
                }
                else
                {
                    this.listBox1.SelectedIndex--;
                }
                if (MusWin.Sound.GetSound().IsPaused())
                {
                    MusWin.Sound.GetSound().Play(false);
                }
                else if (listBox1.SelectedItem.ToString().Length > 0)
                {
                    MusWin.Sound.GetSound().Play(listBox1.SelectedItem.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Список воспроизведения пуст");
            }
            
        }
    }
}
