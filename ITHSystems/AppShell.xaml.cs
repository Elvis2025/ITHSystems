using ITHSystems.Constants;
using ITHSystems.Extensions;
using ITHSystems.Resx;
using System.Globalization;

namespace ITHSystems
{
    public partial class AppShell : Shell
    {
        public static Action<string> ChangeLanguage { get; set; } = new Action<string>((language) => { });
        public AppShell()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            UtilExtensions.RegisterAsRoutes();
            

            ChangeLanguage = async (language) =>
            {
                if (string.IsNullOrEmpty(language)) return;
                await SetLanguageAsync(language);
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var window = Application.Current?.Windows.FirstOrDefault();
                    if (window is null) return;
                    await Task.Delay(300);
                    window.Page = new AppShell();
                });
            };
        }

        public static async Task SetLanguageAsync(string language)
        {
             SetLanguage(language);
             await Task.CompletedTask;
        }
      
        public static void SetLanguage(string language)
        {
            var baseCulture = new CultureInfo(language);

            var customCulture = (CultureInfo)baseCulture.Clone();

            // Cambiamos el formato numérico a estilo americano
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = ",";

            customCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            customCulture.NumberFormat.CurrencyGroupSeparator = ",";

            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = baseCulture;
            CultureInfo.DefaultThreadCurrentCulture = customCulture;
            CultureInfo.DefaultThreadCurrentUICulture = baseCulture;
            IBSResources.Culture = baseCulture;
            Preferences.Set(nameof(Language), language);
        }

    }
}
