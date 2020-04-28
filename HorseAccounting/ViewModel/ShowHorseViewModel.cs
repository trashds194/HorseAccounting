using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Word = Microsoft.Office.Interop.Word;

namespace HorseAccounting.ViewModel
{
    public class ShowHorseViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private string _horseNick;

        private Horse _mainHorse;
        private Horse _selectedHorse;
        private Horse _motherHorse;
        private Horse _fatherHorse;

        private ObservableCollection<Scoring> _mainHorseScoring;
        private ObservableCollection<TribalUse> _mainHorseTribalUses;
        private ObservableCollection<Progression> _mainHorseProgression;

        private bool _addProgressionVisible;
        private bool _parentsVis;
        private bool _maleVis;
        private bool _stallionVis;

        private string _addProgressionButtonText;

        private Progression _addedProgression;

        #endregion

        public ShowHorseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async void OnPageLoad()
        {
            ParentsVis = false;
            AddProgressionVisible = false;

            AddProgressionButtonText = "Добавить";

            await Task.Run(() =>
            {
                MainHorse = (Horse)_navigationService.Parameter;

                SelectedHorse = Horse.GetSelectedHorseAsync(MainHorse.ID).Result;

                if (SelectedHorse.Gender.Equals("Кобыла"))
                {
                    MaleVis = true;
                    StallionVis = false;
                }
                else
                {
                    MaleVis = false;
                    StallionVis = true;
                }

                try
                {
                    HorseNick = SelectedHorse.FullName;

                    if (SelectedHorse.MotherID != 0 || SelectedHorse.FatherID != 0)
                    {
                        ParentsVis = true;
                        MotherHorse = Horse.GetSelectedHorseAsync(SelectedHorse.MotherID).Result;
                        FatherHorse = Horse.GetSelectedHorseAsync(SelectedHorse.FatherID).Result;
                    }
                    else
                    {
                        ParentsVis = false;
                    }

                    _mainHorseProgression = Progression.GetSelectedProgression(SelectedHorse.ID).Result;
                    _mainHorseTribalUses = TribalUse.GetSelectedTribalUse(SelectedHorse.ID).Result;
                    _mainHorseScoring = Scoring.GetSelectedScoring(SelectedHorse.ID).Result;

                    RaisePropertyChanged(() => MainHorseProgression);
                    RaisePropertyChanged(() => MainHorseScoring);
                    RaisePropertyChanged(() => MainHorseTribalUses);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }).ConfigureAwait(true);
        }

        #region Definitions

        public bool MaleVis
        {
            get
            {
                return _maleVis;
            }

            set
            {
                _maleVis = value;
                RaisePropertyChanged(nameof(MaleVis));
            }
        }

        public bool StallionVis
        {
            get
            {
                return _stallionVis;
            }

            set
            {
                _stallionVis = value;
                RaisePropertyChanged(nameof(StallionVis));
            }
        }

        public bool ParentsVis
        {
            get
            {
                return _parentsVis;
            }

            set
            {
                _parentsVis = value;
                RaisePropertyChanged(nameof(ParentsVis));
            }
        }

        public bool AddProgressionVisible
        {
            get
            {
                return _addProgressionVisible;
            }

            set
            {
                _addProgressionVisible = value;
                RaisePropertyChanged(nameof(AddProgressionVisible));
            }
        }

        public string AddProgressionButtonText
        {
            get
            {
                return _addProgressionButtonText;
            }

            set
            {
                _addProgressionButtonText = value;
                RaisePropertyChanged(nameof(AddProgressionButtonText));
            }
        }

        public Progression AddedProgression
        {
            get
            {
                return _addedProgression;
            }

            set
            {
                _addedProgression = value;
                RaisePropertyChanged(nameof(AddedProgression));
            }
        }

        public string HorseNick
        {
            get
            {
                return _horseNick;
            }

            set
            {
                _horseNick = value;
                RaisePropertyChanged(nameof(HorseNick));
            }
        }


        public ObservableCollection<Scoring> MainHorseScoring
        {
            get
            {
                return _mainHorseScoring;
            }

            set
            {
                _mainHorseScoring = value;
                RaisePropertyChanged(nameof(MainHorseScoring));
            }
        }

        public ObservableCollection<Progression> MainHorseProgression
        {
            get
            {
                return _mainHorseProgression;
            }

            set
            {
                _mainHorseProgression = value;
                RaisePropertyChanged(nameof(MainHorseProgression));
            }
        }

        public ObservableCollection<TribalUse> MainHorseTribalUses
        {
            get
            {
                return _mainHorseTribalUses;
            }

            set
            {
                _mainHorseTribalUses = value;
                RaisePropertyChanged(nameof(MainHorseTribalUses));
            }
        }

        public Horse MainHorse
        {
            get
            {
                return _mainHorse;
            }

            set
            {
                if (_mainHorse != value)
                {
                    _mainHorse = value;
                    RaisePropertyChanged(nameof(MainHorse));
                }
            }
        }

        public Horse SelectedHorse
        {
            get
            {
                return _selectedHorse;
            }

            set
            {
                if (_selectedHorse != value)
                {
                    _selectedHorse = value;
                    RaisePropertyChanged(nameof(SelectedHorse));
                }
            }
        }

        public Horse MotherHorse
        {
            get
            {
                return _motherHorse;
            }

            set
            {
                if (_motherHorse != value)
                {
                    _motherHorse = value;
                    RaisePropertyChanged(nameof(MotherHorse));
                }
            }
        }

        public Horse FatherHorse
        {
            get
            {
                return _fatherHorse;
            }

            set
            {
                if (_fatherHorse != value)
                {
                    _fatherHorse = value;
                    RaisePropertyChanged(nameof(FatherHorse));
                }
            }
        }

        #endregion

        #region MenuCommands

        private ICommand _createHorseCard;

        public ICommand CreateHorseCard
        {
            get
            {
                if (_createHorseCard == null)
                {
                    _createHorseCard = new RelayCommand(() =>
                    {
                        Directory.CreateDirectory(Path.Combine(@"C:\Users\Public\Documents\", "Помощник коневода"));
                        string docPath = @"C:\Users\Public\Documents\Помощник коневода\";
                        Directory.CreateDirectory(Path.Combine(docPath, "Карточки лошадей"));
                        string magazinesPath = @"C:\Users\Public\Documents\Помощник коневода\Карточки лошадей\";

                        if (!File.Exists(magazinesPath + SelectedHorse.FullName + ".doc"))
                        {
                            try
                            {
                                Word.Application application = new Word.Application();
                                application.Visible = true;
                                var path = System.IO.Path.GetFullPath(@"Files\Word\Карточка племенной кобылы.dotx");

                                Word.Document document = application.Documents.Open(path);
                                Word.WdSaveFormat format = Word.WdSaveFormat.wdFormatDocument;

                                document.Bookmarks["GpkNum"].Range.Text = SelectedHorse.GpkNum;
                                document.Bookmarks["NickName"].Range.Text = SelectedHorse.NickName;
                                document.Bookmarks["Brand"].Range.Text = SelectedHorse.Brand;

                                document.SaveAs2(FileName: magazinesPath + SelectedHorse.FullName, format);
                                document.Close();
                                application.Quit();

                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Карточка лошади успешно создана!"));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка при создании карточки лошади!"));
                            }
                        }
                        else
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Файл уже создан. Выполняется открытие файла"));
                            try
                            {
                                Word.Application application = new Word.Application();
                                application.Visible = true;
                                var path = System.IO.Path.GetFullPath(magazinesPath + @SelectedHorse.FullName + @".doc");
                                Word.Document document = application.Documents.Open(path);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка при открытии карточки лошади!"));
                            }
                        }
                    });
                }

                return _createHorseCard;
            }

            set
            {
                _createHorseCard = value;
            }
        }

