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

        private ObservableCollection<Horse> _stallionHorseList;
        private ObservableCollection<Horse> _mareHorseList;

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

                RaisePropertyChanged(() => StallionHorseList);
                RaisePropertyChanged(() => MareHorseList);

            }).ConfigureAwait(true);
        }

        #region Definitions

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
                            Directory.CreateDirectory(Path.Combine(@"C:\Users\Public\Documents\", "Помощник коневода"));
                            string docPath = @"C:\Users\Public\Documents\Помощник коневода\";
                            Directory.CreateDirectory(Path.Combine(docPath, "Журналы случки"));
                            string magazinesPath = @"C:\Users\Public\Documents\Помощник коневода\Журналы случки\";
                            if (!File.Exists(magazinesPath + StallionHorse.FullName + @"\" + StallionHorse.FullName + ".xlsx"))
                            {
                                Directory.CreateDirectory(Path.Combine(magazinesPath, StallionHorse.FullName));

                                Excel.Application application = new Excel.Application();
                                application.Visible = true;

                                Excel.Workbook workbook;
                                Excel.Worksheet worksheet;
                                var path = System.IO.Path.GetFullPath(@"Files\Excel\Журнал случки 19г.xlsx");
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
                                }
                                catch (Exception ex)
                                {

                                }

                                try
                                {
                                    workbook.SaveAs(Filename: magazinesPath + StallionHorse.FullName + @"\" + StallionHorse.FullName);
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
                                Excel.Workbook workbook = excel.Workbooks.Open(magazinesPath + StallionHorse.FullName + @"\" + StallionHorse.FullName + ".xlsx", 0 , false);
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
