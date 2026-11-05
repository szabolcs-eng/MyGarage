using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGarage.Models;
using MyGarage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGarage.ViewModels
{
    public partial class ServiceEntryViewModel : ObservableObject
    {
        private readonly IServiceRecordRepository _repo;



        [ObservableProperty]
        private Vehicle? selectedVehicle;

        [ObservableProperty]
        private ServiceRecord? editingRecord;



        [ObservableProperty]
        private string date = DateTime.Now.ToString("yyyy-MM-dd");

        [ObservableProperty]
        private string odometer = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private string cost = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        
        public ServiceEntryViewModel(IServiceRecordRepository repo)
        {
            _repo = repo;
        }


        public void SetVehicle(Vehicle vehicle)
        {
            SelectedVehicle = vehicle;
        }

        public void LoadRecord(ServiceRecord record)
        {
            EditingRecord = record;
            Date = record.Date.ToString("yyyy-MM-dd");
            Odometer = record.Odometer.ToString();
            Description = record.Description;
            Cost = record.Cost.ToString();
        }


        [RelayCommand]
        private async Task SaveAsync()
        {
            ErrorMessage = string.Empty;

            if (SelectedVehicle == null)
            {
                ErrorMessage = "There is no selected vehicle!";
                return;
            }

            if (string.IsNullOrWhiteSpace(Date) ||
            string.IsNullOrWhiteSpace(Odometer) ||
            string.IsNullOrWhiteSpace(Description) ||
            string.IsNullOrWhiteSpace(Cost))
            {
                ErrorMessage = "You have to fill all fields";
                return;
            }

            if (!DateTime.TryParse(Date, out DateTime dateValue))
            {
                ErrorMessage = "Invalid service date!";
                return;
            }

            if (!int.TryParse(Odometer, out int odometerValue) ||
            odometerValue < 0)
            {
                ErrorMessage = "Invalid odometer-reading!";
                return;
            }

            if (!int.TryParse(Cost, out int costValue) ||
                costValue < 0)
            {
                ErrorMessage = "Invalid cost!";
                return;
            }

            ServiceRecord record;

            if (EditingRecord == null)
            {
                record = new ServiceRecord();
            }
            else
            {
                record = EditingRecord;
            }

            record.VehicleId = SelectedVehicle.Id;
            record.Date = dateValue;
            record.Odometer = odometerValue;
            record.Description = Description.Trim();
            record.Cost = costValue;

            await _repo.SaveAsync(record);

            await Shell.Current.GoToAsync("..");
        }
    }
}
