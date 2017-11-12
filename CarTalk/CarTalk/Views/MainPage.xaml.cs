using System;
using System.Collections.ObjectModel;
using CarTalk.Models;
using CarTalk.ViewModels;
using Xamarin.Forms;
using XamarinForms.Sutanto.Bluetooth;

namespace CarTalk.Views
{
    public partial class MainPage
    {
        private static MainPage _instance;
        private bool _initialized = false;
        public MainPageViewModel Model => BindingContext as MainPageViewModel;
        private ITBluetoothManager _bluetoothManager;
        private IPlatformUtils _platformUtils;
        public static MainPage Current => _instance ?? (_instance = new MainPage());

        private MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            _bluetoothManager = DependencyService.Get<ITBluetoothManager>();
            _platformUtils = DependencyService.Get<IPlatformUtils>();
        }


        private async void OnMessageTapped(object sender, EventArgs e)
        {
            var view = sender as View;
            var mesage = view?.BindingContext as Message;
            await view.ScaleTo(0.90, 50, Easing.CubicOut);
            await view.ScaleTo(1, 50, Easing.CubicIn);
            if (mesage != null)
            {
                SendMessage(mesage.Content);
            }
        }

        private void SendMessage(string text)
        {
            text = text + "\n";
            for (int i = 0; i < text.Length; i++)
            {
                _bluetoothManager.Write(text[i], ex => { });
            }
        }

        private async void OnButtonSendTapped(object sender, EventArgs e)
        {
            var view = sender as View;
            await view.ScaleTo(0.90, 50, Easing.CubicOut);
            await view.ScaleTo(1, 50, Easing.CubicIn);
            var text = EditorFreeText.Text;
            if(!String.IsNullOrEmpty(text))
                SendMessage(text);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var messages = _platformUtils.Load<ObservableCollection<Message>>(Constant.Path.Messages);
            Model.Messages = messages;
        }
    }
}
