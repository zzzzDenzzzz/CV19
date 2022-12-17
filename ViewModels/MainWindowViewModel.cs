﻿using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        /*-------------------------------------------------------------------------------------------*/

        public ObservableCollection<Group> Groups { get; }

        public object[] CompositeCollection { get; }

        object _selectedCompositeValue;
        /// <summary>
        /// выбранный непонятный элемент
        /// </summary>
        public object SelectedCompositeValue
        {
            get => _selectedCompositeValue;
            set => Set(ref _selectedCompositeValue, value);
        }

        Group _selectedGroup;
        /// <summary>
        /// Выбранная группа
        /// </summary>
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set => Set(ref _selectedGroup, value);
        }

        int _selectedPageIndex;
        /// <summary>
        /// номер выбранной вкладки
        /// </summary>
        public int SelectedPageIndex
        {
            get => _selectedPageIndex;
            set => Set(ref _selectedPageIndex, value);
        }

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

        /*-------------------------------------------------------------------------------------------*/

        public ICommand CloseApplicationCommand { get; }

        void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        bool CanCloseApplicationCommandExecute(object p) => true;

        public ICommand ChangeTabIndexCommand { get; }

        bool CanChangeTabIndexCommandExecute(object p) => _selectedPageIndex >= 0;

        void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null)
            {
                return;
            }
            SelectedPageIndex += Convert.ToInt32(p);
        }

        /*-------------------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double TO_RAD = Math.PI / 180;
                var y = Math.Sin(x * TO_RAD);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;

            var studentIndex = 1;

            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {studentIndex}",
                Surname = $"Surname {studentIndex}",
                Patronymic = $"Patronymic {studentIndex++}",
                BirthDay = DateTime.Now,
                Rating = 0
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);

            var dataList = new List<object>();
            dataList.Add("Hello World!");
            dataList.Add(42);
            var group = Groups[1];
            dataList.Add(group);
            dataList.Add(group.Students[0]);

            CompositeCollection = dataList.ToArray();
        }

        /*-------------------------------------------------------------------------------------------*/
    }
}
