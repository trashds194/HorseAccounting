using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;

namespace HorseAccounting.ViewModel
{
    public class TaleMagazineViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private Horse _stallionHorse;

        private Horse _mare1Horse;
        private Horse _mare2Horse;
        private Horse _mare3Horse;
        private Horse _mare4Horse;
        private Horse _mare5Horse;
        private Horse _mare6Horse;
        private Horse _mare7Horse;
        private Horse _mare8Horse;
        private Horse _mare9Horse;
        private Horse _mare10Horse;
        private Horse _mare11Horse;
        private Horse _mare12Horse;
        private Horse _mare13Horse;
        private Horse _mare14Horse;
        private Horse _mare15Horse;
        private Horse _mare16Horse;
        private Horse _mare17Horse;
        private Horse _mare18Horse;
        private Horse _mare19Horse;
        private Horse _mare20Horse;

        private Horse _mother1Horse;
        private Horse _mother2Horse;
        private Horse _mother3Horse;
        private Horse _mother4Horse;
        private Horse _mother5Horse;
        private Horse _mother6Horse;
        private Horse _mother7Horse;
        private Horse _mother8Horse;
        private Horse _mother9Horse;
        private Horse _mother10Horse;
        private Horse _mother11Horse;
        private Horse _mother12Horse;
        private Horse _mother13Horse;
        private Horse _mother14Horse;
        private Horse _mother15Horse;
        private Horse _mother16Horse;
        private Horse _mother17Horse;
        private Horse _mother18Horse;
        private Horse _mother19Horse;
        private Horse _mother20Horse;

        private Horse _father1Horse;
        private Horse _father2Horse;
        private Horse _father3Horse;
        private Horse _father4Horse;
        private Horse _father5Horse;
        private Horse _father6Horse;
        private Horse _father7Horse;
        private Horse _father8Horse;
        private Horse _father9Horse;
        private Horse _father10Horse;
        private Horse _father11Horse;
        private Horse _father12Horse;
        private Horse _father13Horse;
        private Horse _father14Horse;
        private Horse _father15Horse;
        private Horse _father16Horse;
        private Horse _father17Horse;
        private Horse _father18Horse;
        private Horse _father19Horse;
        private Horse _father20Horse;

        private ObservableCollection<Horse> _stallionHorseList;
        private ObservableCollection<Horse> _mareHorseList;

        private DateTime _choosenDate;
        private string _year;

        #endregion

        public TaleMagazineViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async void OnPageLoad()
        {
            await Task.Run(() =>
            {
                try
                {
                    _stallionHorseList = Horse.GetFatherHorseAsync().Result;
                    _mareHorseList = Horse.GetMotherHorseAsync().Result;

                    RaisePropertyChanged(() => StallionHorseList);
                    RaisePropertyChanged(() => MareHorseList);
                }
                catch (Exception ex)
                {
                    if (ex is HttpRequestException || ex is SocketException || ex is WebException || ex is AggregateException)
                    {
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                    }
                }
                ChoosenDate = DateTime.Today;
            }).ConfigureAwait(true);
        }

        private void ClearHorses()
        {
            StallionHorse = null;

            Mare1Horse = null;
            Mare2Horse = null;
            Mare3Horse = null;
            Mare4Horse = null;
            Mare5Horse = null;
            Mare6Horse = null;
            Mare7Horse = null;
            Mare8Horse = null;
            Mare9Horse = null;
            Mare10Horse = null;
            Mare11Horse = null;
            Mare12Horse = null;
            Mare13Horse = null;
            Mare14Horse = null;
            Mare15Horse = null;
            Mare16Horse = null;
            Mare17Horse = null;
            Mare18Horse = null;
            Mare19Horse = null;
            Mare20Horse = null;

            Mother1Horse = null;
            Mother2Horse = null;
            Mother3Horse = null;
            Mother4Horse = null;
            Mother5Horse = null;
            Mother6Horse = null;
            Mother7Horse = null;
            Mother8Horse = null;
            Mother9Horse = null;
            Mother10Horse = null;
            Mother11Horse = null;
            Mother12Horse = null;
            Mother13Horse = null;
            Mother14Horse = null;
            Mother15Horse = null;
            Mother16Horse = null;
            Mother17Horse = null;
            Mother18Horse = null;
            Mother19Horse = null;
            Mother20Horse = null;

            Father1Horse = null;
            Father2Horse = null;
            Father3Horse = null;
            Father4Horse = null;
            Father5Horse = null;
            Father6Horse = null;
            Father7Horse = null;
            Father8Horse = null;
            Father9Horse = null;
            Father10Horse = null;
            Father11Horse = null;
            Father12Horse = null;
            Father13Horse = null;
            Father14Horse = null;
            Father15Horse = null;
            Father16Horse = null;
            Father17Horse = null;
            Father18Horse = null;
            Father19Horse = null;
            Father20Horse = null;
        }

