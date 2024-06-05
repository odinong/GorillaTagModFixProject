using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace GorillaTagModFixer
{
    public partial class Form1 : Form
    {
        private string installLocation;

        public Form1()
        {
            InitializeComponent();
            ApplyTheme();
            SetFormIcon("https://github.com/odinong/GorillaTagModFixer/blob/main/ab67616d00001e0268cd419dc5bacb3494c21b65.png?raw=true");
        }

        private void ApplyTheme()
        {
            this.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);

            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = System.Drawing.Color.FromArgb(0, 128, 128);
                    button.ForeColor = System.Drawing.Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 64, 64);
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
                    textBox.ForeColor = System.Drawing.Color.White;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
            }
        }

        private void SetFormIcon(string url)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(url);
                    using (MemoryStream memoryStream = new MemoryStream(data))
                    {
                        using (Bitmap bitmap = new Bitmap(memoryStream))
                        {
                            this.Icon = IconFromBitmap(bitmap);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to set form icon: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Icon IconFromBitmap(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;

                using (Bitmap newBitmap = new Bitmap(memoryStream))
                {
                    Icon icon = Icon.FromHandle(newBitmap.GetHicon());
                    return icon;
                }
            }
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the Gorilla Tag Install Location";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    installLocation = dialog.SelectedPath;
                    textBoxInstallLocation.Text = installLocation;
                }
            }
        }

        private void buttonFixMods_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(installLocation))
            {
                MessageBox.Show("Please select the Gorilla Tag Install Location first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string BepInExURL = "https://github.com/odinong/GorillaTagModFixer/raw/main/BepInEx.zip";
            string GorillaTagPath = Path.Combine(installLocation, "mods.zip");

            try
            {
                DownloadFile(BepInExURL, GorillaTagPath);
                ExtractZipFile(GorillaTagPath, installLocation);
                File.Delete(GorillaTagPath);
                MessageBox.Show("mods SHOULD have been fixed, mod fixer made by odin.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DownloadFile(string url, string destination)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, destination);
            }
        }

        private void ExtractZipFile(string zipPath, string extractPath)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith("/"))
                    {
                        string directoryPath = Path.Combine(extractPath, entry.FullName);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                    }
                }
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (!entry.FullName.EndsWith("/"))
                    {
                        string destinationPath = Path.Combine(extractPath, entry.FullName);

                        // Ensure the directory exists
                        string directoryPath = Path.GetDirectoryName(destinationPath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        entry.ExtractToFile(destinationPath, overwrite: true);
                    }
                }
            }
        }
    }
}
