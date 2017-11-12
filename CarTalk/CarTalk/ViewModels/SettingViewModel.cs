using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms.Sutanto.Bluetooth;
using Device = XamarinForms.Sutanto.Bluetooth.Device;

namespace CarTalk.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        private bool _isConnected;
        public ObservableCollection<Device> Devices { get; set; }
        public ICommand ConnectDisconnectCommand { get; set; }
        private ITBluetoothManager _bluetoothManager;
        private int _selectedDeviceIndex;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value; 
                OnPropertyChanged();
            }
        }

       

        public string ButtonConnectText => IsConnected ? "DISCONNECT" : "CONNECT";

        public int SelectedDeviceIndex
        {
            get => _selectedDeviceIndex;
            set
            {
                _selectedDeviceIndex = value; 
                OnPropertyChanged();
            }
        }

        public SettingViewModel()
        {
            _bluetoothManager = DependencyService.Get<ITBluetoothManager>();
            Devices=new ObservableCollection<Device>();
            Devices.CollectionChanged += OnDevicesCollectionChanged;
            ConnectDisconnectCommand = new Command(ConnectOrDisconnect);
        }

        private void ConnectOrDisconnect()
        {
            Loading = true;
            Task.Factory.StartNew(() =>
            {
                if (!IsConnected)
                {

                    LoadingText = "Connecting...";
                    _bluetoothManager.Connect(Devices[SelectedDeviceIndex],
                        () =>
                        {
                            IsConnected = true;
                        },
                        ex =>
                        {
                            IsConnected = false;
                        });
                }
                else
                {
                    LoadingText = "Disconnecting...";
                    _bluetoothManager.Disconnect();
                    IsConnected = false;
                }

                //Task.Delay(3000);
                Loading = false;
                OnPropertyChanged("ButtonConnectText");
            });

        }

        private void OnDevicesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("DevicesPicker");
        }

        public IList<string> DevicesPicker
        {
            get
            {
                var items = new List<string>();
                if (Devices.Any())
                {
                    items = Devices.Select(i => $"{i.Name}").ToList();
                }
                return items;
            }
        }
    }
}