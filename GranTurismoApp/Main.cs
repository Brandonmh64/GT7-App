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
        private bool _sessionStarted { get; set; }
        private List<TimeTrialInfo> _sessionTimeTrials { get; set; }

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

            // Blacklist
            var loadBlacklistTask = LoadBlacklistTab();
            allLoadTasks.Add(loadBlacklistTask);

            // Track
            var loadTrackTabTasks = Task.Run(() =>
            {
                LoadTimeTrialLocationDropDowns();
                LoadTimeTrialCarOptions();
            });

            // Inventory
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





        /* ===== Blacklist Tab ===== */

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






        /* ===== Track Tab ===== */


        // Location Info
        private void LoadTimeTrialLocationDropDowns()
        {
            LoadTimeTrialRegionList();
            LoadTimeTrialCourseList();
            LoadTimeTrialTrackList();


        }

        private void LoadTimeTrialRegionList(int regionId = 0)
        {
            var regionDao = new RegionDao();
            var regionList = regionDao.GetRegions();

            NewRecord_RegionSelectDropDown.DataSource = regionList;
            NewRecord_RegionSelectDropDown.DisplayMember = "Name";
            NewRecord_RegionSelectDropDown.ValueMember = "RegionId";

            if (regionId > 0)
            {
                var matchingRegion = regionList.FirstOrDefault(r => r.RegionId == regionId);
                NewRecord_RegionSelectDropDown.SelectedItem = matchingRegion;
            }
            else
            {
                NewRecord_RegionSelectDropDown.SelectedIndex = -1;
                NewRecord_RegionSelectDropDown.Text = "";
            }

        }
        private void NewRecord_RegionSelectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NewRecord_RegionSelectDropDown.ContainsFocus)
            {
                var selectedRegion = NewRecord_RegionSelectDropDown.SelectedItem as RegionDto;

                LoadTimeTrialCourseList(selectedRegion.RegionId);
            }
        }

        private void LoadTimeTrialCourseList(int regionId = 0, int trackId = 0)
        {
            var courseDao = new GranTurismoFramework.DataAccess.CourseDao();
            var courseList = courseDao.GetCourses();
            var filteredList = new List<CourseDto>();

            NewRecord_CourseSelectDropDown.DisplayMember = "Name";
            NewRecord_CourseSelectDropDown.ValueMember = "CourseId";

            if (regionId > 0 || trackId > 0)
            {
                var selectedRegion = NewRecord_RegionSelectDropDown.SelectedItem as RegionDto;
                if (selectedRegion != null)
                {
                    filteredList = courseList.Where(course => course.RegionId == selectedRegion.RegionId).ToList();
                    NewRecord_CourseSelectDropDown.SelectedItem = courseList.FirstOrDefault(c => c.RegionId == selectedRegion.RegionId);
                }

                if (trackId > 0)
                {
                    filteredList = courseList.Where(course => course.CourseId == trackId).ToList();
                }

                NewRecord_CourseSelectDropDown.DataSource = filteredList;
            }
            else
            {
                NewRecord_CourseSelectDropDown.DataSource = courseList;

                NewRecord_CourseSelectDropDown.SelectedIndex = -1;
                NewRecord_CourseSelectDropDown.Text = "";
            }
        }
        private void NewRecord_CourseSelectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NewRecord_CourseSelectDropDown.ContainsFocus)
            {
                var selectedCourse = NewRecord_CourseSelectDropDown.SelectedItem as CourseDto;

                LoadTimeTrialRegionList(selectedCourse.RegionId);
                LoadTimeTrialTrackList(selectedCourse.CourseId);
            }
        }

        private void LoadTimeTrialTrackList(int courseId = 0)
        {
            var trackDao = new GranTurismoLibrary.DataAccess.TrackDao();
            var trackList = trackDao.GetAllTrackInfo();
            var filteredList = new List<TrackInfo>();

            NewRecord_TrackSelectDropDown.DisplayMember = "TrackName";
            NewRecord_TrackSelectDropDown.ValueMember = "TrackId";

            if (courseId > 0)
            {
                filteredList = trackList.Where(track => track.CourseId == courseId).ToList();

                NewRecord_TrackSelectDropDown.DataSource = filteredList;
            }
            else
            {
                NewRecord_TrackSelectDropDown.DataSource = null;

                NewRecord_TrackSelectDropDown.SelectedIndex = -1;
                NewRecord_TrackSelectDropDown.Text = "";
            }
        }
        private void NewRecord_TrackSelectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NewRecord_TrackSelectDropDown.ContainsFocus)
            {
                var selectedTrack = NewRecord_TrackSelectDropDown.SelectedItem as TrackInfo;

                LoadInventoryRegionList(selectedTrack.RegionId);
                LoadTimeTrialCourseList(0, selectedTrack.TrackId);
            }
        }


        // Car Info
        private void LoadTimeTrialCarOptions()
        {
            LoadTimeTrialOwnedCars();
            LoadTimeTrialTunes();
            LoadTimeTrialDrivers();
        }

        private void LoadTimeTrialOwnedCars()
        {
            var ownedCarDao = new GranTurismoLibrary.DataAccess.CarDao();
            var ownedCars = ownedCarDao.GetAllOwnedCars();

            NewRecord_CarSelectDropDown.DataSource = ownedCars;
            NewRecord_CarSelectDropDown.DisplayMember = "Nickname";
            NewRecord_CarSelectDropDown.ValueMember = "OwnedCarId";

            NewRecord_CarSelectDropDown.SelectedIndex = -1;
            NewRecord_CarSelectDropDown.Text = "";
        }
        private void NewRecord_CarSelectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NewRecord_CarSelectDropDown.SelectedItem != null)
            {
                var selectedCar = NewRecord_CarSelectDropDown.SelectedItem as OwnedCarInfo;

                if (selectedCar != null)
                {
                    LoadTimeTrialTunes(selectedCar.OwnedCarId);
                }
            }
        }

        private void LoadTimeTrialTunes(int carId = 0)
        {
            var tuneDao = new TuneDao();
            var tunes = tuneDao.GetTunes(carId);

            NewRecord_TuneSelectDropDown.DataSource = tunes;
            NewRecord_TuneSelectDropDown.DisplayMember = "SheetName";
            NewRecord_TuneSelectDropDown.ValueMember = "TuneId";

            NewRecord_TuneSelectDropDown.SelectedIndex = -1;
            NewRecord_TuneSelectDropDown.Text = "";
        }
        private void LoadTimeTrialDrivers()
        {
            var driverDao = new GranTurismoFramework.DataAccess.DriverDao();
            var driverList = driverDao.GetDrivers();

            NewRecord_DriverSelectDropDown.DataSource = driverList;
            NewRecord_DriverSelectDropDown.DisplayMember = "DriverName";
            NewRecord_DriverSelectDropDown.ValueMember = "DriverId";

            NewRecord_DriverSelectDropDown.SelectedIndex = -1;
            NewRecord_DriverSelectDropDown.Text = "";
        }


        // Buttons
        private void NewRecord_StartSessionButton_Click(object sender, EventArgs e)
        {
            if (!_sessionStarted)
            {
                // Start Session

                _sessionStarted = true;
                NewRecord_StartSessionButton.Text = "End Session";

                NewRecord_RegionSelectDropDown.Enabled = false;
                NewRecord_CourseSelectDropDown.Enabled = false;
                NewRecord_TrackSelectDropDown.Enabled = false;
                NewRecord_CarSelectDropDown.Enabled = false;

                NewRecord_SaveRecordButton.Enabled = true;

                _sessionTimeTrials = new List<TimeTrialInfo>();
            }
            else
            {
                // End Session

                _sessionStarted = false;
                NewRecord_StartSessionButton.Text = "Start Session";

                NewRecord_RegionSelectDropDown.Enabled = true;
                NewRecord_CourseSelectDropDown.Enabled = true;
                NewRecord_TrackSelectDropDown.Enabled = true;
                NewRecord_CarSelectDropDown.Enabled = true;

                NewRecord_SaveRecordButton.Enabled = false;
            }
        }

        private void NewRecord_SaveRecordButton_Click(object sender, EventArgs e)
        {
            var timeTrial = new TimeTrialInfo()
            {
                Region = NewRecord_RegionSelectDropDown.SelectedItem as RegionDto,
                Course = NewRecord_CourseSelectDropDown.SelectedItem as CourseDto,
                Track = NewRecord_TrackSelectDropDown.SelectedItem as TrackInfo,

                Time = NewRecord_TimeSpanPicker.Value,
                OwnedCarInfo = NewRecord_CarSelectDropDown.SelectedItem as OwnedCarInfo,
                TuneInfo = NewRecord_TuneSelectDropDown.SelectedItem as TuneInfo,
                Driver = NewRecord_DriverSelectDropDown.SelectedItem as DriverDto                
            };

            _sessionTimeTrials.Add(timeTrial);
        }




        /* ===== Inventory Tab ===== */

        #region InventoryTab

        private async Task LoadInventoryTab()
        {
            await Task.Run(() =>
            {
                LoadOwnedCarList();

                // Add Car Panel
                AddCar_RegionDropDown.Invoke(() => LoadInventoryRegionList());
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
        private void LoadInventoryRegionList(int manufacturerId = 0, int modelId = 0)
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

                    LoadInventoryRegionList(selectedManufacturer, 0);
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

                    LoadInventoryRegionList(0, selectedCarInfo);
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

        #endregion InventoryTab


    }
}