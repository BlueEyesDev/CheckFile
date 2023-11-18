using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CheckFile
{
    public partial class Result : Form
    {
        public Result(Results result)
        {
            InitializeComponent();

            label1.Text = $"{Program.Lang["FileName"]}{result.FileName}";
            label4.Text = $"{Program.Lang["Ext"]}{result.Extension}";
            label5.Text = $"{Program.Lang["RealExt"]}{result.RealExtension}";
            label8.Text = $"VirusTotal : {result.ScanVirustotal}";
            linkLabel1.Text = Program.Lang["view"];
            linkLabel1.Tag = result.Url;
            label10.Text = $"{Program.Lang["Suspect"]}{result.Suspect}";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Tag.ToString());
        }
    }
}
