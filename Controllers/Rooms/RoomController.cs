using BKConnect.BKConnectBE.Common;
using BKConnect.Controllers;
using BKConnectBE.Common.Attributes;
using BKConnectBE.Common.Enumeration;
using BKConnectBE.Model.Dtos.ChatManagement;
using BKConnectBE.Model.Dtos.NotificationManagement;
using BKConnectBE.Model.Dtos.Parameters;
using BKConnectBE.Model.Dtos.RoomManagement;
using BKConnectBE.Service.Rooms;
using BKConnectBE.Service.WebSocket;
using Microsoft.AspNetCore.Mvc;

namespace BKConnectBE.Controllers.Rooms
{
    [CustomAuthorize]
    [ApiController]
    [Route("rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IWebSocketService _webSocketService;

        public RoomController(IRoomService roomService, IWebSocketService webSocketService)
        {
            _roomService = roomService;
            _webSocketService = webSocketService;
        }

        [HttpGet("getRoomsOfUser")]
        public async Task<ActionResult<Responses>> GetListOfRoomsByUserId()
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var listOfRooms = await _roomService.GetListOfRoomsByUserId(userId);
                    return this.Success(listOfRooms, MsgNo.SUCCESS_GET_ROOMS_OF_USER);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpGet("searchRoomsOfUser")]
        public async Task<ActionResult<Responses>> SearchListOfRoomsByUserId([FromQuery] SearchKeyCondition searchKeyCondition)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var listOfRooms = await _roomService.GetListOfRoomsByUserId(userId, searchKeyCondition.SearchKey);
                    return this.Success(listOfRooms, MsgNo.SUCCESS_GET_ROOMS_OF_USER);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpGet("getInformationOfRoom")]
        public async Task<ActionResult<Responses>> GetInformationOfRoom([FromQuery] LongKeyCondition condition)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var room = await _roomService.GetInformationOfRoom(condition.SearchKey, userId);
                    return this.Success(room, MsgNo.SUCCESS_GET_INFORMATION_OF_ROOM);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpGet("getListOfMembersInRoom")]
        public async Task<ActionResult<Responses>> GetListOfMembersInRoom([FromQuery] LongKeyCondition condition)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var listOfMembers = await _roomService.GetListOfMembersInRoomAsync(condition.SearchKey, userId);
                    return this.Success(listOfMembers, MsgNo.SUCCESS_GET_LIST_OF_MEMBERS_IN_ROOM);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpGet("getListOfPublicRooms")]
        public async Task<ActionResult<Responses>> GetListOfPublicRooms()
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var listOfRooms = await _roomService.GetListOfRoomsByTypeAndUserId(nameof(RoomType.PublicRoom), userId);
                    return this.Success(listOfRooms, MsgNo.SUCCESS_GET_ROOMS_OF_USER);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpGet("getListOfClassRooms")]
        public async Task<ActionResult<Responses>> GetListOfClassRooms()
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var listOfRooms = await _roomService.GetListOfRoomsByTypeAndUserId(nameof(RoomType.ClassRoom), userId);
                    return this.Success(listOfRooms, MsgNo.SUCCESS_GET_ROOMS_OF_USER);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpGet("searchListOfPublicRooms")]
        public async Task<ActionResult<Responses>> SearchListOfPublicRooms([FromQuery] SearchKeyCondition condition)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var listOfRooms = await _roomService.SearchListOfRoomsByTypeAndUserId(nameof(RoomType.PublicRoom), userId, condition.SearchKey);
                    return this.Success(listOfRooms, MsgNo.SUCCESS_GET_ROOMS_OF_USER);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpGet("searchListOfClassRooms")]
        public async Task<ActionResult<Responses>> SearchListOfClassRooms([FromQuery] SearchKeyCondition condition)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var listOfRooms = await _roomService.SearchListOfRoomsByTypeAndUserId(nameof(RoomType.ClassRoom), userId, condition.SearchKey);
                    return this.Success(listOfRooms, MsgNo.SUCCESS_GET_ROOMS_OF_USER);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpPost("addUserToRoom")]
        public async Task<ActionResult<Responses>> AddUserToRoom(ChangedMemberDto addMemberDto)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var addMsg = await _roomService.AddUserToRoomAsync(addMemberDto.RoomId, addMemberDto.UserId, userId);

                    var websocketDataMsg = new SendWebSocketData
                    {
                        DataType = WebSocketDataType.IsMessage.ToString(),
                        Message = addMsg
                    };

                    await _webSocketService.SendSystemMessage(websocketDataMsg, userId, addMemberDto.UserId, SystemMessageType.IsInRoom.ToString());

                    var notification = new SendNotificationDto
                    {
                        NotificationType = NotificationType.IsInRoom.ToString(),
                        ReceiverId = addMemberDto.UserId
                    };

                    var websocketDataNotify = new SendWebSocketData
                    {
                        DataType = WebSocketDataType.IsNotification.ToString(),
                        Notification = notification
                    };

                    await _webSocketService.SendRoomNotification(websocketDataNotify, userId, addMemberDto.RoomId);

                    return this.Success(addMemberDto.UserId, MsgNo.SUCCESS_ADD_USER_TO_ROOM);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpPost("removeUserFromRoom")]
        public async Task<ActionResult<Responses>> RemoveUserFromRoom(ChangedMemberDto removeMemberDto)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var removeMsg = await _roomService.RemoveUserFromRoom(removeMemberDto.RoomId, removeMemberDto.UserId, userId);

                    var websocketDataMsg = new SendWebSocketData
                    {
                        DataType = WebSocketDataType.IsMessage.ToString(),
                        Message = removeMsg
                    };

                    await _webSocketService.SendSystemMessage(websocketDataMsg, userId, removeMemberDto.UserId, SystemMessageType.IsOutRoom.ToString());

                    var notification = new SendNotificationDto
                    {
                        NotificationType = NotificationType.IsOutRoom.ToString(),
                        ReceiverId = removeMemberDto.UserId
                    };

                    var websocketDataNotify = new SendWebSocketData
                    {
                        DataType = WebSocketDataType.IsNotification.ToString(),
                        Notification = notification
                    };

                    await _webSocketService.SendRoomNotification(websocketDataNotify, userId, removeMemberDto.RoomId);

                    return this.Success(removeMemberDto.UserId, MsgNo.SUCCESS_REMOVE_USER_FROM_ROOM);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }

        [HttpPost("createGroupRoom")]
        public async Task<ActionResult<Responses>> CreateGroupRoom(AddGroupRoomDto addGroupRoomDto)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    var addMsg = await _roomService.CreateGroupRoomAsync(addGroupRoomDto, userId);

                    var websocketDataMsg = new SendWebSocketData
                    {
                        DataType = WebSocketDataType.IsMessage.ToString(),
                        Message = addMsg
                    };

                    await _webSocketService.SendSystemMessage(websocketDataMsg, userId, "", SystemMessageType.IsCreateGroupRoom.ToString());

                    return this.Success(addMsg.RoomId, MsgNo.SUCCESS_CREATE_GROUP_ROOM);
                }

                return BadRequest(this.Error(MsgNo.ERROR_TOKEN_INVALID));
            }
            catch (Exception e)
            {
                return BadRequest(this.Error(e.Message));
            }
        }
    }
}