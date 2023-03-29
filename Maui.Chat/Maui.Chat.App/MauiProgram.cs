using Maui.Chat.Domain;
using Maui.Chat.Domain.Handlers;

namespace Maui.Chat.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        ConfigureServices(builder.Services);

        return builder.Build();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ReceiveChatMessageCommandHandler>();
        services.AddScoped<SendMessageCommandHandler>();
        services.AddScoped<IClock, Clock>();

        services.AddSingleton<IChatMessageRepository, ChatMessageRepository>();
        services.AddSingleton<IChatMessageCrypto, ChatMessageCryptoAes>();
        services.AddTransient<MainPage>();
        services.AddSingleton<AppShell>();
        services.AddTransient<ChatPage>();
    }
}