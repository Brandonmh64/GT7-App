using GranTurismoLibrary.Models;
using GranTurismoApp.Properties;
using GranTurismoLibrary.DataAccess;
using GranTurismoFramework.DataTransfer.Simple;
using GranTurismoFramework.DataTransfer;
using GranTurismoFramework;

namespace GranTurismoApp
{
    public partial class Main : Form
    {
        private TabPage _loadingTab { get; set; }
        private bool _initialLoad { get; set; }

        private ImageList _carImages { get; set; }


        // Inventory Properties
        private bool _carDataLoaded { get => AddCar_RegionDropDown.DataSource != null && AddCar_ManufacturerDropDown.DataSource != null && AddCar_ModelDropDown.DataSource != null; }



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
            var allLoadTasks = new List<Task>();

            //var loadScreenCountdown = ShowLoadingTab(BlacklistTab);
            //allLoadTasks.Add(loadScreenCountdown);


            var loadBlacklistTask = LoadBlacklistTab();
            allLoadTasks.Add(loadBlacklistTask);

            var loadInventoryTask = LoadInventoryTab();
            allLoadTasks.Add(loadInventoryTask);

            LoadCarImages();
        }

        private void LoadCarImages()
        {
            _carImages.Images.Add("Skyline", Resources.SkylineIcon);
        }


        /* Loading Tab */

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async Task ShowLoadingTab(TabPage nextTab)
        {
            var displayLoadTask = () =>
            {
                if (!TabControl.Contains(_loadingTab))
                {
                    TabControl.TabPages.Add(_loadingTab);
                }

                TabControl.SelectedTab = _loadingTab;

                Task.Delay(3500).Wait();

                if (TabControl.Contains(nextTab))
                {
                    TabControl.Invoke(() => TabControl.SelectedTab = nextTab);
                }

                TabControl.Invoke(() => TabControl.TabPages.Remove(_loadingTab));
            };


            await Task.Run(() => TabControl.Invoke(displayLoadTask));
        }





        /* Blacklist Tab */

        private async Task LoadBlacklistTab()
        {
            await Task.Run(() => LoadBlacklist());
        }

