using System;
using System.Threading.Tasks;
using CarTalk.ViewModels;
using Xamarin.Forms;
using XamarinForms.Sutanto.Bluetooth;
using Device = XamarinForms.Sutanto.Bluetooth.Device;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

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


        private async void OnMessageTapped(object sender, EventArgs e)
        {
            var view = sender as View;
            await view.ScaleTo(0.90, 50, Easing.CubicOut);
            await view.ScaleTo(1, 50, Easing.CubicIn);
        }
    }
}
