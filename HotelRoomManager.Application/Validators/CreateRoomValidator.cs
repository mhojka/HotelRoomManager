namespace HotelRoomManager.Application.Validators;

using HotelRoomManager.Application.DTOs.Requests;

public class CreateRoomValidator : IValidator<CreateRoomRequestDto>
{
    public ValidationResult Validate(CreateRoomRequestDto dto)
    {
        return new ValidationResult
        {
            IsValid = true
        };
    }
}