        private void LoadBlacklist()
        {
            for (int i = 1; i < 16; i++)
            {
                var bL = new BlacklistCarInfo();

                var pictureBox = Controls.Find($"BLImage{i}", true).FirstOrDefault() as PictureBox;
                if (pictureBox != null) { pictureBox.Image = GetBlacklistIcon(bL.Name); }

                var nameLabel = Controls.Find($"BLName{i}", true).FirstOrDefault() as Label;
                if (nameLabel != null) { nameLabel.Text = bL.Name; }

                var performanceLabel = Controls.Find($"BLPerformance{i}", true).FirstOrDefault() as Label;
                if (performanceLabel != null) { performanceLabel.Text = bL.Performance; }

                var timeLabel = Controls.Find($"BLTime{i}", true).FirstOrDefault() as Label;
                if (timeLabel != null) { timeLabel.Text = bL.Time; }
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

        private void AddCar_BrowseButton_Click(object sender, EventArgs e)
        {

        }



        /* Inventory Tab */


        private async Task LoadInventoryTab()
        {
            await Task.Run(() =>
            {
                LoadOwnedCarList();

                // Add Car Panel
                AddCar_RegionDropDown.Invoke(() => LoadRegionList());
                AddCar_ManufacturerDropDown.Invoke(() => LoadManufacturerList());
                AddCar_ModelDropDown.Invoke(() => LoadCarModelList());

                AddCar_DriverDropDown.Invoke(LoadDriverList);


                // Add Tune Panel
                AddTune_AssociatedCarDropDown.Invoke(() => LoadOwnedCarDropDown());
                LoadTireLists();
            });
        }


        // Owned Car Grid
        private void LoadOwnedCarList()
        {
            var carDao = new CarDao();
            var ownedCars = carDao.GetAllOwnedCars();

            OwnedCarGrid.Invoke(() => OwnedCarGrid.DataSource = ownedCars);
        }
        private void OwnedCarGrid_SelectionChanged(object sender, EventArgs e)
        {
            var currentSelection = OwnedCarGrid.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (currentSelection > -1)
            {
                var ownedCarSelection = OwnedCarGrid.Rows[currentSelection].DataBoundItem as OwnedCarInfo;

                var ownedCarId = ownedCarSelection != null ? ownedCarSelection.OwnedCarId : 0;
                LoadTuneList(ownedCarId);
                AddTune_AssociatedCarDropDown.Invoke(() => LoadOwnedCarDropDown());
            }
        }


        // Tune Grid
        private void LoadTuneList(int ownedCarId)
        {
            if (ownedCarId > 0)
            {
                var tuneDao = new GranTurismoLibrary.DataAccess.TuneDao();
                var carTunes = tuneDao.GetTunes(ownedCarId);
                TuneGrid.Invoke(() => TuneGrid.DataSource = carTunes);
            }
            else
            {
                TuneGrid.Invoke(() => TuneGrid.DataSource = null);
            }
        }


        // Add Car Panel
        private void LoadRegionList(int manufacturerId = 0, int modelId = 0)
        {
            var regionDao = new RegionDao();
            var regionList = regionDao.GetRegions();

            AddCar_RegionDropDown.DataSource = regionList;
            AddCar_RegionDropDown.DisplayMember = "Name";
            AddCar_RegionDropDown.ValueMember = "RegionId";

            if (manufacturerId > 0 || modelId > 0)
            {
                var selectedManufacturer = AddCar_ManufacturerDropDown.SelectedValue as ManufacturerDto;
                if (selectedManufacturer != null)
                {
                    AddCar_RegionDropDown.SelectedItem = regionList.FirstOrDefault(r => r.RegionId == selectedManufacturer.RegionId);
                }

                var selectedModel = AddCar_ModelDropDown?.SelectedItem as CarInfoDto;
                if (selectedModel != null)
                {
                    AddCar_RegionDropDown.SelectedItem = regionList.FirstOrDefault(r => r.RegionId == selectedModel.Region.RegionId);
                }
            }
            else
            {
                AddCar_RegionDropDown.SelectedIndex = -1;
                AddCar_RegionDropDown.Text = "";
            }
        }
        private void AddCar_RegionDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddCar_RegionDropDown.ContainsFocus && AddCar_RegionDropDown.SelectedIndex != -1)
            {
                if (_carDataLoaded)
                {
                    int.TryParse(AddCar_RegionDropDown?.SelectedValue?.ToString(), out int selectedRegion);

                    LoadManufacturerList(selectedRegion, 0);
                    LoadCarModelList(selectedRegion, 0);
                }
            }
        }

