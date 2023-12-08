using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BKConnectBE.Common.Enumeration;
using BKConnectBE.Model.Dtos.FriendRequestManagement;
using BKConnectBE.Model.Dtos.MessageManagement;
using BKConnectBE.Model.Dtos.NotificationManagement;
using BKConnectBE.Model.Dtos.RoomManagement;

namespace BKConnectBE.Model.Dtos.ChatManagement
{
    public class SendWebSocketData
    {
        [JsonPropertyName("data_type")]
        [EnumDataType(typeof(WebSocketDataType))]
        public string DataType { get; set; }

        [JsonPropertyName("message")]
        public SendMessageDto Message { get; set; } = null;

        [JsonPropertyName("notification")]
        public SendNotificationDto Notification { get; set; } = null;

        [JsonPropertyName("changed_room_info")]
        public ChangedRoomDto ChangedRoomInfo { get; set; } = null;
    }
}