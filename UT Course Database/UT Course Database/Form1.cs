using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Squirrel;



namespace UT_Course_Database
{
    public partial class Form1 : Form
    {


        public static List<Course> list = new List<Course>();

        public Form1()
        {
            InitializeComponent();

            comboBox1.Text = "(Select a Search Category)";
            comboBox2.Text = "(Select a Search Category)";
            comboBox3.Text = "(Select a Search Category)";

            radioButton1.Checked = true;

            Read.Initiate();

            StreamReader config = new StreamReader("Resources/config.txt");
            string temp = config.ReadLine();
            config.Close();

            if(temp.Split('=')[1] =="0")
                Shown += new EventHandler(showHelp);
        }

        private void showHelp(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();

        }

        List<Course> results;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lbSearchResults.Items.Clear();
            rtbInfo.Clear();

            results = new List<Course>();

            foreach (Course c in list)
            {
                results.Add(c);
            }

            if (radioButton2.Checked)
                foreach (Course c in results.ToList())
                {
                    if (c.GetDivision() != "l")
                    {
                        results.Remove(c);
                    }

                }
            else if (radioButton3.Checked)
                foreach (Course c in results.ToList())
                {
                    if (c.GetDivision() != "u")
                    {
                        results.Remove(c);
                    }

                }

            if (comboBox1.SelectedIndex == 1)
                searchByCourse(results, textBox1);
            else if (comboBox1.SelectedIndex == 2)
                searchByHours(results, textBox1);
            if (comboBox2.SelectedIndex == 1)
                searchByCourse(results, textBox2);
            else if (comboBox2.SelectedIndex == 2)
                searchByHours(results, textBox2);
            if (comboBox3.SelectedIndex == 1)
                searchByCourse(results, textBox3);
            else if (comboBox3.SelectedIndex == 2)
                searchByHours(results, textBox3);


            if(comboBox1.SelectedIndex==0 && comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                MessageBox.Show("Please specify some search queries");
            }
            else
            {
                foreach (Course c in results)
                {
                    lbSearchResults.Items.Add(c.course + " " + c.code);
                }
            }
            

        }

