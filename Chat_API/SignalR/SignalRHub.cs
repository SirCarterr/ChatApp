using Chat_DataAccess;
using Chat_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat_API.SignalR
{
    [Authorize]
    public class SignalRHub : Hub
    {
        public async Task ConnectToChat(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task RemoveFromChat(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task DeleteChat(string groupId)
        {
            await Clients.OthersInGroup(groupId).SendAsync("ChatDeleted", groupId);
        }

        public async Task SendMessage(string groupId)
        {
            await Clients.Caller.SendAsync("Receive", groupId);
            await Clients.OthersInGroup(groupId).SendAsync("ReceiveNotification", groupId);
        }

        public async Task RefreshChat(string groupId)
        {
            await Clients.Group(groupId).SendAsync("Refresh", groupId);
        }

        public async Task AddUserToChat(string groupId, string users)
        {
            await Clients.Others.SendAsync("CheckChat", groupId, users);
        }

        public async Task AddChat(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            await Clients.Caller.SendAsync("ChatAdded");
        }
    }
}
