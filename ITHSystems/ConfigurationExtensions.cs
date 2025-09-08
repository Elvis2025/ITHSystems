using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace ITHSystems;

public static class ConfigurationExtensions
{
    public static MauiApp CreateMauiApp(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .UseMauiCommunityToolkitCamera()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-solid-900.ttf", "FaSolid");
                fonts.AddFont("TypeIconsColor.ttf", "TypeIconsColor");
                fonts.AddFont("MaterialIcons-Regular.ttf", "IconRegular");
                fonts.AddFont("MaterialIconsOutlined-Regular.otf", "IconOutlined");
                fonts.AddFont("MaterialIconsRound-Regular.otf", "IconRound");
                fonts.AddFont("MaterialIconsSharp-Regular.otf", "IconSharp");
                fonts.AddFont("MaterialIconsTwoTone-Regular.otf", "IconTwoTone");
            });


        builder.Services.AddServices();
        builder.Services.AddSingleton<IFileSystem>(FileSystem.Current);
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
