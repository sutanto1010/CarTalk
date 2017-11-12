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
            Messages.Add(new Message(){Title = "Hello",Content = "HELLO"});
            Messages.Add(new Message(){Title = "Turn Left",Content = "LET ME TURN LEFT"});
            Messages.Add(new Message(){Title = "Turn Right",Content = "LET ME TURN RIGHT"});
            Messages.Add(new Message(){Title = "Thanks",Content = "THANKS BRO"});
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