        #region Definitions

        public DateTime ChoosenDate
        {
            get
            {
                return _choosenDate;
            }

            set
            {
                _choosenDate = value;
                RaisePropertyChanged(nameof(ChoosenDate));
            }
        }

        public string Year
        {
            get
            {
                return _year;
            }

            set
            {
                _year = value;
                RaisePropertyChanged(nameof(Year));
            }
        }

        public Horse StallionHorse
        {
            get
            {
                return _stallionHorse;
            }

            set
            {
                if (_stallionHorse != value)
                {
                    _stallionHorse = value;
                    RaisePropertyChanged(nameof(StallionHorse));
                }
            }
        }

        #region MareHorses

        public Horse Mare1Horse
        {
            get
            {
                return _mare1Horse;
            }

            set
            {
                if (_mare1Horse != value)
                {
                    _mare1Horse = value;
                    RaisePropertyChanged(nameof(Mare1Horse));
                }
            }
        }

        public Horse Mare2Horse
        {
            get
            {
                return _mare2Horse;
            }

            set
            {
                if (_mare2Horse != value)
                {
                    _mare2Horse = value;
                    RaisePropertyChanged(nameof(Mare2Horse));
                }
            }
        }

        public Horse Mare3Horse
        {
            get
            {
                return _mare3Horse;
            }

            set
            {
                if (_mare3Horse != value)
                {
                    _mare3Horse = value;
                    RaisePropertyChanged(nameof(Mare3Horse));
                }
            }
        }

        public Horse Mare4Horse
        {
            get
            {
                return _mare4Horse;
            }

            set
            {
                if (_mare4Horse != value)
                {
                    _mare4Horse = value;
                    RaisePropertyChanged(nameof(Mare4Horse));
                }
            }
        }

        public Horse Mare5Horse
        {
            get
            {
                return _mare5Horse;
            }

            set
            {
                if (_mare5Horse != value)
                {
                    _mare5Horse = value;
                    RaisePropertyChanged(nameof(Mare5Horse));
                }
            }
        }

        public Horse Mare6Horse
        {
            get
            {
                return _mare6Horse;
            }

            set
            {
                if (_mare6Horse != value)
                {
                    _mare6Horse = value;
                    RaisePropertyChanged(nameof(Mare6Horse));
                }
            }
        }

        public Horse Mare7Horse
        {
            get
            {
                return _mare7Horse;
            }

            set
            {
                if (_mare7Horse != value)
                {
                    _mare7Horse = value;
                    RaisePropertyChanged(nameof(Mare7Horse));
                }
            }
        }

        public Horse Mare8Horse
        {
            get
            {
                return _mare8Horse;
            }

            set
            {
                if (_mare8Horse != value)
                {
                    _mare8Horse = value;
                    RaisePropertyChanged(nameof(Mare8Horse));
                }
            }
        }

        public Horse Mare9Horse
        {
            get
            {
                return _mare9Horse;
            }

            set
            {
                if (_mare9Horse != value)
                {
                    _mare9Horse = value;
                    RaisePropertyChanged(nameof(Mare9Horse));
                }
            }
        }

        public Horse Mare10Horse
        {
            get
            {
                return _mare10Horse;
            }

            set
            {
                if (_mare10Horse != value)
                {
                    _mare10Horse = value;
                    RaisePropertyChanged(nameof(Mare10Horse));
                }
            }
        }

        public Horse Mare11Horse
        {
            get
            {
                return _mare11Horse;
            }

            set
            {
                if (_mare11Horse != value)
                {
                    _mare11Horse = value;
                    RaisePropertyChanged(nameof(Mare11Horse));
                }
            }
        }

        public Horse Mare12Horse
        {
            get
            {
                return _mare12Horse;
            }

            set
            {
                if (_mare12Horse != value)
                {
                    _mare12Horse = value;
                    RaisePropertyChanged(nameof(Mare12Horse));
                }
            }
        }

        public Horse Mare13Horse
        {
            get
            {
                return _mare13Horse;
            }

            set
            {
                if (_mare13Horse != value)
                {
                    _mare13Horse = value;
                    RaisePropertyChanged(nameof(Mare13Horse));
                }
            }
        }

        public Horse Mare14Horse
        {
            get
            {
                return _mare14Horse;
            }

            set
            {
                if (_mare14Horse != value)
                {
                    _mare14Horse = value;
                    RaisePropertyChanged(nameof(Mare14Horse));
                }
            }
        }

        public Horse Mare15Horse
        {
            get
            {
                return _mare15Horse;
            }

            set
            {
                if (_mare15Horse != value)
                {
                    _mare15Horse = value;
                    RaisePropertyChanged(nameof(Mare15Horse));
                }
            }
        }

