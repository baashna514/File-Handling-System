using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            abc();
        }

        private void abc()
        {
            int i;
            String[] strd = Directory.GetLogicalDrives();
            for (i = 0; i < strd.Length; i++)
            {
                comboBox1.Items.Add(strd[i]);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void btn_sys_Click(object sender, EventArgs e)
        {
            try
            {
                String val = comboBox1.SelectedItem.ToString();
                System.IO.DriveInfo dinfo = new System.IO.DriveInfo(val);

                textBox1.Text = "\nTotal Size: " + dinfo.TotalSize/(1024*1024*1024) + " GB\n AvailableFreeSpace: " + dinfo.AvailableFreeSpace/(1024*1024*1024) + " GB";
               
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
   
            }
        }

        private void create_dir_Click(object sender, EventArgs e)
        {
            try
             {
                if (!Directory.Exists(textBox2.Text))
                {
                    Directory.CreateDirectory(textBox2.Text);
                    MessageBox.Show("Directory created");
                }
                else
                {
                    MessageBox.Show("Please Enter correct directory path and Filename");
                }
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }
        }

        private void create_subdir_Click(object sender, EventArgs e)
        {
            try
            {
                String path = textBox2.Text;
                System.IO.DirectoryInfo sudir = new
                DirectoryInfo(textBox2.Text);
                String subname = textBox3.Text;
                sudir.CreateSubdirectory(subname);
                MessageBox.Show("SubDirectory has successfully created");
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }
        }

        private void show_subdir_Click(object sender, EventArgs e)
        {
            try
            {
                String path = textBox2.Text;
                DirectoryInfo dinfo = new DirectoryInfo(path);
                if (dinfo.Exists)
                {
                    DirectoryInfo[] subdir = dinfo.GetDirectories();
                    foreach (DirectoryInfo s in subdir)
                    {
                        comboBox2.Items.Add(s);
                    }
                    FileInfo[] finfo = dinfo.GetFiles("*");
                    foreach (FileInfo f in finfo)
                    {
                        comboBox2.Items.Add(f);
                    }
                }
                else
                {
                    MessageBox.Show("Directory does not Exist,Please enter valid directory path and name");
                }
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo srcdir = new DirectoryInfo(textBox4.Text);
                DirectoryInfo destdir = new DirectoryInfo(textBox5.Text);
                CopyDirectory(srcdir, destdir);
              //  Directory.Move(srcdir, destdir);
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }
        }

        private void CopyDirectory(DirectoryInfo srcdir, DirectoryInfo destdir)
        {
            try
            {
                if (!destdir.Exists)
                {
                    destdir.Create();
                    FileInfo[] finfo = srcdir.GetFiles();
                    foreach (FileInfo f in finfo)
                    {
                        f.CopyTo(Path.Combine(destdir.FullName, f.Name));
                    }
                    DirectoryInfo[] dinfo = srcdir.GetDirectories();
                    foreach (DirectoryInfo d in dinfo)
                    {
                        String dest = Path.Combine(destdir.FullName, d.Name);
                        CopyDirectory(d, new DirectoryInfo(dest));
                    }
                    MessageBox.Show("Directory has Copied successfully to another Drive");
                }
                else
                {
                    MessageBox.Show("Please enter valid path and Filename");
                }
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }
}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
           {
               FileStream fs = new FileStream(textBox6.Text, FileMode.Create,
               FileAccess.Write);
               StreamWriter sw = new StreamWriter(fs);
               sw.WriteLine(textBox7.Text);
               sw.Flush();
               fs.Close();
               MessageBox.Show("Content is written in file successfully");
           }
           catch (Exception ram)
           {
               MessageBox.Show(ram.Message);
           }
        }

        private void btn_read_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(textBox6.Text, FileMode.Open,
                FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                textBox8.Text = sr.ReadToEnd();
                fs.Close();
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(textBox6.Text, FileMode.Open,
                FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                String str = sr.ReadToEnd();
                int i = (str.IndexOf(textBox9.Text, 0));
                if (i > -1)
                {
                    MessageBox.Show("This word is exist in the file");
                }
                else
                {
                    MessageBox.Show("This word is not exist in the file try another words");
                }
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }
        }

        private void btn_append_Click(object sender, EventArgs e)
        {
            try
            {
                String str = textBox6.Text;
                StreamWriter sw = File.AppendText(str);
                sw.WriteLine(textBox10.Text);
                sw.Close();
                MessageBox.Show("File contents appended successfully");
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }
        }

        private void btn_Replace_Click(object sender, EventArgs e)
        {
            if (textBox12.Text != "" && textBox13.Text != "")
            {
                String text = File.ReadAllText(textBox6.Text);
                String Value = text.Replace(textBox12.Text, textBox13.Text);
                File.WriteAllText(textBox6.Text, Value);
                MessageBox.Show("Successfully Replaced");
                textBox12.Text = "";
                textBox13.Text = "";
            }
            else
            {
                MessageBox.Show("Nothing To Replace");
            }
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            String src = textBox6.Text;
            String dest = textBox11.Text;
            try
            {
                FileInfo srcfile = new FileInfo(src);
                if (srcfile.Exists)
                {
                    srcfile.MoveTo(dest);
                    MessageBox.Show("File is Renamed successfully");
                }
                else
                {
                    MessageBox.Show("Enter correct File Name and Path again");
                }
            }
            catch (Exception ram)
            {
                MessageBox.Show(ram.Message);
            }
        }

        private void btn_move_Click(object sender, EventArgs e)
        {
            string srcdir = textBox4.Text;
            string dstdir = textBox5.Text;

            try
            {
                Directory.Move(srcdir, dstdir);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
