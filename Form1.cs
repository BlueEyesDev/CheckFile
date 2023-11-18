using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web.Script.Serialization;
namespace CheckFile
{
    public partial class Form1 : Form
    {
        public static string ApiKey = "f25133d9068704c23335fc39a7351828fa80c5dde894d731d5450cf8ab8569e8";

        public static Dictionary<string, byte[]> Fs4Bytes = new Dictionary<string, byte[]>()
        {
            { "jpeg", new byte[] { 0xff, 0xd8, 0xff, 0xe0 } },
{ "png", new byte[] { 0x89, 0x50, 0x4e, 0x47 } },
{ "gif", new byte[] { 0x47, 0x49, 0x46, 0x38 } },
{ "bmp", new byte[] { 0x42, 0x4d, 0x38, 0x00 } },
{ "tiff", new byte[] { 0x49, 0x49, 0x2a, 0x00 } },
{ "ico", new byte[] { 0x00, 0x00, 0x01, 0x00 } },
{ "webp", new byte[] { 0x52, 0x49, 0x46, 0x46 } },
{ "tga", new byte[] { 0x00, 0x00, 0x02, 0x00 } },
{ "pcx", new byte[] { 0x0a, 0x00, 0x01, 0x01 } },
{ "pbm", new byte[] { 0x50, 0x34, 0x0a, 0x0a } },
{ "pgm", new byte[] { 0x50, 0x35, 0x0a, 0x0a } },
{ "ppm", new byte[] { 0x50, 0x36, 0x0a, 0x0a } },
{ "eps", new byte[] { 0x25, 0x21, 0x50, 0x53 } },
{ "hdr", new byte[] { 0x23, 0x23, 0x53, 0x50 } },
{ "exr", new byte[] { 0x76, 0x2f, 0x31, 0x01 } },
{ "svg", new byte[] { 0x3c, 0x3f, 0x78, 0x6d } },
{ "heic", new byte[] { 0x00, 0x00, 0x00, 0x0c } },
{ "psd", new byte[] { 0x38, 0x42, 0x50, 0x53 } },
{ "avif", new byte[] { 0x41, 0x56, 0x49, 0x46 } },
{ "cr2", new byte[] { 0x49, 0x49, 0x2a, 0x00 } },
{ "nef", new byte[] { 0x4d, 0x4d, 0x00, 0x2a } },
{ "orf", new byte[] { 0x4f, 0x4c, 0x59, 0x4d } },
{ "raf", new byte[] { 0x46, 0x4f, 0x4a, 0x49 } },
{ "dng", new byte[] { 0x49, 0x49, 0x4d, 0x4d } },
{ "jpeg 2000", new byte[] { 0x00, 0x00, 0x00, 0x0c } },
{ "ico (icns)", new byte[] { 0x69, 0x43, 0x4e, 0x53 } },
{ "xcf (gimp)", new byte[] { 0x67, 0x69, 0x6d, 0x70 } },
{ "wmf", new byte[] { 0xd7, 0xcd, 0xc6, 0x9a } },
{ "emf", new byte[] { 0x01, 0x00, 0x00, 0x00 } },
{ "jfif", new byte[] { 0xff, 0xd8, 0xff, 0xe1 } },
{ "pdf", new byte[] { 0x25, 0x50, 0x44, 0x46 } },
{ "pdf (version 1.0)", new byte[] { 0x25, 0x21, 0x50, 0x33 } },
{ "pdf (version 1.1)", new byte[] { 0x25, 0x21, 0x50, 0x34 } },
{ "pdf (version 1.2)", new byte[] { 0x25, 0x21, 0x50, 0x35 } },
{ "pdf (version 1.3)", new byte[] { 0x25, 0x21, 0x50, 0x36 } },
{ "pdf (version 1.4)", new byte[] { 0x25, 0x21, 0x50, 0x37 } },
{ "pdf (version 1.5)", new byte[] { 0x25, 0x21, 0x50, 0x38 } },
{ "pdf (version 1.6)", new byte[] { 0x25, 0x21, 0x50, 0x39 } },
{ "pdf (version 1.7)", new byte[] { 0x25, 0x21, 0x50, 0x41 } },
{ "exe", new byte[] { 0x4d, 0x5a, 0x90, 0x00 } },
{ "dll", new byte[] { 0x4d, 0x5a, 0x90, 0x00 } },
{ "docx", new byte[] { 0x50, 0x4b, 0x03, 0x04 } },
{ "xlsx", new byte[] { 0x50, 0x4b, 0x03, 0x04 } },
{ "pptx", new byte[] { 0x50, 0x4b, 0x03, 0x04 } },
{ "zip", new byte[] { 0x50, 0x4b, 0x03, 0x04 } },
{ "rar", new byte[] { 0x52, 0x61, 0x72, 0x21 } },
{ "csv", new byte[] { 0x22, 0x2c, 0x22, 0x0d } },
{ "mp3", new byte[] { 0xff, 0xfb, 0x90, 0x64 } },
{ "mp4", new byte[] { 0x66, 0x74, 0x79, 0x70 } },
{ "avi", new byte[] { 0x52, 0x49, 0x46, 0x46 } },
{ "mkv", new byte[] { 0x1a, 0x45, 0xdf, 0xa3 } },
{ "mov", new byte[] { 0x6d, 0x6f, 0x6f, 0x76 } },
{ "wmv", new byte[] { 0x30, 0x26, 0xb2, 0x75 } },
{ "flv", new byte[] { 0x46, 0x4c, 0x56, 0x01 } },
{ "rmvb", new byte[] { 0x2e, 0x52, 0x4d, 0x46 } },
{ "mpeg", new byte[] { 0x00, 0x00, 0x01, 0xba } },
{ "ts", new byte[] { 0x47, 0x41, 0x59, 0x20 } },
{ "m2ts", new byte[] { 0x00, 0x00, 0x01, 0xba } },
{ "3gp", new byte[] { 0x66, 0x74, 0x79, 0x70 } },
{ "webm", new byte[] { 0x1a, 0x45, 0xdf, 0xa3 } },
{ "ogv", new byte[] { 0x4f, 0x67, 0x67, 0x53 } },
{ "swf", new byte[] { 0x43, 0x57, 0x53, 0x09 } },
{ "divx", new byte[] { 0x44, 0x49, 0x56, 0x58 } },
{ "vob", new byte[] { 0x00, 0x00, 0x01, 0xba } },
{ "wm", new byte[] { 0x30, 0x26, 0xb2, 0x75 } },
{ "wav", new byte[] { 0x52, 0x49, 0x46, 0x46 } },
{ "ogg", new byte[] { 0x4f, 0x67, 0x67, 0x53 } },
{ "aac", new byte[] { 0xff, 0xf1, 0x50, 0x4d } },
{ "flac", new byte[] { 0x66, 0x4c, 0x61, 0x43 } },
{ "wma", new byte[] { 0x30, 0x26, 0xb2, 0x75 } },
{ "midi", new byte[] { 0x4d, 0x54, 0x68, 0x64 } },
{ "ape", new byte[] { 0x4d, 0x5a, 0x90, 0x00 } },
{ "m4a", new byte[] { 0x00, 0x00, 0x00, 0x20 } },
{ "aiff", new byte[] { 0x46, 0x4f, 0x52, 0x4d } },
{ "mp2", new byte[] { 0x49, 0x44, 0x33, 0x04 } },
{ "amr", new byte[] { 0x23, 0x21, 0x41, 0x4d } },
{ "ac3", new byte[] { 0x0b, 0x77, 0x0e, 0x40 } },
{ "mpc", new byte[] { 0x4d, 0x50, 0x43, 0x4d } }
        };