        private ICommand _openHorseCard;

        public ICommand OpenHorseCard
        {
            get
            {
                if (_openHorseCard == null)
                {
                    _openHorseCard = new RelayCommand(() =>
                    {
                        string magazinesPath = @"C:\Users\Public\Documents\Помощник коневода\Карточки лошадей\";
                        if (File.Exists(magazinesPath + SelectedHorse.FullName + ".doc"))
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Выполняется открытие файла"));
                            try
                            {
                                Word.Application application = new Word.Application();
                                application.Visible = true;
                                var path = System.IO.Path.GetFullPath(magazinesPath + @SelectedHorse.FullName + @".doc");
                                Word.Document document = application.Documents.Open(path);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка при открытии карточки лошади!"));
                            }
                        }
                        else
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Файл отсутствует!"));
                        }
                    });
                }

                return _openHorseCard;
            }

            set
            {
                _openHorseCard = value;
            }
        }

        private ICommand _openCardsFolder;

        public ICommand OpenCardsFolder
        {
            get
            {
                if (_openCardsFolder == null)
                {
                    _openCardsFolder = new RelayCommand(() =>
                    {
                        Process.Start("explorer.exe", @"C:\Users\Public\Documents\Помощник коневода\Карточки лошадей\");
                    });
                }

                return _openCardsFolder;
            }

            set
            {
                _openCardsFolder = value;
            }
        }

        #endregion

        #region WindowCommands

        private ICommand _backToHorsesList;

        public ICommand BackToList
        {
            get
            {
                if (_backToHorsesList == null)
                {
                    _backToHorsesList = new RelayCommand(() =>
                    {
                        MainHorse = null;
                        SelectedHorse = null;
                        MotherHorse = null;
                        FatherHorse = null;
                        MainHorseScoring = null;
                        MainHorseProgression = null;
                        MainHorseTribalUses = null;
                        _navigationService.NavigateTo("HorsesList");
                    });
                }

                return _backToHorsesList;
            }

            set
            {
                _backToHorsesList = value;
            }
        }

        private ICommand _changeHorse;

        public ICommand ChangeHorse
        {
            get
            {
                if (_changeHorse == null)
                {
                    _changeHorse = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("ChangeHorsePage", MainHorse);
                    });
                }

                return _changeHorse;
            }

            set
            {
                _changeHorse = value;
            }
        }

        private ICommand _addProgression;

        public ICommand AddProgression
        {
            get
            {
                if (_addProgression == null)
                {
                    AddedProgression = new Progression();
                    _addProgression = new RelayCommand(() =>
                    {
                        if (AddProgressionButtonText.Equals("Добавить"))
                        {
                            AddProgressionVisible = true;
                            AddProgressionButtonText = "Сохранить";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(AddedProgression.Date))
                            {
                                if (Progression.AddProgressionAsync(AddedProgression.Date, AddedProgression.Destination, AddedProgression.Comment, MainHorse.ID).Result)
                                {
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили движение лошади"));
                                    if (AddedProgression.Destination.Equals("продажа") || AddedProgression.Destination.Equals("списание")
                                    || AddedProgression.Destination.Equals("прирезан") || AddedProgression.Destination.Equals("обмен"))
                                    {
                                        if (SelectedHorse.Gender.Equals("Жеребец"))
                                        {
                                            Horse.ChangeHorseStateAsync(SelectedHorse.ID, "Выбыл");
                                        }
                                        else
                                        {
                                            Horse.ChangeHorseStateAsync(SelectedHorse.ID, "Выбыла");
                                        }
                                    }
                                    else
                                    {
                                        if (SelectedHorse.Gender.Equals("Жеребец"))
                                        {
                                            Horse.ChangeHorseStateAsync(SelectedHorse.ID, "Действующий");
                                        }
                                        else
                                        {
                                            Horse.ChangeHorseStateAsync(SelectedHorse.ID, "Действующая");
                                        }
                                    }
                                    AddedProgression.CleanProgressionData();
                                    AddProgressionVisible = false;
                                    AddProgressionButtonText = "Добавить";
                                    _mainHorseProgression = Progression.GetSelectedProgression(SelectedHorse.ID).Result;
                                    RaisePropertyChanged(nameof(MainHorseProgression));
                                    SelectedHorse = Horse.GetSelectedHorseAsync(MainHorse.ID).Result;
                                    RaisePropertyChanged(nameof(SelectedHorse));
                                }
                                else
                                {
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка при добавлении движения, проверьте корректность введенных данных!"));
                                }
                            }
                        }
                    });
                }

                return _addProgression;
            }
        }

        private ICommand _addScoring;

        public ICommand AddScoring
        {
            get
            {
                if (_addScoring == null)
                {
                    _addScoring = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("AddScoringPage", MainHorse);
                    });
                }

                return _addScoring;
            }

            set
            {
                _addScoring = value;
            }
        }

        private ICommand _addTribalUse;

        public ICommand AddTribalUse
        {
            get
            {
                if (_addTribalUse == null)
                {
                    _addTribalUse = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("AddTribalUsePage", MainHorse);
                    });
                }

                return _addTribalUse;
            }

            set
            {
                _addTribalUse = value;
            }
        }

        #endregion
    }
}
