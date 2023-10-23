namespace GranTurismoApp
{
    partial class Main
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
            TabControl = new TabControl();
            LoadingTab = new TabPage();
            LoadingLayout = new TableLayoutPanel();
            Logo = new PictureBox();
            BlacklistTab = new TabPage();
            TabControl.SuspendLayout();
            LoadingTab.SuspendLayout();
            LoadingLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            SuspendLayout();
            // 
            // TabControl
            // 
            TabControl.Controls.Add(LoadingTab);
            TabControl.Controls.Add(BlacklistTab);
            TabControl.Dock = DockStyle.Fill;
            TabControl.Location = new Point(0, 0);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(1484, 805);
            TabControl.TabIndex = 0;
            // 
            // LoadingTab
            // 
            LoadingTab.BackColor = Color.FromArgb(32, 32, 32);
            LoadingTab.Controls.Add(LoadingLayout);
            LoadingTab.Location = new Point(4, 24);
            LoadingTab.Name = "LoadingTab";
            LoadingTab.Padding = new Padding(3);
            LoadingTab.Size = new Size(1476, 777);
            LoadingTab.TabIndex = 1;
            LoadingTab.Text = "Loading";
            // 
            // LoadingLayout
            // 
            LoadingLayout.ColumnCount = 3;
            LoadingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LoadingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 700F));
            LoadingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LoadingLayout.Controls.Add(Logo, 1, 0);
            LoadingLayout.Dock = DockStyle.Fill;
            LoadingLayout.Location = new Point(3, 3);
            LoadingLayout.Name = "LoadingLayout";
            LoadingLayout.RowCount = 2;
            LoadingLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 59.7924767F));
            LoadingLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40.2075233F));
            LoadingLayout.Size = new Size(1470, 771);
            LoadingLayout.TabIndex = 1;
            // 
            // Logo
            // 
            Logo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Logo.Image = Properties.Resources.Gt7mw;
            Logo.Location = new Point(388, 114);
            Logo.Name = "Logo";
            Logo.Size = new Size(694, 344);
            Logo.TabIndex = 0;
            Logo.TabStop = false;
            // 
            // BlacklistTab
            // 
            BlacklistTab.BackColor = Color.FromArgb(32, 32, 32);
            BlacklistTab.Location = new Point(4, 24);
            BlacklistTab.Name = "BlacklistTab";
            BlacklistTab.Padding = new Padding(3);
            BlacklistTab.Size = new Size(1476, 777);
            BlacklistTab.TabIndex = 0;
            BlacklistTab.Text = "Blacklist";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1484, 805);
            Controls.Add(TabControl);
            Name = "Main";
            Text = "GT7 Time Tracker";
            WindowState = FormWindowState.Maximized;
            Load += Main_Load;
            TabControl.ResumeLayout(false);
            LoadingTab.ResumeLayout(false);
            LoadingLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl TabControl;
        private TabPage BlacklistTab;
        private TabPage LoadingTab;
        private TableLayoutPanel LoadingLayout;
        private PictureBox Logo;
    }
}