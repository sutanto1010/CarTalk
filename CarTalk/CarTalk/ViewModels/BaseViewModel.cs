using System.ComponentModel;
using System.Runtime.CompilerServices;
using CarTalk.Annotations;

namespace CarTalk.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _loading;
        private string _loadingText;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string LoadingText
        {
            get => _loadingText;
            set
            {
                _loadingText = value;
                OnPropertyChanged();
            }
        }

        public bool Loading
        {
            get => _loading;
            set
            {
                _loading = value;
                OnPropertyChanged();
            }
        }
    }
}
