namespace HotelRoomManager.Application.DTOs.Responses;

using System.Net;

public class ResponseDto<TData>()
{
    public TData? Data { get; set; }

    public string Message { get; set; } = string.Empty;

    public HttpStatusCode StatusCode { get; set; }

    public static ResponseDto<TData> CreateNotFoundResponse(string message)
    {
        return new ResponseDto<TData>
        {
            Message = message,
            StatusCode = HttpStatusCode.NotFound
        };
    }

    public static ResponseDto<TData> CreateOkResponse(TData data)
    {
        return new ResponseDto<TData>
        {
            Data = data,
            StatusCode = HttpStatusCode.OK
        };
    }

    public static ResponseDto<TData> CreateBadRequest(string message)
    {
        return new ResponseDto<TData>
        {
            StatusCode = HttpStatusCode.BadRequest,
            Message = message
        };
    }
}