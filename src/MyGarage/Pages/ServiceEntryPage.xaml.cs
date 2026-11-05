using MyGarage.Models;
using MyGarage.ViewModels;

namespace MyGarage.Pages;

public partial class ServiceEntryPage : ContentPage, IQueryAttributable
{
    private readonly ServiceEntryViewModel _viewModel;

	public ServiceEntryPage(ServiceEntryViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Vehicle", out object? vehicleValue) &&
            vehicleValue is Vehicle vehicle)
        {
            _viewModel.SetVehicle(vehicle);
        }

        if (query.TryGetValue("Record", out object? recordValue) &&
            recordValue is ServiceRecord record)
        {
            _viewModel.LoadRecord(record);
        }
    }
}