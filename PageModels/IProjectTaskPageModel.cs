using CommunityToolkit.Mvvm.Input;
using WeatherStation.Models;

namespace WeatherStation.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}