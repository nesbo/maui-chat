using Maui.Chat.Domain;
using Maui.Chat.Domain.Configuration;
using Maui.Chat.Domain.Handlers;
using Maui.Chat.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Maui.Chat.App;

public partial class ChatPage : ContentPage
{
    private readonly ChatConfiguration _chatConfiguration;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly HubConnection _hubConnection;
    private readonly SendMessageCommandHandler _sendMessageCommandHandler;
    private readonly ReceiveChatMessageCommandHandler _receiveChatMessageCommandHandler;

    public IEnumerable<ChatMessage> Messages => _chatMessageRepository.Messages;

    public ChatPage(IServiceProvider serviceProvider, ChatConfiguration chatConfiguration)
    {
        _chatConfiguration = chatConfiguration;
        _chatMessageRepository = serviceProvider.GetRequiredService<IChatMessageRepository>();
        _sendMessageCommandHandler = serviceProvider.GetRequiredService<SendMessageCommandHandler>();
        _receiveChatMessageCommandHandler = serviceProvider.GetRequiredService<ReceiveChatMessageCommandHandler>();

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(ServerConfiguration.GetUrl(chatConfiguration.Host))
            .Build();

        _hubConnection.On<string>("ReceiveMessage", ReceiveMessage);

        Task.Run(() =>
            Dispatcher.Dispatch(async () => { await _hubConnection.StartAsync(); }));

        InitializeComponent();
        
        MessagesListView.ItemsSource = Messages;
    }

    private async void ReceiveMessage(string message)
    {
        var command = new ReceiveChatMessageCommand(message, _chatConfiguration.EncryptionKey);
        
        await _receiveChatMessageCommandHandler
            .ReceiveChatMessageAsync(command, default);
    }
    
    private async void SendMessage(string messageText)
    {
        var command = new SendMessageCommand(
            _chatConfiguration.SenderName,
            _chatConfiguration.SenderId,
            messageText,
            _chatConfiguration.EncryptionKey);

        var publisher = new SignalRChatMessagePublisher(_hubConnection);

        await _sendMessageCommandHandler
            .SendMessageAsync(command, default, publisher);
    }


    private void Button_OnClicked(object sender, EventArgs e)
    {
        SendMessage(MessageTextBox.Text);
        
        MessageTextBox.Text = string.Empty;
    }
}