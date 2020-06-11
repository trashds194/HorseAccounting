using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class ShowDiagramsViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private LiveCharts.SeriesCollection _pieChartSeriesCollection = new LiveCharts.SeriesCollection();
        private SeriesCollection _cartesianChartSeriesCollection = new SeriesCollection();

        private ObservableCollection<Diagram> _diagrams;
        private ObservableCollection<MenuItem> _menuList;

        private MenuItem _selectedMenuItem;

        private bool _pieVis;
        private bool _cartesianVis;

        private List<string> _labels;

        Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

        public Func<double, string> Formatter { get; set; }

        #endregion

        public ShowDiagramsViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;

            _menuList = new ObservableCollection<MenuItem>
            {
                new MenuItem{ID = 0, Name = "Соотношение жеребцов и кобыл" },
                new MenuItem{ID = 1, Name = "Соотношение мест рождения" },
                new MenuItem{ID = 2, Name = "График пополнения племсостава" },
            };
        }

        public void OnSelectionChanged()
        {
            try
            {
                if (SelectedMenuItem != null)
                {
                    switch (SelectedMenuItem.ID)
                    {
                        case 0:
                            PieVis = true;
                            CartesianVis = false;

                            PieChartSeriesCollection.Clear();
                            _diagrams = Diagram.GetGenderDiagram().Result;
                            RaisePropertyChanged(() => Diagrams);

                            foreach (Diagram item in _diagrams)
                            {
                                PieChartSeriesCollection.Add(new PieSeries { Title = item.Title, Values = new ChartValues<int> { item.Value }, DataLabels = true, LabelPoint = labelPoint });
                            }
                            break;
                        case 1:
                            PieVis = true;
                            CartesianVis = false;

                            PieChartSeriesCollection.Clear();
                            _diagrams = Diagram.GetBirthPlaceDiagram().Result;
                            RaisePropertyChanged(() => Diagrams);

                            foreach (Diagram item in _diagrams)
                            {
                                PieChartSeriesCollection.Add(new PieSeries { Title = item.Title, Values = new ChartValues<int> { item.Value }, DataLabels = true, LabelPoint = labelPoint });
                            }
                            break;
                        case 2:
                            PieVis = false;
                            CartesianVis = true;

                            CartesianChartSeriesCollection.Clear();
                            _diagrams = Diagram.GetStallionYearDiagram().Result;
                            RaisePropertyChanged(() => Diagrams);

                            Diagram stallionCartes = new Diagram();
                            ChartValues<int> stallionValues = new ChartValues<int>();
                            Labels = new List<string>();


                            foreach (Diagram item in _diagrams)
                            {
                                stallionCartes.Title = item.Title;
                                stallionValues.Add(item.Value);
                                Labels.Add(item.Year);
                            }

                            CartesianChartSeriesCollection.Add(new ColumnSeries
                            {
                                Title = stallionCartes.Title,
                                Values = stallionValues
                            });

                            _diagrams = Diagram.GetMareYearDiagram().Result;
                            RaisePropertyChanged(() => Diagrams);

                            Diagram mareCartes = new Diagram();
                            ChartValues<int> mareValues = new ChartValues<int>();

                            foreach (Diagram item in _diagrams)
                            {
                                mareCartes.Title = item.Title;
                                mareValues.Add(item.Value);
                            }

                            CartesianChartSeriesCollection.Add(new ColumnSeries
                            {
                                Title = mareCartes.Title,
                                Values = mareValues
                            });
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение."));
            }
        }

        #region Definitions

        public LiveCharts.SeriesCollection PieChartSeriesCollection
        {
            get
            {
                return _pieChartSeriesCollection;
            }
            set
            {
                _pieChartSeriesCollection = value;
                RaisePropertyChanged(nameof(PieChartSeriesCollection));
            }
        }

        public SeriesCollection CartesianChartSeriesCollection
        {
            get
            {
                return _cartesianChartSeriesCollection;
            }

            set
            {
                _cartesianChartSeriesCollection = value;
                RaisePropertyChanged(nameof(CartesianChartSeriesCollection));
            }
        }

        public ObservableCollection<MenuItem> MenuList
        {
            get
            {
                return _menuList;
            }
        }

        public ObservableCollection<Diagram> Diagrams
        {
            get
            {
                return _diagrams;
            }
        }

        public MenuItem SelectedMenuItem
        {
            get
            {
                return _selectedMenuItem;
            }

            set
            {
                _selectedMenuItem = value;
                RaisePropertyChanged(nameof(SelectedMenuItem));
            }
        }

        public bool PieVis
        {
            get
            {
                return _pieVis;
            }

            set
            {
                _pieVis = value;
                RaisePropertyChanged(nameof(PieVis));
            }
        }

        public bool CartesianVis
        {
            get
            {
                return _cartesianVis;
            }

            set
            {
                _cartesianVis = value;
                RaisePropertyChanged(nameof(CartesianVis));
            }
        }

        public List<string> Labels
        {
            get
            {
                return _labels;
            }

            set
            {
                _labels = value;
                RaisePropertyChanged(nameof(Labels));
            }
        }

        #endregion

        #region ChartCommands



        #endregion


        #region WindowCommands

        private ICommand _horsesList;

        public ICommand BackToList
        {
            get
            {
                if (_horsesList == null)
                {
                    _horsesList = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("HorsesList");
                    });
                }

                return _horsesList;
            }

            set
            {
                _horsesList = value;
            }
        }

        #endregion
    }

    public class MenuItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
