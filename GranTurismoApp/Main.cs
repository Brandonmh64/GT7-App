using GranTurismoLibrary.Models;
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
            _carImages = new ImageList();

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
            for (int i = 1; i < 16; i++)
            {
                var bL = new BlacklistCar();

                var pictureBox = Controls.Find($"BLImage{i}", true).FirstOrDefault() as PictureBox;
                if (pictureBox != null) { pictureBox.Image = GetBlacklistIcon(bL.Name); }
                
                var nameLabel = Controls.Find($"BLName{i}", true).FirstOrDefault() as Label;
                if (nameLabel != null) { nameLabel.Text = bL.Name; }

                var performanceLabel = Controls.Find($"BLPerformance{i}", true).FirstOrDefault() as Label;
                if (performanceLabel != null) { performanceLabel.Text = bL.Performance; }

                var timeLabel = Controls.Find($"BLTime{i}", true).FirstOrDefault() as Label;
                if (timeLabel != null) {  timeLabel.Text = bL.Time;}
            }
        }

        private Image? GetBlacklistIcon(string carName)
        {
            if (_carImages.Images.ContainsKey(carName))
            {
                return _carImages.Images[carName];
            }
            else
            {
                return null;
            }
        }
    }
}