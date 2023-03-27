using System.Text.RegularExpressions;

namespace Maui.Chat.App;

public partial class MainPage : ContentPage
{
    private static readonly Regex _ipAddressRegex =
        new(
            @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$",
            RegexOptions.Compiled);

    public List<string> ErrorMessages = new()
    {
        "Enter server address",
        "Enter encryption key"
    };

    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChatPage(), true);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var serverText = ServerTextBox.Text;
        var encryptionKey = EncryptionKeyTextBox.Text;

        ErrorMessages.Clear();

        var serverTextValid = ValidateServerText(serverText);
        var keyValid = ValidateEncryptionKey(encryptionKey);
        
        ToChatButton.IsEnabled = serverTextValid && keyValid;

        var errorText = string.Concat(
            ErrorMessages
                .Select(e => "• " + e + Environment.NewLine)
                .ToArray());

        ErrorsListView.Text = errorText;
    }


    private bool ValidateEncryptionKey(string encryptionKey)
    {
        if (string.IsNullOrEmpty(encryptionKey))
        {
            ErrorMessages.Add("Enter encryption key");
            return false;
        }

        if (encryptionKey.Length >= 16) return true;

        ErrorMessages.Add("Key min length 16 characters");
        return false;
    }

    private bool ValidateServerText(string serverText)
    {
        if (string.IsNullOrEmpty(serverText))
        {
            ErrorMessages.Add("Enter server address");
            return false;
        }

        if (_ipAddressRegex.IsMatch(serverText)) return true;

        ErrorMessages.Add("Not valid server address");
        return false;
    }
}