        public Form1()
        {
            InitializeComponent();
        }
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    Results rs = new Results();
                    try
                    {

                        string[] ext = file.Split('.');


                        rs.FileName = new FileInfo(file).Name;
                        rs.Extension = ext[ext.Length - 1];

                        using (FileStream fs = File.OpenRead(file))
                        {
                            byte[] buffer = new byte[4];
                            int bytesRead = fs.Read(buffer, 0, 4);

                            if (bytesRead >= 4)
                            {
                                rs.RealExtension = Program.Lang["unknown"];
                                foreach (KeyValuePair<string, byte[]> item in Fs4Bytes)
                                {
                                    if (AreByteArraysEqual(item.Value, buffer))
                                    {
                                        rs.RealExtension = item.Key;
                                        break;
                                    }
                                }

                                if (rs.RealExtension.Contains("PDF"))
                                {

                                    using (StreamReader reader = new StreamReader(fs))
                                    {
                                        string fileContent = reader.ReadToEnd();

                                        if (fileContent.Contains("/JavaScript"))
                                        {
                                            rs.Suspect = Program.Lang["PDFJS"];
                                        }

                                    }
                                }
                            }
                            else
                            {
                                rs.RealExtension = Program.Lang["unknown"];
                            }
                        }

                        if (rs.RealExtension != rs.Extension)
                        {
                            rs.Suspect = Program.Lang["SuspectExt"];
                        }
                        string hashString = BitConverter.ToString(CalculateSHA256(file)).Replace("-", String.Empty).ToLower();
                        string result = ScanAntivirus(hashString);
                        if (result != null)
                        {
                            rs.ScanVirustotal = result;
                            rs.Url = $"https://www.virustotal.com/gui/file/{hashString}";
                        }
                    }
                    catch (Exception ex) { }
                    new Result(rs).Show();
                }
            }
        }
        public static byte[] CalculateSHA256(string fs)
        {
            using (SHA256 sha256 = SHA256.Create())
            using (FileStream fileStream = File.OpenRead(fs))
            {
                return sha256.ComputeHash(fileStream);
            }
        }
        static string ScanAntivirus(string CheckSum)
        {
            try
            {
                string url = $"https://www.virustotal.com/api/v3/files/{CheckSum}";
                string jsonvt = new WebClient() { Headers = new WebHeaderCollection() { { HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36" }, { HttpRequestHeader.ContentType, "application/json" }, { "x-apikey", ApiKey } } }.DownloadString(url);

                Root t = new JavaScriptSerializer().Deserialize<Root>(jsonvt);

                return $"{t.data.attributes.last_analysis_stats.malicious} / {(t.data.attributes.last_analysis_stats.malicious + t.data.attributes.last_analysis_stats.undetected)}";

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404")) {
                    return null;
                }
            }
            return null;

        }
        public class Root
        {
            public Data data { get; set; }
        }

        public class Data
        {
            public Attributes attributes { get; set; }
        }
        public class Attributes
        {
            public LastAnalysisStats last_analysis_stats { get; set; }
        }

        public class LastAnalysisStats
        {
            public int malicious { get; set; }
            public int undetected { get; set; }
        }

        static bool AreByteArraysEqual(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
    public class Results
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string RealExtension { get; set; }
        public string ScanVirustotal { get; set; }
        public string Url { get; set; }
        public string Suspect { get; set; }
    }
}
