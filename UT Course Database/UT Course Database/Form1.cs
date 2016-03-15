// Author: Tung To

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Squirrel;
using System.Xml;

namespace UT_Course_Database
{
    public partial class Form1 : Form
    {
        Version applicationVersion = new Version("0.2.3.2");

        public static List<Course> list = new List<Course>();
        public static List<Field> fieldList = new List<Field>();
        Config config;

        public Form1()
        {
            InitializeComponent();

            string defaulttext = "(Select a Search Category)";

            comboBox1.Text = defaulttext;
            comboBox2.Text = defaulttext;
            comboBox3.Text = defaulttext;
            tbHours.BackColor = System.Drawing.Color.LimeGreen;

            radioButton1.Checked = true;

            Read.Initiate();

            StreamReader fieldRead = new StreamReader("Resources/field.txt");
            while (!fieldRead.EndOfStream)
            {
                string k = fieldRead.ReadLine();
                fieldList.Add(new Field(k.Split('\t')[0], k.Split('\t')[1]));
            }

            fieldRead.Close();

            StreamReader configReader = new StreamReader("Resources/config.txt");
            config = new Config(configReader.ReadToEnd());
            configReader.Close();

            if (config.ShowHelp)
            {
                Shown += new EventHandler(showHelp);
            }
            else
            {
                disabToolStripMenuItem.Checked = true;
            }

            if (config.IsUnstable)
            {
                useUnstableToolStripMenuItem.Enabled = false;
            }
            else
                useStableToolStripMenuItem.Enabled = false;

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
                    if (c.IsUpperDiv())
                    {
                        results.Remove(c);
                    }

                }
            else if (radioButton3.Checked)
                foreach (Course c in results.ToList())
                {
                    if (!c.IsUpperDiv())
                    {
                        results.Remove(c);
                    }

                }

            if (comboBox1.SelectedIndex == 1)
                searchByCourse(results, textBox1);
            else if (comboBox1.SelectedIndex == 2)
                searchByHours(results, textBox1);
            else if (comboBox1.SelectedIndex == 3)
                searchByKeyWord(results, textBox1);
            if (comboBox2.SelectedIndex == 1)
                searchByCourse(results, textBox2);
            else if (comboBox2.SelectedIndex == 2)
                searchByHours(results, textBox2);
            else if (comboBox2.SelectedIndex == 3)
                searchByKeyWord(results, textBox2);
            if (comboBox3.SelectedIndex == 1)
                searchByCourse(results, textBox3);
            else if (comboBox3.SelectedIndex == 2)
                searchByHours(results, textBox3);
            else if (comboBox3.SelectedIndex == 3)
                searchByKeyWord(results, textBox3);


