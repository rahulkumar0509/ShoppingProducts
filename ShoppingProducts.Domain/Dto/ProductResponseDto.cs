public class ProductResponseDto
{
    public required Guid Id {get; set;}
    public required string Name {get; set;}
    public required float Price {get; set;}
    public required List<string> CategoryNames {get; set;}
    public required string BrandName {get; set;}
}