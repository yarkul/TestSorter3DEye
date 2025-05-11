using System.Diagnostics;
using TestFileGenerator.Core;

namespace TestFileGenerator
{
    public partial class GenerateTestFileForm : Form
    {
        private Stopwatch _stopWatch;
        private readonly ILargeFileGenerator _largeFileGenerator;

        public GenerateTestFileForm(ILargeFileGenerator largeFileGenerator)
        {
            _largeFileGenerator = largeFileGenerator;
            InitializeComponent();
        }

        private void OnSelectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private async void OnGenerateFile_ClickAsync(object sender, EventArgs e)
        {
            string folderPath = label1.Text;

            _stopWatch = new Stopwatch();
            _stopWatch.Start();

            await _largeFileGenerator.GenerateLargeFileAsync(
                    folderPath,
                    (long)megabytesUpDown.Value,
                    UpdateProgressLabel,
                    UpdateProgressLabel);
        }

        private void UpdateProgressLabel(long writtenBytes)
        {
            progressLabel.Text = (writtenBytes / 1024 / 1024).ToString() + " Mb";
        }

        private void UpdateProgressLabel(bool isSuccess)
        {
            _stopWatch.Stop();

            TimeSpan duration = _stopWatch.Elapsed;

            progressLabel.Text = isSuccess ? $"SUCESS {duration.TotalSeconds:F2} sec" : $"FAILED {duration.TotalSeconds:F2} sec";
        }
    }
}
