using Microsoft.AspNetCore.Mvc;

namespace HotelRoomManager.Api.Controllers
{
    using HotelRoomManager.Application.Commands;
    using HotelRoomManager.Application.DTOs;
    using HotelRoomManager.Application.DTOs.Requests;
    using HotelRoomManager.Application.Interfaces;

    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly ICommandHandler<CreateRoomCommand, Guid> _createRoomRequestHandler;

        public RoomsController(
            ILogger<RoomsController> logger,
            ICommandHandler<CreateRoomCommand, Guid> createRoomRequestHandler)
        {
            _logger = logger;
            _createRoomRequestHandler = createRoomRequestHandler;
        }

        [HttpGet(Name = "GetRooms")]
        public IEnumerable<RoomDto> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet(Name = "GetRoom")]
        public RoomDto Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost(Name = "CreateRoom")]
        public IActionResult CreateRoom([FromBody] CreateRoomRequestDto createRoomRequestDto)
        {
            var id = _createRoomRequestHandler.Handle(new CreateRoomCommand(createRoomRequestDto));

            return Ok(id);
        }
    }
}
