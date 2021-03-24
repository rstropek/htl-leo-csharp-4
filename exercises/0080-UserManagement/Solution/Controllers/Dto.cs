namespace UserManagement.Controllers
{
    // This file contains records that represent the results of the
    // ASP.NET Core API controllers. Such data structures are called
    // *Data Transfer Objects* (short DTO) because they are used for
    // data transfer.

    public record GroupResult(int Id, string Name);

    public record UserResult(int Id, string NameIdentifier, string Email, string? FirstName, string? LastName);
}
