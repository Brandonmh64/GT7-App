namespace GranTurismoApp
{
    public partial class Main : Form
    {
        private TabPage _loadingTab { get; set; }


        public Main()
        {
            InitializeComponent();
            _loadingTab = LoadingTab;

            TabControl.SelectedIndex = 0;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ShowLoadingTab(BlacklistTab);
        }

        private async void ShowLoadingTab(TabPage nextTab)
        {
            var loadingTask = () =>
            {
                if (!TabControl.Contains(_loadingTab))
                {
                    TabControl.TabPages.Add(_loadingTab);
                }

                TabControl.SelectedTab = _loadingTab;

                Task.Delay(3000).Wait();

                TabControl.TabPages.Remove(LoadingTab);

                if (TabControl.Contains(nextTab))
                {
                    TabControl.SelectedTab = nextTab;
                }

                TabControl.TabPages.Remove(_loadingTab);
            };

            await Task.Run(() => TabControl.Invoke(loadingTask));
        }



        /// <summary>
        /// Hide tabs that are only meant to be seen by dev
        /// </summary>
        private void HideTabs()
        {
        }

        
    }
}