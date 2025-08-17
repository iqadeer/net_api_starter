namespace NetAPI.Application.Dtos;

public record PersonErrorResponse(
    string Message,
    Dictionary<string, string[]>? Errors = null
);