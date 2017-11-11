using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinForms.Sutanto.Bluetooth
{
    public interface ITBluetoothManager
    {
        IList<Device> GetPairedDevices();
        void Connect(Device device, Action OnSuccess, Action<Exception> OnError);
        Task ConnectAsync(Device device, Action OnSuccess, Action<Exception> OnError);
        void Disconnect();
        void Write(char text, Action<Exception> OnError);
    }

    
}
