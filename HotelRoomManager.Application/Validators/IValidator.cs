namespace HotelRoomManager.Application.Validators;

public interface IValidator<in TDto>
{
    ValidationResult Validate(TDto dto);
}