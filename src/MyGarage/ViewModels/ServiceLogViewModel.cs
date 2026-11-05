using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGarage.Models;
using MyGarage.Pages;
using MyGarage.Repositories;
using MyGarage.Repositories.Interfaces;
using System.Collections.ObjectModel;

namespace MyGarage.ViewModels
{
    public partial class ServiceLogViewModel : ObservableObject
    {
        private readonly IServiceRecordRepository _repo;


        [ObservableProperty]
        private Vehicle? selectedVehicle;    

        public ObservableCollection<ServiceRecord> ServiceRecords { get; } = new();

        public ServiceLogViewModel(IServiceRecordRepository repo)
        {
            _repo = repo;
        }

        public async Task LoadServiceRecordAsync(int vehicleId)
        {
            var records = await _repo.GetByVehicleIdAsync(vehicleId);

            ServiceRecords.Clear();

            foreach (var record in records)
            {
                ServiceRecords.Add(record);
            }
        }


        [RelayCommand]
        private async Task AddServiceRecordAsync()
        {
            if (SelectedVehicle == null) return;

            await Shell.Current.GoToAsync(nameof(ServiceEntryPage), new Dictionary<string, object>
            {
                ["Vehicle"] = SelectedVehicle
            });
        }


        [RelayCommand]
        private async Task EditServiceRecordAsync(ServiceRecord? record)
        {
            if (record == null) return;

            await Shell.Current.GoToAsync(nameof(ServiceEntryPage), new Dictionary<string, object>
                {
                    ["Vehicle"] = SelectedVehicle!,
                    ["Record"] = record
                });
        }

        [RelayCommand]
        private async Task DeleteServiceRecordAsync(ServiceRecord? record)
        {
            if (record == null) return;

            bool confirm = await Shell.Current.DisplayAlertAsync(
                "Delete service record",
                "Are you sure you want to delete this service record?",
                "Yes",
                "Cancel");

            if (!confirm) return;

            await _repo.DeleteAsync(record);

            ServiceRecords.Remove(record);
        }
    }
}
