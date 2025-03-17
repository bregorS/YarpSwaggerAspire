public class CreateDocumentRequest
{
    public string? DisplayName { get; init; }

    public string? Description { get; init; }

    public List<string>? Tags { get; init; }

    public IFormFile file { get; init; }
}
