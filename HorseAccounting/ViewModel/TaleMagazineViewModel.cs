using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
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

                _stallionHorseList = Horse.GetFatherHorse();
                _mareHorseList = Horse.GetMotherHorse();
                ChoosenDate = DateTime.Today;

                RaisePropertyChanged(() => StallionHorseList);
                RaisePropertyChanged(() => MareHorseList);

            }).ConfigureAwait(true);
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
                                    worksheet.Cells[3, 2].Value = Mare1Horse.FullName;
                                    worksheet.Cells[8, 2].Value = Mare2Horse.FullName;
                                    worksheet.Cells[13, 2].Value = Mare3Horse.FullName;
                                    worksheet.Cells[18, 2].Value = Mare4Horse.FullName;
                                    worksheet.Cells[23, 2].Value = Mare5Horse.FullName;
                                    worksheet.Cells[28, 2].Value = Mare6Horse.FullName;
                                    worksheet.Cells[33, 2].Value = Mare7Horse.FullName;
                                    worksheet.Cells[38, 2].Value = Mare8Horse.FullName;
                                    worksheet.Cells[43, 2].Value = Mare9Horse.FullName;
                                    worksheet.Cells[48, 2].Value = Mare10Horse.FullName;
                                    worksheet.Cells[53, 2].Value = Mare11Horse.FullName;
                                    worksheet.Cells[58, 2].Value = Mare12Horse.FullName;
                                    worksheet.Cells[63, 2].Value = Mare13Horse.FullName;
                                    worksheet.Cells[68, 2].Value = Mare14Horse.FullName;
                                    worksheet.Cells[73, 2].Value = Mare15Horse.FullName;
                                    worksheet.Cells[78, 2].Value = Mare16Horse.FullName;
                                    worksheet.Cells[83, 2].Value = Mare17Horse.FullName;
                                    worksheet.Cells[88, 2].Value = Mare18Horse.FullName;
                                    worksheet.Cells[93, 2].Value = Mare19Horse.FullName;
                                    worksheet.Cells[98, 2].Value = Mare20Horse.FullName;
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
                                Excel.Workbook workbook = excel.Workbooks.Open(magazinesPath + Year + @"\" + StallionHorse.FullName + ".xlsx", 0 , false);
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