        private void LoadManufacturerList(int regionId = 0, int modelId = 0)
        {
            var manufacturerDao = new GranTurismoFramework.DataAccess.ManufacturerDao();
            var manufacturerList = manufacturerDao.GetManufacturers();
            var filteredList = new List<ManufacturerDto>();

            AddCar_ManufacturerDropDown.DisplayMember = "Name";
            AddCar_ManufacturerDropDown.ValueMember = "ManufacturerId";

            if (regionId > 0 || modelId > 0)
            {
                if (regionId > 0)
                {
                    filteredList = manufacturerList.Where(m => m.RegionId == regionId).ToList();
                }
                if (modelId > 0)
                {
                    var selectedModel = AddCar_ModelDropDown?.SelectedItem as CarInfoDto;

                    if (selectedModel != null)
                    {
                        filteredList = manufacturerList.Where(m => m.ManufacturerId == selectedModel.Manufacturer.ManufacturerId).ToList();
                    }
                }

                AddCar_ManufacturerDropDown.DataSource = filteredList;
                AddCar_ManufacturerDropDown.SelectedItem = filteredList.FirstOrDefault();
            }
            else
            {
                AddCar_ManufacturerDropDown.DataSource = manufacturerList;
                AddCar_ManufacturerDropDown.SelectedIndex = -1;
                AddCar_ManufacturerDropDown.SelectedItem = null;
                AddCar_ManufacturerDropDown.Text = "";
            }
        }
        private void AddCar_ManufacturerDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddCar_ManufacturerDropDown.ContainsFocus && AddCar_ManufacturerDropDown.SelectedIndex != -1)
            {
                if (_carDataLoaded)
                {
                    int.TryParse(AddCar_ManufacturerDropDown?.SelectedValue?.ToString(), out int selectedManufacturer);

                    LoadRegionList(selectedManufacturer, 0);
                    LoadCarModelList(0, selectedManufacturer);
                }
            }
        }

        private void LoadCarModelList(int regionId = 0, int manufacturerId = 0)
        {
            var carDao = new CarDao();
            var carList = carDao.GetAllCarInfo();
            var filteredList = new List<CarInfoDto>();

            AddCar_ModelDropDown.DisplayMember = "CarName";
            AddCar_ModelDropDown.ValueMember = "CarId";

            if (regionId > 0 || manufacturerId > 0)
            {
                if (regionId > 0)
                {
                    filteredList = carList.Where(c => c.Region.RegionId == regionId).ToList();
                }
                if (manufacturerId > 0)
                {
                    filteredList = carList.Where(c => c.Manufacturer.ManufacturerId == manufacturerId).ToList();
                }

                AddCar_ModelDropDown.DataSource = filteredList;
                AddCar_ModelDropDown.SelectedItem = filteredList.FirstOrDefault();
            }
            else
            {
                AddCar_ModelDropDown.DataSource = carList;
                AddCar_ModelDropDown.SelectedIndex = -1;
                AddCar_ModelDropDown.SelectedItem = null;
                AddCar_ModelDropDown.Text = "";
            }
        }
        private void AddCar_ModelDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddCar_ModelDropDown.ContainsFocus && AddCar_ModelDropDown.SelectedIndex != -1)
            {
                if (_carDataLoaded)
                {
                    int.TryParse(AddCar_ModelDropDown?.SelectedValue?.ToString(), out int selectedCarInfo);

                    LoadRegionList(0, selectedCarInfo);
                    LoadManufacturerList(0, selectedCarInfo);
                }
            }
        }

        private void LoadDriverList()
        {
            var driverDao = new CarDao();
            var drivers = driverDao.GetAllDrivers();

            AddCar_DriverDropDown.DataSource = drivers;
            AddCar_DriverDropDown.ValueMember = "DriverId";
            AddCar_DriverDropDown.DisplayMember = "DriverName";
        }

        private void AddCar_SaveButton_Click(object sender, EventArgs e)
        {
            var region = AddCar_RegionDropDown.SelectedItem as RegionDto;
            var manufacturer = AddCar_ManufacturerDropDown.SelectedItem as ManufacturerDto;
            var carModel = AddCar_ModelDropDown.SelectedItem as CarInfoDto;
            var driver = AddCar_DriverDropDown.SelectedItem as DriverDto;

            var carInfoToSave = new OwnedCarInfo()
            {
                CarId = carModel.CarId,
                FullName = carModel.CarName,
                Nickname = AddCar_NicknameTextBox?.Text ?? "",
                ImageName = "",
                PrimaryDriverId = driver.DriverId,
                PrimaryDriverName = driver.DriverName,
                ManufacturerId = manufacturer.ManufacturerId,
                ManufacturerName = manufacturer.Name,
                RegionId = region.RegionId,
                RegionName = region.Name,
            };

            var carDao = new CarDao();
            carDao.SaveNewOwnedcar(carInfoToSave);

            AddCar_RegionDropDown.SelectedItem = null;
            LoadManufacturerList(0, 0);
            LoadCarModelList(0, 0);
            LoadDriverList();
            AddCar_NicknameLabel.Text = "";

            LoadOwnedCarList();
        }


        // Add Tune Panel
        private void LoadOwnedCarDropDown()
        {
            var carDao = new CarDao();
            var ownedCars = carDao.GetAllOwnedCars();

            AddTune_AssociatedCarDropDown.DataSource = ownedCars;
            AddTune_AssociatedCarDropDown.DisplayMember = "Nickname";
            AddTune_AssociatedCarDropDown.ValueMember = "OwnedCarId";

            AddTune_AssociatedCarDropDown.SelectedItem = null;
            AddTune_AssociatedCarDropDown.Text = "";
        }

        private void LoadTireLists()
        {
            var tireDao = new CarDao();
            var typeList = tireDao.GetTireTypes().OrderBy(tire => tire.TireId).ToList();
            var typeList2 = new List<TireTypeDto>();

            foreach (var tireType in typeList)
            {
                typeList2.Add(new TireTypeDto()
                {
                    TireId = tireType.TireId,
                    Abreviation = tireType.Abreviation,
                    Name = tireType.Name,
                });
            }

            AddTune_TiresFrontDropDown.Invoke(() =>
            {
                AddTune_TiresFrontDropDown.DataSource = typeList;
                AddTune_TiresFrontDropDown.DisplayMember = "TireInfo";
                AddTune_TiresFrontDropDown.ValueMember = "TireId";
                AddTune_TiresFrontDropDown.SelectedIndex = -1;
                AddTune_TiresFrontDropDown.Text = "";
            });

            AddTune_TiresRearDropDown.Invoke(() =>
            {
                AddTune_TiresRearDropDown.DataSource = typeList2;
                AddTune_TiresRearDropDown.DisplayMember = "TireInfo";
                AddTune_TiresRearDropDown.ValueMember = "TireId";
                AddTune_TiresRearDropDown.SelectedIndex = -1;
                AddTune_TiresRearDropDown.Text = "";
            });
        }


        private void AddTune_SaveButton_Click(object sender, EventArgs e)
        {
            var associatedOwnedCar = AddTune_AssociatedCarDropDown.SelectedItem as OwnedCarInfo;
            var tiresF = AddTune_TiresFrontDropDown.SelectedItem as TireTypeDto;
            var tiresR = AddTune_TiresRearDropDown.SelectedItem as TireTypeDto;

            float.TryParse(AddTune_PPTextBox.Text, out float performancePoints);
            float.TryParse(AddTune_HPTextBox.Text, out float horsePower);
            float.TryParse(AddTune_WeightTextBox.Text, out float weight);

            var tune = new TuneDto()
            {
                CarId = associatedOwnedCar.CarId,
                PerformancePoints = performancePoints,
                HorsePower = horsePower,
                Weight = weight,
                TiresFrontId = tiresF.TireId,
                TiresFront = tiresF.Abreviation,
                TiresRearId = tiresR.TireId,
                TiresRear = tiresR.Abreviation,
                SheetName = AddTune_SheetNameTextBox.Text,
                Notes = AddTune_NotesTextBox.Text,
            };

            var tuneDao = new TuneDao();
            tuneDao.SaveTune(tune);

            AddTune_SheetNameTextBox.Text = "";
            AddTune_PPTextBox.Text = "";
            AddTune_HPTextBox.Text = "";
            AddTune_WeightTextBox.Text = "";
            AddTune_NotesTextBox.Text = "";

            AddTune_AssociatedCarDropDown.SelectedItem = null;
            AddTune_TiresFrontDropDown.SelectedItem = null;
            AddTune_TiresRearDropDown.SelectedItem = null;

            var currentSelection = OwnedCarGrid.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (currentSelection > -1)
            {
                var ownedCarSelection = OwnedCarGrid.Rows[currentSelection].DataBoundItem as OwnedCarInfo;

                var ownedCarId = ownedCarSelection != null ? ownedCarSelection.OwnedCarId : 0;
                LoadTuneList(ownedCarId);
            }
        }

        private void TuneGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}