#define TRACE_ON
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
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

        Word.Application application;
        Word.Document document;

        #endregion

        public ShowHorseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [Conditional("TRACE_ON")]
        [Conditional("DEBUG")]
        public async void OnPageLoad()
        {
            ParentsVis = false;
            AddProgressionVisible = false;

            AddProgressionButtonText = "Добавить";

            await Task.Run(() =>
            {
                try
                {
                    MainHorse = (Horse)_navigationService.Parameter;
                    if (MainHorse == null)
                    {
                        MainHorse = new Horse();
                        MainHorse.ID = 0;
                        Console.WriteLine("Ошибка при загрузке данных лошади");
                    }
                    SelectedHorse = Horse.GetSelectedHorseAsync(MainHorse.ID).Result;
                    Console.WriteLine("Данные лошади загружены");

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
                    RaisePropertyChanged(() => MainHorseProgression);

                    _mainHorseScoring = Scoring.GetSelectedScoring(SelectedHorse.ID).Result;
                    RaisePropertyChanged(() => MainHorseScoring);

                    _mainHorseTribalUses = TribalUse.GetSelectedTribalUse(SelectedHorse.ID).Result;
                    RaisePropertyChanged(() => MainHorseTribalUses);

                }
                catch (Exception ex)
                {
                    if (ex is Newtonsoft.Json.JsonReaderException)
                    {
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Данные отсутствуют!"));
                    }
                    else if (ex is HttpRequestException || ex is SocketException || ex is WebException || ex is AggregateException)
                    {
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                    } else 
                    {
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Данные отсутствуют!"));
                    }
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
                                var path = "";

                                if (SelectedHorse.Gender.Equals("Кобыла"))
                                {
                                    path = System.IO.Path.GetFullPath(@"Files\Word\Карточка племенной кобылы.dotx");
                                }
                                else
                                {
                                    path = System.IO.Path.GetFullPath(@"Files\Word\Карточка жеребца.dotx");
                                }
                                application = new Word.Application();

                                document = application.Documents.Open(path);
                                Word.WdSaveFormat format = Word.WdSaveFormat.wdFormatDocument;

                                document.Bookmarks["GpkNum"].Range.Text = SelectedHorse.GpkNum;
                                document.Bookmarks["NickName"].Range.Text = SelectedHorse.NickName;
                                document.Bookmarks["Brand"].Range.Text = SelectedHorse.Brand;
                                document.Bookmarks["Bloodiness"].Range.Text = SelectedHorse.Bloodiness;                               
                                document.Bookmarks["BirthDate"].Range.Text = SelectedHorse.BirthDate;
                                document.Bookmarks["Color"].Range.Text = SelectedHorse.Color;
                                document.Bookmarks["BirthPlace"].Range.Text = SelectedHorse.BirthPlace;
                                document.Bookmarks["Owner"].Range.Text = SelectedHorse.Owner;
                                document.Bookmarks["Breed"].Range.Text = SelectedHorse.Breed;

                                if (SelectedHorse.MotherID != 0 || SelectedHorse.FatherID != 0)
                                {
                                    if (MotherHorse != null)
                                    {
                                        document.Bookmarks["MotherGpkNum"].Range.Text = MotherHorse.GpkNum;
                                        document.Bookmarks["MotherNickName"].Range.Text = MotherHorse.NickName;
                                        document.Bookmarks["MotherBrand"].Range.Text = MotherHorse.Brand;
                                        document.Bookmarks["MotherColor"].Range.Text = MotherHorse.Color;
                                        document.Bookmarks["MotherBirthDate"].Range.Text = MotherHorse.BirthDate;
                                    }

                                    if (FatherHorse != null)
                                    {
                                        document.Bookmarks["FatherGpkNum"].Range.Text = FatherHorse.GpkNum;
                                        document.Bookmarks["FatherNickName"].Range.Text = FatherHorse.NickName;
                                        document.Bookmarks["FatherBrand"].Range.Text = FatherHorse.Brand;
                                        document.Bookmarks["FatherColor"].Range.Text = FatherHorse.Color;
                                        document.Bookmarks["FatherBirthDate"].Range.Text = FatherHorse.BirthDate;
                                    }
                                }

                                if (MainHorseScoring != null)
                                {
                                    int i = 3;
                                    Word.Table scoringTable = document.Tables[2];
                                    foreach (Scoring s in MainHorseScoring)
                                    {
                                        scoringTable.Cell(i, 1).Range.Text = s.Date;
                                        scoringTable.Cell(i, 2).Range.Text = s.Age;
                                        scoringTable.Cell(i, 3).Range.Text = s.Boniter;
                                        scoringTable.Cell(i, 4).Range.Text = s.Origin.ToString();
                                        scoringTable.Cell(i, 5).Range.Text = s.Typicality.ToString();
                                        scoringTable.Cell(i, 6).Range.Text = s.Measurements.ToString();
                                        scoringTable.Cell(i, 7).Range.Text = s.Exterior.ToString();
                                        scoringTable.Cell(i, 8).Range.Text = s.WorkingCapacity.ToString();
                                        scoringTable.Cell(i, 9).Range.Text = s.OffspringQuality.ToString();
                                        scoringTable.Cell(i, 10).Range.Text = s.TheClass.ToString();
                                        scoringTable.Cell(i, 11).Range.Text = s.Comment;
                                        i++;
                                    }
                                }

                                if (SelectedHorse.Gender.Equals("Кобыла"))
                                {
                                    document.Bookmarks["FullName"].Range.Text = SelectedHorse.FullName;

                                    if (MainHorseTribalUses != null)
                                    {
                                        int i = 2;
                                        Word.Table tribalUseTable = document.Tables[3];
                                        foreach (TribalUse t in MainHorseTribalUses)
                                        {
                                            tribalUseTable.Cell(i, 1).Range.Text = t.Year.ToString();
                                            tribalUseTable.Cell(i, 2).Range.Text = t.LastDate;
                                            tribalUseTable.Cell(i, 3).Range.Text = t.MatingType;
                                            tribalUseTable.Cell(i, 4).Range.Text = t.FatherFullName;
                                            tribalUseTable.Cell(i, 5).Range.Text = t.FatherBreed;
                                            tribalUseTable.Cell(i, 6).Range.Text = t.FatherClass;
                                            tribalUseTable.Cell(i, 7).Range.Text = t.FoalDate;
                                            tribalUseTable.Cell(i, 8).Range.Text = t.FoalGender;
                                            tribalUseTable.Cell(i, 9).Range.Text = t.FoalColor;
                                            tribalUseTable.Cell(i, 10).Range.Text = t.FoalNickName;
                                            tribalUseTable.Cell(i, 11).Range.Text = t.FoalBrand;
                                            tribalUseTable.Cell(i, 12).Range.Text = t.FoalDestination;
                                            i++;
                                        }
                                    }
                                }

                                document.SaveAs2(FileName: magazinesPath + SelectedHorse.FullName, format);
                                document.Close();
                                application.Quit();

                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Карточка лошади успешно создана!"));

                                Word.Application word = new Word.Application();
                                word.Visible = true;
                                var cardPath = System.IO.Path.GetFullPath(magazinesPath + @SelectedHorse.FullName + @".doc");
                                Word.Document card = word.Documents.Open(cardPath);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка при создании карточки лошади!"));
                                document.Close();
                                application.Quit();
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

        private ICommand _deleteHorse;

        public ICommand DeleteHorse
        {
            get
            {
                if (_deleteHorse == null)
                {
                    _deleteHorse = new RelayCommand(() =>
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить данные лошади?\n(Это действие нельзя отменить)", "Удалить лошадь?", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                Horse.DeleteHorseAsync(SelectedHorse.ID);

                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно удалили данные лошади"));

                                MainHorse = null;
                                SelectedHorse = null;
                                MotherHorse = null;
                                FatherHorse = null;
                                MainHorseScoring = null;
                                MainHorseProgression = null;
                                MainHorseTribalUses = null;
                                _navigationService.NavigateTo("HorsesList");
                                break;
                        }
                    });
                }

                return _deleteHorse;
            }

            set
            {
                _deleteHorse = value;
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
                        _navigationService.NavigateTo("ChangeHorsePage", SelectedHorse);
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
                                if (Progression.AddProgressionAsync(AddedProgression.Date, AddedProgression.Destination, AddedProgression.Comment, SelectedHorse.ID).Result)
                                {
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили движение лошади"));
                                    if (AddedProgression.Destination.Equals("продажа") || AddedProgression.Destination.Equals("списание")
                                    || AddedProgression.Destination.Equals("прирезан") || AddedProgression.Destination.Equals("обмен")
                                    || AddedProgression.Destination.Equals("пал"))
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
                        _navigationService.NavigateTo("AddScoringPage", SelectedHorse);
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
                        _navigationService.NavigateTo("AddTribalUsePage", SelectedHorse);
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
