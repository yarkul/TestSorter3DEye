namespace TestFileSorter
{
    partial class SortBigFileForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog1 = new OpenFileDialog();
            button1 = new Button();
            filePathLabel = new Label();
            sortFileButton = new Button();
            sortResultLabel = new Label();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "*.txt";
            // 
            // button1
            // 
            button1.Location = new Point(61, 43);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "Select File to Sort";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // filePathLabel
            // 
            filePathLabel.AutoSize = true;
            filePathLabel.Location = new Point(179, 47);
            filePathLabel.Name = "filePathLabel";
            filePathLabel.Size = new Size(130, 20);
            filePathLabel.TabIndex = 1;
            filePathLabel.Text = "D:\\MySuperFile.txt";
            // 
            // sortFileButton
            // 
            sortFileButton.Location = new Point(179, 120);
            sortFileButton.Name = "sortFileButton";
            sortFileButton.Size = new Size(110, 29);
            sortFileButton.TabIndex = 2;
            sortFileButton.Text = "Sort";
            sortFileButton.UseVisualStyleBackColor = true;
            sortFileButton.Click += SortButton_Click;
            // 
            // sortResultLabel
            // 
            sortResultLabel.AutoSize = true;
            sortResultLabel.Location = new Point(61, 197);
            sortResultLabel.Name = "sortResultLabel";
            sortResultLabel.Size = new Size(159, 20);
            sortResultLabel.TabIndex = 3;
            sortResultLabel.Text = "Pick file and click Sort..";
            // 
            // SortBigFileForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 280);
            Controls.Add(sortResultLabel);
            Controls.Add(sortFileButton);
            Controls.Add(filePathLabel);
            Controls.Add(button1);
            Name = "SortBigFileForm";
            Text = "Sort Big Files";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private Button button1;
        private Label filePathLabel;
        private Button sortFileButton;
        private Label sortResultLabel;
    }
}
