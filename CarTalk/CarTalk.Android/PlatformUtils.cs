using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Widget;
using CarTalk.Droid;
using CarTalk.Models;
using Newtonsoft.Json;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformUtils))]
namespace CarTalk.Droid
{
    public class PlatformUtils : IPlatformUtils
    {
        public static string PersonalFolder => Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public void Save<T>(T item, string fileName)
        {
            try
            {
                var path = $"{PersonalFolder}/{fileName}";
                if(File.Exists(path))
                    File.Delete(path);

                File.WriteAllText(path, JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                //TODO: Don't hide your sin :P
            }
        }

        public T Load<T>(string fileName)
        {
            var result = default(T);
            try
            {
                var path = $"{PersonalFolder}/{fileName}";
                var jsonText = File.ReadAllText(path);
                result = JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (Exception ex)
            {
                //TODO: Don't hide your sin :P
            }
            return result;
        }

        public void Notify(string message, bool isLongDuration = false)
        {
            var length = isLongDuration ? ToastLength.Long : ToastLength.Short;
            Toast.MakeText(Application.Context, message, length).Show();
        }

        public void Init()
        {
            try
            {
                var path = $"{PersonalFolder}/{Constant.Path.Messages}";
                if (!File.Exists(path))
                {
                    var messages = new List<Message>();
                    messages.Add(new Message() { Title = "Hello", Content = "HELLO" });
                    messages.Add(new Message() { Title = "Turn Left", Content = "LET ME TURN LEFT" });
                    messages.Add(new Message() { Title = "Turn Right", Content = "LET ME TURN RIGHT" });
                    messages.Add(new Message() { Title = "Thanks", Content = "THANKS BRO" });
                    Save(messages, Constant.Path.Messages);
                }

            }
            catch (Exception ex)
            {
                //TODO: Don't eat this nasty message
            }
        }
    }
}