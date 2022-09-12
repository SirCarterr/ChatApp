using Chat_DataAccess;
using Chat_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat_API.SignalR
{
    [Authorize]
    public class SignalRHub : Hub
    {
        public async Task ConnectToChat(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromChat(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task DeleteChat(string groupName)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ChatDeleted", groupName);
        }

        public async Task SendMessage(string groupName)
        {
            await Clients.Caller.SendAsync("Receive", groupName);
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveNotification", groupName);
        }

        public async Task RefreshChat(string groupName)
        {
            await Clients.Group(groupName).SendAsync("Refresh", groupName);
        }

        public async Task AddUserToChat(string groupName, string users)
        {
            await Clients.Others.SendAsync("CheckChat", groupName, users);
        }

        public async Task AddChat(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.SendAsync("ChatAdded");
        }
    }
}
