public class ProductReviewDTO
{
    public int ProductId { get; set; }
    public int Quality { get; set; }
    public int Design { get; set; }
    public int Usability { get; set; }
    public int Durability { get; set; }
    public int ValueForMoney { get; set; }

    public string? ReviewerName { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}