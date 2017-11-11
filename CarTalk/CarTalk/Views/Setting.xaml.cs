using Xamarin.Forms.Xaml;

namespace CarTalk.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Setting
    {
        private static Setting _instance;
        public static Setting Current => _instance ?? (_instance = new Setting());
        private Setting()
        {
            InitializeComponent();
        }
    }
}