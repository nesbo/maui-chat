using System.Text.RegularExpressions;
using Maui.Chat.Domain.Configuration;

namespace Maui.Chat.App;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    private static readonly Regex IpAddressRegex =
        new(
            @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$",
            RegexOptions.Compiled);

    private readonly List<string> _errorMessages = new()
    {
        "Enter server address",
        "Enter encryption key"
    };

    public MainPage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        var chatSettings = new ChatConfiguration(
            "John Doe",
            ServerTextBox.Text,
            EncryptionKeyTextBox.Text);
        
        await Navigation.PushAsync(new ChatPage(_serviceProvider, chatSettings), true);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var serverText = ServerTextBox.Text;
        var encryptionKey = EncryptionKeyTextBox.Text;

        _errorMessages.Clear();

        var serverTextValid = ValidateServerText(serverText);
        var keyValid = ValidateEncryptionKey(encryptionKey);
        
        ToChatButton.IsEnabled = serverTextValid && keyValid;

        var errorText = string.Concat(
            _errorMessages
                .Select(e => "• " + e + Environment.NewLine)
                .ToArray());

        ErrorsListView.Text = errorText;
    }


    private bool ValidateEncryptionKey(string encryptionKey)
    {
        if (string.IsNullOrEmpty(encryptionKey))
        {
            _errorMessages.Add("Enter encryption key");
            return false;
        }

        if (encryptionKey.Length >= 16) return true;

        _errorMessages.Add("Key min length 16 characters");
        return false;
    }

    private bool ValidateServerText(string serverText)
    {
        if (string.IsNullOrEmpty(serverText))
        {
            _errorMessages.Add("Enter server address");
            return false;
        }

        if (IpAddressRegex.IsMatch(serverText)) return true;

        _errorMessages.Add("Not valid server address");
        return false;
    }
}