using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeebook._2Gf
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SourceTextBox.Text = @"D:\wt.txt";
            TargetTextBox.Text = @"D:\data.csv";
        }

        void ErrorRecordEventHandler(Object sender, string data)
        {
            ErrorListBox.Items.Add(data);
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            ErrorListBox.Items.Clear();

            ZszqTxt2GfCsv converter = new ZszqTxt2GfCsv();
            converter.ErrorRecordEvent += ErrorRecordEventHandler;
            converter.Convert(SourceTextBox.Text, TargetTextBox.Text);
        }

        private void SourceButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            SourceTextBox.Text = OpenFileDialog.FileName;
        }

        private void TargetButton_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            TargetTextBox.Text = SaveFileDialog.FileName;
        }
    }
}
