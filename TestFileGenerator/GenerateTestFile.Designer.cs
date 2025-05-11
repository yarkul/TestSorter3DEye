namespace TestFileGenerator
{
    partial class GenerateTestFileForm
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
            selectFolderButton = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            label1 = new Label();
            generateFileButton = new Button();
            progressLabel = new Label();
            megabytesUpDown = new NumericUpDown();
            label3 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)megabytesUpDown).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // selectFolderButton
            // 
            selectFolderButton.Location = new Point(35, 30);
            selectFolderButton.Name = "selectFolderButton";
            selectFolderButton.Size = new Size(127, 40);
            selectFolderButton.TabIndex = 0;
            selectFolderButton.Text = "Select Folder";
            selectFolderButton.UseVisualStyleBackColor = true;
            selectFolderButton.Click += OnSelectFolder_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(191, 40);
            label1.Name = "label1";
            label1.Size = new Size(29, 20);
            label1.TabIndex = 1;
            label1.Text = "D:\\";
            // 
            // generateFileButton
            // 
            generateFileButton.Location = new Point(121, 28);
            generateFileButton.Name = "generateFileButton";
            generateFileButton.Size = new Size(127, 40);
            generateFileButton.TabIndex = 2;
            generateFileButton.Text = "Generate File";
            generateFileButton.UseVisualStyleBackColor = true;
            generateFileButton.Click += OnGenerateFile_ClickAsync;
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.Location = new Point(143, 108);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(0, 20);
            progressLabel.TabIndex = 4;
            // 
            // megabytesUpDown
            // 
            megabytesUpDown.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            megabytesUpDown.Location = new Point(191, 90);
            megabytesUpDown.Maximum = new decimal(new int[] { 102400, 0, 0, 0 });
            megabytesUpDown.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            megabytesUpDown.Name = "megabytesUpDown";
            megabytesUpDown.Size = new Size(92, 27);
            megabytesUpDown.TabIndex = 5;
            megabytesUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 97);
            label3.Name = "label3";
            label3.Size = new Size(88, 20);
            label3.TabIndex = 6;
            label3.Text = "Set File Size";
            // 
            // panel1
            // 
            panel1.Controls.Add(generateFileButton);
            panel1.Controls.Add(progressLabel);
            panel1.Location = new Point(35, 153);
            panel1.Name = "panel1";
            panel1.Size = new Size(414, 171);
            panel1.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(289, 92);
            label2.Name = "label2";
            label2.Size = new Size(31, 20);
            label2.TabIndex = 8;
            label2.Text = "Mb";
            // 
            // GenerateTestFileForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(472, 352);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(megabytesUpDown);
            Controls.Add(label1);
            Controls.Add(selectFolderButton);
            Name = "GenerateTestFileForm";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Generate Test File";
            ((System.ComponentModel.ISupportInitialize)megabytesUpDown).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button selectFolderButton;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label1;
        private Button generateFileButton;
        private ProgressBar progressBar1;
        private Label progressLabel;
        private NumericUpDown megabytesUpDown;
        private Label label3;
        private Panel panel1;
        private Label label2;
    }
}
