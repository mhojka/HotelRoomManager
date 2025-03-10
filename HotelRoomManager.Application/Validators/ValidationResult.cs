namespace HotelRoomManager.Application.Validators;

using System.Diagnostics.CodeAnalysis;

public class ValidationResult()
{
    [MemberNotNullWhen(false, nameof(Message))]
    public required bool IsValid { get; init; }

    
    public string? Message { get; set; }
}