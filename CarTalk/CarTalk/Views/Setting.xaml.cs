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
            MessageEditor.Current.OnSaveButton = OnSaveMessage;
        }

        private void OnSaveMessage(string title, string content)
        {
            var message = Model.Messages.FirstOrDefault(i => i.Title == title);
            if (message != null)
            {
                message.Content = content;
            }
            else
            {
                message = new Message() {Title = title, Content = content};
                Model.Messages.Add(message);
            }

            SaveMessage();
        }

        private void SaveMessage()
        {
            _platformUtils.Save(Model.Messages, Constant.Path.Messages);
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


        private async void OnAddButtonTapped(object sender, EventArgs e)
        {
            var view = sender as View;
            await view.ScaleTo(0.80, 50, Easing.CubicOut);
            await view.ScaleTo(1, 50, Easing.CubicIn);
            MessageEditor.Current.Show();
        }

        private void OnSwipeDeleteTapped(object sender, EventArgs e)
        {
            var view = sender as View;
            var message = view.BindingContext as Message;
            Model.Messages.Remove(message);
            SaveMessage();
        }
    }
}