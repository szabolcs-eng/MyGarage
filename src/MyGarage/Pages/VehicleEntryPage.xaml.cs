using MyGarage.Models;
using MyGarage.ViewModels;

namespace MyGarage.Pages;

public partial class VehicleEntryPage : ContentPage, IQueryAttributable
{
    private readonly VehicleEntryViewModel _viewModel;

    public VehicleEntryPage(VehicleEntryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Vehicle", out object? value) && value is Vehicle vehicle)
        {
            _viewModel.LoadVehicle(vehicle);
        }
    } 
}