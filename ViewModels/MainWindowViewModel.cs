using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        IEnumerable<DataPoint> _testDataPoints;

        /// <summary>Тестовый набор данных для визуализации графики</summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        }


        string _title = "Анализ статистики CV19";

        /// <summary>заголовок окна</summary>
        public string Title
        {
            get => _title;
            //set
            //{
            //    //if (Equals(_title, value))
            //    //{
            //    //    return;
            //    //}
            //    //_title = value;
            //    //OnPropertyChanged();
            //    Set(ref _title, value);
            //}
            set => Set(ref _title, value);
        }

        string _status = "Готов!";

        /// <summary>статус программы</summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        public ICommand CloseApplicationCommand { get; }

        void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        bool CanCloseApplicationCommandExecute(object p) => true;

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double TO_RAD = Math.PI / 180;
                var y = Math.Sin(x * TO_RAD);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;
        }
    }
}