        public Horse Mare16Horse
        {
            get
            {
                return _mare16Horse;
            }

            set
            {
                if (_mare16Horse != value)
                {
                    _mare16Horse = value;
                    RaisePropertyChanged(nameof(Mare16Horse));
                }
            }
        }

        public Horse Mare17Horse
        {
            get
            {
                return _mare17Horse;
            }

            set
            {
                if (_mare17Horse != value)
                {
                    _mare17Horse = value;
                    RaisePropertyChanged(nameof(Mare17Horse));
                }
            }
        }

        public Horse Mare18Horse
        {
            get
            {
                return _mare18Horse;
            }

            set
            {
                if (_mare18Horse != value)
                {
                    _mare18Horse = value;
                    RaisePropertyChanged(nameof(Mare18Horse));
                }
            }
        }

        public Horse Mare19Horse
        {
            get
            {
                return _mare19Horse;
            }

            set
            {
                if (_mare19Horse != value)
                {
                    _mare19Horse = value;
                    RaisePropertyChanged(nameof(Mare19Horse));
                }
            }
        }

        public Horse Mare20Horse
        {
            get
            {
                return _mare20Horse;
            }

            set
            {
                if (_mare20Horse != value)
                {
                    _mare20Horse = value;
                    RaisePropertyChanged(nameof(Mare20Horse));
                }
            }
        }

        #endregion

        #region MotherHorses

        public Horse Mother1Horse
        {
            get
            {
                return _mother1Horse;
            }

            set
            {
                if (_mother1Horse != value)
                {
                    _mother1Horse = value;
                    RaisePropertyChanged(nameof(Mother1Horse));
                }
            }
        }

        public Horse Mother2Horse
        {
            get
            {
                return _mother2Horse;
            }

            set
            {
                if (_mother2Horse != value)
                {
                    _mother2Horse = value;
                    RaisePropertyChanged(nameof(Mother2Horse));
                }
            }
        }

        public Horse Mother3Horse
        {
            get
            {
                return _mother3Horse;
            }

            set
            {
                if (_mother3Horse != value)
                {
                    _mother3Horse = value;
                    RaisePropertyChanged(nameof(Mother3Horse));
                }
            }
        }

        public Horse Mother4Horse
        {
            get
            {
                return _mother4Horse;
            }

            set
            {
                if (_mother4Horse != value)
                {
                    _mother4Horse = value;
                    RaisePropertyChanged(nameof(Mother4Horse));
                }
            }
        }

        public Horse Mother5Horse
        {
            get
            {
                return _mother5Horse;
            }

            set
            {
                if (_mother5Horse != value)
                {
                    _mother5Horse = value;
                    RaisePropertyChanged(nameof(Mother5Horse));
                }
            }
        }

        public Horse Mother6Horse
        {
            get
            {
                return _mother6Horse;
            }

            set
            {
                if (_mother6Horse != value)
                {
                    _mother6Horse = value;
                    RaisePropertyChanged(nameof(Mother6Horse));
                }
            }
        }

        public Horse Mother7Horse
        {
            get
            {
                return _mother7Horse;
            }

            set
            {
                if (_mother7Horse != value)
                {
                    _mother7Horse = value;
                    RaisePropertyChanged(nameof(Mother7Horse));
                }
            }
        }

        public Horse Mother8Horse
        {
            get
            {
                return _mother8Horse;
            }

            set
            {
                if (_mother8Horse != value)
                {
                    _mother8Horse = value;
                    RaisePropertyChanged(nameof(Mother8Horse));
                }
            }
        }

        public Horse Mother9Horse
        {
            get
            {
                return _mother9Horse;
            }

            set
            {
                if (_mother9Horse != value)
                {
                    _mother9Horse = value;
                    RaisePropertyChanged(nameof(Mother9Horse));
                }
            }
        }

        public Horse Mother10Horse
        {
            get
            {
                return _mother10Horse;
            }

            set
            {
                if (_mother10Horse != value)
                {
                    _mother10Horse = value;
                    RaisePropertyChanged(nameof(Mother10Horse));
                }
            }
        }

        public Horse Mother11Horse
        {
            get
            {
                return _mother11Horse;
            }

            set
            {
                if (_mother11Horse != value)
                {
                    _mother11Horse = value;
                    RaisePropertyChanged(nameof(Mother11Horse));
                }
            }
        }

        public Horse Mother12Horse
        {
            get
            {
                return _mother12Horse;
            }

            set
            {
                if (_mother12Horse != value)
                {
                    _mother12Horse = value;
                    RaisePropertyChanged(nameof(Mother12Horse));
                }
            }
        }

        public Horse Mother13Horse
        {
            get
            {
                return _mother13Horse;
            }

            set
            {
                if (_mother13Horse != value)
                {
                    _mother13Horse = value;
                    RaisePropertyChanged(nameof(Mother13Horse));
                }
            }
        }

