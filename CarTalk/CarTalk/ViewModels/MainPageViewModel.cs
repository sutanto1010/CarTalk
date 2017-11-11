using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using XamarinForms.Sutanto.Bluetooth;

namespace CarTalk.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<Device> Devices { get; set; }

        public MainPageViewModel()
        {
            Devices=new ObservableCollection<Device>();
            Devices.CollectionChanged += OnDevicesCollectionChanged;
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