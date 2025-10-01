using ITHSystems.Constants;

namespace ITHSystems;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Current!.UserAppTheme = AppTheme.Light;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        AppShell.SetLanguage(Preferences.Get(nameof(Language), Language.Spanish.Code));
        return new Window(new AppShell());
    }
}