        public Horse Mother14Horse
        {
            get
            {
                return _mother14Horse;
            }

            set
            {
                if (_mother14Horse != value)
                {
                    _mother14Horse = value;
                    RaisePropertyChanged(nameof(Mother14Horse));
                }
            }
        }

        public Horse Mother15Horse
        {
            get
            {
                return _mother15Horse;
            }

            set
            {
                if (_mother15Horse != value)
                {
                    _mother15Horse = value;
                    RaisePropertyChanged(nameof(Mother15Horse));
                }
            }
        }

        public Horse Mother16Horse
        {
            get
            {
                return _mother16Horse;
            }

            set
            {
                if (_mother16Horse != value)
                {
                    _mother16Horse = value;
                    RaisePropertyChanged(nameof(Mother16Horse));
                }
            }
        }

        public Horse Mother17Horse
        {
            get
            {
                return _mother17Horse;
            }

            set
            {
                if (_mother17Horse != value)
                {
                    _mother17Horse = value;
                    RaisePropertyChanged(nameof(Mother17Horse));
                }
            }
        }

        public Horse Mother18Horse
        {
            get
            {
                return _mother18Horse;
            }

            set
            {
                if (_mother18Horse != value)
                {
                    _mother18Horse = value;
                    RaisePropertyChanged(nameof(Mother18Horse));
                }
            }
        }

        public Horse Mother19Horse
        {
            get
            {
                return _mother19Horse;
            }

            set
            {
                if (_mother19Horse != value)
                {
                    _mother19Horse = value;
                    RaisePropertyChanged(nameof(Mother19Horse));
                }
            }
        }

        public Horse Mother20Horse
        {
            get
            {
                return _mother20Horse;
            }

            set
            {
                if (_mother20Horse != value)
                {
                    _mother20Horse = value;
                    RaisePropertyChanged(nameof(Mother20Horse));
                }
            }
        }

        #endregion

        #region FatherHorses

        public Horse Father1Horse
        {
            get
            {
                return _father1Horse;
            }

            set
            {
                if (_father1Horse != value)
                {
                    _father1Horse = value;
                    RaisePropertyChanged(nameof(Father1Horse));
                }
            }
        }

        public Horse Father2Horse
        {
            get
            {
                return _father2Horse;
            }

            set
            {
                if (_father2Horse != value)
                {
                    _father2Horse = value;
                    RaisePropertyChanged(nameof(Father2Horse));
                }
            }
        }

        public Horse Father3Horse
        {
            get
            {
                return _father3Horse;
            }

            set
            {
                if (_father3Horse != value)
                {
                    _father3Horse = value;
                    RaisePropertyChanged(nameof(Father3Horse));
                }
            }
        }

        public Horse Father4Horse
        {
            get
            {
                return _father4Horse;
            }

            set
            {
                if (_father4Horse != value)
                {
                    _father4Horse = value;
                    RaisePropertyChanged(nameof(Father4Horse));
                }
            }
        }

        public Horse Father5Horse
        {
            get
            {
                return _father5Horse;
            }

            set
            {
                if (_father5Horse != value)
                {
                    _father5Horse = value;
                    RaisePropertyChanged(nameof(Father5Horse));
                }
            }
        }

        public Horse Father6Horse
        {
            get
            {
                return _father6Horse;
            }

            set
            {
                if (_father6Horse != value)
                {
                    _father6Horse = value;
                    RaisePropertyChanged(nameof(Father6Horse));
                }
            }
        }

        public Horse Father7Horse
        {
            get
            {
                return _father7Horse;
            }

            set
            {
                if (_father7Horse != value)
                {
                    _father7Horse = value;
                    RaisePropertyChanged(nameof(Father7Horse));
                }
            }
        }

        public Horse Father8Horse
        {
            get
            {
                return _father8Horse;
            }

            set
            {
                if (_father8Horse != value)
                {
                    _father8Horse = value;
                    RaisePropertyChanged(nameof(Father8Horse));
                }
            }
        }

        public Horse Father9Horse
        {
            get
            {
                return _father9Horse;
            }

            set
            {
                if (_father9Horse != value)
                {
                    _father9Horse = value;
                    RaisePropertyChanged(nameof(Father9Horse));
                }
            }
        }

        public Horse Father10Horse
        {
            get
            {
                return _father10Horse;
            }

            set
            {
                if (_father10Horse != value)
                {
                    _father10Horse = value;
                    RaisePropertyChanged(nameof(Father10Horse));
                }
            }
        }

        public Horse Father11Horse
        {
            get
            {
                return _father11Horse;
            }

            set
            {
                if (_father11Horse != value)
                {
                    _father11Horse = value;
                    RaisePropertyChanged(nameof(Father11Horse));
                }
            }
        }

