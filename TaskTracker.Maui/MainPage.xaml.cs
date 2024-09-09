using System.Collections.ObjectModel;
using TaskTracker.Maui.Models;

namespace TaskTracker.Maui
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<TaskModel> tasks;
        public MainPage()
        {
            InitializeComponent();
            tasks = new ObservableCollection<TaskModel>();
            tasksList.ItemsSource = tasks;
        }

        private void addTaskBtn_Clicked(object sender, EventArgs e)
        {
            var taskName = newTaskEntry.Text;
            if (string.IsNullOrWhiteSpace(taskName)) {
                DisplayAlert("Error", "The name of the task cannot be empty!", "Okay");
                return;
            }
            tasks.Add(new TaskModel { Name = taskName});
            newTaskEntry.Text = string.Empty;
        }

        private async void CompleteButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var task = (TaskModel)button.BindingContext;

            bool answer = await DisplayAlert("Complete Task", "By completing the task the task will be removed, are you sure?", "Yes", "No");

            if (answer)
            {
                tasks.Remove(task);
            }
        }
    }
}
