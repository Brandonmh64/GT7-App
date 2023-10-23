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
            TruenoCoverPic = new PictureBox();
            Logo = new PictureBox();
            SkylineCoverPic = new PictureBox();
            BlacklistTab = new TabPage();
            BlacklistLayout = new TableLayoutPanel();
            BlacklistListView = new ListView();
            TabControl.SuspendLayout();
            LoadingTab.SuspendLayout();
            LoadingLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TruenoCoverPic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SkylineCoverPic).BeginInit();
            BlacklistTab.SuspendLayout();
            BlacklistLayout.SuspendLayout();
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
            TabControl.Size = new Size(1904, 1041);
            TabControl.TabIndex = 0;
            // 
            // LoadingTab
            // 
            LoadingTab.BackColor = Color.FromArgb(32, 32, 32);
            LoadingTab.Controls.Add(LoadingLayout);
            LoadingTab.Location = new Point(4, 24);
            LoadingTab.Name = "LoadingTab";
            LoadingTab.Padding = new Padding(3);
            LoadingTab.Size = new Size(1896, 1013);
            LoadingTab.TabIndex = 1;
            LoadingTab.Text = "Loading";
            // 
            // LoadingLayout
            // 
            LoadingLayout.ColumnCount = 6;
            LoadingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            LoadingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LoadingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            LoadingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            LoadingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LoadingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            LoadingLayout.Controls.Add(TruenoCoverPic, 3, 2);
            LoadingLayout.Controls.Add(Logo, 2, 1);
            LoadingLayout.Controls.Add(SkylineCoverPic, 1, 2);
            LoadingLayout.Dock = DockStyle.Fill;
            LoadingLayout.Location = new Point(3, 3);
            LoadingLayout.Name = "LoadingLayout";
            LoadingLayout.RowCount = 4;
            LoadingLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            LoadingLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 356F));
            LoadingLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 406F));
            LoadingLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            LoadingLayout.Size = new Size(1890, 1007);
            LoadingLayout.TabIndex = 1;
            // 
            // TruenoCoverPic
            // 
            TruenoCoverPic.Anchor = AnchorStyles.Right;
            TruenoCoverPic.BackColor = Color.Black;
            LoadingLayout.SetColumnSpan(TruenoCoverPic, 2);
            TruenoCoverPic.Image = Properties.Resources.TruenoFullReducedSize;
            TruenoCoverPic.Location = new Point(1029, 443);
            TruenoCoverPic.Margin = new Padding(0, 0, 3, 3);
            TruenoCoverPic.Name = "TruenoCoverPic";
            TruenoCoverPic.Size = new Size(798, 398);
            TruenoCoverPic.TabIndex = 2;
            TruenoCoverPic.TabStop = false;
            // 
            // Logo
            // 
            Logo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LoadingLayout.SetColumnSpan(Logo, 2);
            Logo.Image = Properties.Resources.Gt7mw;
            Logo.Location = new Point(598, 88);
            Logo.Name = "Logo";
            Logo.Size = new Size(694, 350);
            Logo.TabIndex = 0;
            Logo.TabStop = false;
            // 
            // SkylineCoverPic
            // 
            SkylineCoverPic.Anchor = AnchorStyles.Left;
            SkylineCoverPic.BackColor = Color.Black;
            LoadingLayout.SetColumnSpan(SkylineCoverPic, 2);
            SkylineCoverPic.Image = Properties.Resources.SkylineFullReducedSize;
            SkylineCoverPic.Location = new Point(63, 443);
            SkylineCoverPic.Margin = new Padding(3, 0, 0, 3);
            SkylineCoverPic.Name = "SkylineCoverPic";
            SkylineCoverPic.Size = new Size(798, 398);
            SkylineCoverPic.TabIndex = 1;
            SkylineCoverPic.TabStop = false;
            // 
            // BlacklistTab
            // 
            BlacklistTab.BackColor = Color.FromArgb(32, 32, 32);
            BlacklistTab.Controls.Add(BlacklistLayout);
            BlacklistTab.Location = new Point(4, 24);
            BlacklistTab.Name = "BlacklistTab";
            BlacklistTab.Padding = new Padding(3);
            BlacklistTab.Size = new Size(1896, 1013);
            BlacklistTab.TabIndex = 0;
            BlacklistTab.Text = "Blacklist";
            // 
            // BlacklistLayout
            // 
            BlacklistLayout.ColumnCount = 3;
            BlacklistLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BlacklistLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1200F));
            BlacklistLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BlacklistLayout.Controls.Add(BlacklistListView, 1, 0);
            BlacklistLayout.Dock = DockStyle.Fill;
            BlacklistLayout.Location = new Point(3, 3);
            BlacklistLayout.Name = "BlacklistLayout";
            BlacklistLayout.RowCount = 1;
            BlacklistLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            BlacklistLayout.Size = new Size(1890, 1007);
            BlacklistLayout.TabIndex = 1;
            // 
            // BlacklistListView
            // 
            BlacklistListView.Dock = DockStyle.Fill;
            BlacklistListView.Location = new Point(348, 3);
            BlacklistListView.Name = "BlacklistListView";
            BlacklistListView.Size = new Size(1194, 1001);
            BlacklistListView.TabIndex = 0;
            BlacklistListView.UseCompatibleStateImageBehavior = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(TabControl);
            Name = "Main";
            Text = "GT7 Time Tracker";
            WindowState = FormWindowState.Maximized;
            Load += Main_Load;
            TabControl.ResumeLayout(false);
            LoadingTab.ResumeLayout(false);
            LoadingLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TruenoCoverPic).EndInit();
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            ((System.ComponentModel.ISupportInitialize)SkylineCoverPic).EndInit();
            BlacklistTab.ResumeLayout(false);
            BlacklistLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl TabControl;
        private TabPage BlacklistTab;
        private TabPage LoadingTab;
        private TableLayoutPanel LoadingLayout;
        private PictureBox Logo;
        private TableLayoutPanel BlacklistLayout;
        private ListView BlacklistListView;
        private PictureBox TruenoCoverPic;
        private PictureBox SkylineCoverPic;
    }
}