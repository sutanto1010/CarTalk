using System;
using System.Collections;
using System.Collections.ObjectModel;
using CarTalk.Models;

namespace CarTalk.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Message> _messages;

        public MainPageViewModel()
        {
            Messages = new ObservableCollection<Message>();
        }

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set
            {
                _messages = value; 
                OnPropertyChanged();
            }
        }
    }
}