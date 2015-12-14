using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UT_Course_Database
{
    public partial class ViewList : Form
    {

        public ViewList()
        {
            InitializeComponent();
        }
        public void DisplayCourse(Course course)
        {
            //rtbViewList.Font = new Font(rtbViewList.Font, FontStyle.Bold);
            rtbViewList.AppendText(course.ToString() + " - " + course.name);
            //rtbViewList.Font = new Font(rtbViewList.Font, FontStyle.Regular);
            rtbViewList.AppendText("\n\n" + course.description + "\n\n");

            rtbViewList.SelectionStart = 0;
            rtbViewList.ScrollToCaret();

        }

        public void Clear()
        {
            rtbViewList.Text = "";
        }
    }
}
