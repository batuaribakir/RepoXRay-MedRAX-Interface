using System;
using System.Windows.Input;
using File_Manager_MEDGEN.View;
using File_Manager_MEDGEN.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public ICommand ShowHomeCommand { get; }
    public ICommand ShowSharedFilesCommand { get; }
    public ICommand ShowReportsCommand { get; }
    public ICommand ShowStatisticsCommand { get; }
    public ICommand ShowSettingsCommand { get; }
    public ICommand ShowSupportCommand { get; }

    public MainWindowViewModel()
    {
        CurrentView = new HomeViewModel();
        ShowHomeCommand = new ViewModelCommand(o => ShowHome(new HomeViewModel()));
        ShowSharedFilesCommand = new ViewModelCommand(o => ShowView(new SharedFilesViewModel()));
        ShowReportsCommand = new ViewModelCommand(o => ShowView(new ReportsViewModel()));
        //ShowStatisticsCommand = new ViewModelCommand(o => ShowView(new StatisticsViewModel()));
        //ShowSettingsCommand = new ViewModelCommand(o => ShowView(new SettingsViewModel()));
        //ShowSupportCommand = new ViewModelCommand(o => ShowView(new SupportViewModel()));
    }

    private void ShowHome(object viewModel)
    {
        CurrentView = viewModel;
    }

    private void ShowView(object viewModel)
    {
        CurrentView = viewModel;
    }

    private void ShowReports(object viewModel)
    {
        CurrentView = viewModel;
    }
}