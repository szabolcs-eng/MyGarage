using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGarage.Models;
using MyGarage.Repositories.Interfaces;

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

        [ObservableProperty]
        private string invoiceImagePath = string.Empty;

        [ObservableProperty]
        private double? locationLat;

        [ObservableProperty]
        private double? locationLon;


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
            InvoiceImagePath = record.InvoiceImagePath;
            LocationLat = record.LocationLat;
            LocationLon = record.LocationLon;
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
            record.InvoiceImagePath = InvoiceImagePath;
            record.LocationLat = LocationLat;
            record.LocationLon = LocationLon;

            await _repo.SaveAsync(record);

            await Shell.Current.GoToAsync("..");
        }



        [RelayCommand]
        private async Task TakePhotoAsync()
        {
            try
            {
                if (!MediaPicker.Default.IsCaptureSupported)
                {
                    ErrorMessage = "Photo capture is not supported!";
                    return;
                }

                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo == null) return;

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream destinationStream = File.Create(filePath);

                await sourceStream.CopyToAsync(destinationStream);

                InvoiceImagePath = filePath;
                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Failed to capture photo!" + ex.Message;
            }
        }



        [RelayCommand]
        private async Task GetLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                Location? location = await Geolocation.Default.GetLocationAsync(request);

                if (location == null)
                {
                    ErrorMessage = "Location not found!";
                    return;
                }

                LocationLat = location.Latitude;
                LocationLon = location.Longitude;

                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Failed to get location!" + ex.Message;
            }
        }

    }
}
