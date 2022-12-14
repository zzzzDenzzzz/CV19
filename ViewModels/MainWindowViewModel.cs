using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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
        }
    }
}