        public Horse Father12Horse
        {
            get
            {
                return _father12Horse;
            }

            set
            {
                if (_father12Horse != value)
                {
                    _father12Horse = value;
                    RaisePropertyChanged(nameof(Father12Horse));
                }
            }
        }

        public Horse Father13Horse
        {
            get
            {
                return _father13Horse;
            }

            set
            {
                if (_father13Horse != value)
                {
                    _father13Horse = value;
                    RaisePropertyChanged(nameof(Father13Horse));
                }
            }
        }

        public Horse Father14Horse
        {
            get
            {
                return _father14Horse;
            }

            set
            {
                if (_father14Horse != value)
                {
                    _father14Horse = value;
                    RaisePropertyChanged(nameof(Father14Horse));
                }
            }
        }

        public Horse Father15Horse
        {
            get
            {
                return _father15Horse;
            }

            set
            {
                if (_father15Horse != value)
                {
                    _father15Horse = value;
                    RaisePropertyChanged(nameof(Father15Horse));
                }
            }
        }

        public Horse Father16Horse
        {
            get
            {
                return _father16Horse;
            }

            set
            {
                if (_father16Horse != value)
                {
                    _father16Horse = value;
                    RaisePropertyChanged(nameof(Father16Horse));
                }
            }
        }

        public Horse Father17Horse
        {
            get
            {
                return _father17Horse;
            }

            set
            {
                if (_father17Horse != value)
                {
                    _father17Horse = value;
                    RaisePropertyChanged(nameof(Father17Horse));
                }
            }
        }

        public Horse Father18Horse
        {
            get
            {
                return _father18Horse;
            }

            set
            {
                if (_father18Horse != value)
                {
                    _father18Horse = value;
                    RaisePropertyChanged(nameof(Father18Horse));
                }
            }
        }

        public Horse Father19Horse
        {
            get
            {
                return _father19Horse;
            }

            set
            {
                if (_father19Horse != value)
                {
                    _father19Horse = value;
                    RaisePropertyChanged(nameof(Father19Horse));
                }
            }
        }

        public Horse Father20Horse
        {
            get
            {
                return _father20Horse;
            }

            set
            {
                if (_father20Horse != value)
                {
                    _father20Horse = value;
                    RaisePropertyChanged(nameof(Father20Horse));
                }
            }
        }

        #endregion

        public ObservableCollection<Horse> StallionHorseList
        {
            get
            {
                return _stallionHorseList;
            }
        }

        public ObservableCollection<Horse> MareHorseList
        {
            get
            {
                return _mareHorseList;
            }
        }

        #endregion

        #region WindowCommands

        private ICommand _backToHorse;

        public ICommand Back
        {
            get
            {
                if (_backToHorse == null)
                {
                    _backToHorse = new RelayCommand(() =>
                    {
                        ClearHorses();
                        _navigationService.NavigateTo("HorsesList");
                    });
                }

                return _backToHorse;
            }

            set
            {
                _backToHorse = value;
            }
        }


        private ICommand _createTaleMagazine;

