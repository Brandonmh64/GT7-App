using GranTurismoApp.Properties;

namespace GranTurismoApp
{
    public partial class Main : Form
    {
        private TabPage _loadingTab { get; set; }

        private ImageList _carImages { get; set; }



        /* Initialize */

        public Main()
        {
            InitializeComponent();
            _loadingTab = LoadingTab;

            TabControl.SelectedIndex = 0;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            ShowLoadingTab(BlacklistTab);

            LoadCarImages();
            LoadBlacklist();
        }

        private void LoadCarImages()
        {
            _carImages = new ImageList();

            _carImages.Images.Add("Skyline", Resources.SkylineIcon);
        }


        /* Loading Tab */

        private async void ShowLoadingTab(TabPage nextTab)
        {
            var loadingTask = () =>
            {
                if (!TabControl.Contains(_loadingTab))
                {
                    TabControl.TabPages.Add(_loadingTab);
                }

                TabControl.SelectedTab = _loadingTab;

                Task.Delay(5000).Wait();

                TabControl.TabPages.Remove(LoadingTab);

                if (TabControl.Contains(nextTab))
                {
                    TabControl.SelectedTab = nextTab;
                }

                TabControl.TabPages.Remove(_loadingTab);
            };

            await Task.Run(() => TabControl.Invoke(loadingTask));
        }




        /* Blacklist Tab */

        private void LoadBlacklist()
        {
            //BlacklistListView.LargeImageList = _carImages;

            var skyline = new ListViewItem();
            skyline.ImageKey = "Skyline";

            skyline.Text = "R34 Skyline";

            //BlacklistListView.Items.Add(skyline);
        }
    }
}