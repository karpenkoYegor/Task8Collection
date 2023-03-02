using Microsoft.AspNetCore.SignalR;

namespace Task8Collection.Hubs;

public class CommentHub : Hub
{
    public async Task JoinUser(string itemId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, itemId);
    }
    public async Task SendComment(string itemId)
    {
        await Clients.Group(itemId).SendAsync("sendMessage");
    }
}