using Mattger_DAL.Entities.Enums;

public class ProductQueryParams
{
    public string? Search { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public ProductStatus? Status { get; set; }
    public string? Type { get; set; }
    public int? ProductTypeId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int? Rating { get; set; } 
}