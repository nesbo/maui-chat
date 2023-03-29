using Maui.Chat.Domain;
using Maui.Chat.Domain.Configuration;
using Maui.Chat.Domain.Handlers;
using Maui.Chat.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Maui.Chat.App;

public partial class ChatPage : ContentPage
{
    private ChatConfiguration _chatConfiguration;
    private readonly IChatMessageRepository _chatMessageRepository;
    private HubConnection _hubConnection;
    private readonly SendMessageCommandHandler _sendMessageCommandHandler;
    private readonly ReceiveChatMessageCommandHandler _receiveChatMessageCommandHandler;

    private IEnumerable<ChatMessage> Messages => _chatMessageRepository.Messages;

    public ChatPage(IChatMessageRepository chatMessageRepository,
        SendMessageCommandHandler sendMessageCommandHandler,
        ReceiveChatMessageCommandHandler receiveChatMessageCommandHandler)
    {
        _chatMessageRepository = chatMessageRepository;
        _sendMessageCommandHandler = sendMessageCommandHandler;
        _receiveChatMessageCommandHandler = receiveChatMessageCommandHandler;
        
        InitializeComponent();
    }
    public void SetChatConfiguration(ChatConfiguration chatConfiguration)
    {
        _chatConfiguration = chatConfiguration;
        
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(ServerConfiguration.GetUrl(chatConfiguration.Host))
            .Build();

        _hubConnection.On<string>("ReceiveMessage", ReceiveMessage);

        Task.Run(() =>
            Dispatcher.Dispatch(async () =>
            {
                try
                {
                    await _hubConnection.StartAsync();
                    MessagesListView.ItemsSource = Messages;
                }
                catch (Exception e)
                {
                    ErrorLabel.Text = e.Message;
                }
            }));
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

        try
        {
            await _sendMessageCommandHandler
                .SendMessageAsync(command, default, publisher);
        }
        catch (Exception e)
        {
            ErrorLabel.Text = e.Message;
        }
    }


    private void Button_OnClicked(object sender, EventArgs e)
    {
        SendMessage(MessageTextBox.Text);
        
        MessageTextBox.Text = string.Empty;
    }
}