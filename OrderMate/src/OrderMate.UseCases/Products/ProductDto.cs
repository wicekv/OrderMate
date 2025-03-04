namespace OrderMate.UseCases.Products;

public sealed record ProductDto(string Name, decimal Price, int Stock, string Category);