        public ICommand CreateTaleMagazine
        {
            get
            {
                if (_createTaleMagazine == null)
                {
                    _createTaleMagazine = new RelayCommand(() =>
                    {
                        if (StallionHorse != null)
                        {
                            Year = ChoosenDate.ToString("yyyy");
                            Directory.CreateDirectory(Path.Combine(@"C:\Users\Public\Documents\", "Помощник коневода"));
                            string docPath = @"C:\Users\Public\Documents\Помощник коневода\";
                            Directory.CreateDirectory(Path.Combine(docPath, "Журналы случки"));
                            string magazinesPath = @"C:\Users\Public\Documents\Помощник коневода\Журналы случки\";
                            if (!File.Exists(magazinesPath + Year + @"\" + StallionHorse.FullName + ".xlsx"))
                            {
                                Directory.CreateDirectory(Path.Combine(magazinesPath, Year));

                                Excel.Application application = new Excel.Application();
                                application.Visible = true;

                                Excel.Workbook workbook;
                                Excel.Worksheet worksheet;
                                var path = System.IO.Path.GetFullPath(@"Files\Excel\Журнал случки.xlsx");
                                workbook = application.Workbooks.Open(path, 0, false);

                                worksheet = (Excel.Worksheet)workbook.Sheets[1];
                                worksheet.Name = StallionHorse.FullName;

                                try
                                {
                                    worksheet.Cells[1, 1].Value = StallionHorse.FullName;
                                    if (Mare1Horse != null)
                                    {
                                        worksheet.Cells[3, 2].Value = Mare1Horse.FullName;
                                        if (Mare1Horse.MotherID != 0)
                                        {
                                            Mother1Horse = Horse.GetSelectedHorseAsync(Mare1Horse.MotherID).Result;
                                            worksheet.Cells[4, 2].Value = Mother1Horse.FullName;
                                        }
                                        if (Mare1Horse.FatherID != 0)
                                        {
                                            Father1Horse = Horse.GetSelectedHorseAsync(Mare1Horse.FatherID).Result;
                                            worksheet.Cells[4, 3].Value = Father1Horse.FullName;
                                        }
                                    }
                                    if (Mare2Horse != null)
                                    {
                                        worksheet.Cells[8, 2].Value = Mare2Horse.FullName;
                                        if (Mare2Horse.MotherID != 0)
                                        {
                                            Mother2Horse = Horse.GetSelectedHorseAsync(Mare2Horse.MotherID).Result;
                                            worksheet.Cells[9, 2].Value = Mother2Horse.FullName;
                                        }
                                        if (Mare2Horse.FatherID != 0)
                                        {
                                            Father2Horse = Horse.GetSelectedHorseAsync(Mare2Horse.FatherID).Result;
                                            worksheet.Cells[9, 3].Value = Father2Horse.FullName;
                                        }
                                    }
                                    if (Mare3Horse != null)
                                    {
                                        worksheet.Cells[13, 2].Value = Mare3Horse.FullName;
                                        if (Mare3Horse.MotherID != 0)
                                        {
                                            Mother3Horse = Horse.GetSelectedHorseAsync(Mare3Horse.MotherID).Result;
                                            worksheet.Cells[14, 2].Value = Mother3Horse.FullName;
                                        }
                                        if (Mare3Horse.FatherID != 0)
                                        {
                                            Father3Horse = Horse.GetSelectedHorseAsync(Mare3Horse.FatherID).Result;
                                            worksheet.Cells[14, 3].Value = Father3Horse.FullName;
                                        }
                                    }
                                    if (Mare4Horse != null)
                                    {
                                        worksheet.Cells[18, 2].Value = Mare4Horse.FullName;
                                        if (Mare4Horse.MotherID != 0)
                                        {
                                            Mother4Horse = Horse.GetSelectedHorseAsync(Mare4Horse.MotherID).Result;
                                            worksheet.Cells[19, 2].Value = Mother4Horse.FullName;
                                        }
                                        if (Mare4Horse.FatherID != 0)
                                        {
                                            Father4Horse = Horse.GetSelectedHorseAsync(Mare4Horse.FatherID).Result;
                                            worksheet.Cells[19, 3].Value = Father4Horse.FullName;
                                        }
                                    }
                                    if (Mare5Horse != null)
                                    {
                                        worksheet.Cells[23, 2].Value = Mare5Horse.FullName;
                                        if (Mare5Horse.MotherID != 0)
                                        {
                                            Mother5Horse = Horse.GetSelectedHorseAsync(Mare5Horse.MotherID).Result;
                                            worksheet.Cells[24, 2].Value = Mother5Horse.FullName;
                                        }
                                        if (Mare5Horse.FatherID != 0)
                                        {
                                            Father5Horse = Horse.GetSelectedHorseAsync(Mare5Horse.FatherID).Result;
                                            worksheet.Cells[24, 3].Value = Father5Horse.FullName;
                                        }
                                    }
                                    if (Mare6Horse != null)
                                    {
                                        worksheet.Cells[28, 2].Value = Mare6Horse.FullName;
                                        if (Mare6Horse.MotherID != 0)
                                        {
                                            Mother6Horse = Horse.GetSelectedHorseAsync(Mare6Horse.MotherID).Result;
                                            worksheet.Cells[29, 2].Value = Mother6Horse.FullName;
                                        }
                                        if (Mare6Horse.FatherID != 0)
                                        {
                                            Father6Horse = Horse.GetSelectedHorseAsync(Mare6Horse.FatherID).Result;
                                            worksheet.Cells[29, 3].Value = Father6Horse.FullName;
                                        }
                                    }
                                    if (Mare7Horse != null)
                                    {
                                        worksheet.Cells[33, 2].Value = Mare7Horse.FullName;
                                        if (Mare7Horse.MotherID != 0)
                                        {
                                            Mother7Horse = Horse.GetSelectedHorseAsync(Mare7Horse.MotherID).Result;
                                            worksheet.Cells[34, 2].Value = Mother7Horse.FullName;
                                        }
                                        if (Mare7Horse.FatherID != 0)
                                        {
                                            Father7Horse = Horse.GetSelectedHorseAsync(Mare7Horse.FatherID).Result;
                                            worksheet.Cells[34, 3].Value = Father7Horse.FullName;
                                        }
                                    }
                                    if (Mare8Horse != null)
                                    {
                                        worksheet.Cells[38, 2].Value = Mare8Horse.FullName;
                                        if (Mare8Horse.MotherID != 0)
                                        {
                                            Mother8Horse = Horse.GetSelectedHorseAsync(Mare8Horse.MotherID).Result;
                                            worksheet.Cells[39, 2].Value = Mother8Horse.FullName;
                                        }
                                        if (Mare8Horse.FatherID != 0)
                                        {
                                            Father8Horse = Horse.GetSelectedHorseAsync(Mare8Horse.FatherID).Result;
                                            worksheet.Cells[39, 3].Value = Father8Horse.FullName;
                                        }
                                    }
                                    if (Mare9Horse != null)
                                    {
                                        worksheet.Cells[43, 2].Value = Mare9Horse.FullName;
                                        if (Mare9Horse.MotherID != 0)
                                        {
                                            Mother9Horse = Horse.GetSelectedHorseAsync(Mare9Horse.MotherID).Result;
                                            worksheet.Cells[44, 2].Value = Mother9Horse.FullName;
                                        }
                                        if (Mare9Horse.FatherID != 0)
                                        {
                                            Father9Horse = Horse.GetSelectedHorseAsync(Mare9Horse.FatherID).Result;
                                            worksheet.Cells[44, 3].Value = Father9Horse.FullName;
                                        }
                                    }
                                    if (Mare10Horse != null)
                                    {
                                        worksheet.Cells[48, 2].Value = Mare10Horse.FullName;
                                        if (Mare10Horse.MotherID != 0)
                                        {
                                            Mother10Horse = Horse.GetSelectedHorseAsync(Mare10Horse.MotherID).Result;
                                            worksheet.Cells[49, 2].Value = Mother10Horse.FullName;
                                        }
                                        if (Mare10Horse.FatherID != 0)
                                        {
                                            Father10Horse = Horse.GetSelectedHorseAsync(Mare10Horse.FatherID).Result;
                                            worksheet.Cells[49, 3].Value = Father10Horse.FullName;
                                        }
                                    }
                                    if (Mare11Horse != null)
                                    {
                                        worksheet.Cells[53, 2].Value = Mare11Horse.FullName;
                                        if (Mare11Horse.MotherID != 0)
                                        {
                                            Mother11Horse = Horse.GetSelectedHorseAsync(Mare11Horse.MotherID).Result;
                                            worksheet.Cells[54, 2].Value = Mother1Horse.FullName;
                                        }
                                        if (Mare11Horse.FatherID != 0)
                                        {
                                            Father11Horse = Horse.GetSelectedHorseAsync(Mare11Horse.FatherID).Result;
                                            worksheet.Cells[54, 3].Value = Father11Horse.FullName;
                                        }
                                    }
                                    if (Mare12Horse != null)
                                    {
                                        worksheet.Cells[58, 2].Value = Mare12Horse.FullName;
                                        if (Mare12Horse.MotherID != 0)
                                        {
                                            Mother12Horse = Horse.GetSelectedHorseAsync(Mare12Horse.MotherID).Result;
                                            worksheet.Cells[59, 2].Value = Mother12Horse.FullName;
                                        }
                                        if (Mare12Horse.FatherID != 0)
                                        {
                                            Father12Horse = Horse.GetSelectedHorseAsync(Mare12Horse.FatherID).Result;
                                            worksheet.Cells[59, 3].Value = Father12Horse.FullName;
                                        }
                                    }
                                    if (Mare13Horse != null)
                                    {
                                        worksheet.Cells[63, 2].Value = Mare13Horse.FullName;
                                        if (Mare13Horse.MotherID != 0)
                                        {
                                            Mother13Horse = Horse.GetSelectedHorseAsync(Mare13Horse.MotherID).Result;
                                            worksheet.Cells[64, 2].Value = Mother13Horse.FullName;
                                        }
                                        if (Mare13Horse.FatherID != 0)
                                        {
                                            Father13Horse = Horse.GetSelectedHorseAsync(Mare13Horse.FatherID).Result;
                                            worksheet.Cells[64, 3].Value = Father13Horse.FullName;
                                        }
                                    }
                                    if (Mare14Horse != null)
                                    {
                                        worksheet.Cells[68, 2].Value = Mare14Horse.FullName;
                                        if (Mare14Horse.MotherID != 0)
                                        {
                                            Mother14Horse = Horse.GetSelectedHorseAsync(Mare14Horse.MotherID).Result;
                                            worksheet.Cells[69, 2].Value = Mother14Horse.FullName;
                                        }
                                        if (Mare14Horse.FatherID != 0)
                                        {
                                            Father14Horse = Horse.GetSelectedHorseAsync(Mare14Horse.FatherID).Result;
                                            worksheet.Cells[69, 3].Value = Father14Horse.FullName;
                                        }
                                    }
                                    if (Mare15Horse != null)
                                    {
                                        worksheet.Cells[73, 2].Value = Mare15Horse.FullName;
                                        if (Mare15Horse.MotherID != 0)
                                        {
                                            Mother15Horse = Horse.GetSelectedHorseAsync(Mare15Horse.MotherID).Result;
                                            worksheet.Cells[74, 2].Value = Mother15Horse.FullName;
                                        }
                                        if (Mare15Horse.FatherID != 0)
                                        {
                                            Father15Horse = Horse.GetSelectedHorseAsync(Mare15Horse.FatherID).Result;
                                            worksheet.Cells[74, 3].Value = Father15Horse.FullName;
                                        }
                                    }
                                    if (Mare16Horse != null)
                                    {
                                        worksheet.Cells[78, 2].Value = Mare16Horse.FullName;
                                        if (Mare16Horse.MotherID != 0)
                                        {
                                            Mother16Horse = Horse.GetSelectedHorseAsync(Mare16Horse.MotherID).Result;
                                            worksheet.Cells[79, 2].Value = Mother16Horse.FullName;
                                        }
                                        if (Mare16Horse.FatherID != 0)
                                        {
                                            Father16Horse = Horse.GetSelectedHorseAsync(Mare16Horse.FatherID).Result;
                                            worksheet.Cells[79, 3].Value = Father16Horse.FullName;
                                        }
                                    }
                                    if (Mare17Horse != null)
                                    {
                                        worksheet.Cells[83, 2].Value = Mare17Horse.FullName;
                                        if (Mare17Horse.MotherID != 0)
                                        {
                                            Mother17Horse = Horse.GetSelectedHorseAsync(Mare17Horse.MotherID).Result;
                                            worksheet.Cells[84, 2].Value = Mother17Horse.FullName;
                                        }
                                        if (Mare17Horse.FatherID != 0)
                                        {
                                            Father17Horse = Horse.GetSelectedHorseAsync(Mare17Horse.FatherID).Result;
                                            worksheet.Cells[84, 3].Value = Father17Horse.FullName;
                                        }
                                    }
                                    if (Mare18Horse != null)
                                    {
                                        worksheet.Cells[88, 2].Value = Mare18Horse.FullName;
                                        if (Mare18Horse.MotherID != 0)
                                        {
                                            Mother18Horse = Horse.GetSelectedHorseAsync(Mare18Horse.MotherID).Result;
                                            worksheet.Cells[89, 2].Value = Mother18Horse.FullName;
                                        }
                                        if (Mare18Horse.FatherID != 0)
                                        {
                                            Father18Horse = Horse.GetSelectedHorseAsync(Mare18Horse.FatherID).Result;
                                            worksheet.Cells[89, 3].Value = Father18Horse.FullName;
                                        }
                                    }
                                    if (Mare19Horse != null)
                                    {
                                        worksheet.Cells[93, 2].Value = Mare19Horse.FullName;
                                        if (Mare19Horse.MotherID != 0)
                                        {
                                            Mother19Horse = Horse.GetSelectedHorseAsync(Mare19Horse.MotherID).Result;
                                            worksheet.Cells[94, 2].Value = Mother19Horse.FullName;
                                        }
                                        if (Mare19Horse.FatherID != 0)
                                        {
                                            Father19Horse = Horse.GetSelectedHorseAsync(Mare19Horse.FatherID).Result;
                                            worksheet.Cells[94, 3].Value = Father19Horse.FullName;
                                        }
                                    }
                                    if (Mare20Horse != null)
                                    {
                                        worksheet.Cells[98, 2].Value = Mare20Horse.FullName;
                                        if (Mare20Horse.MotherID != 0)
                                        {
                                            Mother20Horse = Horse.GetSelectedHorseAsync(Mare20Horse.MotherID).Result;
                                            worksheet.Cells[99, 2].Value = Mother20Horse.FullName;
                                        }
                                        if (Mare20Horse.FatherID != 0)
                                        {
                                            Father20Horse = Horse.GetSelectedHorseAsync(Mare20Horse.FatherID).Result;
                                            worksheet.Cells[99, 3].Value = Father20Horse.FullName;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                }

                                try
                                {
                                    workbook.SaveAs(Filename: magazinesPath + Year + @"\" + StallionHorse.FullName);
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно создали журнал случки"));
                                }
                                catch (System.Runtime.InteropServices.COMException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Невозможно создать журнал случки"));
                                }
                            }
                            else
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Файл уже создан. Выполняется открытие файла"));
                                //System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + StallionHorse.NickName + @"\" + StallionHorse.NickName + ".xlsx");
                                Excel.Application excel = new Excel.Application();
                                excel.Visible = true;
                                Excel.Workbook workbook = excel.Workbooks.Open(magazinesPath + Year + @"\" + StallionHorse.FullName + ".xlsx", 0, false);
                            }
                        }
                        else
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы не можете создать журнал случки без жеребца"));
                        }
                    });
                }

                return _createTaleMagazine;
            }

            set
            {
                _createTaleMagazine = value;
            }
        }

        #endregion
    }
}