            if (comboBox1.SelectedIndex==0 && comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                MessageBox.Show("Please specify some search queries", "UT Semester Manager");
            }
            else
            {
                foreach (Course c in results)
                {
                    lbSearchResults.Items.Add(c);
                    //if (c.IsUpperDiv())
                        //var lbi= lbSearchResults.Items[lbSearchResults.Items.IndexOf(c.course + " " + c.code)] as ListBoxItem;
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
            if (textbox.Text == "")
            {
                MessageBox.Show("Please fill in the blank", "Query");
                results.Clear();
                return;
            }
            string [] queries = textbox.Text.ToUpper().Replace(" ", "").Split(',');

            bool p;

            foreach (Course c in results.ToList())
            {
                p = false;
                for(int i = 0; i < queries.Length; i++)
                {
                    if (c.course.Replace(" ", "") == queries[i])
                    {
                        p = true;
                    }
                }

                if (!p)
                    results.Remove(c);
            }
        }

        public void searchByHours(List<Course> results, TextBox textbox)
        {
            if (textbox.Text == "")
            {
                MessageBox.Show("Please fill in the blank", "Query");
                results.Clear();
                return;
            }
            int query = 0;
            Int32.TryParse(textbox.Text, out query);

            foreach (Course c in results.ToList())
            {
                if (c.GetHours() != query)
                {
                    results.Remove(c);
                }
            }
        }

        public void searchByKeyWord(List<Course> results, TextBox textbox)
        {
            if (textbox.Text == "")
            {
                MessageBox.Show("Please fill in the blank", "Query");
                results.Clear();
                return;
            }

            string query = textbox.Text.ToUpper().Replace(" ", "");

            foreach (Course c in results.ToList())
            {
                if (!c.name.ToUpper().Replace(" ", "").Contains(query) && !c.description.ToUpper().Replace(" ", "").Contains(query))
                {
                    results.Remove(c);
                }

            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if(tbEnter.Text == "r")
            {
                Random r = new Random();
                int randomIndex = (int)(r.NextDouble() * list.Count);
                rtbOutput.Text = list[randomIndex].name + " (" + list[randomIndex].ToString() + ")\n\n" + list[randomIndex].description;
            }

            Course c = getCourse(tbEnter.Text);

            if (c != null)
            {
                rtbOutput.Text = c.name + "\n\n" + c.description;
                rtbOutput.SelectionStart = 0;
                rtbOutput.ScrollToCaret();
            }
            else if (tbEnter.Text.Equals(""))
                MessageBox.Show("Enter a course", "Exception");
            else
                MessageBox.Show("Invalid Course", "Exception");
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

                Course testCourse = getCourse(comp);
                if (testCourse == null)
                    MessageBox.Show("Invalid Course", "Exception");
                else {
                    semList[lbSemesters.SelectedIndex].list.Add(getCourse(comp));
                    lbCourses.Items.Add(getCourse(comp));
                    semList[lbSemesters.SelectedIndex].notes.Add("");
                    if (lbCourses.SelectedIndex == -1)
                        lbCourses.SelectedIndex = 0;
                    else
                    {
                        int k = lbCourses.SelectedIndex;                    //Roundabout way of maintaining index 
                        lbSemesters_SelectedIndexChanged(sender, e);
                        lbCourses.SelectedIndex = k;
                    }
                }
            }

            else
                MessageBox.Show("Select a semester", "Exception");

        }

        private void btnAddSem_Click(object sender, EventArgs e)
        {
            sem = new AddSemester();
            sem.Show();
            sem.VisibleChanged += new EventHandler(this.UpdateSem);
        }

        public void UpdateSem(object sender, EventArgs e)
        {
            Semester semester = new Semester(sem.sem, sem.year);    //Once AddSemester form closes,
            lbSemesters.Items.Clear();                              //clear lbSemesters, sort then re-add
            if (semList.Contains(semester))
                MessageBox.Show("Cannot add duplicate semester", "Exception");
            else {
                semList.Add(semester);
                semList.Sort();
                foreach (Semester r in semList)
                    lbSemesters.Items.Add(r);
            }
            sem.Close();
            lbSemesters.SelectedIndex = 0;
        }

        private void btnRemSem_Click(object sender, EventArgs e)
        {
            if (lbSemesters.SelectedIndex != -1)
            {
                semList.RemoveAt(lbSemesters.SelectedIndex);
                lbSemesters.SelectedIndex -= 1;
                lbSemesters.Items.RemoveAt(lbSemesters.SelectedIndex);
                if (lbSemesters.Items.Count > 0 && lbSemesters.SelectedIndex < 0)
                    lbSemesters.SelectedIndex = 0;

            }

            lbSemesters_SelectedIndexChanged(sender, e);
        }

        private void lbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbNotes.Clear();
            
            if(lbCourses.SelectedIndex > -1 && (lbCourses.Items.Count > 0))
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
            }
            else
                btnAdd.Text = "Add To Semester";

            if (lbCourses.Items.Count > 0)
            {
                lbCourses.SelectedIndex = 0;
                rtbNotes.Text = semList[lbSemesters.SelectedIndex].notes[0];

                int hours = 0;
                foreach (string n in lbCourses.Items)
                {
                    hours += getCourse(n).GetHours();
                }
                tbHours.Text = hours + "";
            }
            else
            {
                rtbNotes.Clear();
                tbHours.Text = "0";
            }
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

            rtbNotes.Clear();
            rtbNotes.ReadOnly = true;
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

                for(int i = 0; i < s.notes.Count; i++)
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
                    string[] courses = load.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    List<Course> list1 = new List<Course>();

                    foreach (string k in courses)
                    {
                        list1.Add(getCourse(k));
                    }

                    List<string> notes1 = new List<string>();

                    foreach (string k in notes)
                    {
                        notes1.Add(k);
                    }

                    semList.Add(new Semester(semester[0], semester[1], list1, notes1));
                }

                foreach (Semester s in semList)
                {
                    lbSemesters.Items.Add(s.semester + " " + s.year);

                    foreach (Course c in s.list)
                    {
                        lbCourses.Items.Add(c);
                    }
                }

                int hours = 0;

                foreach(Course c in semList[0].list)
                {
                    hours += c.GetHours();
                }

                tbHours.Text = hours + "";

                load.Close();

                if (config.IsUnstable)
                {
                    useStableToolStripMenuItem.Enabled = true;
                    useUnstableToolStripMenuItem.Enabled = false;
                }

                lbSemesters.SelectedIndex = 0;
                lbCourses.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong file!", "Exception");
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
                semList[lbSemesters.SelectedIndex].notes.RemoveAt(index);

                for (int i = 0; i < semList[lbSemesters.SelectedIndex].notes.Count - 1; i++)
                {
                    if (semList[lbSemesters.SelectedIndex].notes[i] == "")
                    {
                        semList[lbSemesters.SelectedIndex].notes[i] = semList[lbSemesters.SelectedIndex].notes[i + 1];
                        semList[lbSemesters.SelectedIndex].notes[i + 1] = "";
                    }
                }

