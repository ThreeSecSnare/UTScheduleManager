namespace UT_Course_Database
{
    partial class ViewList
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
            this.rtbViewList = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbViewList
            // 
            this.rtbViewList.Location = new System.Drawing.Point(16, 15);
            this.rtbViewList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtbViewList.Name = "rtbViewList";
            this.rtbViewList.Size = new System.Drawing.Size(1012, 660);
            this.rtbViewList.TabIndex = 0;
            this.rtbViewList.Text = "";
            // 
            // ViewList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.rtbViewList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ViewList";
            this.Text = "View List";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbViewList;
    }
}