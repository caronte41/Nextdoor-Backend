using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.ClassLibrary.FriendshipConnection.Request;
using NextDoorBackend.ClassLibrary.FriendshipConnection.Response;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;
using YourNamespace.Entities;

namespace NextDoorBackend.Business.FriendshipConnection
{
    public class FriendshipConnectionInteractions : IFriendshipConnectionInteractions
    {
        private readonly AppDbContext _context;

        public FriendshipConnectionInteractions(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SendFriendshipConnectionResponse> SendFriendshipRequest(SendFriendshipConnectionRequest request)
        {
            var existingRequest = await _context.FriendshipConnections
        .FirstOrDefaultAsync(fc => (fc.RequestorAccountId == request.requestorId && fc.ReceiverAccountId == request.receiverId)
                                || (fc.RequestorAccountId == request.receiverId && fc.ReceiverAccountId == request.requestorId));

            if (existingRequest != null)
            {
                throw new InvalidOperationException("A friendship request already exists between these accounts.");
            }

            // Create a new friendship connection with Pending status
            var newRequest = new FriendshipConnectionsEntity
            {
                Id = Guid.NewGuid(),
                RequestorAccountId = request.requestorId,
                ReceiverAccountId = request.receiverId,
                Status = FriendshipStatus.Pending,
                RequestedAt = DateTime.UtcNow,
                IsMutual = false // You can modify this logic later if mutual status is determined elsewhere
            };

            await _context.FriendshipConnections.AddAsync(newRequest);
            await _context.SaveChangesAsync();

            // Create a notification for the receiver
            var notification = new NotificationsEntity
            {
                Id = Guid.NewGuid(),
                AccountId = (Guid)request.receiverId,
                Type = NotificationType.FriendshipRequest,
                Message = "You have a new friendship request.",
                CreatedAt = DateTime.UtcNow,
                RelatedEntityId = newRequest.Id // The friendship connection Id
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return new SendFriendshipConnectionResponse { };
        }
        public async Task<SendFriendshipConnectionResponse> RespondToFriendshipRequest(RespondToFriendshipConnectionRequest request)
        {
            var friendshipRequest = await _context.FriendshipConnections
       .FirstOrDefaultAsync(fc => fc.Id == request.RequestId);

            if (friendshipRequest == null)
            {
                throw new InvalidOperationException("Friendship request not found.");
            }

            if (friendshipRequest.Status != FriendshipStatus.Pending)
            {
                throw new InvalidOperationException("Friendship request is no longer pending.");
            }

            // Update the friendship status
            friendshipRequest.Status = (bool)request.IsAccepted ? FriendshipStatus.Accepted : FriendshipStatus.Rejected;
            friendshipRequest.RespondedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Notify the requestor of the response
            var notification = new NotificationsEntity
            {
                Id = Guid.NewGuid(),
                AccountId = friendshipRequest.RequestorAccountId.Value,
                Type = NotificationType.FriendshipResponse,
                Message = (bool)request.IsAccepted ? "Your friendship request has been accepted." : "Your friendship request has been rejected.",
                CreatedAt = DateTime.UtcNow,
                RelatedEntityId = friendshipRequest.Id
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return new SendFriendshipConnectionResponse { };
        }
    }
}
