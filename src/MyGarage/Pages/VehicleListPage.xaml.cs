using MyGarage.ViewModels;

namespace MyGarage.Pages;

public partial class VehicleListPage : ContentPage
{
    private readonly VehicleListViewModel _viewModel;

    public VehicleListPage(VehicleListViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadVehiclesCommand.ExecuteAsync(null);
    }
}