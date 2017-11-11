using Xamarin.Forms.Xaml;

namespace CarTalk.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Index
    {
        private static Index _instance;
        public static Index Current => _instance ?? (_instance = new Index());

        private Index()
        {
            InitializeComponent();
            Children.Add(MainPage.Current);
            Children.Add(Setting.Current);
        }
    }
}