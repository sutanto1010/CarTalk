using System;
using System.Threading.Tasks;
using CarTalk.ViewModels;
using Xamarin.Forms;
using XamarinForms.Sutanto.Bluetooth;
using Device = XamarinForms.Sutanto.Bluetooth.Device;

namespace CarTalk.Views
{
    public partial class MainPage
    {
        private static MainPage _instance;
        private bool _initialized = false;
        public MainPageViewModel Model => BindingContext as MainPageViewModel;
        private ITBluetoothManager _bluetoothManager;
        public static MainPage Current => _instance ?? (_instance = new MainPage());

        private MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            _bluetoothManager = DependencyService.Get<ITBluetoothManager>();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetPairedDevices();
        }

       

        private void ScanButtonTapped(object sender, EventArgs e)
        {
            GetPairedDevices();
        }

        private void GetPairedDevices()
        {
            Model.Devices.Clear();
            var devices = _bluetoothManager.GetPairedDevices();
            foreach (var device in devices)
            {
                Model.Devices.Add(device);
            }
        }

        private void OnSelectedDeviceChanged(object sender, EventArgs e)
        {

        }

        private async void ConnectToDevice()
        {

            var device = Model.Devices[pickerDevice.SelectedIndex];
            _bluetoothManager.Connect(device, OnDeviceConnected, OnError);
        }

       


        private void OnDeviceConnected()
        {
            DisplayAlert("Success", $"Connected to device!", "Okay");
        }

        private void OnError(Exception obj)
        {
            DisplayAlert("Error", $"Unable connect to device: {obj.Message}", "Okay");
        }

        private void ButtonConnectTapped(object sender, EventArgs e)
        {
            ConnectToDevice();
        }

        private void ButtonSend(object sender, EventArgs e)
        {
            var text = editorText.Text+"\n";
            for (int i = 0; i < text.Length; i++)
            {
                _bluetoothManager.Write(text[i], ex => { });
                //{
                //    DisplayAlert("Error", $"Unable to send message: {ex.Message}", "Okay");
                //});
            }
        }

        private void OnSendCommandError(Exception obj)
        {
            DisplayAlert("Error", obj.Message, "Okay");
        }

        private void OnSendCommandSuccess()
        {
            DisplayAlert("Success", "Hooray!", "Okay");
        }
    }
}
