namespace InventoryHub.Application.Exceptions;

public abstract class AppException(string message, int statusCode) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}

public class NotFoundException(string message) : AppException(message, 404);

public class BadRequestException(string message) : AppException(message, 400);

public class ConflictException(string message) : AppException(message, 409);
