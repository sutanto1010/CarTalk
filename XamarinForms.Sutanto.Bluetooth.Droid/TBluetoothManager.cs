﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Widget;
using Java.Util;
using XamarinForms.Sutanto.Bluetooth.Droid;
using XamarinForms.Sutanto.Bluetooth.Extensions;

[assembly: Xamarin.Forms.Dependency(typeof(TBluetoothManager))]
namespace XamarinForms.Sutanto.Bluetooth.Droid
{
    public class TBluetoothManager : ITBluetoothManager
    {
        private static Application _app;
        private static BluetoothDevice _btDevice;
        private static BluetoothSocket _btSocket;
        private static UUID _uuid = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

        public IList<Device> GetPairedDevices()
        {
            var devices = new List<Device>();
            try
            {
                var adapter = BluetoothAdapter.DefaultAdapter;
                foreach (var device in adapter.BondedDevices)
                {
                    devices.Add(new Device()
                    {
                        Address = device.Address,
                        Name = device.Name
                    });
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, "Error on getting bluetooth devices: " + ex.Message,
                    ToastLength.Long).Show();
            }
            return devices;
        }

        public void Connect(Device device, Action onSuccess, Action<Exception> onError)
        {
            try
            {
                var adapter = BluetoothAdapter.DefaultAdapter;
                _btDevice = adapter.GetRemoteDevice(device.Address);
                _btSocket = _btDevice.CreateInsecureRfcommSocketToServiceRecord(_uuid);
                BluetoothAdapter.DefaultAdapter.CancelDiscovery();
                _btSocket.Connect();
                onSuccess();
            }
            catch (Exception e)
            {
                onError(e);
            }
        }

        public async Task ConnectAsync(Device device, Action OnSuccess, Action<Exception> OnError)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var adapter = BluetoothAdapter.DefaultAdapter;
                    _btDevice = adapter.GetRemoteDevice(device.Address);
                    _btSocket = _btDevice.CreateInsecureRfcommSocketToServiceRecord(_uuid);
                    BluetoothAdapter.DefaultAdapter.CancelDiscovery();
                    _btSocket.Connect();
                    OnSuccess();
                }
                catch (Exception e)
                {
                    OnError(e);
                }
            });
        }


        public void Disconnect()
        {
            //TODO:
            try
            {
                _btSocket.Close();
            }
            catch (Exception ex)
            {
                //TODO: Don't swallow this error message
            }
        }

        public void Write(char text, Action<Exception> OnError)
        {
            try
            {
                var bytes = new []{text.ToBytes()};
                _btSocket.OutputStream.Write(bytes,0,bytes.Length);
                
            }
            catch (Exception e)
            {
                OnError(e);
            }
        }
    }


}
