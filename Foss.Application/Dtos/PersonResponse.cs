using NetAPI.Domain.Entities;

namespace NetAPI.Application.Dtos;

public record PersonResponse(
    Person Person,
    PersonErrorResponse? Error = null
);