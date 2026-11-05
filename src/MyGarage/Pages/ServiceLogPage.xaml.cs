using MyGarage.Models;
using MyGarage.ViewModels;

namespace MyGarage.Pages;

public partial class ServiceLogPage : ContentPage, IQueryAttributable
{
    private readonly ServiceLogViewModel _viewModel;

	public ServiceLogPage(ServiceLogViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
	}


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Vehicle", out object? value) && value is Vehicle vehicle)
        {
            _viewModel.SelectedVehicle = vehicle;
        }
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel.SelectedVehicle != null)
        {
            await _viewModel.LoadServiceRecordAsync(_viewModel.SelectedVehicle.Id);
        }
    }
}