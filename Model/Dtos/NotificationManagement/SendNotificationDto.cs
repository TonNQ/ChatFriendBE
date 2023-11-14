using System.Text.Json.Serialization;
using BKConnectBE.Model.Dtos.FriendRequestManagement;

namespace BKConnectBE.Model.Dtos.NotificationManagement
{
    public class SendNotificationDto
    {
        [JsonPropertyName("send_time")]
        public DateTime SendTime { get; set; }

        [JsonPropertyName("notification_type")]
        public string NotificationType { get; set; }

        [JsonPropertyName("receiver_id")]
        public string ReceiverId { get; set; }
    }
}