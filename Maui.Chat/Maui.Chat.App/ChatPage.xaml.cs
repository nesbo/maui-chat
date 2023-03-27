using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Maui.Chat.App;

public partial class ChatPage : ContentPage
{
    private readonly HubConnection _hubConnection;
    public ChatPage()
    {
        InitializeComponent();
    }
}