                tbHours.Text = int.Parse(tbHours.Text) - getCourse(lbCourses.Items[index].ToString()).GetHours() + "";

                lbCourses.SelectedIndex -= 1;
                lbCourses.Items.RemoveAt(index);
                if (lbCourses.SelectedIndex < 0 && lbCourses.Items.Count > 0)
                    lbCourses.SelectedIndex = 0;
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

                    for (int i = 0; i < s.notes.Count; i++)
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
            config.ShowHelp = !config.ShowHelp;
            config.Save();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string downloadUrl = "";
            string downloadUrlu = "";
            Version newVersion = null;
            Version newVersionu = null;
            string xmlURL = "http://utsemman.netai.net/update.xml";
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(xmlURL);
                reader.MoveToContent();
                string elementName = "";
                if((reader.NodeType == XmlNodeType.Element) && (reader.Name == "UT_Course_Database"))
                {
                    while (reader.Read())
                    {
                        if(reader.NodeType == XmlNodeType.Element)
                        {
                            elementName = reader.Name;
                        }
                        else
                        {
                            if((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                            {
                                switch (elementName)
                                {
                                    case "version":
                                        newVersion = new Version(reader.Value);
                                        break;
                                    case "versionu":
                                        newVersionu = new Version(reader.Value);
                                        break;
                                    case "url":
                                        downloadUrl = reader.Value;
                                        break;
                                    case "urlu":
                                        downloadUrlu = reader.Value;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }

            finally
            {
                if (reader != null)
                    reader.Close();
            }

            if (!config.IsUnstable)
            {
                if (applicationVersion.CompareTo(newVersion) < 0)
                {
                    DialogResult response = MessageBox.Show("Version " + newVersion.Major + "." + newVersion.Minor + "." + newVersion.Build + " is available. Do you want to update?", "Update Check", MessageBoxButtons.YesNo);
                    if (response == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(downloadUrl);
                    }
                }
                else
                    MessageBox.Show("This software is up to date.", "Update Check");
            }
            else
            {
                if (applicationVersion.CompareTo(newVersionu) < 0)
                {
                    DialogResult response = MessageBox.Show("Unstable Version " + newVersionu.Major + "." + newVersionu.Minor + "." + newVersionu.Build + "." + newVersionu.Revision + " is available. Do you want to update?", "Update Check", MessageBoxButtons.YesNo);
                    if (response == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(downloadUrlu);
                    }
                }
                else
                    MessageBox.Show("This software is up to date.", "Update Check");
            }
        }

        private void btnCheckReq_Click(object sender, EventArgs e)
        {
            if (lbCourses.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a course", "UT Semester Manager");
                return;
            }
            
        }

        /// <summary>
        /// Returns a Course from a string with course and code. Ex.: bch 369
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private Course getCourse(String s)
        {
            string k = s.ToUpper().Replace(" ", "");

            foreach(Course c in list)
            {
                if (k == c.course.Replace(" ", "") + c.code)
                    return c;
            }

            return null;
        }

        private bool startsWithLetter(string k)
        {
            string a = k.Substring(0, 1);
            int u = 0;
            return !int.TryParse(a, out u);
        }

        private void rtbNotes_Enter(object sender, EventArgs e)
        {
            if(lbCourses.SelectedIndex > -1)
            {
                rtbNotes.ReadOnly = false;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void useStableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            config.IsUnstable = false;
            useStableToolStripMenuItem.Enabled = false;
            useUnstableToolStripMenuItem.Enabled = true;

            config.Save();

            MessageBox.Show("This software will check for the newest stable releases.", "Stable Release");
        }

        private void useUnstableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            config.IsUnstable = true;
            useUnstableToolStripMenuItem.Enabled = false;
            useStableToolStripMenuItem.Enabled = true;

            config.Save();

            MessageBox.Show("This software will check for the newest unstable releases.", "Unstable Release");
        }

        private void btnViewList_Click(object sender, EventArgs e)
        {
            
            if (lbSearchResults.Items.Count == 0)
            {
                MessageBox.Show("Please populate the course results box first", "View List");
                return;
            }

            ViewList form = new ViewList();
            
            foreach (Course course in lbSearchResults.Items)
            {
                form.DisplayCourse(course);
            }
            form.ScrollToCaret();
            form.Show();
        }

        private void tbHours_TextChanged(object sender, EventArgs e)
        {
            int u = 0;
            Int32.TryParse(tbHours.Text, out u);
            if (u > 17)
            {
                tbHours.BackColor = System.Drawing.Color.Red;
            }
            else
                tbHours.BackColor = System.Drawing.Color.LimeGreen;
        }
    }
}
