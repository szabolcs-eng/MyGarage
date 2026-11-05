using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGarage.Models;
using MyGarage.Pages;
using MyGarage.Repositories.Interfaces;
using System.Collections.ObjectModel;

namespace MyGarage.ViewModels
{
    public partial class VehicleListViewModel : ObservableObject
    {
        private readonly IVehicleRepository _repo;

        public ObservableCollection<Vehicle> Vehicles { get; } = new();



        public VehicleListViewModel(IVehicleRepository vehicleRepository)
        {
            _repo = vehicleRepository;
        }



        [RelayCommand]
        public async Task LoadVehiclesAsync()
        {
            var vehicles = await _repo.GetAllAsync();

            Vehicles.Clear();

            foreach (var vehicle in vehicles)
            {
                Vehicles.Add(vehicle);
            }
        }


        [RelayCommand]
        private async Task AddVehicleAsync()
        {
            await Shell.Current.GoToAsync(nameof(VehicleEntryPage));
        }

        [RelayCommand]
        private async Task EditVehicleAsync(Vehicle? vehicle)
        {
            if (vehicle == null) return;

            await Shell.Current.GoToAsync(nameof(VehicleEntryPage), new Dictionary<string, object>
            {
                ["Vehicle"] = vehicle
            });
        }


        [RelayCommand]
        private async Task DeleteVehicleAsync(Vehicle? vehicle)
        {
            if (vehicle is null) return;

            bool confirm = await Shell.Current.DisplayAlertAsync(
                "Delete vehicle",
                $"Are you sure you want to delete this vehicle: {vehicle.Brand} {vehicle.Model}?",
                "Yes",
                "Cancel");

            if (!confirm) return;

            await _repo.DeleteAsync(vehicle);

            Vehicles.Remove(vehicle);
        }


        [RelayCommand]
        private async Task OpenServiceLogAsync(Vehicle? vehicle)
        {
            if (vehicle == null) return;

            await Shell.Current.GoToAsync(nameof(ServiceLogPage), new Dictionary<string, object>
            {
                ["Vehicle"] = vehicle
            });
        }
        
    }
}
