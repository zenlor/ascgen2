namespace JMSoftware.Widgets
{
    partial class WidgetImage
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
            this.jmSelectablePictureBox1 = new JMSoftware.Controls.JMSelectablePictureBox();
            this.SuspendLayout();
            // 
            // jmSelectablePictureBox1
            // 
            this.jmSelectablePictureBox1.AllowDrop = true;
            this.jmSelectablePictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jmSelectablePictureBox1.DrawingImage = true;
            this.jmSelectablePictureBox1.FillSelectionRectangle = true;
            this.jmSelectablePictureBox1.ImageLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.jmSelectablePictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.jmSelectablePictureBox1.Location = new System.Drawing.Point(0, 0);
            this.jmSelectablePictureBox1.Name = "jmSelectablePictureBox1";
            this.jmSelectablePictureBox1.SelectedArea = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.jmSelectablePictureBox1.SelectionBorderColor = System.Drawing.Color.DarkBlue;
            this.jmSelectablePictureBox1.SelectionFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(173)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            this.jmSelectablePictureBox1.SelectionLocked = false;
            this.jmSelectablePictureBox1.Size = new System.Drawing.Size(209, 166);
            this.jmSelectablePictureBox1.SizeMode = JMSoftware.Controls.JMPictureBox.JMPictureBoxSizeMode.FitCenter;
            this.jmSelectablePictureBox1.TabIndex = 0;
            this.jmSelectablePictureBox1.SelectionChanged += new System.EventHandler(this.JMSelectablePictureBox1_SelectionChanged);
            this.jmSelectablePictureBox1.SelectionChanging += new System.EventHandler(this.JMSelectablePictureBox1_SelectionChanging);
            this.jmSelectablePictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.JMSelectablePictureBox1_DragDrop);
            this.jmSelectablePictureBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.JMSelectablePictureBox1_DragEnter);
            this.jmSelectablePictureBox1.DoubleClick += new System.EventHandler(this.JMSelectablePictureBox1_DoubleClick);
            // 
            // WidgetImage
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 166);
            this.Controls.Add(this.jmSelectablePictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "WidgetImage";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.WidgetImage_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.WidgetImage_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.JMSelectablePictureBox jmSelectablePictureBox1;
    }
}