namespace UT_Course_Database
{
    partial class AddSemester
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSemester));
            this.tbYear = new System.Windows.Forms.TextBox();
            this.cbSem = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(184, 46);
            this.tbYear.Margin = new System.Windows.Forms.Padding(4);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(132, 22);
            this.tbYear.TabIndex = 0;
            // 
            // cbSem
            // 
            this.cbSem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSem.FormattingEnabled = true;
            this.cbSem.Items.AddRange(new object[] {
            "Fall",
            "Spring",
            "Summer"});
            this.cbSem.Location = new System.Drawing.Point(16, 46);
            this.cbSem.Margin = new System.Windows.Forms.Padding(4);
            this.cbSem.Name = "cbSem";
            this.cbSem.Size = new System.Drawing.Size(160, 24);
            this.cbSem.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(16, 79);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(303, 50);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Add Semester";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select a Semester && Enter a Year";
            // 
            // AddSemester
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 144);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cbSem);
            this.Controls.Add(this.tbYear);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddSemester";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add a Semester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.ComboBox cbSem;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
    }
}