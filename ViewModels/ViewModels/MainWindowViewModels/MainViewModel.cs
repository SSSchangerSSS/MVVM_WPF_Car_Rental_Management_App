using System;
using System.Windows.Threading;
using ViewModels.Store;

namespace ViewModels.ViewModels.MainWindowViewModels
{
    /// <summary>
    /// <see cref="MainViewModel"/> class is a viewModel (dataContext) for main window view 
    ///</summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        private readonly DispatcherTimer ClockTimer;
        private string _currentTime;
        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }
        private string _currentDate;
        public string CurrentDate
        {
            get
            {
                return _currentDate;
            }
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="navigationStore">A store for current view model</param>
        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            ClockTimer = new DispatcherTimer();
            ClockTimer.Tick += ClockTimer_Click;
            ClockTimer.Interval = new TimeSpan(0, 0, 1);
            ClockTimer.Start();
        }

        /// <summary>
        /// Will be called when current view model changes.
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        /// <summary>
        /// Will be called when timer ticks.
        /// </summary>
        public void ClockTimer_Click(object sender, EventArgs e)
        {
            CurrentTime = $"Time = {DateTime.Now.ToString("HH:mm:ss")}";
            CurrentDate = $"Date = {DateTime.Now.ToString("d")}";
        }
    }
}
