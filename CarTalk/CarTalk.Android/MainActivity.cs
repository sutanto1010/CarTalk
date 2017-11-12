using System.IO;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace CarTalk.Droid
{
    [Activity(Label = "CarTalk", Icon = "@drawable/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Plugin.Iconize.Iconize
                .With(new Plugin.Iconize.Fonts.FontAwesomeModule())
                .With(new Plugin.Iconize.Fonts.IoniconsModule());
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsPlugin.Iconize.Droid.IconControls.Init();
            new PlatformUtils().Init();
            LoadApplication(new App());
        }
    }
}

