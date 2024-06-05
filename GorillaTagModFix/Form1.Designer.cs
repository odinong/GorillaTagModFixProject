namespace GorillaTagModFixer
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button buttonSelectFolder;
        private TextBox textBoxInstallLocation;
        private Button buttonFixMods;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            buttonSelectFolder = new Button();
            textBoxInstallLocation = new TextBox();
            buttonFixMods = new Button();
            SuspendLayout();
            // 
            // buttonSelectFolder
            // 
            buttonSelectFolder.Location = new Point(12, 12);
            buttonSelectFolder.Name = "buttonSelectFolder";
            buttonSelectFolder.Size = new Size(120, 23);
            buttonSelectFolder.TabIndex = 0;
            buttonSelectFolder.Text = "Select Install Location";
            buttonSelectFolder.UseVisualStyleBackColor = true;
            buttonSelectFolder.Click += buttonSelectFolder_Click;
            // 
            // textBoxInstallLocation
            // 
            textBoxInstallLocation.Location = new Point(138, 12);
            textBoxInstallLocation.Name = "textBoxInstallLocation";
            textBoxInstallLocation.ReadOnly = true;
            textBoxInstallLocation.Size = new Size(250, 23);
            textBoxInstallLocation.TabIndex = 1;
            // 
            // buttonFixMods
            // 
            buttonFixMods.Location = new Point(12, 41);
            buttonFixMods.Name = "buttonFixMods";
            buttonFixMods.Size = new Size(376, 23);
            buttonFixMods.TabIndex = 2;
            buttonFixMods.Text = "Fix Mods";
            buttonFixMods.UseVisualStyleBackColor = true;
            buttonFixMods.Click += buttonFixMods_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(400, 80);
            Controls.Add(buttonFixMods);
            Controls.Add(textBoxInstallLocation);
            Controls.Add(buttonSelectFolder);
            Name = "Form1";
            Text = "Gorilla Tag Mod Fixer";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
