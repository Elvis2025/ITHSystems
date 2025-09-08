using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace ITHSystems
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // ... tu código opcional (UI flags, status bar, etc.)
        }

        // Reenviar permisos a MAUI Essentials
        public override void OnRequestPermissionsResult(
            int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Microsoft.Maui.ApplicationModel.Platform
                .OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // Reenviar intents (App Links, auth callbacks, etc.)
        protected override void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);
            Microsoft.Maui.ApplicationModel.Platform.OnNewIntent(intent);
        }
    }
}
