using System;
using System.Collections.ObjectModel;
using System.Linq;
using CarTalk.Models;
using CarTalk.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms.Sutanto.Bluetooth;

namespace CarTalk.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Setting
    {
        private static Setting _instance;
        private ITBluetoothManager _bluetoothManager;
        private IPlatformUtils _platformUtils;
        public SettingViewModel Model => BindingContext as SettingViewModel;
        public static Setting Current => _instance ?? (_instance = new Setting());
        private Setting()
        {
            InitializeComponent();
            BindingContext = new SettingViewModel();
            _bluetoothManager = DependencyService.Get<ITBluetoothManager>();
            _platformUtils = DependencyService.Get<IPlatformUtils>();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(!Model.Devices.Any())
                GetPairedDevices();
            var messages = _platformUtils.Load<ObservableCollection<Message>>(Constant.Path.Messages);
            Model.Messages = messages;
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
            var text = editorText.Text + "\n";
            for (int i = 0; i < text.Length; i++)
            {
                _bluetoothManager.Write(text[i], ex => { });
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

        private async void OnAddButtonTapped(object sender, EventArgs e)
        {
            var view = sender as View;
            await view.ScaleTo(0.80, 50, Easing.CubicOut);
            await view.ScaleTo(1, 50, Easing.CubicIn);
            MessageEditor.Current.Show();
        }
    }
}