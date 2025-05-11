using System.Diagnostics;
using System.Windows.Forms;
using TestFileSorter.Core;

namespace TestFileSorter
{
    public partial class SortBigFileForm : Form
    {
        private Stopwatch _stopWatch;
        private readonly ILargeFileSorter _largeFileSorter;

        public SortBigFileForm(ILargeFileSorter largeFileSorter)
        {
            _largeFileSorter = largeFileSorter;
            InitializeComponent();
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            sortFileButton.Enabled = false;
            _ = _largeFileSorter.GenerateSortedFileAsync(filePathLabel.Text, OnComplete, OnCurrentStatusChanged);
        }

        private void OnCurrentStatusChanged(string message)
        {
            sortResultLabel.Invoke(() => { sortResultLabel.Text = message; });
        }

        private void OnComplete(string message)
        {
            TimeSpan duration = _stopWatch.Elapsed;

            sortResultLabel.Invoke(() => { sortResultLabel.Text = $"{message}. {duration.TotalSeconds:F2} sec"; });

            sortFileButton.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePathLabel.Text = openFileDialog1.FileName;
            }
        }
    }
}
