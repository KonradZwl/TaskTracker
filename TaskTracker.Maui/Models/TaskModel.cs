using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TaskTracker.Maui.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string StartButtonColor => timerIsRunning ? "DarkGreen" : "Green";
        public string EndButtonColor => !timerIsRunning ? "DarkRed" : "Red";
        public bool IsCompleteButtonEnabled => !timerIsRunning;


        public ICommand StartTimerCommand => new Command(StartTimer);
        public ICommand StopTimerCommand => new Command(StopTimer);


        private string elapsedTime = "00:00:00:000";
        public string ElapsedTime
        {
            get => elapsedTime;
            private set
            {
                if (elapsedTime != value)
                {
                    elapsedTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private Stopwatch stopwatch;
        private bool timerIsRunning;

        public TaskModel()
        {
            stopwatch = new Stopwatch();
        }

        public void StartTimer()
        {
            if (!timerIsRunning)
            {
                stopwatch.Start();
                timerIsRunning = true;
                OnPropertyChanged(nameof(StartButtonColor));
                OnPropertyChanged(nameof(EndButtonColor));
                OnPropertyChanged(nameof(IsCompleteButtonEnabled));
                _ = UpdateElapsedTime();
       
            }
        }

        public void StopTimer()
        {
            if (timerIsRunning)
            {
                stopwatch.Stop();
                timerIsRunning = false;
                OnPropertyChanged(nameof(StartButtonColor));
                OnPropertyChanged(nameof(EndButtonColor));
                OnPropertyChanged(nameof(IsCompleteButtonEnabled));
            }
        }

        private async Task UpdateElapsedTime()
        {
            while (timerIsRunning)
            {
                ElapsedTime = FormatTime(stopwatch.Elapsed);
                await Task.Delay(50);
            }
        }

        private string FormatTime(TimeSpan time)
        {
            return time.ToString(@"hh\:mm\:ss\:fff");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
