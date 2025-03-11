using Microsoft.AspNetCore.Mvc;

namespace HotelRoomManager.Api.Controllers
{
    using System.Net;
    using HotelRoomManager.Application.Commands.CreateRoom;
    using HotelRoomManager.Application.Commands.MakeRoomAvailable;
    using HotelRoomManager.Application.Commands.OccupyRoom;
    using HotelRoomManager.Application.Commands.UpdateRoom;
    using HotelRoomManager.Application.DTOs;
    using HotelRoomManager.Application.DTOs.Requests;
    using HotelRoomManager.Application.DTOs.Responses;
    using HotelRoomManager.Application.Interfaces;
    using HotelRoomManager.Application.Queries.GetRoom;
    using HotelRoomManager.Application.Queries.GetRooms;

    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly ICommandHandler<CreateRoomCommand, ResponseDto<RoomDto>> _createRoomRequestHandler;
        private readonly ICommandHandler<UpdateRoomCommand, ResponseDto<RoomDto>> _updateRoomRequestHandler;
        private readonly ICommandHandler<OccupyRoomCommand, ResponseDto<RoomDto>> _occupyRoomRequestHandler;
        private readonly ICommandHandler<MakeRoomAvailableCommand, ResponseDto<RoomDto>> _makeRoomAvailableRequestHandler;
        private readonly IQueryHandler<GetRoomsQuery, List<RoomDto>> _getRoomsQueryHandler;
        private readonly IQueryHandler<GetRoomQuery, ResponseDto<RoomDto>> _getRoomQueryHandler;

        public RoomsController(
            IQueryHandler<GetRoomsQuery, List<RoomDto>> getRoomsQueryHandler,
            IQueryHandler<GetRoomQuery, ResponseDto<RoomDto>> getRoomQueryHandler,
            ICommandHandler<CreateRoomCommand, ResponseDto<RoomDto>> createRoomRequestHandler,
            ICommandHandler<UpdateRoomCommand, ResponseDto<RoomDto>> updateRoomRequestHandler,
            ICommandHandler<OccupyRoomCommand, ResponseDto<RoomDto>> occupyRoomRequestHandler,
            ICommandHandler<MakeRoomAvailableCommand, ResponseDto<RoomDto>> makeRoomAvailableRequestHandler)
        {
            _getRoomsQueryHandler = getRoomsQueryHandler;
            _getRoomQueryHandler = getRoomQueryHandler;
            _createRoomRequestHandler = createRoomRequestHandler;
            _updateRoomRequestHandler = updateRoomRequestHandler;
            _occupyRoomRequestHandler = occupyRoomRequestHandler;
            _makeRoomAvailableRequestHandler = makeRoomAvailableRequestHandler;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<RoomDto>), 200)]
        public async Task<List<RoomDto>> Get(GetRoomsRequestDto requestDto)
        {
            var result = await _getRoomsQueryHandler.Handle(
                new GetRoomsQuery(requestDto.Number, requestDto.Size, requestDto.Availability));

            return result;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(RoomDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _getRoomQueryHandler.Handle(new GetRoomQuery(id));

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoomDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequestDto createRoomRequestDto)
        {
            var response = await _createRoomRequestHandler.Handle(new CreateRoomCommand(createRoomRequestDto));

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(RoomDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, UpdateRoomRequestDto requestDto)
        {
            var response = await _updateRoomRequestHandler.Handle(new UpdateRoomCommand(id, requestDto));

            return response.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(response.Message),
                HttpStatusCode.BadRequest => BadRequest(response.Message),
                _ => Ok(response.Data)
            };
        }

        [HttpPost("{id:int}/occupy")]
        [ProducesResponseType(typeof(RoomDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> OccupyRoom(int id, [FromBody] OccupyRequestDto request)
        {
            var response = await _occupyRoomRequestHandler.Handle(new OccupyRoomCommand(id, request));

            return response.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(response.Message),
                HttpStatusCode.BadRequest => BadRequest(response.Message),
                _ => Ok(response.Data)
            };
        }

        [HttpPost("{id:int}/available")]
        [ProducesResponseType(typeof(RoomDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateRoom(int id)
        {
            var response = await _makeRoomAvailableRequestHandler.Handle(new MakeRoomAvailableCommand(id));

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

    }
}
