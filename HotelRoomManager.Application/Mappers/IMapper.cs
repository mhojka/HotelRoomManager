namespace HotelRoomManager.Application.Mappers;

public interface IMapper<in TFrom, out TTo>
{
    TTo Map(TFrom from);
}