        private void lbSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbInfo.Text = results[lbSearchResults.SelectedIndex].name +"\n\n"+results[lbSearchResults.SelectedIndex].description;
            rtbInfo.SelectionStart = 0;
            rtbInfo.ScrollToCaret();
        }

        public void searchByCourse(List<Course> results, TextBox textbox)
        {
            string query = textbox.Text.ToUpper().Replace(" ", "");

            foreach(Course c in results.ToList())
            {
                if (c.course.Replace(" ","") != query)
                {
                    results.Remove(c);
                    
                }

            }
        }

        public void searchByHours(List<Course> results, TextBox textbox)
        {
            int query = 0;
            Int32.TryParse(textbox.Text, out query);

            foreach (Course c in results.ToList())
            {
                if (c.GetHours() != query)
                    results.Remove(c);

            }

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string comp = tbEnter.Text.ToUpper().Replace(" ", "");
            
            foreach(Course c in list)
            {
                if (c.course.Replace(" ", "")+ c.code == comp)
                {
                    rtbOutput.Text = c.name + "\n\n" + c.description;
                    break;
                }
            }
        }

        private void tbEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(sender, e);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        AddSemester sem;
        List<Semester> semList = new List<Semester>();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lbSemesters.SelectedIndex != -1)
            {
                string comp = tbEnter.Text.ToUpper().Replace(" ", "");

                foreach (Course c in list)
                {
                    if (c.course.Replace(" ", "") + c.code == comp)
                    {
                        semList[lbSemesters.SelectedIndex].list.Add(c);
                        lbCourses.Items.Add(c.course + " " + c.code);
                        break;
                    }
                }
            }

            else
                MessageBox.Show("Select a semester");

        }

        private void btnAddSem_Click(object sender, EventArgs e)
        {
            sem = new AddSemester();
            sem.Show();
            sem.VisibleChanged += new EventHandler(this.UpdateSem);
        }

        public void UpdateSem(Object sender, EventArgs e)
        {
            lbSemesters.Items.Add(sem.sem + " " + sem.year);
            semList.Add(new Semester(sem.sem, sem.year));
            sem.Close();
            lbSemesters.SelectedIndex = 0;
        }

        private void btnRemSem_Click(object sender, EventArgs e)
        {
            if(lbSemesters.SelectedIndex !=-1)
                lbSemesters.Items.RemoveAt(lbSemesters.SelectedIndex);
        }

        private void lbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbNotes.Clear();
            
            if(lbCourses.SelectedIndex != -1)
            {
                rtbNotes.Text = semList[lbSemesters.SelectedIndex].notes[lbCourses.SelectedIndex];
            }
        }

        private void lbSemesters_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbCourses.Items.Clear();

            if (lbSemesters.SelectedIndex != -1)
            {
                for (int i = 0; i < semList[lbSemesters.SelectedIndex].list.Count; i++)
                {
                    lbCourses.Items.Add(semList[lbSemesters.SelectedIndex].list[i].course + " " + semList[lbSemesters.SelectedIndex].list[i].code);
                }

                btnAdd.Text = "Add To Semester (" + lbSemesters.Items[lbSemesters.SelectedIndex] + ")";

            }
            else
                btnAdd.Text = "Add To Semester";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            lbSearchResults.Items.Clear();
            rtbInfo.Clear();
        }

        private void rtbNotes_Leave(object sender, EventArgs e)
        {
            if(lbSemesters.SelectedIndex >= 0 && lbCourses.SelectedIndex >= 0)
            {
                semList[lbSemesters.SelectedIndex].notes[lbCourses.SelectedIndex] = rtbNotes.Text;
            }
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        string name = "";

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            name = saveFileDialog1.FileName;

            string text = "";

            foreach(Semester s in semList)
            {
                text += s.semester + "\t" + s.year + "\n";

                for(int i = 0; i < s.notes.Length; i++)
                {
                    text += s.notes[i]+"`";
                }

                text += "\n";
                
                foreach(Course c in s.list)
                {
                    text += c.course.Replace(" ", "") + c.code+ " ";
                }

                text += "\n";
            }

            File.WriteAllText(name, text);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                semList.Clear();
                lbSemesters.Items.Clear();
                lbCourses.Items.Clear();
                rtbNotes.Clear();

                StreamReader load = new StreamReader(openFileDialog1.FileName);

                while (!load.EndOfStream)
                {
                    string[] semester = load.ReadLine().Split('\t');
                    string[] notes = load.ReadLine().Split('`');
                    string[] courses = load.ReadLine().Split(' ');

                    List<Course> list1 = new List<Course>();

                    foreach (string k in courses)
                    {
                        foreach (Course c in list)
                        {
                            if (c.course.Replace(" ", "") + c.code == k)
                                list1.Add(c);
                        }
                    }

                    semList.Add(new Semester(semester[0], semester[1], list1, notes));
                }

                foreach (Semester s in semList)
                {
                    lbSemesters.Items.Add(s.semester + " " + s.year);

                    foreach (Course c in s.list)
                    {
                        lbCourses.Items.Add(c);
                    }
                }

                load.Close();

                lbSemesters.SelectedIndex = 0;
                lbCourses.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("This happened: "+ex.Message);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = lbCourses.SelectedIndex;
            
            if(index >= 0)
            {
                semList[lbSemesters.SelectedIndex].list.RemoveAt(index);
                semList[lbSemesters.SelectedIndex].notes[index] = "";

                for (int i = 0; i < semList[lbSemesters.SelectedIndex].notes.Length - 1; i++)
                {
                    if (semList[lbSemesters.SelectedIndex].notes[i] == "")
                    {
                        semList[lbSemesters.SelectedIndex].notes[i] = semList[lbSemesters.SelectedIndex].notes[i + 1];
                        semList[lbSemesters.SelectedIndex].notes[i + 1] = "";
                    }
                }

                lbCourses.Items.RemoveAt(index);
                lbCourses.SelectedIndex = index;

                lbCourses.Focus();

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (name == "")
                saveasToolStripMenuItem_Click(sender, e);

            else
            {
                string text = "";

                foreach (Semester s in semList)
                {
                    text += s.semester + "\t" + s.year + "\n";

                    for (int i = 0; i < s.notes.Length; i++)
                    {
                        text += s.notes[i] + "`";
                    }

                    text += "\n";

                    foreach (Course c in s.list)
                    {
                        text += c.course.Replace(" ", "") + c.code + " ";
                    }

                    text += "\n";
                }

                File.WriteAllText(name, text);
            }
        }

        private void disabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter config = new StreamWriter("Resources/config.txt");
            config.WriteLine("popup=1");
            config.Close();
        }
    }
}
