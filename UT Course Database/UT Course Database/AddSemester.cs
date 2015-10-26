// Author: Tung To

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
    public partial class AddSemester : Form
    {
        public AddSemester()
        {
            InitializeComponent();

            cbSem.Text = "Fall";
        }

        public String sem;
        public string year;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            

            if (tbYear.Text.Length != 4)
                MessageBox.Show("Year is not 4-digits");
            else
            {
                int k = 0;
                if (!Int32.TryParse(tbYear.Text, out k))
                    MessageBox.Show("Enter only numbers for the 'year' field.");
                else
                {
                    sem = cbSem.Text;
                    year = tbYear.Text;
                    this.Visible = false;
                }
            }
        }
    }
}
