using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class AddHorseSimpleViewModel : ViewModelBase
    {
        #region Consts

        private const string _stallionGender = "Жеребец";
        private const string _mareGender = "Кобыла";

        #endregion

        #region Vars

        private string _fullName;
        private string _nickName;
        private string _brand;
        private string _year;

        private char[] delimiterChars = { ' ', '-' };

        private string[] _namePart;

        private Gender _gender = Gender.Mare;

        #endregion

        public void OnClosing()
        {
            FullName = string.Empty;
        }

        #region Definitions

        public string FullName
        {
            get
            {
                return _fullName;
            }

            set
            {
                _fullName = value;
                RaisePropertyChanged(nameof(FullName));
            }
        }

        public string NickName
        {
            get
            {
                return _nickName;
            }

            set
            {
                _nickName = value;
                RaisePropertyChanged(nameof(NickName));
            }
        }

        public string Brand
        {
            get
            {
                return _brand;
            }

            set
            {
                _brand = value;
                RaisePropertyChanged(nameof(Brand));
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

        public string[] NamePart
        {
            get
            {
                return _namePart;
            }

            set
            {
                _namePart = value;
                RaisePropertyChanged(nameof(NamePart));
            }
        }

        public Gender Gender
        {
            get
            {
                return _gender;
            }

            set
            {
                if (_gender == value)
                {
                    return;
                }

                _gender = value;
                RaisePropertyChanged(nameof(Gender));
                RaisePropertyChanged(nameof(IsStallion));
                RaisePropertyChanged(nameof(IsMare));
                RaisePropertyChanged(nameof(GetGenderResult));
            }
        }

        public static string StallionGender => _stallionGender;
        public static string MareGender => _mareGender;

        #endregion

        #region RadioButtons

        public bool IsStallion
        {
            get
            {
                return Gender == Gender.Stallion;
            }

            set
            {
                Gender = value ? Gender.Stallion : Gender;
            }
        }

        public bool IsMare
        {
            get
            {
                return Gender == Gender.Mare;
            }

            set
            {
                Gender = value ? Gender.Mare : Gender;
            }
        }

        public string GetGenderResult
        {
            get
            {
                switch (Gender)
                {
                    case Gender.Stallion:
                        return StallionGender;
                    case Gender.Mare:
                        return MareGender;
                }

                return null;
            }
        }

        #endregion

        #region Commands

        private ICommand _addHorse;

        public ICommand AddHorseToList
        {
            get
            {
                if (_addHorse == null)
                {
                    _addHorse = new RelayCommand(() =>
                    {
                        FullName = string.Empty;
                        NamePart = FullName.Split(delimiterChars);

                        try
                        {
                            NickName = NamePart[0];
                            Brand = NamePart[1];
                            Year = "01.01." + NamePart[2];

                            if (Horse.AddHorseAsync(NickName, Brand, GetGenderResult, Year).Result)
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили запись лошади"));
                                FullName = string.Empty;
                            }
                            else
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось добавить запись лошади, проверьте корректность введенных данных!"));
                            }
                        }
                        catch
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Введите верную кличку!"));
                        }
                    });
                }

                return _addHorse;
            }

            set
            {
                _addHorse = value;
            }
        }

        #endregion

    }
}
