using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGarage.Models;
using MyGarage.Repositories.Interfaces;

namespace MyGarage.ViewModels
{
    public partial class VehicleEntryViewModel : ObservableObject
    {
        private readonly IVehicleRepository _repo;

        [ObservableProperty]
        private Vehicle? _editingVehicle;

        [ObservableProperty]
        private string brand = string.Empty;

        [ObservableProperty]
        private string model = string.Empty;

        [ObservableProperty]
        private string licensePlate = string.Empty;

        [ObservableProperty]
        private int year;

        [ObservableProperty]
        private string errorMessage = string.Empty;



        public VehicleEntryViewModel(IVehicleRepository vehicleRepository)
        {
            _repo = vehicleRepository;
        }


        public void LoadVehicle(Vehicle vehicle)
        {
            EditingVehicle = vehicle;
            Brand = vehicle.Brand;
            Model = vehicle.Model;
            LicensePlate = vehicle.LicensePlate;
            Year = vehicle.Year;
        }



        [RelayCommand]
        private async Task SaveAsync()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Brand) ||
                string.IsNullOrWhiteSpace(Model) ||
                string.IsNullOrWhiteSpace(LicensePlate) ||
                Year == 0)
            {
                ErrorMessage = "You have to fill all fields!";
                return;
            }

            if (Year < 1900 || Year > DateTime.Now.Year)
            {
                ErrorMessage = "Invalid production year!";
                return;
            }

            Vehicle vehicle;

            if (EditingVehicle == null)
            {
                vehicle = new Vehicle();
            }
            else
            {
                vehicle = EditingVehicle;
            }

            vehicle.Brand = Brand.Trim();
            vehicle.Model = Model.Trim();
            vehicle.LicensePlate = LicensePlate.Trim();
            vehicle.Year = Year;

            await _repo.SaveAsync(vehicle);

            await Shell.Current.GoToAsync("..");
        }
    }
}
